namespace AppLogic
{
    using System;
    using System.Text;

    public class Board
    {
        readonly char[,] m_Borad;
        private int m_SizeBoard;

        public Board(int i_Size)
        {
            m_SizeBoard = i_Size;
            m_Borad = new char[i_Size, i_Size];
            for (int i = 0; i < m_SizeBoard; i++)
            {
                for (int j = 0; j < m_SizeBoard; j++)
                {
                    m_Borad[i, j] = ' ';
                }
            }
        }

        public char[,] Bord
        {
            get { return m_Borad; }
        }

        public int SizeBoard
        {
            get { return m_SizeBoard; }
        } 

        public override string ToString()
        {
            StringBuilder m_BoardString = new StringBuilder();
            for (int i = 0; i < m_SizeBoard + 1; i++)
            {
                for (int j = 0; j < m_SizeBoard; j++)
                {
                    if (i == 0)
                    {
                        m_BoardString.Append("  ");
                        m_BoardString.AppendFormat("{0} ", j + 1);
                    }
                    else
                    {
                        if (j == 0)
                        {
                            m_BoardString.AppendFormat("{0}| {1} |", i, m_Borad[i - 1, j]);
                        }
                        else
                        {
                            m_BoardString.AppendFormat(" {0} |", m_Borad[i - 1, j]);
                        }
                    }
                }

                if (i == 0)
                {
                    ////for first line no need of = sperator
                    m_BoardString.AppendLine(string.Empty);
                }
                else
                {
                    m_BoardString.AppendLine(string.Empty);
                    m_BoardString.Append(' '); // 4 is the number of chars per cell in the print
                    m_BoardString.Append('=', (m_SizeBoard * 4) + 1); // 4 is the number of chars per cell in the print
                    m_BoardString.AppendLine(string.Empty);
                }
            }

            return m_BoardString.ToString();
        }

        public bool IsBoardFull()
        {
            bool isFull = true;

            for (int i = 0; i < SizeBoard; i++) 
            {
                for (int j = 0; j < SizeBoard; j++) 
                {
                    if (m_Borad[i, j] == ' ') 
                    {
                        isFull = false;
                        break;
                    }
                }
            }

            return isFull;
        }

        public bool IsSpaceFree(int i_Row, int i_Col)
        {
            bool spaceIsFree = true;
            if (m_Borad[i_Row - 1, i_Col - 1] == (char)Game.ePlayerSign.Player1 || m_Borad[i_Row - 1, i_Col - 1] == (char)Game.ePlayerSign.Player2)
            {
                spaceIsFree = false;
            }

            return spaceIsFree;
        }

        public void ClearBoard() 
        {
            for (int i = 0; i < SizeBoard; i++)
            {
                for (int j = 0; j < SizeBoard; j++)
                {
                    m_Borad[i, j] = ' ';
                }
            }
        }

        public bool CheckWinner() 
        {
            const int v_NumOfWinsOption = 8;
            bool hasWon = true;
            int[] checkigWin = new int[v_NumOfWinsOption];

            for (int i = 0; i < SizeBoard; i++)
            {
                Array.Clear(checkigWin, 0, checkigWin.Length);
                for (int j = 0; j < SizeBoard; j++)
                {
                    //// rows - 'X'
                    if (m_Borad[i, j] == (char)Game.ePlayerSign.Player1)
                    {
                        checkigWin[0]++;
                    }
                    //// columns - 'X'
                    if (m_Borad[j, i] == (char)Game.ePlayerSign.Player1)
                    {
                        checkigWin[1]++;
                    }
                    //// Main diagonal - 'X'
                    if (m_Borad[j, j] == (char)Game.ePlayerSign.Player1)
                    {
                        checkigWin[2]++;
                    }
                    //// rows - 'O'
                    if (m_Borad[i, j] == (char)Game.ePlayerSign.Player2)
                    {
                        checkigWin[3]++;
                    }
                    //// columns - 'O'
                    if (m_Borad[j, i] == (char)Game.ePlayerSign.Player2)
                    {
                        checkigWin[4]++;
                    }
                    //// Main diagonal - 'O'
                    if (m_Borad[j, j] == (char)Game.ePlayerSign.Player2)
                    {
                        checkigWin[5]++;
                    }
                    //// Secondary diagonal - 'O'
                    if (m_Borad[j, SizeBoard - 1 - j] == (char)Game.ePlayerSign.Player2)
                    {
                        checkigWin[6]++;
                    }
                    //// Secondary diagonal - 'X'
                    if (m_Borad[j, SizeBoard - 1 - j] == (char)Game.ePlayerSign.Player1)
                    {
                        checkigWin[7]++;
                    }
                }

                for (int k = 0; k < 8; k++) 
                {
                    if (checkigWin[k] == SizeBoard) 
                    {
                        hasWon = false;
                        break;
                    }
                }
            }

            return !hasWon;
        }
    }
}
