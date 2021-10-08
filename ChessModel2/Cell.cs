using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Cell
    {

        public int RowNumber { get; set; }
        public int ColNumber { get; set; }
        public bool CurrentlyOccupied { get; set; }
        public bool LegalNextMove { get; set; }

        public Cell(int x, int y)
        {
            ColNumber = x;
            RowNumber = y;
        }
    }
}
