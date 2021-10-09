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
            Wall = 5;
        }

        // Input
        public static Player[] SetPlayerNames()
        {
            Console.WriteLine("Enter player name:");
            String playerName1 = Console.ReadLine();

            Player player1 = new Player(0, new Cell(8, 4), 'O');
            player1.Name = playerName1;

            Bot player2 = new Bot(1, new Cell(0, 4), '■');
            player2.Name = "Bot";

            Player[] players = new Player[2];
            players[0] = player1;
            players[1] = player2;

            return players;
        }

        public new void PlayerMakesMove(Board myBoard, Graph graph)
        {
            Random random = new System.Random();

            if (random.Next(2) >= 0.5 && Wall > 0)
                SetNextWall(myBoard, graph);
            else
                SetNextCell(myBoard);
        }

        private bool SetNextCell(Board myBoard)
        {
            List<Cell> legalMovesList = myBoard.MarkLegalMoves(this);

            Random random = new System.Random();

            int randomIndex = random.Next(legalMovesList.Count);

            Cell randomCell = new Cell(legalMovesList[randomIndex].RowNumber, legalMovesList[randomIndex].ColNumber);

            Console.WriteLine(randomCell.ColNumber + " " + randomCell.RowNumber);

            if (CheckCoordinates(randomCell, myBoard))
            {
                SetNextCell(myBoard);
                return true;
            }
            else
                return false;
        }

        private void SetNextWall(Board myBoard, Graph graph)
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
                Wall--;
                myBoard.DisplayWall(wall1, wall2);
            }
            else // Or trying to build the wall again
            {
                SetNextWall(myBoard, graph);
            }
        }
    }
}
