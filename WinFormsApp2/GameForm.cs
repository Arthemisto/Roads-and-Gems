using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Indigo
{
    public partial class GameForm : Form
    {
        List<Tile> tiles = new List<Tile>();
        List<Gem> gems = new List<Gem>();                           // lists of main objects
        List<PlayerToken> playerTokens = new List<PlayerToken>();

        Vector2[] points;
        List<int> picNumbers = new List<int>();
        Tile[] placedTiles;
        List<Gem> movingGems = new List<Gem>();                     // additional lists of objects
        List<float> playersPoints = new List<float>();
        List<PictureBox> playerIcons = new List<PictureBox>();
        List<Label> playerScoreLabels = new List<Label>();
        List<string> playerColors = new List<string>();
        List<int[]> gatewayOwners = new List<int[]>();

        BoardImage boardImage;
        Tile? selectedTile;                                         // important objects
        Movement m1 = new Movement();
        Bitmap? staticLayer;

        int rings = 5;
        int totalTiles = 0;
        int totalGems = 12;                                         // game parameters
        float distanceFromCtoC = 10000;
        int numOfPlayers = 0;
        float scale = 1;

        int xPos = 50;
        int boardSeparation = 20;
        int tileNumber = -1;                                        // tile creation and movement visuals
        int lineAnimation = 0;
        int currentPlayerIndex = 0;
        int gemsLeft = 0;

        readonly int localPlayerId; //for multiplayer
        readonly bool isOnlineGame;
        readonly Func<TurnMessage, Task>? sendTurnAsync;
        bool yourTurn = false;

        static int widthOffset = 20;
        static int heightOffset = 5;                                // persition by eye

        bool debugMode = false;
        bool hideMode = false;                                      // modes

        bool leftDown = false;
        bool rightDown = false;                                     // mouse buttons
        readonly string gameStateLogPath = Path.Combine(AppContext.BaseDirectory, "game_state_log.jsonl");

        internal GameForm(
            int[] sizesOfObjects,
            float percent,
            int playerCount,
            int localPlayerId = 0,
            Func<TurnMessage, Task>? sendTurnAsync = null,
            List<string>? onlineColors = null)
        {
            InitializeComponent();

            placedTiles = new Tile[3 * rings * rings - 3 * rings + 1];
            numOfPlayers = playerCount;
            scale = percent;

            this.localPlayerId = localPlayerId;
            yourTurn = localPlayerId == 0;
            this.sendTurnAsync = sendTurnAsync;
            isOnlineGame = sendTurnAsync != null;
            yourTurn = localPlayerId == 0;          //Maybe unnecesarry

            SizeAdjustments(sizesOfObjects, percent);

            boardImage = new BoardImage();
            boardImage.position.X = boardSeparation * 2 + Tile.width;
            boardImage.position.Y = boardSeparation;

            distanceFromCtoC = Tile.width;

            var centerX = boardImage.position.X + BoardImage.width / 2;
            var centerY = boardImage.position.Y + BoardImage.height / 2;
            Vector2 center = new Vector2(centerX, centerY);

            points = CreateHexGrid(center, rings, distanceFromCtoC);

            SetUpApp();
            SetUpPlayerUi();

            BuildStaticLayer();

            if (isOnlineGame && onlineColors?.Any() == true)
            {
                playersButton.Visible = false;
                MakeTokens(onlineColors);
            }
        }
        private bool IsLocalPlayersTurn()
        {
            return !isOnlineGame || yourTurn;
        }
        public string CaptureStateHash()
        {
            GameStateSnapshot snapshot = CaptureSnapshot();
            string json = JsonSerializer.Serialize(snapshot);
            byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(json));
            return Convert.ToHexString(hashBytes);
        }
        public GameStateSnapshot CaptureSnapshot()
        {
            return new GameStateSnapshot
            {
                CurrentPlayerIndex = currentPlayerIndex,
                GemsLeft = gemsLeft,
                PlayerScores = new List<float>(playersPoints),
                Tiles = tiles.Select((tile, index) => new TileSnapshot
                {
                    TileIndex = index,
                    BoardIndex = tile.index,
                    Rotation = tile.numOfRotation,
                    PositionX = tile.position.X,
                    PositionY = tile.position.Y,
                    Active = tile.active,
                    Paths = tile.paths.ToList(),
                    Neighbors = tile.neighbors.ToList()
                }).ToList(),
                Gems = gems.Select((gem, index) => new GemSnapshot
                {
                    GemIndex = index,
                    OnTile = gem.onTile,
                    OnPath = gem.onPath,
                    PositionX = gem.position.X,
                    PositionY = gem.position.Y,
                    Active = gem.active
                }).ToList()
            };
        }
        public void ApplySnapshot(GameStateSnapshot snapshot)
        {
            if (snapshot.Tiles.Count != tiles.Count || snapshot.Gems.Count != gems.Count)
                return;

            placedTiles = new Tile[placedTiles.Length];
            movingGems.Clear();

            foreach (Tile tile in tiles)
                tile.gemsInside.Clear();

            foreach (TileSnapshot tileState in snapshot.Tiles.OrderBy(t => t.TileIndex))
            {
                Tile tile = tiles[tileState.TileIndex];

                tile.index = tileState.BoardIndex;
                tile.numOfRotation = ((tileState.Rotation % 6) + 6) % 6;
                tile.position = new Point(tileState.PositionX, tileState.PositionY);
                tile.rect.X = tile.position.X;
                tile.rect.Y = tile.position.Y + Tile.height / 8;
                tile.active = tileState.Active;
                tile.paths = tileState.Paths.Take(6).Concat(Enumerable.Repeat(-1, 6)).Take(6).ToArray();
                tile.neighbors = tileState.Neighbors.Take(6).Concat(Enumerable.Repeat(-1, 6)).Take(6).ToArray();

                if (tile.name != "Center" && tile.name != "Edge")
                    tile.picture = CreateTileImage(tile, tile.numOfRotation);

                if (tile.index >= 0 && tile.index < placedTiles.Length)
                    placedTiles[tile.index] = tile;
            }

            foreach (GemSnapshot gemState in snapshot.Gems.OrderBy(g => g.GemIndex))
            {
                Gem gem = gems[gemState.GemIndex];
                gem.onTile = gemState.OnTile;
                gem.onPath = gemState.OnPath;
                gem.position = new Point(gemState.PositionX, gemState.PositionY);
                gem.active = gemState.Active;

                if (gem.active)
                {
                    movingGems.Add(gem);
                    continue;
                }

                if (gem.onTile >= 0 && gem.onTile < placedTiles.Length && placedTiles[gem.onTile] != null)
                    placedTiles[gem.onTile].gemsInside.Add(gem);
            }

            currentPlayerIndex = snapshot.CurrentPlayerIndex;
            playersPoints = new List<float>(snapshot.PlayerScores);
            gemsLeft = snapshot.GemsLeft;
            yourTurn = currentPlayerIndex == localPlayerId;

            if (movingGems.Any())
            {
                m1 = new Movement();
                GemTimer.Start();
            }
            else
            {
                GemTimer.Stop();
            }

            UpdateScoreLabels();
            ShowCurrentTurnBanner();
            BuildStaticLayer();
            Board.Invalidate();
        }
        public bool ApplyAuthoritativeTurn(TurnMessage turn)
        {
            if (turn.TileIndex < 0 || turn.TileIndex >= tiles.Count)
                return false;
            if (turn.BoardIndex < 0 || turn.BoardIndex >= points.Length)
                return false;

            Tile tile = tiles[turn.TileIndex];
            if (tile.index != -1)
                return false;

            tile.active = false;
            SetTileRotation(tile, turn.Rotation);
            tile.position = new Point(
                (int)(points[turn.BoardIndex].X - Tile.width / 2f),
                (int)(points[turn.BoardIndex].Y - Tile.height / 2f)
            );

            bool applied = Snap(tile, false);
            if (!applied)
                return false;

            currentPlayerIndex = (turn.PlayerIndex + 1) % numOfPlayers;
            yourTurn = currentPlayerIndex == localPlayerId;
            ShowCurrentTurnBanner();
            BuildStaticLayer();
            Board.Invalidate();
            return true;
        }
        public void FinalizeAuthoritativeTurn(int playerIndex)
        {
            currentPlayerIndex = (playerIndex + 1) % numOfPlayers;
            yourTurn = currentPlayerIndex == localPlayerId;
            ShowCurrentTurnBanner();
            BuildStaticLayer();
            Board.Invalidate();
        }
        private bool CanConfigureOnlinePlayers()
        {
            return !isOnlineGame || localPlayerId == 0;
        }

        private void SetUpPlayerUi()
        {
            playerIcons = [player0, player1, player2, player3];
            playerScoreLabels = [playerScore0, playerScore1, playerScore2, playerScore3];
        }
        private void SizeAdjustments(int[] sizesOfObjects, float percent)
        {
            this.Width = (int)(this.Width * (percent + 0.1));
            this.Height = (int)(this.Height * (percent + 0.1));

            BoardImage.width = sizesOfObjects[0] + widthOffset;
            BoardImage.height = sizesOfObjects[1] + heightOffset;

            BoardImage.width = (int)(BoardImage.width * percent);
            BoardImage.height = (int)(BoardImage.height * percent);

            Tile.width = (int)(sizesOfObjects[2] * percent);
            Tile.height = (int)(sizesOfObjects[3] * percent);

            Gem.width = (int)(sizesOfObjects[4] * percent);
            Gem.height = (int)(sizesOfObjects[5] * percent);

            PlayerToken.width = (int)(sizesOfObjects[6] * percent);
            PlayerToken.height = (int)(sizesOfObjects[7] * percent);

            boardSeparation = (int)(boardSeparation * (percent + 0.1));
            Board.Width = boardSeparation * 3 + Tile.width + BoardImage.width;
            Board.Height = boardSeparation * 2 + BoardImage.height;

            //this.Width = Board.Location.X + BoardImage.width + boardSeparation;
            //this.Height = Board.Location.Y + BoardImage.height + boardSeparation;
        }
        private void GameForm_ResizeEnd(object sender, EventArgs e)
        {
            //int newWidth = this.Width - Board.Location.X - 40;
            //int newWidth = BoardImage.width + boardImage.position.X ;
            //int newHeight = BoardImage.height + boardImage.position.Y * 2;

            //if (newWidth > 0 && newHeight > 0)
            //{
            //    Board.Width = newWidth;
            //    Board.Height = newHeight;
            //}
            //else
            //    return;

            BuildStaticLayer();
            Board.Invalidate();

            debugLabel1.Text = "                (w by h) \nWindow: " + Width + " by " + Height +
                "\nBoard: \t\t" + Board.Width + " by " + Board.Height +
                "\nBoardImage: " + BoardImage.width + " by " + BoardImage.height;
        }
        private void SetUpApp()
        {
            for (int i = 0; i < 7; i++)
                picNumbers.Add(i);

            List<int> temp = new List<int>(picNumbers);

            temp.Remove(0);
            temp.Remove(1);     // center and edge

            for (int i = 0; i < 5; i++)
            {
                picNumbers.Insert(1, 1);        // add edge
                picNumbers.AddRange(temp);      // add 5 normal tiles i times
            }

            temp.Remove(2);
            temp.Remove(5);     // goBack and overlap


            for (int i = 0; i < 14 - 6; i++)
                picNumbers.AddRange(temp);

            totalTiles = picNumbers.Count;
            for (int i = 0; i < totalTiles; i++)
                MakeTiles();

            picNumbers.Clear();

            for (int i = 0; i < 3; i++)
                picNumbers.Add(i);

            for (int i = 0; i < 4; i++)
            {
                picNumbers.Insert(1, picNumbers[1]);

                int lasGem = picNumbers.Count - 1;
                picNumbers.Insert(lasGem, picNumbers[lasGem]);
            }
            picNumbers.Insert(7, picNumbers[7]);

            for (int i = 0; i < totalGems; i++)
            {
                MakeGems(i);
                gemsLeft++;
            }
                
            debugLabel1.Text = "                (w by h) \nWindow: " + Width + " by " + Height +
                "\nBoard: \t\t" + Board.Width + " by " + Board.Height +
                "\nBoardImage: " + BoardImage.width + " by " + BoardImage.height;
        }

        private void MakeTiles()
        {
            tileNumber++;
            Tile newTile = new Tile(picNumbers[tileNumber]);
            int placedIndex = -1;

            int x_1;
            int y_1;

            if (newTile.name == "Center")
            {
                x_1 = (int)(points[0].X - Tile.width / 2f);
                y_1 = (int)(points[0].Y - Tile.height / 2f);
                newTile.index = 0;
                placedIndex = 0;
            }
            else if (newTile.name == "Edge")
            {
                int[] temp = new int[6];
                int p = 37 + (tileNumber + 2) % 6;
                newTile.index = p;
                placedIndex = p;

                x_1 = (int)(points[p].X - Tile.width / 2f);
                y_1 = (int)(points[p].Y - Tile.height / 2f);

                if (tileNumber > 1)
                {
                    newTile.picture = ImageUtils.RotateHex(newTile.picture, 60f * (tileNumber - 1), Tile.width, Tile.height);

                    for (int i = 0; i < 6; i++)
                    {
                        temp[i] = newTile.paths[(i + 5 + (2 - tileNumber)) % 6];
                        if (temp[i] != -1)
                            temp[i] = (temp[i] + tileNumber - 1) % 6;
                    }

                    newTile.paths = temp;
                }
            }
            else
            {
                x_1 = boardSeparation;
                y_1 = (int)(((picNumbers[tileNumber] - 2) * 215 + xPos) * scale); //  215 = magic number

                if (tileNumber < 37 && (tileNumber - 7) % 6 == 0)
                    xPos -= 5;

                if (tileNumber >= 37 && (tileNumber - 7) % 4 == 0)
                    xPos -= 5;
            }

            newTile.position.X = x_1;
            newTile.position.Y = y_1;
            newTile.rect.X = newTile.position.X;
            newTile.rect.Y = newTile.position.Y + Tile.height / 8;

            tiles.Add(newTile);
            if (placedIndex != -1)
                placedTiles[placedIndex] = newTile;
        }
        private void MakeGems(int gemNumber)
        {
            Gem newGem = new Gem(picNumbers[gemNumber]);
            int x_1 = 0;
            int y_1 = 0;
            if (newGem.name == "Blue")
            {
                newGem.onTile = 0;
                x_1 = (int)(points[0].X - Gem.width / 2f);
                y_1 = (int)(points[0].Y - Gem.height / 2f);
            }
            else if (newGem.name == "Green")
            {
                newGem.onTile = 0;
                x_1 = (int)(points[0].X - Gem.width / 2f + 30 * scale * (float)Math.Cos(gemNumber * 72 * Math.PI / 180f));
                y_1 = (int)(points[0].Y - Gem.height / 2f + 30 * scale * (float)Math.Sin(gemNumber * 72 * Math.PI / 180f));     //  30 = magic number
            }
            else if (newGem.name == "Yellow")
            {
                newGem.onTile = 37 + gemNumber % 6;
                newGem.onPath = (4 + gemNumber % 6) % 6;

                Vector2 v_1 = Vector2.Lerp(points[newGem.onTile], points[newGem.onTile - 18], 1 / 4f);
                x_1 = (int)(v_1.X - Gem.width / 2f);
                y_1 = (int)(v_1.Y - Gem.height / 2f);
            }
            newGem.position.X = x_1;
            newGem.position.Y = y_1;

            gems.Add(newGem);
            placedTiles[newGem.onTile].gemsInside.Add(newGem);
        }
        private void MakeTokens(List<string> colors)
        {
            playerTokens.Clear();
            playerColors = new List<string>(colors);
            gatewayOwners = CreateGatewayOwners(colors.Count);

            foreach (int[] owners in gatewayOwners)
            {
                foreach (int owner in owners)
                    playerTokens.Add(new PlayerToken(owner, colors[owner]));
            }

            var r = Tile.height / 2 * 3 - 5;
            var numOfRotations = 7;

            for (int i = 0; i < playerTokens.Count; i++)
            {
                playerTokens[i].position.X = (int)(points[25 + i].X + r * (float)Math.Sin(numOfRotations * 60 * Math.PI / 180f) - PlayerToken.width / 2);
                playerTokens[i].position.Y = (int)(points[25 + i].Y + r * (float)Math.Cos(numOfRotations * 60 * Math.PI / 180f) - PlayerToken.width / 2);

                playerTokens[i + 1].position.X = (int)(points[26 + i].X + r * (float)Math.Sin(numOfRotations * 60 * Math.PI / 180f) - PlayerToken.width / 2);
                playerTokens[i + 1].position.Y = (int)(points[26 + i].Y + r * (float)Math.Cos(numOfRotations * 60 * Math.PI / 180f) - PlayerToken.width / 2);

                numOfRotations--;
                i++;
            }

            playersPoints.Clear();
            for (int i = 0; i < numOfPlayers; i++)
                playersPoints.Add(0);
            UpdatePlayerSidebar();
            UpdateScoreLabels();
            currentPlayerIndex = 0;
            ShowCurrentTurnBanner();

            foreach (var gem in gems.Where(g => g.onTile >= 43))
                ScoreUpdate(gem);

            playersButton.BackColor = Color.DarkGray;

            BuildStaticLayer();
            Board.Invalidate();
        }
        private List<int[]> CreateGatewayOwners(int playerCount)
        {
            return playerCount switch
            {
                2 =>
                [
                    [0, 0],
                    [1, 1],
                    [0, 0],
                    [1, 1],
                    [0, 0],
                    [1, 1]
                ],
                3 =>
                [
                    [0, 0],
                    [0, 1],
                    [2, 2],
                    [2, 0],
                    [1, 1],
                    [1, 2]
                ],
                4 =>
                [
                    [0, 1],
                    [1, 2],
                    [0, 3],
                    [3, 1],
                    [2, 0],
                    [2, 3]
                ],
                _ => throw new InvalidOperationException($"Unsupported player count: {playerCount}")
            };
        }
        private Image GetPlayerPicture(int playerIndex)
        {
            return new PlayerToken(playerIndex, playerColors[playerIndex]).picture;
        }
        public Vector2[] CreateHexGrid(Vector2 center, int totalNumOfRings, float originalR)
        {
            var points = new List<Vector2> { center };
            var r = originalR;

            for (int ring = 1; ring < totalNumOfRings; ring++)
            {
                for (int a = 0; a < 6; a++)
                {
                    var x_1 = center.X + r * (float)Math.Cos(a * 60 * Math.PI / 180f);
                    var y_1 = center.Y + r * (float)Math.Sin(a * 60 * Math.PI / 180f);
                    points.Add(new Vector2(x_1, y_1));
                }
                r += originalR;

                if (totalNumOfRings < 3 || ring == 1)
                    continue;

                int first = points.Count() - 6;

                for (int i = 0; i < 5; i++)
                    for (int j = 1; j < ring; j++)
                    {
                        Vector2 middlepoint = (Vector2.Lerp(points[first + i], points[first + i + 1], (float)j / ring));
                        points.Add(middlepoint);
                    }

                for (int j = 1; j < ring; j++)
                    points.Add(Vector2.Lerp(points[first + 5], points[first], (float)j / ring));
            }
            return points.ToArray();
        }

        public int GetClosestIndex(Vector2 v1)
        {
            Vector2 closestPoint = points.OrderByDescending(v2 => Vector2.Distance(v1, v2)).Last();

            if (Vector2.Distance(v1, closestPoint) > distanceFromCtoC / 2)
                return -1;

            int index = Array.IndexOf(points, closestPoint);

            return index;
        }
        private bool BorderApproved(Tile tile, int index)
        {
            if (index % 3 != 0)
                index += 3 - index % 3;

            int pathToBorder = ((index - 45) / 3 + 1) % 6;
            bool shortPathPlacedToBorder = tile.paths[pathToBorder] == (pathToBorder + 1) % 6;

            if (shortPathPlacedToBorder)
                return false;
            else
                return true;
        }
        private List<int> FindNeighbors(Vector2 pos)
        {
            List<Vector2> neighborsV = points.OrderBy(v => Vector2.Distance(pos, v)).Take(7).ToList();
            List<int> realNeighbors = new List<int>();

            if (neighborsV[0] == pos)
                neighborsV.RemoveAt(0);
            else
            {
                debugLabel1.Text = "Error in FindNeighbors";
                return realNeighbors;
            }

            foreach (Vector2 v in neighborsV)
            {
                int vIndex = Array.IndexOf(points, v);

                if (placedTiles[vIndex] != null && Vector2.Distance(pos, v) < distanceFromCtoC * 1.5)
                    realNeighbors.Add(vIndex);
            }

            return realNeighbors;
        }
        private bool Snap(Tile tile, bool broadcastTurn = true)
        {
            string stateHashBefore = string.Empty;
            if (broadcastTurn && isOnlineGame && yourTurn && sendTurnAsync != null)
                stateHashBefore = CaptureStateHash();

            Vector2 pos = new Vector2(tile.position.X + Tile.width / 2, tile.position.Y + Tile.height / 2);
            int index = GetClosestIndex(pos);

            if (index < 0 || placedTiles[index] != null)
                return false;

            if (index >= 43)
                if (!BorderApproved(tile, index))
                    return false;

            tile.index = index;
            Vector2 new_pos = points[index];

            int newX = (int)(new_pos.X - Tile.width / 2f);
            int newY = (int)(new_pos.Y - Tile.height / 2f);

            tile.position = new Point(newX, newY);
            placedTiles[index] = tile;

            if (broadcastTurn && isOnlineGame && yourTurn && sendTurnAsync != null)
            {
                int tileIndex = tiles.IndexOf(tile);

                TurnMessage turn = new TurnMessage
                {
                    PlayerIndex = currentPlayerIndex,
                    TileIndex = tileIndex,
                    Rotation = tile.numOfRotation,
                    BoardIndex = tile.index,
                    StateHashBefore = stateHashBefore
                };

                _ = sendTurnAsync(turn);

                yourTurn = false;
            }

            List<int> neighborIndexies = FindNeighbors(new_pos);
            if (!neighborIndexies.Any())
                return true;

            EventsAfterPlacement(tile, neighborIndexies);
            return true;
        }
        private void ShowCurrentTurnBanner()
        {
            if (numOfPlayers <= 0 || !playerColors.Any())
                return;

            turnBanner.Text = $"Player {currentPlayerIndex + 1} turn";
            turnBanner.Visible = true;
            turnBanner.BringToFront();
            TurnBannerTimer.Stop();
            TurnBannerTimer.Start();
        }
        private void AdvanceTurn()
        {
            if (numOfPlayers <= 0)
                return;

            currentPlayerIndex = (currentPlayerIndex + 1) % numOfPlayers;
            ShowCurrentTurnBanner();
        }
        private void UpdateScoreLabels()
        {
            for (int i = 0; i < playerScoreLabels.Count; i++)
            {
                if (i < playersPoints.Count)
                    playerScoreLabels[i].Text = " " + (int)playersPoints[i];
            }
        }
        private void UpdatePlayerSidebar()
        {
            for (int i = 0; i < playerIcons.Count; i++)
            {
                bool isUsed = i < numOfPlayers && i < playerColors.Count;
                playerIcons[i].Visible = isUsed;
                playerScoreLabels[i].Visible = isUsed;

                if (!isUsed)
                    continue;

                playerIcons[i].Image = GetPlayerPicture(i);
                playerScoreLabels[i].Text = " 0";
            }
        }
        private void EventsAfterPlacement(Tile placedTile, List<int> neighborIndexies)
        {
            foreach (var index in neighborIndexies)
            {
                Tile neighbor = placedTiles[index];
                int direction = FindDirection(placedTile, neighbor);

                if (direction == -1)
                {
                    debugLabel1.Text = "Error in FindDirection";
                    return;
                }

                placedTile.neighbors[direction] = index;

                direction = (direction + 3) % 6;
                neighbor.neighbors[direction] = placedTile.index;

                if (neighbor.gemsInside == null)
                    continue;

                if (neighbor.index == 0)
                {
                    int num = neighbor.gemsInside.Count - 1;
                    gems[num].onPath = direction;

                    var midPoint = (points[0] + points[placedTile.index]) / 2;

                    gems[num].position.X = (int)(midPoint.X - Gem.width / 2);
                    gems[num].position.Y = (int)(midPoint.Y - Gem.height / 2);
                }

                List<Gem> temp = new List<Gem>(neighbor.gemsInside);

                foreach (Gem gem in neighbor.gemsInside)
                    if (gem.onPath == direction)
                    {
                        if (neighbor.index > 36 && neighbor.index <= 42)
                        {
                            var midPoint = (points[neighbor.index - 18] + points[neighbor.index]) / 2;

                            gem.position.X = (int)(midPoint.X - Gem.width / 2);
                            gem.position.Y = (int)(midPoint.Y - Gem.height / 2);
                        }

                        gem.active = true;
                        movingGems.Add(gem);
                        GemTimer.Start();

                        temp.Remove(gem);

                    }

                neighbor.gemsInside = new List<Gem>(temp);
            }
        }
        Vector2 Bezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
        {
            float u = 1 - t;
            return u * u * p0 +
                   2 * u * t * p1 +
                   t * t * p2;
        }
        private int FindDirection(Tile tile, Tile neighbor)
        {
            var deltaX = tile.position.X - neighbor.position.X;
            var deltaY = tile.position.Y - neighbor.position.Y;

            if (deltaX < 0)
            {
                if (deltaY > 0)
                    return 0;
                else if (deltaY < 0)
                    return 2;
                return 1;
            }
            if (deltaX > 0)
            {
                if (deltaY < 0)
                    return 3;
                else if (deltaY > 0)
                    return 5;
                return 4;
            }
            return -1;
        }
        private Image CreateTileImage(Tile tile, int rotation)
        {
            int normalizedRotation = ((rotation % 6) + 6) % 6;
            Image image = new Bitmap(tile.originalPic);

            if (normalizedRotation == 0)
                return image;

            if (normalizedRotation == 3)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                return image;
            }

            return ImageUtils.RotateHex(image, 60f * normalizedRotation, Tile.width, Tile.height);
        }
        private void SetTileRotation(Tile tile, int rotation)
        {
            Tile template = new Tile(tile.templateId);

            tile.numOfRotation = 0;
            tile.paths = (int[])template.paths.Clone();
            tile.picture = new Bitmap(tile.originalPic);

            int normalizedRotation = ((rotation % 6) + 6) % 6;
            for (int i = 0; i < normalizedRotation; i++)
                RotateTile(tile, true);
        }
        private void RotateTile(Tile tile, bool clockwise)
        {
            int num = 1;
            if (!clockwise)
                num = -1;

            tile.numOfRotation = (6 + tile.numOfRotation + num) % 6;

            if (tile.numOfRotation == 0)
                tile.picture = new Bitmap(tile.originalPic);
            else if (tile.numOfRotation == 3)
            {
                tile.picture = new Bitmap(tile.originalPic);
                tile.picture.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else
            {
                var rotation = 60f;
                if (!clockwise)
                    rotation = -60f;

                tile.picture = ImageUtils.RotateHex(tile.picture, rotation, Tile.width, Tile.height);
            }

            int[] temp = new int[6];
            if (clockwise)
                for (int i = 0; i < 6; i++)
                {
                    temp[i] = tile.paths[(i + 5) % 6];
                    if (temp[i] != -1)
                        temp[i] = (temp[i] + 1) % 6;
                }
            else
                for (int i = 0; i < 6; i++)
                {
                    temp[i] = tile.paths[(i + 1) % 6];
                    if (temp[i] != -1)
                        temp[i] = (temp[i] + 5) % 6;
                }

            tile.paths = temp;
        }
        private void ScoreUpdate(Gem gem)
        {
            float score;
            int tileIndex = gem.onTile;

            if (tileIndex % 3 != 0)
                tileIndex += 3 - tileIndex % 3;

            int border = (tileIndex - 45) / 3 % 6;

            if (debugMode)
                debugLabel2.Text = "Border parameters: \nGem path: " + gem.onPath +
                    "\nBorder num: " + border + "    tile index = " + tileIndex +
                    "\nExit paths: " + (border + 1) % 6 + " or " + (border + 2) % 6;

            if ((border + 1) % 6 != gem.onPath && (border + 2) % 6 != gem.onPath)       // Bug fix
                return;

            if (gem.name == "Blue")
                score = 3;
            else if (gem.name == "Green")
                score = 2;
            else
                score = 1;

            foreach (int owner in gatewayOwners[border].Distinct())
                playersPoints[owner] += score;

            UpdateScoreLabels();

            gemsLeft--;
            if (gemsLeft == 0)
                GameEnd();
        }
        private void GameEnd()
        {
            bool youWon = true;

            for (int i = 0; i < numOfPlayers; i++)
                if (playersPoints[localPlayerId] < playersPoints[i])
                    youWon = false;

            using (var form = new GameEndForm(youWon)) 
            {
                if (form.ShowDialog() == DialogResult.OK)
                    Close();
            };
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsLocalPlayersTurn())
                return;

            if (selectedTile == null)
                return;

            if (debugMode)
            {
                debugLabel2.Text = "Rotation: \nCurrent rotation num: " + selectedTile.numOfRotation + "\nPaths: \n[" + selectedTile.paths[0];
                for (int i = 0; i < 5; i++)
                    debugLabel2.Text += ", " + selectedTile.paths[i + 1];
                debugLabel2.Text += "]";
            }

            switch (e.KeyCode)                      // TODO: Keys.Left and Keys.Righ not working
            {
                case Keys.A:
                case Keys.Left:
                    RotateTile(selectedTile, false);
                    break;
                case Keys.D:
                case Keys.Right:
                    RotateTile(selectedTile, true);
                    break;
                default:
                    break;
            }

            if (debugMode)
            {
                debugLabel2.Text += "\n\nLast rotation num: " + selectedTile.numOfRotation + "\nPaths: \n[" + selectedTile.paths[0];
                for (int i = 0; i < 5; i++)
                    debugLabel2.Text += ", " + selectedTile.paths[i + 1];
                debugLabel2.Text += "]";
            }

            Board.Invalidate();
        }
        private void BoardMouseDown(object sender, MouseEventArgs e)
        {
            if (!IsLocalPlayersTurn())
                return;

            if (hideMode)
                return;

            if (e.Button == MouseButtons.Left)
                leftDown = true;
            if (e.Button == MouseButtons.Right)
                rightDown = true;

            if (rightDown && selectedTile != null)
            {
                RotateTile(selectedTile, true);
                Board.Invalidate();
            }

            if (leftDown)
            {
                Point mousePosition = new Point(e.X, e.Y);

                foreach (Tile newTile in tiles)
                    if (newTile.rect.Contains(mousePosition) && newTile.index == -1)
                    {
                        selectedTile = newTile;
                        newTile.active = true;

                        BuildStaticLayer();

                        return;
                    }
            }
        }
        private void BoardMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsLocalPlayersTurn())
                return;

            if (leftDown && selectedTile != null && Board.DisplayRectangle.Contains(e.X, e.Y))
            {
                selectedTile.position.X = e.X - (Tile.width / 2);
                selectedTile.position.Y = e.Y - (Tile.height / 2);

                if (debugMode)
                    debugLabel1.Text = "mousePosition: " + e.X + " " + e.Y +
                        "\nTile number: " + tiles.FindIndex(t => t == selectedTile);
            }
        }
        private void BoardMouseUp(object sender, MouseEventArgs e)
        {
            if (!IsLocalPlayersTurn())
                return;

            if (e.Button == MouseButtons.Left)
                leftDown = false;
            if (e.Button == MouseButtons.Right)
                rightDown = false;

            Tile? temp = null;

            if (!leftDown && selectedTile != null)
            {
                bool placedSuccessfully = Snap(selectedTile);
                temp = selectedTile;

                selectedTile.active = false;
                selectedTile = null;

                lineAnimation = 0;

                if (placedSuccessfully && playerTokens.Any() && !isOnlineGame)
                    AdvanceTurn();
            }

            BuildStaticLayer();
            Board.Invalidate();

            if (!debugMode || temp == null)
                return;

            debugLabel1.Text = "Tile was plased " + placedTiles.Count(x => x != null) + "th";

            debugLabel1.Text += "\n\nIndex: " + temp.index + "\nNeighbors: \n[0, 1, 2, 3, 4, 5] \n[" + temp.paths[0];
            for (int i = 0; i < 5; i++)
                debugLabel1.Text += ", " + temp.paths[i + 1];
            debugLabel1.Text += "] \n[" + temp.neighbors[0];

            for (int i = 0; i < 5; i++)
                debugLabel1.Text += ", " + temp.neighbors[i + 1];
            debugLabel1.Text += ']';

            temp = placedTiles[0];
            debugLabel1.Text += "\n\nIndex: " + temp.index + "\nNeighbors: \n[  0,  1,  2,  3,  4,  5]    \n[" + temp.neighbors[0];
            for (int i = 0; i < 5; i++)
                debugLabel1.Text += ", " + temp.neighbors[i + 1];
            debugLabel1.Text += ']';
        }

        private void FormTimerEvent(object sender, EventArgs e)
        {
            if (selectedTile != null)
            {
                selectedTile.rect.X = selectedTile.position.X;
                selectedTile.rect.Y = selectedTile.position.Y + Tile.height / 8;

                if (lineAnimation < 5)
                    lineAnimation++;

                Board.Invalidate();
            }
            //Board.Invalidate()
        }
        private void GemTimerEvent(object sender, EventArgs e)
        {
            Board.Invalidate();

            if (!movingGems.Any())
            {
                GemTimer.Stop();
                return;
            }
            Gem gem = movingGems[0];

            if (m1.t == 0f)
            {
                Tile currentTile = placedTiles[gem.onTile];
                int neighborIndex = currentTile.neighbors[gem.onPath];
                Tile nextTile = placedTiles[neighborIndex];

                int enteringBy = (gem.onPath + 3) % 6;
                m1.willExitBy = nextTile.paths[enteringBy];

                Vector2 currentPoint = points[currentTile.index];
                Vector2 middlePoint = points[nextTile.index];

                Vector2 startPoint = (currentPoint + middlePoint) / 2;

                var x_1 = middlePoint.X + distanceFromCtoC / 2 * (float)Math.Cos((m1.willExitBy - 1) * 60 * Math.PI / 180f);
                var y_1 = middlePoint.Y + distanceFromCtoC / 2 * (float)Math.Sin((m1.willExitBy - 1) * 60 * Math.PI / 180f);

                Vector2 endPoint = new Vector2(x_1, y_1);

                int diff = Math.Abs(enteringBy - m1.willExitBy);
                if (diff > 3)
                    diff = 6 - diff;

                if (diff == 1)
                {
                    Vector2 mid = (startPoint + endPoint) / 2;
                    Vector2 direction = Vector2.Normalize(mid - middlePoint);

                    middlePoint += direction * 20f;
                    m1.speed = 1.5f;
                }

                m1.startPoint = startPoint;
                m1.middlePoint = middlePoint;
                m1.endPoint = endPoint;
                m1.nextTile = nextTile;
                m1.diff = diff;
            }

            if (m1.t > 1f)                          // if reached the end of road
            {
                if (m1.nextTile == null)
                    return;

                gem.onPath = m1.willExitBy;
                gem.onTile = m1.nextTile.index;

                int anotherTile = m1.nextTile.neighbors[gem.onPath];

                Vector2 anotherPoint = m1.endPoint;

                if (anotherTile == -1)              // if no further road
                {
                    gem.active = false;
                    movingGems.Remove(gem);
                    m1.nextTile.gemsInside.Add(gem);
                    if (gem.onTile >= 43 && playerTokens.Any())
                        ScoreUpdate(gem);
                }
                else
                    anotherPoint = (points[gem.onTile] + points[anotherTile]) / 2f;


                gem.position.X = (int)(anotherPoint.X - Gem.width / 2);
                gem.position.Y = (int)(anotherPoint.Y - Gem.height / 2);

                m1.t = 0f;
                m1.speed = 1f;

                List<Gem> activeGems = gems.Where(g => g.active == true).ToList();
                activeGems.Remove(gem);

                if (activeGems.Any())               // gem collision
                    foreach (var anotherGem in activeGems)
                        if (gem.position == anotherGem.position)
                        {
                            gems.Remove(anotherGem);
                            gems.Remove(gem);
                            movingGems.Remove(anotherGem);
                            movingGems.Remove(gem);
                            return;
                        }

                BuildStaticLayer();
                return;
            }

            m1.t += 0.04f * m1.speed;

            var currentPosition = Bezier(m1.startPoint, m1.middlePoint, m1.endPoint, m1.t);

            gem.position.X = (int)currentPosition.X - Gem.width / 2;
            if (m1.diff != 3 || m1.willExitBy % 3 != 1)
                gem.position.Y = (int)currentPosition.Y - Gem.height / 2;

            Invalidate();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;

            //Get the middle of the panel
            var x_0 = panel1.Width / 2;
            var y_0 = panel1.Height / 2;

            var shape = new PointF[6];

            var r = 70; //70 px radius 

            //Create 6 points
            for (int a = 0; a < 6; a++)
            {
                shape[a] = new PointF(
                    x_0 + r * (float)Math.Sin(a * 60 * Math.PI / 180f),
                    y_0 + r * (float)Math.Cos(a * 60 * Math.PI / 180f)
                    );
            }


            graphics.DrawPolygon(Pens.Red, shape);
        }       //unused
        private void BuildStaticLayer()
        {
            staticLayer?.Dispose();

            staticLayer = new Bitmap(Board.Width, Board.Height);

            using (Graphics g = Graphics.FromImage(staticLayer))
            {
                g.DrawImage(
                    boardImage.picture,
                    boardImage.position.X,
                    boardImage.position.Y,
                    BoardImage.width,
                    BoardImage.height
                );

                if (!hideMode)
                {
                    foreach (Tile tile in tiles)
                    {
                        if (tile.active)
                            continue;

                        g.DrawImage(tile.picture, tile.position.X, tile.position.Y, Tile.width, Tile.height);
                    }

                    for (int i = 0; i < playerTokens.Count(); i++)
                    {
                        g.DrawImage(playerTokens[i].picture, playerTokens[i].position.X, playerTokens[i].position.Y, PlayerToken.width, PlayerToken.height);

                        if (debugMode)
                        {
                            g.DrawString(i.ToString(), Font, Brushes.Red, playerTokens[i].position.X + 25, playerTokens[i].position.Y);
                            g.FillRectangle(Brushes.Gray, playerTokens[i].position.X - 3, playerTokens[i].position.Y - 3, 6, 6);
                        }
                    }

                    for (int i = 0; i < gems.Count(); i++)
                    {
                        if (gems[i].active)
                            continue;

                        g.DrawImage(gems[i].picture, gems[i].position.X, gems[i].position.Y, Gem.width, Gem.height);

                        if (debugMode)
                        {
                            g.DrawString(i.ToString(), Font, Brushes.Red, gems[i].position.X + 25, gems[i].position.Y);
                            g.FillRectangle(Brushes.Gray, gems[i].position.X, gems[i].position.Y, 5, 5);
                        }
                    }
                }
            }
        }
        private void Board_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            if (staticLayer != null)
                g.DrawImage(staticLayer, 0, 0);

            if (!hideMode)
            {
                for (int i = 0; i < gems.Count(); i++)
                {
                    if (!gems[i].active)
                        continue;

                    g.DrawImage(gems[i].picture, gems[i].position.X, gems[i].position.Y, Gem.width, Gem.height);
                    if (debugMode)
                    {
                        g.DrawString(i.ToString(), Font, Brushes.Red, gems[i].position.X + 25, gems[i].position.Y);
                        g.FillRectangle(Brushes.Gray, gems[i].position.X, gems[i].position.Y, 5, 5);
                    }
                }

                if (selectedTile != null)
                {
                    g.DrawImage(selectedTile.picture, selectedTile.position.X, selectedTile.position.Y, Tile.width, Tile.height);

                    Pen outline = new Pen(Color.Maroon, lineAnimation);
                    g.DrawRectangle(outline, selectedTile.rect);
                }
            }

            if (debugMode)
            {
                Brush[] brushes = [Brushes.Green, Brushes.Red, Brushes.Blue, Brushes.Yellow, Brushes.Magenta, Brushes.DarkBlue];

                var shape = new PointF[6];
                var r = Tile.height / 2;
                int pointOffset = 2;

                g.FillRectangle(Brushes.Black, points[0].X, points[0].Y, 5, 5);
                for (int a = 0; a < 6; a++)
                {
                    shape[a] = new PointF(
                        points[0].X + r * (float)Math.Sin(a * 60 * Math.PI / 180f),
                        points[0].Y + r * (float)Math.Cos(a * 60 * Math.PI / 180f)
                        );
                }
                g.DrawPolygon(Pens.Red, shape);

                for (int i = 1; i < points.Count(); i++)
                {
                    Vector2 p = points[i];

                    g.FillRectangle(
                        brushes[i % 6],
                        p.X - pointOffset,
                        p.Y - pointOffset,
                        pointOffset * 2,
                        pointOffset * 2
                    );

                    g.DrawString(
                        i.ToString(),
                        Font,
                        Brushes.Black,
                        p.X + 12,
                        p.Y
                    );


                    for (int a = 0; a < 6; a++)
                    {
                        shape[a] = new PointF(
                            p.X + r * (float)Math.Sin(a * 60 * Math.PI / 180f),
                            p.Y + r * (float)Math.Cos(a * 60 * Math.PI / 180f)
                            );
                    }

                    g.DrawPolygon(Pens.Red, shape);
                }


                pointOffset = 5;
                g.FillRectangle(
                    Brushes.Cyan,
                    m1.startPoint.X - pointOffset,
                    m1.startPoint.Y - pointOffset,
                    pointOffset * 2,
                    pointOffset * 2
                );
                g.FillRectangle(
                        Brushes.DarkGray,
                        m1.middlePoint.X - pointOffset,
                        m1.middlePoint.Y - pointOffset,
                        pointOffset * 2,
                        pointOffset * 2
                    );
                g.FillRectangle(
                        Brushes.Orange,
                        m1.endPoint.X - pointOffset,
                        m1.endPoint.Y - pointOffset,
                        pointOffset * 2,
                        pointOffset * 2
                    );

                g.DrawPolygon(Pens.Red, shape);
            }

            return;
        }

        private void PlayersButton_Click(object sender, EventArgs e)
        {
            if (!CanConfigureOnlinePlayers())
                return;

            if (player0.Image != null)
                return;

            using (var form = new PlayerForm(numOfPlayers))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    MakeTokens(form.playerColors);
            }
        }
        private void RulesButton_Click(object sender, EventArgs e)
        {
            shortRules.Visible = !shortRules.Visible;
            controlsPicture.Visible = !controlsPicture.Visible;

            if (shortRules.Visible)
                rulesButton.BackColor = Color.DarkGray;
            else
                rulesButton.BackColor = Color.White;
        }
        private void DebugButton_Click(object sender, EventArgs e)
        {
            debugMode = !debugMode;

            if (debugMode)
                debugButton.BackColor = Color.DarkGray;
            else
                debugButton.BackColor = Color.White;

            debugLabel1.Visible = debugMode;
            debugLabel2.Visible = debugMode;
            panel1.Visible = debugMode;

            label2.Visible = !debugMode;
            player0.Visible = !debugMode;
            player1.Visible = !debugMode;
            player2.Visible = !debugMode && numOfPlayers >= 3;
            player3.Visible = !debugMode && numOfPlayers >= 4;
            playerScore0.Visible = !debugMode;
            playerScore1.Visible = !debugMode;
            playerScore2.Visible = !debugMode && numOfPlayers >= 3;
            playerScore3.Visible = !debugMode && numOfPlayers >= 4;
            controlsLabel.Visible = !debugMode;
            turnBanner.Visible = !debugMode && turnBanner.Visible;

            BuildStaticLayer();
            Board.Invalidate();
        }
        private void HideTilesButton_Click(object sender, EventArgs e)
        {
            hideMode = !hideMode;

            if (hideMode)
                hideButton.BackColor = Color.DarkGray;
            else
                hideButton.BackColor = Color.White;

            BuildStaticLayer();
            Board.Invalidate();
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void TurnBannerTimer_Tick(object sender, EventArgs e)
        {
            TurnBannerTimer.Stop();
            turnBanner.Visible = false;
        }
    }
}

