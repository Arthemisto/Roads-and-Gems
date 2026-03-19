namespace Indigo
{
    public partial class GameEndForm : Form
    {
        public GameEndForm(bool youWon)
        {
            InitializeComponent();

            if (youWon)
                pictureBox1.Image = Properties.Resources.Win;
            else
                pictureBox1.Image = Properties.Resources.Lose;
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
