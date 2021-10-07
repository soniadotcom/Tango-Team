using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Board
    {

        public int Size { get; set; }

        public Cell[,] theGrid { get; set; }
        public Board (int s)
        {
            Size = s;

            theGrid = new Cell[Size, Size];
            for (int i=0; i< Size; i++)
            {
                theGrid[i, j] = new Cell(i, j);
            }
        }


        public void MarkLegalMoves(Cell currentCell, string chessPiece)
        {
            for (int i=0; i < Size; i++)
            {
                for (int j=0; j < Size; j++)
                {
                    theGrid[i, j].LegalNextMove = false;
                    theGrid[i, j].CurrentlyOccupied = false;
                }
            }

            switch (chessPiece)
            {
                case "Knight":
                    theGrid[currentCell.RowNumber + 2, currentCell.ColNumber + 1].LegalNextMove = true;
                    theGrid[currentCell.RowNumber + 2, currentCell.ColNumber - 1].LegalNextMove = true;
                    theGrid[currentCell.RowNumber - 2, currentCell.ColNumber + 1].LegalNextMove = true;
                    theGrid[currentCell.RowNumber - 2, currentCell.ColNumber - 1].LegalNextMove = true;
                    theGrid[currentCell.RowNumber + 1, currentCell.ColNumber + 2].LegalNextMove = true;
                    theGrid[currentCell.RowNumber + 1, currentCell.ColNumber - 2].LegalNextMove = true;
                    theGrid[currentCell.RowNumber - 1, currentCell.ColNumber + 2].LegalNextMove = true;
                    theGrid[currentCell.RowNumber - 1, currentCell.ColNumber - 2].LegalNextMove = true;
                    break;
                case "King":

                    break;
                case "Rook":

                    break;
                case "Bishop":

                    break;
                case "Queen":

                    break;
                case "Quoridor":
                    theGrid[currentCell.RowNumber + 1, currentCell.ColNumber].LegalNextMove = true;
                    theGrid[currentCell.RowNumber - 1, currentCell.ColNumber].LegalNextMove = true;
                    theGrid[currentCell.RowNumber, currentCell.ColNumber + 1].LegalNextMove = true;
                    theGrid[currentCell.RowNumber, currentCell.ColNumber - 1].LegalNextMove = true;
                    break;
                default:

                    break;
            }
        }
    }
}
