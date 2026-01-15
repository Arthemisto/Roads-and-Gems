namespace Indigo
{
    partial class Form1
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
            debugLabel2 = new Label();
            GemTimer = new System.Windows.Forms.Timer(components);
            debugButton = new Button();
            twoPlayersButton = new Button();
            label1 = new Label();
            hideButton = new Button();
            player0 = new PictureBox();
            palyer1 = new PictureBox();
            playerScore0 = new Label();
            playerScore1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)player0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)palyer1).BeginInit();
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
            panel1.Location = new Point(240, 20);
            panel1.Name = "panel1";
            panel1.Size = new Size(147, 163);
            panel1.TabIndex = 1;
            panel1.Paint += Panel1_Paint;
            // 
            // Board
            // 
            Board.BackColor = Color.Navy;
            Board.Location = new Point(402, 20);
            Board.Name = "Board";
            Board.Size = new Size(1416, 1329);
            Board.TabIndex = 0;
            Board.Paint += Board_Paint;
            Board.MouseDown += BoardMouseDown;
            Board.MouseMove += BoardMouseMove;
            Board.MouseUp += BoardMouseUp;
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
            debugButton.Location = new Point(139, 155);
            debugButton.Name = "debugButton";
            debugButton.Size = new Size(95, 30);
            debugButton.TabIndex = 3;
            debugButton.Text = "Debugging";
            debugButton.UseVisualStyleBackColor = true;
            debugButton.Click += DebugButton_Click;
            // 
            // twoPlayersButton
            // 
            twoPlayersButton.Location = new Point(13, 77);
            twoPlayersButton.Name = "twoPlayersButton";
            twoPlayersButton.Size = new Size(95, 30);
            twoPlayersButton.TabIndex = 5;
            twoPlayersButton.Text = "2 Players";
            twoPlayersButton.UseVisualStyleBackColor = true;
            twoPlayersButton.Click += TwoPlayersButton_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.Gray;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(221, 52);
            label1.TabIndex = 6;
            label1.Text = "Options:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // hideButton
            // 
            hideButton.Location = new Point(13, 155);
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
            // palyer1
            // 
            palyer1.BackColor = Color.Gray;
            palyer1.Location = new Point(25, 388);
            palyer1.Name = "palyer1";
            palyer1.Size = new Size(50, 50);
            palyer1.SizeMode = PictureBoxSizeMode.StretchImage;
            palyer1.TabIndex = 9;
            palyer1.TabStop = false;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1719, 1236);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(playerScore1);
            Controls.Add(playerScore0);
            Controls.Add(palyer1);
            Controls.Add(player0);
            Controls.Add(hideButton);
            Controls.Add(label1);
            Controls.Add(twoPlayersButton);
            Controls.Add(debugButton);
            Controls.Add(debugLabel2);
            Controls.Add(debugLabel1);
            Controls.Add(Board);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "Form1";
            Text = "Indigo";
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)player0).EndInit();
            ((System.ComponentModel.ISupportInitialize)palyer1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label debugLabel1;
        private System.Windows.Forms.Timer FormTimer;
        private Panel panel1;
        private DoubleBufferedPanel Board;
        private Label debugLabel2;
        private System.Windows.Forms.Timer GemTimer;
        private Button debugButton;
        private Button twoPlayersButton;
        private Label label1;
        private Button hideButton;
        private PictureBox player0;
        private PictureBox palyer1;
        private Label playerScore0;
        private Label playerScore1;
        private Label label2;
    }
}
