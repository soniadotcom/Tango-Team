using System;

namespace ConsoleChessApp
{
    public class Program
    {
        static Board myBoard = new Board(9);

        static Player player1 = new Player(0, new Cell(8, 4));

        static Player player2 = new Player(1, new Cell(0, 4));

        static Player winner;

        static Graph graph = new Graph(81);

        static void Main(string[] args)
        {

            for (int i = 0; i < 81; i++)
            {
                if (i - 9 >= 0)
                {
                    graph.addEdge(i, i - 9);
                }
                if (i + 1 < 81 && i % 9 != 8)
                {
                    graph.addEdge(i, i + 1);
                }
                if (i + 9 < 81)
                {
                    graph.addEdge(i, i + 9);
                }
                if (i - 1 >= 0 && i % 9 != 0)
                {
                    graph.addEdge(i, i - 1);
                }
            }
            /*
            graph.printGraph();
            
            graph.adjLists[7].Find(8).Value = 7; // стінка)
            graph.adjLists[8].Find(7).Value = 8;
            graph.adjLists[16].Find(17).Value = 16;
            graph.adjLists[17].Find(16).Value = 17;
            */


            /*
            graph.adjLists[53].Find(62).Value = 53;
            graph.adjLists[62].Find(53).Value = 62;
            graph.adjLists[61].Find(62).Value = 61;
            graph.adjLists[62].Find(61).Value = 62;
            graph.adjLists[71].Find(70).Value = 71;
            graph.adjLists[70].Find(71).Value = 70;
            
            graph.adjLists[71].Find(80).Value = 71;
            graph.adjLists[80].Find(71).Value = 80;
            graph.printGraph();
            */
            //graph.CheckMoveCorrectness();


            //graph.PrintMoveCorrectness();

            graph.BuildAWall(0, 1, 9, 10);
            //graph.printGraph();
            //Console.WriteLine(graph.CheckMoveCorrectness());

            graph.BuildAWall(18, 9, 19, 10);
            //graph.printGraph();
            //Console.WriteLine(graph.CheckMoveCorrectness());




            //Playing solo or against a Bot?

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
            String gameMode = Console.ReadLine();

            if (gameMode == "players" || gameMode == "player" || gameMode == "Player" || gameMode == "Players" || gameMode == "PLAYERS")
                setPlayerNames();
            else
            {
                setPlayerName();
            }
            PutPlayerOnBoard(player1);
            PutPlayerOnBoard(player2);

            bool gameInProgress = true;
            int moveNumber = 1;

            while (gameInProgress)
            {
                Console.Clear();


                if (moveNumber % 2 == 1)
                {
                    PrintMoveNumber(moveNumber, player1);
                    myBoard.MarkLegalMoves(player1);
                    PrintBoard(myBoard);
                    SetNextCell(player1);
                    if (isAWinner(player1))
                        break;
                }
                else
                {
                    PrintMoveNumber(moveNumber, player2);
                    myBoard.MarkLegalMoves(player2);
                    PrintBoard(myBoard);
                    SetNextCell(player2);
                    if (isAWinner(player2))
                        break;
                }

                moveNumber++;
            }

            Console.Clear();
            PrintMoveNumber(moveNumber, winner);

            Console.WriteLine("The Winner is: " + winner.Name);

            PrintBoard(myBoard);

            Console.ReadLine();

        }

        private static void setPlayerName()
        {
            Console.WriteLine("Enter player name:");
            String playerName1 = Console.ReadLine();

            player1.Name = playerName1;
            player2.Name = "Bot";
        }

        private static void setPlayerNames()
        {
            Console.WriteLine("\nEnter the first player name:");
            String playerName1 = Console.ReadLine();

            Console.WriteLine("\nEnter the second player name:");
            String playerName2 = Console.ReadLine();

            player1.Name = playerName1;
            player2.Name = playerName2;
        }

        private static bool isAWinner(Player player)
        {
            if ((player.Id == 0 && player.Cell.RowNumber == 0) ||
                (player.Id == 1 && player.Cell.RowNumber == 8))
            {
                winner = player;
                myBoard.ClearLegalMoves();
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void PrintMoveNumber(int i, Player player)
        {
            Console.WriteLine("Move #" + i + "\nPlayer: " + player.Name);
        }

        private static void PutPlayerOnBoard(Player player)
        {
            myBoard.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = true;
        }

        //Makes move
        private static void SetNextCell(Player player)
        {
            myBoard.MarkLegalMoves(player);
            Console.WriteLine("Enter the next row number");
            int nextRow = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the next column number");
            int nextCol = int.Parse(Console.ReadLine());

            //Checking move
            if (CheckCoordinates(new Cell(nextRow, nextCol), player))
            {
                SetNextCell(player);
            }
        }

        //Checking whether the move is valid
        private static bool CheckCoordinates(Cell nextCell, Player player)
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

        private static void PrintBoard(Board myBoard)
        {

            Console.Write("╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");
            for (int i = 0; i < myBoard.Size; i++)
            {
                Console.Write("\n║");
                for (int j = 0; j < myBoard.Size; j++)
                {
                    Cell c = myBoard.theGrid[i, j];

                    if (c.CurrentlyOccupied == true)
                    {
                        Console.Write(" O ║");
                    }
                    else if (c.LegalNextMove == true)
                    {
                        Console.Write(" + ║");
                    }
                    else
                    {
                        Console.Write("   ║");
                    }
                }
                if (i < myBoard.Size - 1)
                {
                    Console.Write("\n╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣");
                }
            }
            Console.WriteLine("\n╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");
            /*
            Console.WriteLine("========================================");
            Console.WriteLine("╔═══╦═══╦═══╗");
            Console.WriteLine("║   ║ @ █   ║");
            Console.WriteLine("╠═══╬═══█═══╣");
            Console.WriteLine("║   ║ O █   ║");
            Console.WriteLine("╚═══╩═══╩═══╝");
            */
        }
    }
}