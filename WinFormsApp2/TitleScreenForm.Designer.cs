namespace Indigo
{
    partial class TitleScreenForm
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
            pictureBox1 = new PictureBox();
            startButton = new Button();
            label2 = new Label();
            st2Players = new Button();
            st3Players = new Button();
            st4Players = new Button();
            scaleLabel = new Label();
            stScale = new NumericUpDown();
            label1 = new Label();
            video1Button = new Button();
            video2Button = new Button();
            rules1Button = new Button();
            rules2Button = new Button();
            onlineButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stScale).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Title;
            pictureBox1.Location = new Point(214, 15);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(690, 587);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // startButton
            // 
            startButton.AutoSize = true;
            startButton.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            startButton.Location = new Point(927, 747);
            startButton.Name = "startButton";
            startButton.Size = new Size(175, 53);
            startButton.TabIndex = 1;
            startButton.Text = "Start Game";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // label2
            // 
            label2.BackColor = Color.Gray;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label2.Location = new Point(583, 625);
            label2.Name = "label2";
            label2.Size = new Size(321, 52);
            label2.TabIndex = 7;
            label2.Text = "Settings:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // st2Players
            // 
            st2Players.BackColor = Color.FromArgb(192, 255, 192);
            st2Players.Font = new Font("Segoe UI", 12F);
            st2Players.Location = new Point(583, 700);
            st2Players.Name = "st2Players";
            st2Players.Size = new Size(101, 40);
            st2Players.TabIndex = 8;
            st2Players.Text = "2 Players";
            st2Players.UseVisualStyleBackColor = false;
            st2Players.Click += st2Players_Click;
            // 
            // st3Players
            // 
            st3Players.BackColor = Color.FromArgb(255, 192, 192);
            st3Players.Font = new Font("Segoe UI", 12F);
            st3Players.Location = new Point(694, 700);
            st3Players.Name = "st3Players";
            st3Players.Size = new Size(101, 40);
            st3Players.TabIndex = 9;
            st3Players.Text = "3 Players";
            st3Players.UseVisualStyleBackColor = false;
            st3Players.Click += st3Players_Click;
            // 
            // st4Players
            // 
            st4Players.BackColor = Color.FromArgb(255, 192, 192);
            st4Players.Font = new Font("Segoe UI", 12F);
            st4Players.Location = new Point(803, 700);
            st4Players.Name = "st4Players";
            st4Players.Size = new Size(101, 40);
            st4Players.TabIndex = 10;
            st4Players.Text = "4 Players";
            st4Players.UseVisualStyleBackColor = false;
            st4Players.Click += st4Players_Click;
            // 
            // scaleLabel
            // 
            scaleLabel.BackColor = Color.Gray;
            scaleLabel.Font = new Font("Segoe UI", 12F);
            scaleLabel.Location = new Point(583, 758);
            scaleLabel.Name = "scaleLabel";
            scaleLabel.Size = new Size(211, 38);
            scaleLabel.TabIndex = 13;
            scaleLabel.Text = "Game scale: (%)";
            scaleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // stScale
            // 
            stScale.Font = new Font("Segoe UI", 14F);
            stScale.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            stScale.Location = new Point(803, 758);
            stScale.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            stScale.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            stScale.Name = "stScale";
            stScale.Size = new Size(101, 39);
            stScale.TabIndex = 14;
            stScale.Value = new decimal(new int[] { 100, 0, 0, 0 });
            stScale.ValueChanged += stScale_ValueChanged;
            // 
            // label1
            // 
            label1.BackColor = Color.Gray;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.Location = new Point(214, 625);
            label1.Name = "label1";
            label1.Size = new Size(321, 52);
            label1.TabIndex = 15;
            label1.Text = "Game Rules:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // video1Button
            // 
            video1Button.Font = new Font("Segoe UI", 12F);
            video1Button.Location = new Point(390, 700);
            video1Button.Name = "video1Button";
            video1Button.Size = new Size(145, 40);
            video1Button.TabIndex = 16;
            video1Button.Text = "Video (Rus)";
            video1Button.UseVisualStyleBackColor = true;
            video1Button.Click += video1Button_Click;
            // 
            // video2Button
            // 
            video2Button.Font = new Font("Segoe UI", 12F);
            video2Button.Location = new Point(214, 700);
            video2Button.Name = "video2Button";
            video2Button.Size = new Size(145, 40);
            video2Button.TabIndex = 17;
            video2Button.Text = "Video (Eng)";
            video2Button.UseVisualStyleBackColor = true;
            video2Button.Click += video2Button_Click;
            // 
            // rules1Button
            // 
            rules1Button.Font = new Font("Segoe UI", 12F);
            rules1Button.Location = new Point(390, 756);
            rules1Button.Name = "rules1Button";
            rules1Button.Size = new Size(145, 40);
            rules1Button.TabIndex = 18;
            rules1Button.Text = "Rules (Rus)";
            rules1Button.UseVisualStyleBackColor = true;
            rules1Button.Click += rules1Button_Click;
            // 
            // rules2Button
            // 
            rules2Button.Font = new Font("Segoe UI", 12F);
            rules2Button.Location = new Point(214, 756);
            rules2Button.Name = "rules2Button";
            rules2Button.Size = new Size(145, 40);
            rules2Button.TabIndex = 19;
            rules2Button.Text = "Rules (Eng)";
            rules2Button.UseVisualStyleBackColor = true;
            rules2Button.Click += rules2Button_Click;
            // 
            // onlineButton
            // 
            onlineButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            onlineButton.Location = new Point(927, 686);
            onlineButton.Name = "onlineButton";
            onlineButton.Size = new Size(175, 49);
            onlineButton.TabIndex = 20;
            onlineButton.Text = "Online Multiplayer";
            onlineButton.UseVisualStyleBackColor = true;
            onlineButton.Click += onlineButton_Click;
            // 
            // TitleScreenForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1125, 815);
            Controls.Add(rules2Button);
            Controls.Add(rules1Button);
            Controls.Add(video2Button);
            Controls.Add(video1Button);
            Controls.Add(onlineButton);
            Controls.Add(label1);
            Controls.Add(stScale);
            Controls.Add(scaleLabel);
            Controls.Add(st4Players);
            Controls.Add(st3Players);
            Controls.Add(st2Players);
            Controls.Add(label2);
            Controls.Add(startButton);
            Controls.Add(pictureBox1);
            Name = "TitleScreenForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TitleScreenForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)stScale).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button startButton;
        private Label label2;
        private Button st2Players;
        private Button st3Players;
        private Button st4Players;
        private Label scaleLabel;
        private NumericUpDown stScale;
        private Label label1;
        private Button video1Button;
        private Button video2Button;
        private Button rules1Button;
        private Button rules2Button;
        private Button onlineButton;
    }
}
