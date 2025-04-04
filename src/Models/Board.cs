namespace GameOfLife.Models
{
    public class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int[][] Cells { get; set; }
    }
}
