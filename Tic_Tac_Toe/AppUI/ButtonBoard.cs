namespace AppUI
{
    using System.Windows.Forms;

    public class ButtonBoard : Button
    {
        private readonly int r_Row;
        private readonly int r_Col; 

        public int Row
        {
            get { return r_Row; }           
        }

        public int Col
        {
            get { return r_Col; }           
        }

        public ButtonBoard(int i_Row, int i_Col) 
        {
            r_Col = i_Col;
            r_Row = i_Row;
        }
    }
}
