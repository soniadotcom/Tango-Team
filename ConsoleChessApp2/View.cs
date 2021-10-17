using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    class View
    {
        public static void ChooseGameMode()
        {
            Console.WriteLine("Choose GameMode: \n");
            Console.WriteLine("   ____  U  ___ u _____          ____      _         _      __   __U _____ u   ____     ____     ");
            Console.WriteLine("U | __\")u \\/\"_ \\/|_ \" _|       U|  _\"\\ u  |\"|    U  /\"\\  u  \\ \\ / /\\| ___\"|/U |  _\"\\ u / __\"| u  ");
            Console.WriteLine(" \\|  _ \\/ | | | |  | |         \\| |_) |/U | | u   \\/ _ \\/    \\ V /  |  _|\"   \\| |_) |/<\\___ \\/   ");
            Console.WriteLine("  | |_) | | |_| | /| |\\         |  __/   \\| |/__  / ___ \\   U_|\"|_u | |___    |  _ <   u___) |   ");
            Console.WriteLine("  |____/ )-\\___/ u |_|U         |_|       |_____|/_/   \\_\\    |_|   |_____|   |_| \\_\\  |____/>>  ");
            Console.WriteLine(" _|| \\\\_    \\\\   _// \\\\_        ||>>_     //  \\\\  \\\\    >>.-,//|(_  <<   >>   //   \\\\_  )(  (__) ");
            Console.WriteLine("(__) (__)  (__) (__) (__)      (__)__)   (_\")(\"_)(__)  (__)\\_) (__)(__) (__) (__)  (__)(__)");
            Console.WriteLine("\n");

            Console.WriteLine("GameMode:");

        }

        public static void PrintMoveNumber(int moveNumber, Player player)
        {
            Console.WriteLine("Move #" + moveNumber + "\nPlayer: " + player.Name + "\nWalls: " + player.Wall);
        }

        public static void PrintBoard(Board myBoard, Player player1, Player player2)
        {
            int i = 0;
            Console.Write("╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");
            for (int y = 0; y < myBoard.Size; y++)
            {
                Console.Write("\n║");
                for (int x = 0; x < myBoard.Size; x++)
                {
                    Cell c = myBoard.theGrid[x, y];

                    if (c.CurrentlyOccupied == true)
                    {
                        if (c.ColNumber == player1.Cell.RowNumber &&
                           c.RowNumber == player1.Cell.ColNumber)
                            Console.Write(" " + player1.Symbol + " ");
                        else
                            Console.Write(" " + player2.Symbol + " ");
                    }
                    else if (c.LegalNextMove == true)
                    {
                        Console.Write(" + ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    if (myBoard.theGrid[x, y].VerticalWall == 1)
                    {
                        Console.Write("█");
                    }
                    else if (x < 8 && myBoard.theGrid[x, y].VerticalWall == 2)
                    {
                        Console.Write("█");
                    }
                    else
                    {
                        Console.Write("║");
                    }

                    i++;
                }
                if (y < myBoard.Size - 1)
                {
                    Console.Write("\n╠");
                    for (int x = 0; x < myBoard.Size; x++)
                    {
                        if (myBoard.theGrid[x, y].HorizontalWall == 1)
                            Console.Write("■■■");
                        else if (myBoard.theGrid[x, y].HorizontalWall == 2)
                            Console.Write("■■■");
                        else
                            Console.Write("═══");


                        if (myBoard.theGrid[x, y].VerticalWall == 1 &&
                            myBoard.theGrid[x, y + 1].VerticalWall == 2)
                            Console.Write("█");
                        else if (myBoard.theGrid[x, y].HorizontalWall == 1 &&
                            myBoard.theGrid[x + 1, y].HorizontalWall == 2)
                            Console.Write("■");
                        else if (x < 8)
                            Console.Write("╬");

                    }
                    Console.Write("╣");
                }
            }
            Console.WriteLine("\n╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");
        }


        public static void GameOver(int moveNumber, Player winner, Board myBoard, Player player1, Player player2)
        {
            Console.Clear();
            PrintMoveNumber(moveNumber, winner);
            PrintBoard(myBoard, player1, player2);
            Console.WriteLine("\nGame Over");
            Console.WriteLine("The Winner is: " + winner.Name);
        }

        public static void ExplainRules()
        {
            int i = 0;
            Console.Write("╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");
            for (int y = 0; y < 9; y++)
            {
                Console.Write("\n║");
                for (int x = 0; x < 9; x++)
                {

                    Console.Write(" " + (i / 10).ToString() + (i % 10).ToString());

                    Console.Write("║");

                    i++;
                }
                if (y < 8)
                {
                    Console.Write("\n╠");
                    for (int x = 0; x < 9; x++)
                    {
                        if(i != 15)
                            Console.Write("═══");
                        else
                            Console.Write("■■■");
                        if (x < 8)
                            Console.Write("╬");

                    }
                    Console.Write("╣");
                }
            }
            Console.WriteLine("\n╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");

            Console.WriteLine("\nTo move you have to write cell number.");
            
            Input.PressAnyKey();

            Console.WriteLine("╔═══╦═══╗");
            Console.WriteLine("║ a █   ║");
            Console.WriteLine("╠═══█═══╣");
            Console.WriteLine("║ b █   ║");
            Console.WriteLine("╚═══╩═══╝\n");
            Console.WriteLine("To build the vertical wall first write a-number then b-number.");

            Input.PressAnyKey();

            Console.WriteLine("╔═══╦═══╗");
            Console.WriteLine("║ a ║ b ║");
            Console.WriteLine("╠■■■■■■■╣");
            Console.WriteLine("║   ║   ║");
            Console.WriteLine("╚═══╩═══╝");
            Console.WriteLine("To build the gorizontal wall first write a-number then b-number.");

            Input.PressAnyKey();
        }
    }
}
