namespace Indigo
{
    public partial class PlayerForm : Form
    {
        public List<string> playerColors { get; set; } = new List<string>();

        int numOfPlayers;
        Label[] labels;
        string[] colors;
        public PlayerForm(int numOfPlayers)
        {
            InitializeComponent();

            this.numOfPlayers = numOfPlayers;
            labels = [playerLabel1, playerLabel2, playerLabel3, playerLabel4];
            colors = ["Cyan", "Purple", "Red", "White"];
        }

        private void Cyan_Click(object sender, EventArgs e)
        {
            chooseColor(0);
        }
        private void Purple_Click(object sender, EventArgs e)
        {
            chooseColor(1);
        }
        private void Red_Click(object sender, EventArgs e)
        {
            chooseColor(2);
        }
        private void White_Click(object sender, EventArgs e)
        {
            chooseColor(3);
        }

        public void chooseColor(int num)
        {
            string colorName = colors[num];
            Label label = labels[num];

            if (!label.Visible && playerColors.Count < numOfPlayers)
            {
                playerColors.Add(colorName);

                label.Visible = true;
                label.Text = "Player " + playerColors.Count;
            }
            else
            {
                playerColors.Remove(colorName);
                label.Visible = false;

                if (!playerColors.Any())
                    return;

                for (int i = 0; i < 4; i++)
                    if (labels[i].Visible)
                        labels[i].Text = "Player " + (playerColors.IndexOf(colors[i]) + 1);
            }
        }
        private void doneButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
