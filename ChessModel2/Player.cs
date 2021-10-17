using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Player
    {
        public int Id { get; set; }
        public Cell Cell{ get; set; }
        public String Name { get; set; }
        public int Wall { get; set; }
        public char Symbol { get; set; }

        public Player(int id, Cell cell, char symbol)
        {
            Id = id;
            Cell = cell;
            Symbol = symbol;
            Wall = 10;
        }

        public Player()
        {

        }


        public static void PlayerMakesMove(Player player, Board myBoard, Graph graph)
        {
            // If the player didn't move and he still has walls, let him build the wall
            if (!SetNextCell(player, myBoard) && player.Wall > 0)
                SetNextWall(player, myBoard, graph);
        }

        private static bool SetNextCell(Player player, Board myBoard)
        {
            myBoard.MarkLegalMoves(player);


            Console.WriteLine("Enter next coordinate or press Enter to build the wall");
            try
            {
                int number = int.Parse(Console.ReadLine());

                //Checking move
                if (CheckCoordinates(player, new Cell(number), myBoard))
                {
                    SetNextCell(player, myBoard);
                    return true;
                }
                else
                    return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("You decided not to move");
                return false;
            }
        }


        private static void SetNextWall(Player player, Board myBoard, Graph graph)
        {
            try
            {
                Console.WriteLine("Input the first wall coordinate:");
                int wall1 = int.Parse(Console.ReadLine());

                Console.WriteLine("Input the second wall coordinate:");
                int wall2 = int.Parse(Console.ReadLine());

                if (graph.BuildAWall(wall1, wall2)) // If the wall doesn't breaks the rules, we add it to the board
                {
                    player.Wall--;
                    myBoard.DisplayWall(wall1, wall2);
                }
                else
                {
                    SetNextWall(player, myBoard, graph);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Your input is incorrect. Try again");
                SetNextWall(player, myBoard, graph);
            }
        }


        //Checking whether the move is valid
        public static bool CheckCoordinates(Player player, Cell nextCell,Board myBoard)
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
                Console.WriteLine("Your move is not valid. Try another.");
                return true;
            }
        }

        public static Player[] SetPlayerNames(String[] playerNames)
        {

            Player player1 = new Player(0, new Cell(8, 4), 'O');
            Player player2 = new Player(1, new Cell(0, 4), '■');

            player1.Name = playerNames[0];
            player2.Name = playerNames[1];

            Player[] players = new Player[2];
            players[0] = player1;
            players[1] = player2;

            return players;
        }


        public static bool IsAWinner(Player player, Board myBoard)
        {
            Console.WriteLine(player.Name + " " + player.Cell.RowNumber);
            if ((player.Id == 0 && player.Cell.ColNumber == 0) ||
                (player.Id == 1 && player.Cell.ColNumber == 8))
            {
                myBoard.ClearLegalMoves();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
