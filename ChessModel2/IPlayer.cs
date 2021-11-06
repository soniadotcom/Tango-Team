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

        public bool MakeNewMove(IPlayer player, Board myBoard, Graph graph, String input);



        //Checking whether the move is valid
        public static bool CheckCoordinates(IPlayer player, Cell nextCell, Board myBoard)
        {
            if (myBoard.isSave(nextCell.RowNumber, nextCell.ColNumber) &&
                myBoard.theGrid[nextCell.RowNumber, nextCell.ColNumber].LegalNextMove == true)
            {
                myBoard.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = false;
                player.Cell = nextCell;
                myBoard.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = true;
                return false;
            }
            else
            {
                Console.WriteLine("Your move is not valid. Skipped.");
                return true;
            }
        }
    }
}
