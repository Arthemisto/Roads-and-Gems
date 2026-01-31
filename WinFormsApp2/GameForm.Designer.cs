namespace Indigo
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            debugLabel1 = new Label();
            FormTimer = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            Board = new DoubleBufferedPanel();
            controlsPicture = new PictureBox();
            shortRules = new PictureBox();
            debugLabel2 = new Label();
            GemTimer = new System.Windows.Forms.Timer(components);
            debugButton = new Button();
            playersButton = new Button();
            label1 = new Label();
            hideButton = new Button();
            player0 = new PictureBox();
            player1 = new PictureBox();
            playerScore0 = new Label();
            playerScore1 = new Label();
            label2 = new Label();
            backButton = new Button();
            rulesButton = new Button();
            playerScore3 = new Label();
            playerScore2 = new Label();
            player3 = new PictureBox();
            player2 = new PictureBox();
            controlsLabel = new Label();
            Board.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)controlsPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)shortRules).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player2).BeginInit();
            SuspendLayout();
            // 
            // debugLabel1
            // 
            debugLabel1.BackColor = SystemColors.ControlDarkDark;
            debugLabel1.Font = new Font("Segoe UI", 14F);
            debugLabel1.ForeColor = SystemColors.ActiveCaptionText;
            debugLabel1.Location = new Point(12, 197);
            debugLabel1.Name = "debugLabel1";
            debugLabel1.Size = new Size(374, 399);
            debugLabel1.TabIndex = 0;
            debugLabel1.Text = "Card 1 of 10";
            debugLabel1.Visible = false;
            // 
            // FormTimer
            // 
            FormTimer.Enabled = true;
            FormTimer.Interval = 20;
            FormTimer.Tick += FormTimerEvent;
            // 
            // panel1
            // 
            panel1.Location = new Point(239, 997);
            panel1.Name = "panel1";
            panel1.Size = new Size(147, 163);
            panel1.TabIndex = 1;
            panel1.Visible = false;
            panel1.Paint += Panel1_Paint;
            // 
            // Board
            // 
            Board.BackColor = Color.Navy;
            Board.Controls.Add(controlsPicture);
            Board.Controls.Add(shortRules);
            Board.Location = new Point(402, 20);
            Board.Name = "Board";
            Board.Size = new Size(1201, 1079);
            Board.TabIndex = 0;
            Board.Paint += Board_Paint;
            Board.MouseDown += BoardMouseDown;
            Board.MouseMove += BoardMouseMove;
            Board.MouseUp += BoardMouseUp;
            // 
            // controlsPicture
            // 
            controlsPicture.BackColor = Color.Gray;
            controlsPicture.Image = Properties.Resources.Controls;
            controlsPicture.Location = new Point(3, 454);
            controlsPicture.Name = "controlsPicture";
            controlsPicture.Size = new Size(500, 435);
            controlsPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            controlsPicture.TabIndex = 1;
            controlsPicture.TabStop = false;
            controlsPicture.Visible = false;
            // 
            // shortRules
            // 
            shortRules.BackColor = Color.Transparent;
            shortRules.Image = Properties.Resources.Screen;
            shortRules.Location = new Point(3, 3);
            shortRules.Name = "shortRules";
            shortRules.Size = new Size(836, 367);
            shortRules.SizeMode = PictureBoxSizeMode.StretchImage;
            shortRules.TabIndex = 0;
            shortRules.TabStop = false;
            shortRules.Visible = false;
            // 
            // debugLabel2
            // 
            debugLabel2.BackColor = SystemColors.ControlDarkDark;
            debugLabel2.Font = new Font("Segoe UI", 14F);
            debugLabel2.ForeColor = SystemColors.ActiveCaptionText;
            debugLabel2.Location = new Point(12, 605);
            debugLabel2.Name = "debugLabel2";
            debugLabel2.Size = new Size(374, 389);
            debugLabel2.TabIndex = 2;
            debugLabel2.Text = "Rotation:";
            debugLabel2.Visible = false;
            // 
            // GemTimer
            // 
            GemTimer.Interval = 16;
            GemTimer.Tick += GemTimerEvent;
            // 
            // debugButton
            // 
            debugButton.Location = new Point(291, 153);
            debugButton.Name = "debugButton";
            debugButton.Size = new Size(95, 30);
            debugButton.TabIndex = 3;
            debugButton.Text = "Debugging";
            debugButton.UseVisualStyleBackColor = true;
            debugButton.Click += DebugButton_Click;
            // 
            // playersButton
            // 
            playersButton.Font = new Font("Segoe UI", 12F);
            playersButton.Location = new Point(12, 81);
            playersButton.Name = "playersButton";
            playersButton.Size = new Size(163, 36);
            playersButton.TabIndex = 5;
            playersButton.Text = "Choose players";
            playersButton.UseVisualStyleBackColor = true;
            playersButton.Click += PlayersButton_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.Gray;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.Location = new Point(92, 20);
            label1.Name = "label1";
            label1.Size = new Size(221, 52);
            label1.TabIndex = 6;
            label1.Text = "Options:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // hideButton
            // 
            hideButton.Location = new Point(165, 153);
            hideButton.Name = "hideButton";
            hideButton.Size = new Size(95, 30);
            hideButton.TabIndex = 7;
            hideButton.Text = "Hide all";
            hideButton.UseVisualStyleBackColor = true;
            hideButton.Click += HideTilesButton_Click;
            // 
            // player0
            // 
            player0.BackColor = Color.Gray;
            player0.Location = new Point(25, 318);
            player0.Name = "player0";
            player0.Size = new Size(50, 50);
            player0.SizeMode = PictureBoxSizeMode.StretchImage;
            player0.TabIndex = 8;
            player0.TabStop = false;
            // 
            // player1
            // 
            player1.BackColor = Color.Gray;
            player1.Location = new Point(25, 388);
            player1.Name = "player1";
            player1.Size = new Size(50, 50);
            player1.SizeMode = PictureBoxSizeMode.StretchImage;
            player1.TabIndex = 9;
            player1.TabStop = false;
            // 
            // playerScore0
            // 
            playerScore0.BackColor = Color.Gray;
            playerScore0.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            playerScore0.Location = new Point(92, 319);
            playerScore0.Name = "playerScore0";
            playerScore0.Size = new Size(67, 49);
            playerScore0.TabIndex = 10;
            playerScore0.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // playerScore1
            // 
            playerScore1.BackColor = Color.Gray;
            playerScore1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            playerScore1.Location = new Point(92, 389);
            playerScore1.Name = "playerScore1";
            playerScore1.Size = new Size(67, 49);
            playerScore1.TabIndex = 11;
            playerScore1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.BackColor = Color.Gray;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label2.Location = new Point(25, 253);
            label2.Name = "label2";
            label2.Size = new Size(235, 52);
            label2.TabIndex = 12;
            label2.Text = "Players' points:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // backButton
            // 
            backButton.Font = new Font("Segoe UI", 12F);
            backButton.Location = new Point(223, 668);
            backButton.Name = "backButton";
            backButton.Size = new Size(134, 67);
            backButton.TabIndex = 13;
            backButton.Text = "Back to title screen";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += BackButton_Click;
            // 
            // rulesButton
            // 
            rulesButton.Font = new Font("Segoe UI", 12F);
            rulesButton.Location = new Point(223, 81);
            rulesButton.Name = "rulesButton";
            rulesButton.Size = new Size(163, 36);
            rulesButton.TabIndex = 14;
            rulesButton.Text = "Short rules";
            rulesButton.UseVisualStyleBackColor = true;
            rulesButton.Click += RulesButton_Click;
            // 
            // playerScore3
            // 
            playerScore3.BackColor = Color.Gray;
            playerScore3.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            playerScore3.Location = new Point(290, 390);
            playerScore3.Name = "playerScore3";
            playerScore3.Size = new Size(67, 49);
            playerScore3.TabIndex = 18;
            playerScore3.TextAlign = ContentAlignment.MiddleLeft;
            playerScore3.Visible = false;
            // 
            // playerScore2
            // 
            playerScore2.BackColor = Color.Gray;
            playerScore2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            playerScore2.Location = new Point(290, 320);
            playerScore2.Name = "playerScore2";
            playerScore2.Size = new Size(67, 49);
            playerScore2.TabIndex = 17;
            playerScore2.TextAlign = ContentAlignment.MiddleLeft;
            playerScore2.Visible = false;
            // 
            // player3
            // 
            player3.BackColor = Color.Gray;
            player3.Location = new Point(223, 389);
            player3.Name = "player3";
            player3.Size = new Size(50, 50);
            player3.SizeMode = PictureBoxSizeMode.StretchImage;
            player3.TabIndex = 16;
            player3.TabStop = false;
            player3.Visible = false;
            // 
            // player2
            // 
            player2.BackColor = Color.Gray;
            player2.Location = new Point(223, 319);
            player2.Name = "player2";
            player2.Size = new Size(50, 50);
            player2.SizeMode = PictureBoxSizeMode.StretchImage;
            player2.TabIndex = 15;
            player2.TabStop = false;
            player2.Visible = false;
            // 
            // controlsLabel
            // 
            controlsLabel.BackColor = Color.Gray;
            controlsLabel.Font = new Font("Segoe UI", 13F);
            controlsLabel.Location = new Point(25, 474);
            controlsLabel.Name = "controlsLabel";
            controlsLabel.Size = new Size(332, 174);
            controlsLabel.TabIndex = 19;
            controlsLabel.Text = "Tiles with short path can not be \r\nplaced facing edge of the board\r\n\r\nPress D to rotate tile clockwise  \r\nor A for counterclockwise ";
            controlsLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1719, 1236);
            Controls.Add(backButton);
            Controls.Add(controlsLabel);
            Controls.Add(playerScore3);
            Controls.Add(playerScore2);
            Controls.Add(player3);
            Controls.Add(player2);
            Controls.Add(rulesButton);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(debugButton);
            Controls.Add(hideButton);
            Controls.Add(playersButton);
            Controls.Add(label2);
            Controls.Add(playerScore1);
            Controls.Add(playerScore0);
            Controls.Add(player1);
            Controls.Add(player0);
            Controls.Add(debugLabel2);
            Controls.Add(Board);
            Controls.Add(debugLabel1);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Indigo";
            KeyDown += Form1_KeyDown;
            Board.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)controlsPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)shortRules).EndInit();
            ((System.ComponentModel.ISupportInitialize)player0).EndInit();
            ((System.ComponentModel.ISupportInitialize)player1).EndInit();
            ((System.ComponentModel.ISupportInitialize)player3).EndInit();
            ((System.ComponentModel.ISupportInitialize)player2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer FormTimer;
        private System.Windows.Forms.Timer GemTimer;
        private DoubleBufferedPanel Board;
        private Label label1;
        private Label label2;
        private Label controlsLabel;
        private Button backButton;
        private Button playersButton;
        private Button rulesButton;
        private Button hideButton;
        private Button debugButton;
        private PictureBox player0;
        private PictureBox player1;
        private PictureBox player2;
        private PictureBox player3;
        private Label playerScore0;
        private Label playerScore1;
        private Label playerScore2;
        private Label playerScore3;
        private Label debugLabel1;
        private Label debugLabel2;
        private Panel panel1;
        private PictureBox shortRules;
        private PictureBox controlsPicture;
    }
}
