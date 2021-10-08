using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Cell
    {

        public int RowNumber { get; set; }
        public int ColNumber { get; set; }
        public int HorizontalWall { get; set; }
        public int VerticalWall { get; set; }
        public int Number { get; set; }
        public bool CurrentlyOccupied { get; set; }
        public bool LegalNextMove { get; set; }

        public Cell(int x, int y)
        {
            ColNumber = x;
            RowNumber = y;
            Number = x + y * 9;
        }

        public Cell(int n)
        {
            Number = n;
            ColNumber = n / 9;
            RowNumber = n % 9;
        }
    }
}
