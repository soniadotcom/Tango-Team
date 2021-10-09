using System;

namespace ConsoleChessApp
{
    public class Program
    {
        private static Player player0;

        static void Main(string[] args)
        {
            StartMenu();
        }

        private static void StartMenu()
        {
            String gameMode = ChooseGameMode();
            if (gameMode == "Players")
            {
                StartTheGame();
            }
            else
            {
                StartTheGameWithBot();
            }
        }

        private static void StartTheGame()
        {
            Board myBoard = new Board(9);

            Player winner = new Player();

            Graph graph = new Graph(81);

            bool gameInProgress = true;

            int moveNumber = 1;



            Player[] players = Player.SetPlayerNames();

            Player player1 = players[0];
            Player player2 = players[1];

            Board.PutPlayerOnBoard(player1, myBoard);
            Board.PutPlayerOnBoard(player2, myBoard);

            while (gameInProgress)
            {
                Console.Clear();

                if (moveNumber % 2 == 1)
                {
                    PrintMoveNumber(moveNumber, player1);
                    myBoard.MarkLegalMoves(player1);
                    PrintBoard(myBoard, player1, player2);

                    player1.PlayerMakesMove(myBoard, graph);

                    if (IsAWinner(player1, myBoard))
                    {
                        winner = player1;
                        break;
                    }
                }
                else
                {
                    PrintMoveNumber(moveNumber, player2);
                    myBoard.MarkLegalMoves(player2);
                    PrintBoard(myBoard, player1, player2);

                    player2.PlayerMakesMove(myBoard, graph);

                    if (IsAWinner(player2, myBoard))
                    {
                        winner = player2;
                        break;
                    }
                }

                moveNumber++;
            }

            GameOver(moveNumber, winner, myBoard, player1, player2);
        }

        private static void StartTheGameWithBot()
        {
            Board myBoard = new Board(9);

            Player winner = new Player();

            Graph graph = new Graph(81);

            bool gameInProgress = true;

            int moveNumber = 1;

            Player[] players = Bot.SetPlayerNames();

            Player player1 = players[0];
            Bot player2 = (Bot)players[1];

            Board.PutPlayerOnBoard(player1, myBoard);
            Board.PutPlayerOnBoard(player2, myBoard);

            while (gameInProgress)
            {
                Console.Clear();

                if (moveNumber % 2 == 1)
                {
                    PrintMoveNumber(moveNumber, player1);
                    myBoard.MarkLegalMoves(player1);
                    PrintBoard(myBoard, player1, player2);

                    player1.PlayerMakesMove(myBoard, graph);

                    if (IsAWinner(player1, myBoard))
                    {
                        winner = player1;
                        break;
                    }
                }
                else
                {
                    PrintMoveNumber(moveNumber, player2);
                    myBoard.MarkLegalMoves(player2);
                    PrintBoard(myBoard, player1, player2);

                    player2.PlayerMakesMove(myBoard, graph);

                    if (IsAWinner(player2, myBoard))
                    {
                        winner = player2;
                        break;
                    }
                }

                moveNumber++;
            }

            GameOver(moveNumber, winner, myBoard, player1, player2);
        }

        private static bool IsAWinner(Player player, Board myBoard)
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

        private static void GameOver(int moveNumber, Player winner, Board myBoard, Player player1, Player player2)
        {
            Console.Clear();
            PrintMoveNumber(moveNumber, winner);
            PrintBoard(myBoard, player1, player2);
            Console.WriteLine("\nGame Over");
            Console.WriteLine("The Winner is: " + winner.Name);

            Console.WriteLine("\nTo play again write \"again\":");
            try
            {
                String again = Console.ReadLine();
                if (again == "again" || again == "Again" || again == "AGAIN")
                {
                    Console.Clear();
                    StartMenu();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("End.");
            }
        }

        // View
        private static String ChooseGameMode()
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
            try
            {
                String gameMode = Console.ReadLine();
                if (gameMode == "players" || gameMode == "player" || gameMode == "Player" || gameMode == "Players" || gameMode == "PLAYERS" || gameMode == "PLAYER")
                {
                    gameMode = "Players";
                }
                else
                {
                    gameMode = "Bot";
                }
                return gameMode;
            }
            catch (Exception e)
            {
                return "Players";
            }
        }

        // View
        private static void PrintMoveNumber(int moveNumber, Player player)
        {
            Console.WriteLine("Move #" + moveNumber + "\nPlayer: " + player.Name + "\nWalls: " + player.Wall);
        }

        // View
        private static void PrintBoard(Board myBoard, Player player1, Player player2)
        {

            Console.Write("╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");
            for (int y = 0; y < myBoard.Size; y++)
            {
                Console.Write("\n║");
                for (int x = 0; x < myBoard.Size; x++)
                {
                    Cell c = myBoard.theGrid[x, y];

                    if (c.CurrentlyOccupied == true)
                    {
                        if(c.ColNumber == player1.Cell.RowNumber &&
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

                }
                if (y< myBoard.Size - 1)
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
    }
}
