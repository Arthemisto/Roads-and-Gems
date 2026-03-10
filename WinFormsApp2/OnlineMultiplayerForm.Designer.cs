namespace Indigo
{
    partial class OnlineMultiplayerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            titleLabel = new Label();
            nameLabel = new Label();
            playerNameTextBox = new TextBox();
            lobbyTabs = new TabControl();
            hostPage = new TabPage();
            hostStatusLabel = new Label();
            hostInfoLabel = new Label();
            stopHostingButton = new Button();
            startHostingButton = new Button();
            hostPortInput = new NumericUpDown();
            hostPortLabel = new Label();
            hostMaxPlayersInput = new NumericUpDown();
            hostMaxPlayersLabel = new Label();
            joinPage = new TabPage();
            disconnectButton = new Button();
            joinStatusLabel = new Label();
            connectButton = new Button();
            joinPortInput = new NumericUpDown();
            joinPortLabel = new Label();
            joinIpTextBox = new TextBox();
            joinIpLabel = new Label();
            playersLabel = new Label();
            playersListBox = new ListBox();
            logLabel = new Label();
            logTextBox = new TextBox();
            closeButton = new Button();
            formRefreshTimer = new System.Windows.Forms.Timer(components);
            lobbyTabs.SuspendLayout();
            hostPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)hostPortInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hostMaxPlayersInput).BeginInit();
            joinPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)joinPortInput).BeginInit();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.BackColor = Color.Gray;
            titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            titleLabel.Location = new Point(25, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(835, 48);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Online Multiplayer";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            nameLabel.BackColor = Color.Gray;
            nameLabel.Font = new Font("Segoe UI", 12F);
            nameLabel.Location = new Point(25, 84);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(160, 34);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "Your name";
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // playerNameTextBox
            // 
            playerNameTextBox.Location = new Point(198, 88);
            playerNameTextBox.MaxLength = 24;
            playerNameTextBox.Name = "playerNameTextBox";
            playerNameTextBox.Size = new Size(246, 27);
            playerNameTextBox.TabIndex = 2;
            playerNameTextBox.Text = "Player";
            // 
            // lobbyTabs
            // 
            lobbyTabs.Controls.Add(hostPage);
            lobbyTabs.Controls.Add(joinPage);
            lobbyTabs.Location = new Point(25, 135);
            lobbyTabs.Name = "lobbyTabs";
            lobbyTabs.SelectedIndex = 0;
            lobbyTabs.Size = new Size(419, 342);
            lobbyTabs.TabIndex = 3;
            // 
            // hostPage
            // 
            hostPage.BackColor = Color.FromArgb(80, 80, 80);
            hostPage.Controls.Add(hostStatusLabel);
            hostPage.Controls.Add(hostInfoLabel);
            hostPage.Controls.Add(stopHostingButton);
            hostPage.Controls.Add(startHostingButton);
            hostPage.Controls.Add(hostPortInput);
            hostPage.Controls.Add(hostPortLabel);
            hostPage.Controls.Add(hostMaxPlayersInput);
            hostPage.Controls.Add(hostMaxPlayersLabel);
            hostPage.Location = new Point(4, 29);
            hostPage.Name = "hostPage";
            hostPage.Padding = new Padding(3);
            hostPage.Size = new Size(411, 309);
            hostPage.TabIndex = 0;
            hostPage.Text = "Host";
            // 
            // hostStatusLabel
            // 
            hostStatusLabel.BackColor = Color.Gray;
            hostStatusLabel.Location = new Point(16, 230);
            hostStatusLabel.Name = "hostStatusLabel";
            hostStatusLabel.Size = new Size(377, 60);
            hostStatusLabel.TabIndex = 7;
            hostStatusLabel.Text = "Host idle";
            // 
            // hostInfoLabel
            // 
            hostInfoLabel.BackColor = Color.Gray;
            hostInfoLabel.Location = new Point(16, 102);
            hostInfoLabel.Name = "hostInfoLabel";
            hostInfoLabel.Size = new Size(377, 112);
            hostInfoLabel.TabIndex = 6;
            hostInfoLabel.Text = "Start hosting to see IP and port";
            // 
            // stopHostingButton
            // 
            stopHostingButton.Enabled = false;
            stopHostingButton.Location = new Point(218, 50);
            stopHostingButton.Name = "stopHostingButton";
            stopHostingButton.Size = new Size(175, 32);
            stopHostingButton.TabIndex = 5;
            stopHostingButton.Text = "Stop Hosting";
            stopHostingButton.UseVisualStyleBackColor = true;
            stopHostingButton.Click += stopHostingButton_Click;
            // 
            // startHostingButton
            // 
            startHostingButton.Location = new Point(16, 50);
            startHostingButton.Name = "startHostingButton";
            startHostingButton.Size = new Size(175, 32);
            startHostingButton.TabIndex = 4;
            startHostingButton.Text = "Start Hosting";
            startHostingButton.UseVisualStyleBackColor = true;
            startHostingButton.Click += startHostingButton_Click;
            // 
            // hostPortInput
            // 
            hostPortInput.Location = new Point(286, 16);
            hostPortInput.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            hostPortInput.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            hostPortInput.Name = "hostPortInput";
            hostPortInput.Size = new Size(107, 27);
            hostPortInput.TabIndex = 3;
            hostPortInput.Value = new decimal(new int[] { 4040, 0, 0, 0 });
            // 
            // hostPortLabel
            // 
            hostPortLabel.BackColor = Color.Gray;
            hostPortLabel.Location = new Point(218, 16);
            hostPortLabel.Name = "hostPortLabel";
            hostPortLabel.Size = new Size(58, 27);
            hostPortLabel.TabIndex = 2;
            hostPortLabel.Text = "Port";
            hostPortLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // hostMaxPlayersInput
            // 
            hostMaxPlayersInput.Location = new Point(140, 16);
            hostMaxPlayersInput.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            hostMaxPlayersInput.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            hostMaxPlayersInput.Name = "hostMaxPlayersInput";
            hostMaxPlayersInput.Size = new Size(58, 27);
            hostMaxPlayersInput.TabIndex = 1;
            hostMaxPlayersInput.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // hostMaxPlayersLabel
            // 
            hostMaxPlayersLabel.BackColor = Color.Gray;
            hostMaxPlayersLabel.Location = new Point(16, 16);
            hostMaxPlayersLabel.Name = "hostMaxPlayersLabel";
            hostMaxPlayersLabel.Size = new Size(114, 27);
            hostMaxPlayersLabel.TabIndex = 0;
            hostMaxPlayersLabel.Text = "Max players";
            hostMaxPlayersLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // joinPage
            // 
            joinPage.BackColor = Color.FromArgb(80, 80, 80);
            joinPage.Controls.Add(disconnectButton);
            joinPage.Controls.Add(joinStatusLabel);
            joinPage.Controls.Add(connectButton);
            joinPage.Controls.Add(joinPortInput);
            joinPage.Controls.Add(joinPortLabel);
            joinPage.Controls.Add(joinIpTextBox);
            joinPage.Controls.Add(joinIpLabel);
            joinPage.Location = new Point(4, 29);
            joinPage.Name = "joinPage";
            joinPage.Padding = new Padding(3);
            joinPage.Size = new Size(411, 309);
            joinPage.TabIndex = 1;
            joinPage.Text = "Join";
            // 
            // disconnectButton
            // 
            disconnectButton.Enabled = false;
            disconnectButton.Location = new Point(218, 91);
            disconnectButton.Name = "disconnectButton";
            disconnectButton.Size = new Size(175, 32);
            disconnectButton.TabIndex = 6;
            disconnectButton.Text = "Disconnect";
            disconnectButton.UseVisualStyleBackColor = true;
            disconnectButton.Click += disconnectButton_Click;
            // 
            // joinStatusLabel
            // 
            joinStatusLabel.BackColor = Color.Gray;
            joinStatusLabel.Location = new Point(16, 146);
            joinStatusLabel.Name = "joinStatusLabel";
            joinStatusLabel.Size = new Size(377, 96);
            joinStatusLabel.TabIndex = 5;
            joinStatusLabel.Text = "Join idle";
            // 
            // connectButton
            // 
            connectButton.Location = new Point(16, 91);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(175, 32);
            connectButton.TabIndex = 4;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // joinPortInput
            // 
            joinPortInput.Location = new Point(286, 47);
            joinPortInput.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            joinPortInput.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            joinPortInput.Name = "joinPortInput";
            joinPortInput.Size = new Size(107, 27);
            joinPortInput.TabIndex = 3;
            joinPortInput.Value = new decimal(new int[] { 4040, 0, 0, 0 });
            // 
            // joinPortLabel
            // 
            joinPortLabel.BackColor = Color.Gray;
            joinPortLabel.Location = new Point(218, 47);
            joinPortLabel.Name = "joinPortLabel";
            joinPortLabel.Size = new Size(58, 27);
            joinPortLabel.TabIndex = 2;
            joinPortLabel.Text = "Port";
            joinPortLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // joinIpTextBox
            // 
            joinIpTextBox.Location = new Point(84, 16);
            joinIpTextBox.Name = "joinIpTextBox";
            joinIpTextBox.Size = new Size(309, 27);
            joinIpTextBox.TabIndex = 1;
            joinIpTextBox.Text = "127.0.0.1";
            // 
            // joinIpLabel
            // 
            joinIpLabel.BackColor = Color.Gray;
            joinIpLabel.Location = new Point(16, 16);
            joinIpLabel.Name = "joinIpLabel";
            joinIpLabel.Size = new Size(58, 27);
            joinIpLabel.TabIndex = 0;
            joinIpLabel.Text = "IP";
            joinIpLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // playersLabel
            // 
            playersLabel.BackColor = Color.Gray;
            playersLabel.Font = new Font("Segoe UI", 12F);
            playersLabel.Location = new Point(462, 135);
            playersLabel.Name = "playersLabel";
            playersLabel.Size = new Size(180, 34);
            playersLabel.TabIndex = 4;
            playersLabel.Text = "Connected Players";
            playersLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // playersListBox
            // 
            playersListBox.FormattingEnabled = true;
            playersListBox.Location = new Point(462, 180);
            playersListBox.Name = "playersListBox";
            playersListBox.Size = new Size(180, 264);
            playersListBox.TabIndex = 5;
            // 
            // logLabel
            // 
            logLabel.BackColor = Color.Gray;
            logLabel.Font = new Font("Segoe UI", 12F);
            logLabel.Location = new Point(660, 135);
            logLabel.Name = "logLabel";
            logLabel.Size = new Size(200, 34);
            logLabel.TabIndex = 6;
            logLabel.Text = "Session Log";
            logLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // logTextBox
            // 
            logTextBox.Location = new Point(660, 180);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.ScrollBars = ScrollBars.Vertical;
            logTextBox.Size = new Size(200, 264);
            logTextBox.TabIndex = 7;
            // 
            // closeButton
            // 
            closeButton.Location = new Point(685, 459);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(175, 36);
            closeButton.TabIndex = 8;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // formRefreshTimer
            // 
            formRefreshTimer.Enabled = true;
            formRefreshTimer.Interval = 500;
            formRefreshTimer.Tick += formRefreshTimer_Tick;
            // 
            // OnlineMultiplayerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(884, 514);
            Controls.Add(closeButton);
            Controls.Add(logTextBox);
            Controls.Add(logLabel);
            Controls.Add(playersListBox);
            Controls.Add(playersLabel);
            Controls.Add(lobbyTabs);
            Controls.Add(playerNameTextBox);
            Controls.Add(nameLabel);
            Controls.Add(titleLabel);
            Name = "OnlineMultiplayerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Online Multiplayer";
            FormClosing += OnlineMultiplayerForm_FormClosing;
            lobbyTabs.ResumeLayout(false);
            hostPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)hostPortInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)hostMaxPlayersInput).EndInit();
            joinPage.ResumeLayout(false);
            joinPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)joinPortInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titleLabel;
        private Label nameLabel;
        private TextBox playerNameTextBox;
        private TabControl lobbyTabs;
        private TabPage hostPage;
        private TabPage joinPage;
        private NumericUpDown hostPortInput;
        private Label hostPortLabel;
        private NumericUpDown hostMaxPlayersInput;
        private Label hostMaxPlayersLabel;
        private Button stopHostingButton;
        private Button startHostingButton;
        private Label hostInfoLabel;
        private Label hostStatusLabel;
        private Button disconnectButton;
        private Label joinStatusLabel;
        private Button connectButton;
        private NumericUpDown joinPortInput;
        private Label joinPortLabel;
        private TextBox joinIpTextBox;
        private Label joinIpLabel;
        private Label playersLabel;
        private ListBox playersListBox;
        private Label logLabel;
        private TextBox logTextBox;
        private Button closeButton;
        private System.Windows.Forms.Timer formRefreshTimer;
    }
}
