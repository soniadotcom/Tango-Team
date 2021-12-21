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
        public String Symbol1 { get; set; }
        public int Symbol2 { get; set; }

        String[] letters = {"A", "B", "C", "D", "E", "F", "G", "H","I"};

        public Cell(int x, int y)
        {
            ColNumber = x;
            RowNumber = y;
            Number = x + y * 9;

            Symbol1 = letters.GetValue(x).ToString();
            Symbol2 = y + 1;
        }

        public Cell(int n)
        {
            Number = n;
            ColNumber = n / 9;
            RowNumber = n % 9;
        }
        public Cell(String coordinate)
        {
            Symbol1 = coordinate.Substring(0,1).ToUpper();
            Symbol2 = int.Parse(coordinate.Substring(1));

            RowNumber = Array.IndexOf(letters, Symbol1);
            ColNumber = Symbol2 - 1;
            Number = RowNumber + ColNumber * 9;

        }
    }
}
