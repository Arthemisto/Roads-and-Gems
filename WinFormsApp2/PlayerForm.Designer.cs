namespace Indigo
{
    partial class PlayerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            doneButton = new Button();
            Cyan = new PictureBox();
            Purple = new PictureBox();
            Red = new PictureBox();
            White = new PictureBox();
            label1 = new Label();
            playerLabel1 = new Label();
            playerLabel2 = new Label();
            playerLabel3 = new Label();
            playerLabel4 = new Label();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)Cyan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Purple).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Red).BeginInit();
            ((System.ComponentModel.ISupportInitialize)White).BeginInit();
            SuspendLayout();
            // 
            // doneButton
            // 
            doneButton.BackColor = Color.DarkGray;
            doneButton.Location = new Point(520, 404);
            doneButton.Name = "doneButton";
            doneButton.Size = new Size(122, 34);
            doneButton.TabIndex = 0;
            doneButton.Text = "Done";
            doneButton.UseVisualStyleBackColor = false;
            doneButton.Click += doneButton_Click;
            // 
            // Cyan
            // 
            Cyan.Image = Properties.Resources.Cyan_point;
            Cyan.Location = new Point(130, 160);
            Cyan.Name = "Cyan";
            Cyan.Size = new Size(100, 100);
            Cyan.SizeMode = PictureBoxSizeMode.StretchImage;
            Cyan.TabIndex = 9;
            Cyan.TabStop = false;
            Cyan.Click += Cyan_Click;
            // 
            // Purple
            // 
            Purple.Image = Properties.Resources.Purple_point;
            Purple.Location = new Point(280, 160);
            Purple.Name = "Purple";
            Purple.Size = new Size(100, 100);
            Purple.SizeMode = PictureBoxSizeMode.StretchImage;
            Purple.TabIndex = 10;
            Purple.TabStop = false;
            Purple.Click += Purple_Click;
            // 
            // Red
            // 
            Red.Image = Properties.Resources.Red_point;
            Red.Location = new Point(430, 160);
            Red.Name = "Red";
            Red.Size = new Size(100, 100);
            Red.SizeMode = PictureBoxSizeMode.StretchImage;
            Red.TabIndex = 11;
            Red.TabStop = false;
            Red.Click += Red_Click;
            // 
            // White
            // 
            White.Image = Properties.Resources.White_point;
            White.Location = new Point(580, 160);
            White.Name = "White";
            White.Size = new Size(100, 100);
            White.SizeMode = PictureBoxSizeMode.StretchImage;
            White.TabIndex = 12;
            White.TabStop = false;
            White.Click += White_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.Gray;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.Location = new Point(215, 30);
            label1.Name = "label1";
            label1.Size = new Size(384, 52);
            label1.TabIndex = 13;
            label1.Text = "Choose colors for players";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // playerLabel1
            // 
            playerLabel1.BackColor = Color.Gray;
            playerLabel1.Font = new Font("Segoe UI", 10F);
            playerLabel1.Location = new Point(130, 274);
            playerLabel1.Name = "playerLabel1";
            playerLabel1.Size = new Size(100, 28);
            playerLabel1.TabIndex = 14;
            playerLabel1.TextAlign = ContentAlignment.MiddleCenter;
            playerLabel1.Visible = false;
            // 
            // playerLabel2
            // 
            playerLabel2.BackColor = Color.Gray;
            playerLabel2.Font = new Font("Segoe UI", 10F);
            playerLabel2.Location = new Point(280, 274);
            playerLabel2.Name = "playerLabel2";
            playerLabel2.Size = new Size(100, 28);
            playerLabel2.TabIndex = 15;
            playerLabel2.TextAlign = ContentAlignment.MiddleCenter;
            playerLabel2.Visible = false;
            // 
            // playerLabel3
            // 
            playerLabel3.BackColor = Color.Gray;
            playerLabel3.Font = new Font("Segoe UI", 10F);
            playerLabel3.Location = new Point(430, 274);
            playerLabel3.Name = "playerLabel3";
            playerLabel3.Size = new Size(100, 28);
            playerLabel3.TabIndex = 16;
            playerLabel3.TextAlign = ContentAlignment.MiddleCenter;
            playerLabel3.Visible = false;
            // 
            // playerLabel4
            // 
            playerLabel4.BackColor = Color.Gray;
            playerLabel4.Font = new Font("Segoe UI", 10F);
            playerLabel4.Location = new Point(580, 274);
            playerLabel4.Name = "playerLabel4";
            playerLabel4.Size = new Size(100, 28);
            playerLabel4.TabIndex = 17;
            playerLabel4.TextAlign = ContentAlignment.MiddleCenter;
            playerLabel4.Visible = false;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(666, 404);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(122, 34);
            cancelButton.TabIndex = 18;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // PlayerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(800, 450);
            Controls.Add(cancelButton);
            Controls.Add(playerLabel4);
            Controls.Add(playerLabel3);
            Controls.Add(playerLabel2);
            Controls.Add(playerLabel1);
            Controls.Add(label1);
            Controls.Add(White);
            Controls.Add(Red);
            Controls.Add(Purple);
            Controls.Add(Cyan);
            Controls.Add(doneButton);
            Name = "PlayerForm";
            Text = "PlayerForm";
            ((System.ComponentModel.ISupportInitialize)Cyan).EndInit();
            ((System.ComponentModel.ISupportInitialize)Purple).EndInit();
            ((System.ComponentModel.ISupportInitialize)Red).EndInit();
            ((System.ComponentModel.ISupportInitialize)White).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button doneButton;
        private PictureBox Cyan;
        private PictureBox Purple;
        private PictureBox Red;
        private PictureBox White;
        private Label label1;
        private Label playerLabel1;
        private Label playerLabel2;
        private Label playerLabel3;
        private Label playerLabel4;
        private Button cancelButton;
    }
}