using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Board
    {

        public int Size { get; set; }

        public Cell[,] theGrid { get; set; }
        public Board(int s)
        {
            Size = s;

            theGrid = new Cell[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j] = new Cell(i, j);
                }
            }
        }


        public void MarkLegalMoves(Player player)
        {
            ClearLegalMoves();

            if (isSave(player.Cell.RowNumber + 1, player.Cell.ColNumber))
                if (theGrid[player.Cell.RowNumber + 1, player.Cell.ColNumber].CurrentlyOccupied)
                {
                    if (isSave(player.Cell.RowNumber + 2, player.Cell.ColNumber))
                        theGrid[player.Cell.RowNumber + 2, player.Cell.ColNumber].LegalNextMove = true;
                }
                else
                    theGrid[player.Cell.RowNumber + 1, player.Cell.ColNumber].LegalNextMove = true;

            if (isSave(player.Cell.RowNumber - 1, player.Cell.ColNumber))
                if (theGrid[player.Cell.RowNumber - 1, player.Cell.ColNumber].CurrentlyOccupied)
                {
                    if (isSave(player.Cell.RowNumber - 2, player.Cell.ColNumber))
                        theGrid[player.Cell.RowNumber - 2, player.Cell.ColNumber].LegalNextMove = true;
                }
                else
                    theGrid[player.Cell.RowNumber - 1, player.Cell.ColNumber].LegalNextMove = true;

            if (isSave(player.Cell.RowNumber, player.Cell.ColNumber + 1))
                if (theGrid[player.Cell.RowNumber, player.Cell.ColNumber + 1].CurrentlyOccupied)
                {
                    if (isSave(player.Cell.RowNumber, player.Cell.ColNumber + 2))
                        theGrid[player.Cell.RowNumber, player.Cell.ColNumber + 2].LegalNextMove = true;
                }
                else
                    theGrid[player.Cell.RowNumber, player.Cell.ColNumber + 1].LegalNextMove = true;

            if (isSave(player.Cell.RowNumber, player.Cell.ColNumber - 1))
                if (theGrid[player.Cell.RowNumber, player.Cell.ColNumber - 1].CurrentlyOccupied)
                {
                    if (isSave(player.Cell.RowNumber, player.Cell.ColNumber - 2))
                        theGrid[player.Cell.RowNumber, player.Cell.ColNumber - 2].LegalNextMove = true;
                }
                else
                    theGrid[player.Cell.RowNumber, player.Cell.ColNumber - 1].LegalNextMove = true;


            theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = true;
        }

        public void ClearLegalMoves()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j].LegalNextMove = false;
                }
            }
        }

        public bool isSave(int rowNumber, int colNumber)
        {
            return rowNumber >= 0 && rowNumber < 9 && colNumber >= 0 && colNumber < 9;
        }
    }
}
