namespace Indigo
{
    partial class GameEndForm
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
            finishButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(187, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(420, 420);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // finishButton
            // 
            finishButton.Location = new Point(666, 350);
            finishButton.Name = "finishButton";
            finishButton.Size = new Size(122, 33);
            finishButton.TabIndex = 19;
            finishButton.Text = "Finish Game";
            finishButton.UseVisualStyleBackColor = true;
            finishButton.Click += finishButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(666, 399);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(122, 33);
            cancelButton.TabIndex = 20;
            cancelButton.Text = "Go Back";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // GameEndForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(800, 450);
            Controls.Add(cancelButton);
            Controls.Add(finishButton);
            Controls.Add(pictureBox1);
            Name = "GameEndForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GameEndForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button finishButton;
        private Button cancelButton;
    }
}