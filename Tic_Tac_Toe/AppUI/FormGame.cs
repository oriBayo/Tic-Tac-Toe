namespace AppUI
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AppLogic;

    public partial class FormGame : Form
    {
        private readonly Game r_Game;
        private readonly ButtonBoard[,] r_ListOfButton;
        private const int v_ButtonSize = 65;
        private const int v_ButtonMargin = 75;

        public FormGame(Game i_Game)
        {
            r_Game = i_Game;
            InitializeComponent();
            int size = v_ButtonMargin * i_Game.BoardSize + v_ButtonMargin - 40;
            ClientSize = new Size(size, size);
            r_ListOfButton = new ButtonBoard[i_Game.BoardSize, i_Game.BoardSize];
            MakeBoard();
            InitializeButton();
            labelPlayer1.Text = GetPlayer1Name() + ":";
            labelPlayer2.Text = GetPlayer2Name() + ":";
            labelScorePlayer1.Location = new Point(labelPlayer1.Right, labelPlayer1.Top);
            labelScorePlayer2.Location = new Point(labelPlayer2.Right, labelPlayer2.Top);
        }

        private string GetPlayer1Name()
        {
            return r_Game.Player1Name;
        }

        private string GetPlayer2Name()
        {
            return r_Game.Player2Name;
        }

        private void MakeBoard()
        {
            int size = r_Game.BoardSize;
            int top;
            int right = 20;
            for (int i = 0; i < size; i++)
            {
                top = 10;
                for (int j = 0; j < size; j++)
                {
                    ButtonBoard button = new ButtonBoard(i, j);
                    Controls.Add(button);
                    button.Location = new Point(right, top);
                    button.Size = new Size(v_ButtonSize, v_ButtonSize);
                    button.TabStop = false;
                    r_ListOfButton[i, j] = button;
                    top += v_ButtonMargin;
                }
                right += v_ButtonMargin;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ButtonBoard button = sender as ButtonBoard;
            button.Text = (r_Game.GetPlayerTurn()).ToString();
            r_Game.MakeMove(button.Row, button.Col);
            button.Enabled = false;

            if (r_Game.GameMod == Game.eGameMode.Player) 
            {
                checkBoardStatus();
            }

            else if (r_Game.GameMod == Game.eGameMode.Computer && !checkBoardStatus())
            {
                ComputerPlay();
            }    
        }

        private bool checkBoardStatus()
        {
            string name = GetPlayer1Name();
            bool flag = false;
            if (r_Game.Board.CheckWinner())
            {
                flag = true;
                if (r_Game.PlayerTurn == (char)Game.ePlayerSign.Player1)
                {
                    //player 2 wins
                    name = GetPlayer2Name();
                    r_Game.AddPointToPlayer(Game.ePlayerSign.Player2);
                }
                else
                {
                    //Player1 1 win
                    r_Game.AddPointToPlayer(Game.ePlayerSign.Player1);
                }
                UpdateScore();
                EndGameMsg(String.Format(@"The winner is {0}!
Would you like to play another round?",
                                    name));
            }
            else if (r_Game.Board.IsBoardFull())
            {
                flag = true;
                EndGameMsg(
                    String.Format(@"Tie!
Would you like to play another round?"));
            }

            return flag;
        }

        private void InitializeButton()
        {
            foreach (ButtonBoard button in r_ListOfButton)
            {
                button.Click += new EventHandler(Button_Click);
            }
        }

        private void EndGameMsg(string i_Message)
        {
            DialogResult result = MessageBox.Show(i_Message, "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //update result;
                r_Game.Board.ClearBoard();
                foreach (ButtonBoard button in r_ListOfButton)
                {
                    button.Text = " ";
                    button.Enabled = true;
                }
                r_Game.PlayerTurn = ' ';
            }
            else
            {
                this.Close();
            }
        }

        private void UpdateScore()
        {
            labelScorePlayer1.Text = r_Game.ScorePlayer1.ToString();
            labelScorePlayer2.Text = r_Game.ScorePlayer2.ToString();
        }

        private void ComputerPlay()
        {
            //ComputerPlay();
            Game.Point bestCell = r_Game.BestMove();
            foreach (ButtonBoard buttonToUpdate in r_ListOfButton)
            {
                if (buttonToUpdate.Row == bestCell.row && buttonToUpdate.Col == bestCell.coulum)
                {
                    buttonToUpdate.Text = (r_Game.GetPlayerTurn()).ToString();
                    r_Game.MakeMove(buttonToUpdate.Row, buttonToUpdate.Col);
                    buttonToUpdate.Enabled = false;
                    break;
                }
            }
        }
    }
}
