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
            link1Button = new Button();
            link2Button = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stScale).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Title;
            pictureBox1.Location = new Point(201, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 510);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 16F);
            startButton.Location = new Point(730, 764);
            startButton.Name = "startButton";
            startButton.Size = new Size(171, 60);
            startButton.TabIndex = 1;
            startButton.Text = "Start Game";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // label2
            // 
            label2.BackColor = Color.Gray;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label2.Location = new Point(580, 545);
            label2.Name = "label2";
            label2.Size = new Size(321, 52);
            label2.TabIndex = 7;
            label2.Text = "Settings:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // st2Players
            // 
            st2Players.Font = new Font("Segoe UI", 12F);
            st2Players.Location = new Point(580, 620);
            st2Players.Name = "st2Players";
            st2Players.Size = new Size(100, 40);
            st2Players.TabIndex = 8;
            st2Players.Text = "2 Players";
            st2Players.UseVisualStyleBackColor = true;
            st2Players.Click += st2Players_Click;
            // 
            // st3Players
            // 
            st3Players.Font = new Font("Segoe UI", 12F);
            st3Players.Location = new Point(691, 620);
            st3Players.Name = "st3Players";
            st3Players.Size = new Size(100, 40);
            st3Players.TabIndex = 9;
            st3Players.Text = "3 Players";
            st3Players.UseVisualStyleBackColor = true;
            st3Players.Click += st3Players_Click;
            // 
            // st4Players
            // 
            st4Players.Font = new Font("Segoe UI", 12F);
            st4Players.Location = new Point(801, 620);
            st4Players.Name = "st4Players";
            st4Players.Size = new Size(100, 40);
            st4Players.TabIndex = 10;
            st4Players.Text = "4 Players";
            st4Players.UseVisualStyleBackColor = true;
            st4Players.Click += st4Players_Click;
            // 
            // scaleLabel
            // 
            scaleLabel.BackColor = Color.Gray;
            scaleLabel.Font = new Font("Segoe UI", 12F);
            scaleLabel.Location = new Point(580, 700);
            scaleLabel.Name = "scaleLabel";
            scaleLabel.Size = new Size(211, 34);
            scaleLabel.TabIndex = 13;
            scaleLabel.Text = "Game scale: (%)";
            scaleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // stScale
            // 
            stScale.Font = new Font("Segoe UI", 12F);
            stScale.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            stScale.Location = new Point(801, 700);
            stScale.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            stScale.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            stScale.Name = "stScale";
            stScale.Size = new Size(100, 34);
            stScale.TabIndex = 14;
            stScale.Value = new decimal(new int[] { 100, 0, 0, 0 });
            stScale.ValueChanged += stScale_ValueChanged;
            // 
            // label1
            // 
            label1.BackColor = Color.Gray;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.Location = new Point(201, 545);
            label1.Name = "label1";
            label1.Size = new Size(321, 52);
            label1.TabIndex = 15;
            label1.Text = "Game Rules:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // link1Button
            // 
            link1Button.Font = new Font("Segoe UI", 12F);
            link1Button.Location = new Point(365, 620);
            link1Button.Name = "link1Button";
            link1Button.Size = new Size(157, 40);
            link1Button.TabIndex = 16;
            link1Button.Text = "Video (Rus)";
            link1Button.UseVisualStyleBackColor = true;
            link1Button.Click += link1Button_Click;
            // 
            // link2Button
            // 
            link2Button.Font = new Font("Segoe UI", 12F);
            link2Button.Location = new Point(201, 620);
            link2Button.Name = "link2Button";
            link2Button.Size = new Size(158, 40);
            link2Button.TabIndex = 17;
            link2Button.Text = "Video (Eng)";
            link2Button.UseVisualStyleBackColor = true;
            link2Button.Click += link2Button_Click;
            // 
            // TitleScreenForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1132, 853);
            Controls.Add(link2Button);
            Controls.Add(link1Button);
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
            Text = "TitleScreenForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)stScale).EndInit();
            ResumeLayout(false);
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
        private Button link1Button;
        private Button link2Button;
    }
}