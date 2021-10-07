using ConsoleChessApp;
using System;

namespace ConsoleChessApp
{
    public class Program
    {
        static Board myBoard = new Board(8);
        static void Main(string[] args)
        {
            printBoard(myBoard);


            Console.ReadLine();
        }

        private static void printBoard(Board myBoard)
        {
            

            for (int i = 0; i < myBoard.Size; i++)
            {
                for (int j = 0; j < myBoard.Size; j++)
                {
                    Cell c = myBoard.theGrid[i, j];

                    if (c.CurrentlyOccupied == true)
                    {
                        Console.Write("X");
                    }
                    else if (c.LegalNextMove == true)
                    {
                        Console.Write("+");

                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
            }
            Console.WriteLine("========================================");
        }
    }
}
