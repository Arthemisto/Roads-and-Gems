using System.Diagnostics;

namespace Indigo
{
    public partial class TitleScreenForm : Form
    {
        int playerCount = 0;
        float percent = 1;

        int[] sizesOfObjects = [
            BoardImage.width,
            BoardImage.height,
            Tile.width,
            Tile.height,
            Gem.width,
            Gem.height,
            PlayerToken.width,
            PlayerToken.height
        ];

        public TitleScreenForm()
        {
            InitializeComponent();

            st2Players.BackColor = Color.LightGreen;
            st3Players.BackColor = Color.LightPink;
            st4Players.BackColor = Color.LightPink;

            startButton.BackColor = Color.Gray;
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            if (playerCount != 2)       //ToDo
            {
                return;
            }

            var gameForm = new GameForm(sizesOfObjects, percent, playerCount);

            gameForm.FormClosed += (s, args) => this.Show();

            gameForm.Show();
            this.Hide();

            //          Another variant but does not free up memory :(
            //
            //using (var gameForm = new GameForm(sizesOfObjects, percent, playerCount))
            //{
            //    this.Hide();
            //    gameForm.ShowDialog(); // blocks here
            //    this.Show();           // executes after GameForm closes
            //}
        }
        private void link1Button_Click(object sender, EventArgs e)
        {
            var url = "https://www.youtube.com/watch?v=I7kXYYuxgro&t=47s";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        private void link2Button_Click(object sender, EventArgs e)
        {
            var url = "https://www.youtube.com/watch?v=LJwy7qXWuNI&t=38s";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void stScale_ValueChanged(object sender, EventArgs e)
        {
            percent = (float)stScale.Value / 100;
        }
        private void st2Players_Click(object sender, EventArgs e)
        {
            playerCount = 2;

            startButton.BackColor = Color.White;
        }
        private void st3Players_Click(object sender, EventArgs e)
        {
            playerCount = 3;

            startButton.BackColor = Color.Gray;
        }
        private void st4Players_Click(object sender, EventArgs e)
        {
            playerCount = 4;

            startButton.BackColor = Color.Gray;
        }
    }
}
