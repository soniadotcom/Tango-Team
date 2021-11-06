using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public Cell Cell { get; set; }
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


        public static void PlayerMakesMove(IPlayer player, Board myBoard, Graph graph)
        {
            // If the player didn't move and he still has walls, let him build the wall
            if (!SetNextCell(player, myBoard) && player.Wall > 0)
                SetNextWall(player, myBoard, graph);
        }

        private static bool SetNextCell(IPlayer player, Board myBoard)
        {
            myBoard.MarkLegalMoves(player);


            //Console.WriteLine("Enter next coordinate or press Enter to build the wall");
            try
            {
                int number = int.Parse(Console.ReadLine());

                //Checking move
                if (IPlayer.CheckCoordinates(player, new Cell(number), myBoard))
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


        private static void SetNextWall(IPlayer player, Board myBoard, Graph graph)
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



        public static IPlayer[] SetPlayerNames(String[] playerNames)
        {

            Player player1 = new Player(0, new Cell(8, 4), 'O');
            Player player2 = new Player(1, new Cell(0, 4), '■');

            player1.Name = playerNames[0];
            player2.Name = playerNames[1];

            IPlayer[] players = new IPlayer[2];
            players[0] = player1;
            players[1] = player2;

            return players;
        }


        public bool MakeNewMove(IPlayer player, Board myBoard, Graph graph, String input)
        {
            myBoard.MarkLegalMoves(player);

            try
            {

                String command = input.Split(' ')[0];
                String coordinate = input.Split(' ')[1];

                Cell newCell;

                switch (command.ToUpper())
                {
                    case "MOVE":
                        //Console.WriteLine(newCell.Symbol1 + " " + newCell.Symbol2 + " " + newCell.Number + " " + newCell.ColNumber + " " + newCell.RowNumber);
                        //Console.ReadLine();
                        //Checking move
                        newCell = new Cell(coordinate);
                        IPlayer.CheckCoordinates(player, newCell, myBoard);
                        return true;
                    case "JUMP":

                        newCell = new Cell(coordinate);
                        IPlayer.CheckCoordinates(player, newCell, myBoard);
                        return true;
                    case "WALL":

                        String Symbol1 = coordinate[0].ToString().ToUpper();
                        String Symbol2 = coordinate[1].ToString();
                        String wallPosition = coordinate[2].ToString();

                        String[] letters = { "S", "T", "U", "V", "W", "X", "Y", "Z" };

                        int RowNumber = Array.IndexOf(letters, Symbol1);
                        int ColNumber = int.Parse(Symbol2) - 1;
                        int Number = RowNumber + ColNumber * 9;

                        int a, b;

                        a = Number;

                        if (wallPosition.ToUpper() == "H")
                        {
                            b = Number + 1;
                        }
                        else
                        {
                            b = Number + 9;
                        }

                        if (graph.BuildAWall(a, b)) // If the wall doesn't breaks the rules, we add it to the board
                        {
                            player.Wall--;
                            myBoard.DisplayWall(a, b);
                        }
                        else
                        {
                            MakeNewMove(player, myBoard, graph, input);
                        }
                        return true;
                    default:
                        return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Your input is not valid.");
                return false;
            }
        }

    }
}
