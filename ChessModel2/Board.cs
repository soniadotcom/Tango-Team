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
        public static void PutPlayerOnBoard(Player player, Board board)
        {
            board.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = true;
        }

        // Визначає можливі ходи для гравця (+)
        public List<Cell> MarkLegalMoves(Player player)
        {
            ClearLegalMoves();

            List<Cell> legalMovesList = new List<Cell>();

            if (isSave(player.Cell.RowNumber + 1, player.Cell.ColNumber) && // Перевірка на те, чи координата не виходить за межі ігрового поля
                theGrid[player.Cell.RowNumber, player.Cell.ColNumber].VerticalWall == 0) // Перевірка на те, чи не заважає стінка
                if (theGrid[player.Cell.RowNumber + 1, player.Cell.ColNumber].CurrentlyOccupied) // Перевірка на те, чи клітинка є зайнятою суперником
                {   // Якщо сусідня клітинка зайнята суперником...
                    if (isSave(player.Cell.RowNumber + 2, player.Cell.ColNumber) && // Перевірити чи клітинка за суперником в межах ігрового поля
                        theGrid[player.Cell.RowNumber + 1, player.Cell.ColNumber].VerticalWall == 0) // Перевірка на те, чи за гравцем є стінка
                    {
                        theGrid[player.Cell.RowNumber + 2, player.Cell.ColNumber].LegalNextMove = true; // Визначає клітинку за суперником легальною для ходу
                        legalMovesList.Add(new Cell (player.Cell.RowNumber + 2, player.Cell.ColNumber));
                    }
                }
                else // Якщо сусідня клітинка не зайнята суперником...
                {
                    theGrid[player.Cell.RowNumber + 1, player.Cell.ColNumber].LegalNextMove = true; // Визначає сусідню клітинку легальною для ходу
                    legalMovesList.Add(new Cell(player.Cell.RowNumber + 1, player.Cell.ColNumber));
                }
            
            if (isSave(player.Cell.RowNumber - 1, player.Cell.ColNumber) &&
                theGrid[player.Cell.RowNumber - 1, player.Cell.ColNumber].VerticalWall == 0)
                if (theGrid[player.Cell.RowNumber - 1, player.Cell.ColNumber].CurrentlyOccupied)
                {
                    if (isSave(player.Cell.RowNumber - 2, player.Cell.ColNumber) &&
                        theGrid[player.Cell.RowNumber - 2, player.Cell.ColNumber].VerticalWall == 0)
                    {
                        theGrid[player.Cell.RowNumber - 2, player.Cell.ColNumber].LegalNextMove = true;
                        legalMovesList.Add(new Cell(player.Cell.RowNumber - 2, player.Cell.ColNumber));
                    }
                }
                else
                {
                    theGrid[player.Cell.RowNumber - 1, player.Cell.ColNumber].LegalNextMove = true;
                    legalMovesList.Add(new Cell(player.Cell.RowNumber - 1, player.Cell.ColNumber));
                }

            if (isSave(player.Cell.RowNumber, player.Cell.ColNumber + 1) &&
                theGrid[player.Cell.RowNumber, player.Cell.ColNumber].HorizontalWall == 0)
                if (theGrid[player.Cell.RowNumber, player.Cell.ColNumber + 1].CurrentlyOccupied)
                {
                    if (isSave(player.Cell.RowNumber, player.Cell.ColNumber + 2) &&
                        theGrid[player.Cell.RowNumber, player.Cell.ColNumber + 1].HorizontalWall == 0)
                    {
                        theGrid[player.Cell.RowNumber, player.Cell.ColNumber + 2].LegalNextMove = true;
                        legalMovesList.Add(new Cell(player.Cell.RowNumber, player.Cell.ColNumber + 2));
                    }
                }
                else
                {
                    theGrid[player.Cell.RowNumber, player.Cell.ColNumber + 1].LegalNextMove = true;
                    legalMovesList.Add(new Cell(player.Cell.RowNumber, player.Cell.ColNumber + 1));
                }

            if (isSave(player.Cell.RowNumber, player.Cell.ColNumber - 1) &&
                theGrid[player.Cell.RowNumber, player.Cell.ColNumber - 1].HorizontalWall == 0)
                if (theGrid[player.Cell.RowNumber, player.Cell.ColNumber - 1].CurrentlyOccupied)
                {
                    if (isSave(player.Cell.RowNumber, player.Cell.ColNumber - 2) &&
                        theGrid[player.Cell.RowNumber, player.Cell.ColNumber - 2].HorizontalWall == 0)
                    {
                        theGrid[player.Cell.RowNumber, player.Cell.ColNumber - 2].LegalNextMove = true;
                        legalMovesList.Add(new Cell(player.Cell.RowNumber, player.Cell.ColNumber - 2));
                    }

                }
                else
                {
                    theGrid[player.Cell.RowNumber, player.Cell.ColNumber - 1].LegalNextMove = true;
                    legalMovesList.Add(new Cell(player.Cell.RowNumber, player.Cell.ColNumber - 1));
                }

            theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = true;

            return legalMovesList;
        }

        // Додає стінку на ігрове поле
        public void DisplayWall(int a, int b)
        {
            int x = a % 9;
            int y = a / 9;

            if (a >= 0 && b>= 0)
                if (b - a == 1)
                {
                    theGrid[x, y].HorizontalWall = 1;
                    theGrid[x + 1, y].HorizontalWall = 2;
                }
                if (b - a == 9)
                {
                    theGrid[x, y].VerticalWall = 1;
                    theGrid[x, y + 1].VerticalWall = 2;
                }
        }

        // Очистка + з ігрового поля (можливих ходів)
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

        // Перевірка на те, чи координата не виходить за межі ігрового поля
        public bool isSave(int rowNumber, int colNumber)
        {
            return rowNumber >= 0 && rowNumber < 9 && colNumber >= 0 && colNumber < 9;
        }
    }
}
