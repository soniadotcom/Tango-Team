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
            Wall = 5;
        }

        public Player()
        {

        }
        /*
        public static Player[] ChoosePlayerNames(String gameMode)
        {
            if (gameMode == "players" || gameMode == "player" || gameMode == "Player" || gameMode == "Players" || gameMode == "PLAYERS")
            {
                gameMode = "Players";
                return Player.SetPlayerNames();
            }
            else
            {
                gameMode = "Bot";
                return Bot.SetPlayerNames();
            }
        }
        */
        // Input
        public static Player[] SetPlayerNames()
        {
            
            Console.WriteLine("\nEnter the first player name:");
            String playerName1 = Console.ReadLine();

            Console.WriteLine("\nEnter the second player name:");
            String playerName2 = Console.ReadLine();

            Player player1 = new Player(0, new Cell(8, 4), 'O');
            Player player2 = new Player(1, new Cell(0, 4), '■');

            player1.Name = playerName1;
            player2.Name = playerName2;

            Player[] players = new Player[2];
            players[0] = player1;
            players[1] = player2;

            return players;
        }


        public void PlayerMakesMove(Board myBoard, Graph graph)
        {
            // If the player didn't move and he still has walls, let him build the wall
            if (!SetNextCell(myBoard) && Wall > 0)
                SetNextWall(myBoard, graph);
        }

        private bool SetNextCell(Board myBoard)
        {
            myBoard.MarkLegalMoves(this);
            Console.WriteLine("Enter next coordinate or press Enter to build the wall");
            try
            {
                int number = int.Parse(Console.ReadLine());

                //Checking move
                if (CheckCoordinates(new Cell(number), myBoard))
                {
                    SetNextCell(myBoard);
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


        private void SetNextWall(Board myBoard, Graph graph)
        {
            try
            {
                Console.WriteLine("Input the first wall coordinate:");
                int wall1 = int.Parse(Console.ReadLine());

                Console.WriteLine("Input the second wall coordinate:");
                int wall2 = int.Parse(Console.ReadLine());

                if (graph.BuildAWall(wall1, wall2)) // If the wall doesn't breaks the rules, we add it to the board
                {
                    Wall--;
                    myBoard.DisplayWall(wall1, wall2);
                }
                else
                {
                    SetNextWall(myBoard, graph);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Your input is incorrect. Try again");
                SetNextWall(myBoard, graph);
            }
        }


        //Checking whether the move is valid
        public bool CheckCoordinates(Cell nextCell,Board myBoard)
        {
            if (myBoard.isSave(nextCell.RowNumber, nextCell.ColNumber) &&
                myBoard.theGrid[nextCell.RowNumber, nextCell.ColNumber].LegalNextMove == true)
            {
                myBoard.theGrid[Cell.RowNumber, Cell.ColNumber].CurrentlyOccupied = false;
                Cell = nextCell;
                myBoard.theGrid[Cell.RowNumber, Cell.ColNumber].CurrentlyOccupied = true;
                return false;
            }
            else
            {
                Console.WriteLine("Your move is not valid. Try another.");
                return true;
            }
        }
    }
}
