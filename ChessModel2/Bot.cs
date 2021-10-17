using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Bot : Player
    {
        public Bot()
        {
        }

        public Bot(int id, Cell cell, char symbol)
        {
            Id = id;
            Cell = cell;
            Symbol = symbol;
            Wall = 10;
        }

        public static new void PlayerMakesMove(Player player, Board myBoard, Graph graph)
        {
            Random random = new System.Random();

            if (random.Next(2) >= 0.5 && player.Wall > 0)
                SetNextWall(player, myBoard, graph);
            else
                SetNextCell(player, myBoard);
        }

        private static bool SetNextCell(Player player, Board myBoard)
        {
            List<Cell> legalMovesList = myBoard.MarkLegalMoves(player);

            Random random = new System.Random();

            int randomIndex = random.Next(legalMovesList.Count);

            Cell randomCell = new Cell(legalMovesList[randomIndex].RowNumber, legalMovesList[randomIndex].ColNumber);

            if (CheckCoordinates(player, randomCell, myBoard))
            {
                SetNextCell(player, myBoard);
                return true;
            }
            else
                return false;
        }

        private static void SetNextWall(Player player, Board myBoard, Graph graph)
        {
            Random random = new System.Random();

            int wall1 = random.Next(72);
            int wall2 = 0;

            if (random.Next(2) > 0.5)
            {
                wall2 = wall1 + 9;
            }
            else if (wall1 % 8 != 0)
            {
                wall2 = wall1 + 1;
            } 
            else
            {
                wall1--;
                wall2 = wall1 + 1;
            }

            if (graph.BuildAWall(wall1, wall2)) // If the wall doesn't breaks the rules, we add it to the board
            {
                player.Wall--;
                myBoard.DisplayWall(wall1, wall2);
            }
            else // Or trying to build the wall again
            {
                SetNextWall(player, myBoard, graph);
            }
        }

        public static void BotMakesMove(Player player, Board myBoard, Graph graph)
        {
            Random random = new System.Random();

            if (random.Next(2) >= 0.5 && player.Wall > 0)
                SetNextWall(player, myBoard, graph);
            else
                SetNextCell(player, myBoard);
        }
    }
}
