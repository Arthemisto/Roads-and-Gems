using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Indigo
{
    public partial class OnlineMultiplayerForm : Form
    {
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        private readonly List<OnlinePlayerInfo> connectedPlayers = new List<OnlinePlayerInfo>();
        private readonly List<HostClientConnection> hostClients = new List<HostClientConnection>();

        private TcpListener? hostListener;
        private TcpClient? clientConnection;
        private StreamWriter? clientWriter;
        private CancellationTokenSource? hostCts;
        private CancellationTokenSource? clientCts;
        private string sessionName = "Indigo Session";
        private bool isHosting;
        private bool isConnectedAsClient;
        private bool isLaunchingGame;
        private int maxPlayers = 4;
        private readonly int[] sizesOfObjects =
        [
            BoardImage.width,
            BoardImage.height,
            Tile.width,
            Tile.height,
            Gem.width,
            Gem.height,
            PlayerToken.width,
            PlayerToken.height
        ];
        private const float GameScale = 0.9f;

        public OnlineMultiplayerForm()
        {
            InitializeComponent();
            playerNameTextBox.Text = $"{Environment.UserName}";
            RefreshPlayerList();
        }

        private async void startHostingButton_Click(object sender, EventArgs e)
        {
            if (isHosting || isConnectedAsClient)
                return;

            string playerName = NormalizePlayerName();
            maxPlayers = Math.Clamp((int)hostMaxPlayersInput.Value, 2, 4);
            int port = (int)hostPortInput.Value;
            sessionName = $"{playerName}'s Indigo Lobby";

            try
            {
                hostCts = new CancellationTokenSource();
                hostListener = new TcpListener(IPAddress.Any, port);
                hostListener.Start();
                isHosting = true;

                connectedPlayers.Clear();
                connectedPlayers.Add(new OnlinePlayerInfo
                {
                    PlayerId = 0,
                    Name = playerName,
                    IsHost = true
                });

                startHostingButton.Enabled = false;
                stopHostingButton.Enabled = true;
                connectButton.Enabled = false;
                hostStatusLabel.Text = $"Hosting active. Max players: {maxPlayers}";
                hostInfoLabel.Text = $"Share with players:\r\nIPs: {string.Join(", ", GetLocalIpv4Addresses())}\r\nPort: {port}\r\nHost counts as player 1";

                AppendLog($"Host started on port {port} with max {maxPlayers} players.");
                RefreshPlayerList();
                await BroadcastLobbyStateAsync();
                _ = AcceptLoopAsync(hostCts.Token);
            }
            catch (Exception ex)
            {
                AppendLog($"Host start failed: {ex.Message}");
                hostStatusLabel.Text = "Host start failed";
                await StopHostingAsync();
            }
        }

        private async Task AcceptLoopAsync(CancellationToken cancellationToken)
        {
            if (hostListener == null)
                return;

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    TcpClient tcpClient = await hostListener.AcceptTcpClientAsync(cancellationToken);
                    _ = HandleIncomingClientAsync(tcpClient, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex)
            {
                AppendLog($"Host accept loop stopped: {ex.Message}");
            }
        }

        private async Task HandleIncomingClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            using NetworkStream stream = tcpClient.GetStream();
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true);
            using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8, 1024, true) { AutoFlush = true };

            string? line = await reader.ReadLineAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(line))
            {
                tcpClient.Close();
                return;
            }

            OnlineSessionEnvelope? request = DeserializeEnvelope(line);
            if (request?.Type != "hello")
            {
                await SendEnvelopeAsync(writer, new OnlineSessionEnvelope
                {
                    Type = "join_rejected",
                    Message = "Invalid handshake"
                }, cancellationToken);
                tcpClient.Close();
                return;
            }

            HostClientConnection? connection = null;

            lock (hostClients)
            {
                if (connectedPlayers.Count >= maxPlayers)
                {
                    connection = null;
                }
                else
                {
                    int playerId = FindNextPlayerId();
                    OnlinePlayerInfo player = new OnlinePlayerInfo
                    {
                        PlayerId = playerId,
                        Name = string.IsNullOrWhiteSpace(request.PlayerName) ? $"Player {playerId + 1}" : request.PlayerName.Trim(),
                        IsHost = false
                    };

                    connectedPlayers.Add(player);
                    connection = new HostClientConnection
                    {
                        TcpClient = tcpClient,
                        Writer = writer,
                        Player = player
                    };
                    hostClients.Add(connection);
                }
            }

            if (connection == null)
            {
                await SendEnvelopeAsync(writer, new OnlineSessionEnvelope
                {
                    Type = "join_rejected",
                    Message = "Session is full. Maximum is 4 players."
                }, cancellationToken);
                AppendLog($"Rejected connection from {request.PlayerName}: session full.");
                tcpClient.Close();
                return;
            }

            await SendEnvelopeAsync(writer, new OnlineSessionEnvelope
            {
                Type = "join_accepted",
                PlayerId = connection.Player.PlayerId,
                MaxPlayers = maxPlayers,
                SessionName = sessionName,
                Message = "Connected to host"
            }, cancellationToken);

            AppendLog($"{connection.Player.Name} joined as player {connection.Player.PlayerId + 1}.");
            UpdateHostStatus();
            RefreshPlayerList();
            await BroadcastLobbyStateAsync();

            try
            {
                while (!cancellationToken.IsCancellationRequested && tcpClient.Connected)
                {
                    string? clientLine = await reader.ReadLineAsync(cancellationToken);
                    if (clientLine == null)
                        break;

                    OnlineSessionEnvelope? message = DeserializeEnvelope(clientLine);
                    if (message?.Type == "leave")
                        break;
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (IOException)
            {
            }
            finally
            {
                RemoveHostClient(connection.Player.PlayerId, $"{connection.Player.Name} disconnected.");
            }
        }

        private int FindNextPlayerId()
        {
            int nextId = 0;
            while (connectedPlayers.Any(p => p.PlayerId == nextId))
                nextId++;
            return nextId;
        }

        private async Task BroadcastLobbyStateAsync()
        {
            OnlineSessionEnvelope state = new OnlineSessionEnvelope
            {
                Type = "lobby_state",
                MaxPlayers = maxPlayers,
                SessionName = sessionName,
                Players = connectedPlayers
                    .OrderBy(p => p.PlayerId)
                    .Select(p => new OnlinePlayerInfo
                    {
                        PlayerId = p.PlayerId,
                        Name = p.Name,
                        IsHost = p.IsHost
                    })
                    .ToList()
            };

            List<HostClientConnection> clientsSnapshot;
            lock (hostClients)
                clientsSnapshot = hostClients.ToList();

            foreach (HostClientConnection client in clientsSnapshot)
            {
                try
                {
                    await SendEnvelopeAsync(client.Writer, state, hostCts?.Token ?? CancellationToken.None);
                }
                catch
                {
                    RemoveHostClient(client.Player.PlayerId, $"{client.Player.Name} dropped during broadcast.");
                }
            }
        }

        private async void stopHostingButton_Click(object sender, EventArgs e)
        {
            await StopHostingAsync();
        }

        private async Task StopHostingAsync()
        {
            if (!isHosting)
                return;

            try
            {
                OnlineSessionEnvelope closingEnvelope = new OnlineSessionEnvelope
                {
                    Type = "session_closed",
                    Message = "Host closed the session."
                };

                List<HostClientConnection> clientsSnapshot;
                lock (hostClients)
                    clientsSnapshot = hostClients.ToList();

                foreach (HostClientConnection client in clientsSnapshot)
                {
                    try
                    {
                        await SendEnvelopeAsync(client.Writer, closingEnvelope, CancellationToken.None);
                    }
                    catch
                    {
                    }
                    client.TcpClient.Close();
                }
            }
            finally
            {
                hostCts?.Cancel();
                hostListener?.Stop();
                hostListener = null;

                foreach (HostClientConnection client in hostClients.ToList())
                    client.TcpClient.Close();

                hostClients.Clear();
                connectedPlayers.Clear();
                isHosting = false;

                startHostingButton.Enabled = true;
                stopHostingButton.Enabled = false;
                connectButton.Enabled = true;
                hostStatusLabel.Text = "Host idle";
                hostInfoLabel.Text = "Start hosting to see IP and port";
                AppendLog("Host stopped.");
                RefreshPlayerList();
            }
        }

        private async void connectButton_Click(object sender, EventArgs e)
        {
            if (isConnectedAsClient || isHosting)
                return;

            string playerName = NormalizePlayerName();
            string ip = joinIpTextBox.Text.Trim();
            int port = (int)joinPortInput.Value;

            try
            {
                clientCts = new CancellationTokenSource();
                clientConnection = new TcpClient();
                await clientConnection.ConnectAsync(ip, port, clientCts.Token);
                NetworkStream stream = clientConnection.GetStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true);
                clientWriter = new StreamWriter(stream, Encoding.UTF8, 1024, true) { AutoFlush = true };

                await SendEnvelopeAsync(clientWriter, new OnlineSessionEnvelope
                {
                    Type = "hello",
                    PlayerName = playerName
                }, clientCts.Token);

                string? responseLine = await reader.ReadLineAsync(clientCts.Token);
                OnlineSessionEnvelope? response = DeserializeEnvelope(responseLine);

                if (response == null || response.Type == "join_rejected")
                {
                    string reason = response?.Message ?? "Unknown join failure";
                    joinStatusLabel.Text = reason;
                    AppendLog($"Join rejected: {reason}");
                    clientConnection.Close();
                    clientConnection = null;
                    clientWriter = null;
                    return;
                }

                if (response.Type != "join_accepted")
                {
                    joinStatusLabel.Text = "Unexpected host response";
                    AppendLog("Join failed: unexpected host response.");
                    clientConnection.Close();
                    clientConnection = null;
                    clientWriter = null;
                    return;
                }

                maxPlayers = Math.Clamp(response.MaxPlayers ?? 4, 2, 4);
                isConnectedAsClient = true;
                connectButton.Enabled = false;
                disconnectButton.Enabled = true;
                startHostingButton.Enabled = false;
                joinStatusLabel.Text = $"Connected to {response.SessionName}";
                AppendLog($"Connected to host {ip}:{port}.");

                _ = ListenToHostAsync(reader, clientCts.Token);
            }
            catch (Exception ex)
            {
                joinStatusLabel.Text = "Connection failed";
                AppendLog($"Join failed: {ex.Message}");
                await DisconnectClientAsync(false);
            }
        }

        private async Task ListenToHostAsync(StreamReader reader, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    string? line = await reader.ReadLineAsync(cancellationToken);
                    if (line == null)
                        break;

                    OnlineSessionEnvelope? envelope = DeserializeEnvelope(line);
                    if (envelope == null)
                        continue;

                    if (envelope.Type == "lobby_state")
                    {
                        connectedPlayers.Clear();
                        connectedPlayers.AddRange(envelope.Players.OrderBy(p => p.PlayerId));
                        maxPlayers = Math.Clamp(envelope.MaxPlayers ?? 4, 2, 4);
                        joinStatusLabel.Text = $"Connected. Players: {connectedPlayers.Count}/{maxPlayers}";
                        RefreshPlayerList();
                    }
                    else if (envelope.Type == "start_game")
                    {
                        int playerCount = Math.Clamp(envelope.PlayerCount ?? connectedPlayers.Count, 2, 4);
                        AppendLog($"Host started a match for {playerCount} players.");
                        joinStatusLabel.Text = $"Starting game for {playerCount} players...";
                        BeginInvoke(() => LaunchOnlineGame(playerCount));
                    }
                    else if (envelope.Type == "session_closed")
                    {
                        AppendLog(envelope.Message ?? "Session closed by host.");
                        joinStatusLabel.Text = envelope.Message ?? "Session closed";
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (IOException)
            {
                AppendLog("Disconnected from host.");
            }
            finally
            {
                await DisconnectClientAsync(false);
            }
        }

        private async void disconnectButton_Click(object sender, EventArgs e)
        {
            await DisconnectClientAsync(true);
        }

        private async Task DisconnectClientAsync(bool notifyHost)
        {
            try
            {
                if (notifyHost && clientWriter != null)
                {
                    await SendEnvelopeAsync(clientWriter, new OnlineSessionEnvelope
                    {
                        Type = "leave",
                        Message = "Client disconnected"
                    }, CancellationToken.None);
                }
            }
            catch
            {
            }
            finally
            {
                clientCts?.Cancel();
                clientConnection?.Close();
                clientConnection = null;
                clientWriter = null;
                isConnectedAsClient = false;
                connectedPlayers.Clear();
                connectButton.Enabled = true;
                disconnectButton.Enabled = false;
                startHostingButton.Enabled = true;
                RefreshPlayerList();
            }
        }

        private void RemoveHostClient(int playerId, string logMessage)
        {
            bool changed = false;

            lock (hostClients)
            {
                HostClientConnection? connection = hostClients.FirstOrDefault(c => c.Player.PlayerId == playerId);
                if (connection != null)
                {
                    connection.TcpClient.Close();
                    hostClients.Remove(connection);
                    changed = true;
                }
            }

            OnlinePlayerInfo? player = connectedPlayers.FirstOrDefault(p => p.PlayerId == playerId);
            if (player != null)
            {
                connectedPlayers.Remove(player);
                changed = true;
            }

            if (!changed)
                return;

            AppendLog(logMessage);
            UpdateHostStatus();
            RefreshPlayerList();
            _ = BroadcastLobbyStateAsync();
        }

        private static async Task SendEnvelopeAsync(StreamWriter writer, OnlineSessionEnvelope envelope, CancellationToken cancellationToken)
        {
            string json = JsonSerializer.Serialize(envelope);
            await writer.WriteLineAsync(json.AsMemory(), cancellationToken);
            await writer.FlushAsync();
        }

        private OnlineSessionEnvelope? DeserializeEnvelope(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            try
            {
                return JsonSerializer.Deserialize<OnlineSessionEnvelope>(json, jsonOptions);
            }
            catch
            {
                return null;
            }
        }

        private string NormalizePlayerName()
        {
            string trimmed = playerNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
                trimmed = "Player";

            playerNameTextBox.Text = trimmed;
            return trimmed;
        }

        private void RefreshPlayerList()
        {
            if (InvokeRequired)
            {
                BeginInvoke(RefreshPlayerList);
                return;
            }

            playersListBox.Items.Clear();
            foreach (OnlinePlayerInfo player in connectedPlayers.OrderBy(p => p.PlayerId))
            {
                string prefix = player.IsHost ? "[HOST]" : "[JOIN]";
                playersListBox.Items.Add($"{prefix} P{player.PlayerId + 1}: {player.Name}");
            }
        }

        private void UpdateHostStatus()
        {
            if (!isHosting)
                return;

            hostStatusLabel.Text = $"Hosting active. Players: {connectedPlayers.Count}/{maxPlayers}";
        }

        private async void startGameButton_Click(object sender, EventArgs e)
        {
            if (!isHosting || connectedPlayers.Count < 2 || isLaunchingGame)
                return;

            int playerCount = connectedPlayers.Count;
            AppendLog($"Starting online match with {playerCount} players.");

            await BroadcastEnvelopeToClientsAsync(new OnlineSessionEnvelope
            {
                Type = "start_game",
                PlayerCount = playerCount,
                SessionName = sessionName
            });

            LaunchOnlineGame(playerCount);
        }

        private async Task BroadcastEnvelopeToClientsAsync(OnlineSessionEnvelope envelope)
        {
            List<HostClientConnection> clientsSnapshot;
            lock (hostClients)
                clientsSnapshot = hostClients.ToList();

            foreach (HostClientConnection client in clientsSnapshot)
            {
                try
                {
                    await SendEnvelopeAsync(client.Writer, envelope, hostCts?.Token ?? CancellationToken.None);
                }
                catch
                {
                    RemoveHostClient(client.Player.PlayerId, $"{client.Player.Name} dropped during broadcast.");
                }
            }
        }

        private void LaunchOnlineGame(int playerCount)
        {
            if (isLaunchingGame)
                return;

            isLaunchingGame = true;
            formRefreshTimer.Stop();
            ToggleLobbyControls(false);

            using GameForm gameForm = new GameForm(sizesOfObjects, GameScale, playerCount);
            Hide();
            try
            {
                gameForm.ShowDialog(this);
            }
            finally
            {
                Show();
                formRefreshTimer.Start();
                isLaunchingGame = false;
                ToggleLobbyControls(true);
                RefreshPlayerList();
                UpdateControlStates();
            }
        }

        private void ToggleLobbyControls(bool enabled)
        {
            playerNameTextBox.Enabled = enabled;
            lobbyTabs.Enabled = enabled;
            closeButton.Enabled = enabled;
            playersListBox.Enabled = enabled;
            startGameButton.Enabled = enabled && isHosting && connectedPlayers.Count >= 2;
        }

        private void UpdateControlStates()
        {
            startHostingButton.Enabled = !isHosting && !isConnectedAsClient && !isLaunchingGame;
            stopHostingButton.Enabled = isHosting && !isLaunchingGame;
            connectButton.Enabled = !isHosting && !isConnectedAsClient && !isLaunchingGame;
            disconnectButton.Enabled = isConnectedAsClient && !isLaunchingGame;
            startGameButton.Enabled = isHosting && connectedPlayers.Count >= 2 && !isLaunchingGame;
        }

        private void AppendLog(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => AppendLog(message));
                return;
            }

            string line = $"[{DateTime.Now:HH:mm:ss}] {message}";
            if (string.IsNullOrWhiteSpace(logTextBox.Text))
                logTextBox.Text = line;
            else
                logTextBox.AppendText(Environment.NewLine + line);
        }

        private static IEnumerable<string> GetLocalIpv4Addresses()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties().UnicastAddresses)
                .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(a => a.Address.ToString())
                .Distinct()
                .DefaultIfEmpty("127.0.0.1");
        }

        private async void OnlineMultiplayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            formRefreshTimer.Stop();
            if (isLaunchingGame)
                return;

            await DisconnectClientAsync(true);
            await StopHostingAsync();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (isHosting)
                UpdateHostStatus();

            UpdateControlStates();
        }

        private sealed class HostClientConnection
        {
            public required TcpClient TcpClient { get; init; }
            public required StreamWriter Writer { get; init; }
            public required OnlinePlayerInfo Player { get; init; }
        }
    }
}
