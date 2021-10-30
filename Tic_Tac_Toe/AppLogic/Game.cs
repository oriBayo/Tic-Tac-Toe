namespace AppLogic
{
    public class Game
    {
        public struct Point
        {
            public int row;
            public int coulum;
        }

        public enum ePlayerSign
        {
            Player1 = 'X',
            Player2 = 'O' //also PC
        }

        public enum eGameMode
        {
            Computer = 0,
            Player        
        }

        private Board m_Board;
        private eGameMode m_GameMod;
        private char m_Playerturn;
        private string m_Player1Name;
        private string m_Player2Name;
        private int m_BoardSize;
        private int[] m_Score; // m_Score[0] = score player1 , m_score[1] = score player2

        public Game()
        {
            m_Board = null;
            m_Score = new int[2];
            m_Player1Name = "Player1";
        }

        public int[] Score
        {
            get { return m_Score; }
        }

        public Board Board
        {
            set {m_Board = value; }
            get { return m_Board; }
        }

        public char PlayerTurn
        {
            set { m_Playerturn = value; }
            get { return m_Playerturn; }
        }

        public eGameMode GameMod
        {
            get { return m_GameMod; }
            set { m_GameMod = value; }
        }
        
        public int ScorePlayer1 
        {
            get { return m_Score[0]; }
        }

        public int ScorePlayer2
        {
            get { return m_Score[1]; }
        }

        public string Player1Name { get => m_Player1Name; set => m_Player1Name = value; }

        public string Player2Name { get => m_Player2Name; set => m_Player2Name = value; }

        public int BoardSize { get => m_BoardSize; set => m_BoardSize = value; }

        public void SizeBoardFromUser(int i_Size)
        {
            m_BoardSize = i_Size;
            m_Board = new Board(i_Size);        
        }

        public void MakeMove(int i_Row, int i_Col)
        {
            m_Board.Bord[i_Row, i_Col] = m_Playerturn;
        }    

        public char GetPlayerTurn()
        {
            if (PlayerTurn != (char)ePlayerSign.Player1 && PlayerTurn != (char)ePlayerSign.Player2)
            {
                PlayerTurn = (char)ePlayerSign.Player1;
            } 
            else
            {
                PlayerTurn = PlayerTurn == (char)ePlayerSign.Player1 ? (char)ePlayerSign.Player2 : (char)ePlayerSign.Player1; 
            }
            return PlayerTurn;
        }

        public Point BestMove()
        {   
            int numOfNeighbors = int.MaxValue;
            Point returnPoint;
            returnPoint.row = 0;
            returnPoint.coulum = 0;
            int numOfNeighborsTemp = 0;
            int row = 0;
            int col = 0;
            Board tempBoard = new Board(m_Board.SizeBoard + 2); ////create board with "ghost" cells so we can check all the cells the same.

            for (int i = 0; i < m_Board.SizeBoard; i++)
            {
                for (int j = 0; j < m_Board.SizeBoard; j++)
                {
                    if (m_Board.Bord[i, j] ==' ')
                    {
                        numOfNeighborsTemp = checkNeighbors(i, j, (char)ePlayerSign.Player2, tempBoard);
                        if (numOfNeighborsTemp == 0)
                        {
                            numOfNeighbors = numOfNeighborsTemp;
                            row = i;
                            col = j;
                            break;
                        }
                        else if (numOfNeighborsTemp < numOfNeighbors)
                        {
                            numOfNeighbors = numOfNeighborsTemp;
                            row = i;
                            col = j;
                        }
                    }
                }

                if (numOfNeighbors == 0)
                {
                    break;
                }
            }

            m_Board.Bord[row, col] = (char)ePlayerSign.Player2;
            returnPoint.row = row;
            returnPoint.coulum = col;
            return returnPoint;
        }

        private int checkNeighbors(int i_Row, int i_Column, char i_PlayerSign, Board i_TempBoard)
        {
            const int v_TempBoardBuffer = 2; //// the temp board was created with this buffer so that all the cell checks will be the same
            int numOfNeighbors = 0;
            for (int i = 0; i < m_Board.SizeBoard; i++)
            {
                for (int j = 0; j < m_Board.SizeBoard; j++)
                {
                    i_TempBoard.Bord[i + 2, j + 2] = m_Board.Bord[i, j];
                }
            }

            for (int i = -1; i < 1; i++)
            {
                for (int j = -1; j < 1; j++)
                {
                    if (i_TempBoard.Bord[i_Row + v_TempBoardBuffer + i, i_Column + v_TempBoardBuffer + j] == i_PlayerSign)
                    {
                        numOfNeighbors++;
                    }
                }
            }

            return numOfNeighbors;
        }

        public void AddPointToPlayer(Game.ePlayerSign i_player)
        {
            if (i_player == Game.ePlayerSign.Player1)
            {
                m_Score[0]++;
            }
            else
            {
                m_Score[1]++;
            }
        }
    }
}
