namespace AppUI
{
    using System;
    using System.Windows.Forms;
    using AppLogic;

    public partial class FormSettings : Form
    {
        private readonly Game r_Game = new Game();

        public FormSettings()
        {
            InitializeComponent();
            r_Game.Player2Name = Player2Name;
        }

        public string Player1Name
        {
            get { return textBoxPlayer1Name.Text; }
        }

        public string Player2Name
        {
            get { return textBoxPlayer2Name.Text; }
        }

        public int BoardSize 
        {
            get { return (int)numericUpDownCols.Value; }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Dispose();
            r_Game.SizeBoardFromUser(BoardSize);
            FormGame form = new FormGame(r_Game);           
            form.ShowDialog();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            r_Game.GameMod = checkBoxPlayer2.Checked ?
            Game.eGameMode.Player : Game.eGameMode.Computer;
            textBoxPlayer2Name.Enabled = !textBoxPlayer2Name.Enabled;       
        }

        private void textBoxPlayer1Name_TextChanged(object sender, EventArgs e)
        {
            r_Game.Player1Name = Player1Name;
        }

        private void textBoxPlayer2Name_TextChanged(object sender, EventArgs e)
        {
            r_Game.Player2Name = Player2Name;
        }

        private void numericUpDownrRows_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCols.Value = numericUpDownrRows.Value;
        }

        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownrRows.Value = numericUpDownCols.Value;
        }
    }
}