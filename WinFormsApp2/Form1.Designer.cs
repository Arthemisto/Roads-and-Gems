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
            label1 = new Label();
            FormTimer = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            Board = new DoubleBufferedPanel();
            label2 = new Label();
            GemTimer = new System.Windows.Forms.Timer(components);
            debugButton = new Button();
            twoPlayersButton = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = SystemColors.ControlDarkDark;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(1132, 197);
            label1.Name = "label1";
            label1.Size = new Size(374, 560);
            label1.TabIndex = 0;
            label1.Text = "Card 1 of 10";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // FormTimer
            // 
            FormTimer.Enabled = true;
            FormTimer.Interval = 20;
            FormTimer.Tick += FormTimerEvent;
            // 
            // panel1
            // 
            panel1.Location = new Point(1132, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(147, 163);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // Board
            // 
            Board.BackColor = Color.Navy;
            Board.Location = new Point(37, 22);
            Board.Name = "Board";
            Board.Size = new Size(1090, 1182);
            Board.TabIndex = 0;
            Board.Paint += Board_Paint;
            Board.MouseDown += BoardMouseDown;
            Board.MouseMove += BoardMouseMove;
            Board.MouseUp += BoardMouseUp;
            // 
            // label2
            // 
            label2.BackColor = SystemColors.ControlDarkDark;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(1132, 784);
            label2.Name = "label2";
            label2.Size = new Size(374, 420);
            label2.TabIndex = 2;
            label2.Text = "Rotation:";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // GemTimer
            // 
            GemTimer.Interval = 16;
            GemTimer.Tick += GemTimerEvent;
            // 
            // debugButton
            // 
            debugButton.Location = new Point(1412, 156);
            debugButton.Name = "debugButton";
            debugButton.Size = new Size(94, 29);
            debugButton.TabIndex = 3;
            debugButton.Text = "Debugging";
            debugButton.UseVisualStyleBackColor = true;
            debugButton.Click += debugButton_Click;
            // 
            // twoPlayersButton
            // 
            twoPlayersButton.Location = new Point(1285, 89);
            twoPlayersButton.Name = "twoPlayersButton";
            twoPlayersButton.Size = new Size(94, 29);
            twoPlayersButton.TabIndex = 5;
            twoPlayersButton.Text = "2 Players";
            twoPlayersButton.UseVisualStyleBackColor = true;
            twoPlayersButton.Click += TwoPlayersButton_Click;
            // 
            // label3
            // 
            label3.BackColor = Color.Gray;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label3.Location = new Point(1285, 22);
            label3.Name = "label3";
            label3.Size = new Size(221, 52);
            label3.TabIndex = 6;
            label3.Text = "Options:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1532, 1253);
            Controls.Add(label3);
            Controls.Add(twoPlayersButton);
            Controls.Add(debugButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(Board);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "Form1";
            Text = "Indigo";
            KeyDown += Form1_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private System.Windows.Forms.Timer FormTimer;
        private Panel panel1;
        private DoubleBufferedPanel Board;
        private Label label2;
        private System.Windows.Forms.Timer GemTimer;
        private Button debugButton;
        private Button twoPlayersButton;
        private Label label3;
    }
}
