using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public interface IPlayer
    {
        int Id { get; set; }
        Cell Cell { get; set; }
        String Name { get; set; }
        int Wall { get; set; }
        char Symbol { get; set; }

        public bool MakeNewMove(IPlayer player, IPlayer opponent, Board myBoard, Graph graph);



        //Checking whether the move is valid
        public static bool CheckCoordinates(IPlayer player, Cell nextCell, Board myBoard)
        {
            myBoard.MarkLegalMoves(player);
            if (myBoard.isSave(nextCell.RowNumber, nextCell.ColNumber) &&
                myBoard.theGrid[nextCell.RowNumber, nextCell.ColNumber].LegalNextMove == true)
            {
                myBoard.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = false;
                player.Cell.RowNumber = nextCell.RowNumber;
                player.Cell.ColNumber = nextCell.ColNumber;
                player.Cell.Number = nextCell.Number;
                myBoard.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = true;
                return false;
            }
            else
            {
                //Console.WriteLine("Your move is not valid. Skipped.");
                return true;
            }
        }



        public static Board CheckNewCoordinates(IPlayer player, Cell nextCell, Board myBoard)
        {
            List<Cell> LegalMoves = myBoard.MarkLegalMoves(player);
            myBoard.MarkLegalMoves(player);
            if (myBoard.isSave(nextCell.RowNumber, nextCell.ColNumber))
            {
                myBoard.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = false;
                player.Cell.RowNumber = nextCell.RowNumber;
                player.Cell.ColNumber = nextCell.ColNumber;
                player.Cell.Number = nextCell.Number;
                myBoard.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = true;
            }
            else
            {
                Console.WriteLine("Your move is not valid. Skipped.");
            }
            return myBoard;
        }
    }
}
