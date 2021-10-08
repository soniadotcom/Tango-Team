using System;

namespace ConsoleChessApp
{
    public class Program
    {
        static Board myBoard = new Board(9);

        static Player player1 = new Player(0, new Cell(8, 4), 'O');

        static Player player2 = new Player(1, new Cell(0, 4), '■');
        
        static Player winner;

        static Graph graph = new Graph(81);

        static String gameMode;

        static bool gameInProgress = true;

        static int moveNumber = 1;

        static void Main(string[] args)
        {

            graph.addAllEdges();

            ChooseGameMode();

            ChoosePlayerNames();

            Board.PutPlayerOnBoard(player1, myBoard);
            Board.PutPlayerOnBoard(player2, myBoard);

            while (gameInProgress)
            {
                Console.Clear();
                
                if (moveNumber % 2 == 1)
                {
                    PrintMoveNumber(moveNumber, player1);
                    myBoard.MarkLegalMoves(player1);
                    PrintBoard(myBoard);

                    PlayerMakesMove(player1);

                    if (IsAWinner(player1))
                        break;
                }
                else
                {
                    PrintMoveNumber(moveNumber, player2);
                    myBoard.MarkLegalMoves(player2);
                    PrintBoard(myBoard);

                    PlayerMakesMove(player2);

                    if (IsAWinner(player2))
                        break;
                }

                moveNumber++;
            }

            GameOver();
        }

        private static void GameOver()
        {
            Console.Clear();
            PrintMoveNumber(moveNumber, winner);
            PrintBoard(myBoard);
            Console.WriteLine("\nGame Over");
            Console.WriteLine("The Winner is: " + winner.Name);

            Console.ReadLine();
        }

        private static void ChoosePlayerNames()
        {
            if (gameMode == "players" || gameMode == "player" || gameMode == "Player" || gameMode == "Players" || gameMode == "PLAYERS")
                SetPlayerNames();
            else
            {
                SetPlayerName();
            }
        }

        private static void ChooseGameMode()
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
            gameMode = Console.ReadLine();
        }

        private static void PlayerMakesMove(Player player)
        {
            // If the player didn't move and he still has walls, let him build the wall
            if (!SetNextCell(player) && player.Wall > 0)
                SetNextWall(player);
        }

        private static void SetNextWall(Player player)
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
                    SetNextWall(player);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Your input is incorrect. Try again");
                SetNextWall(player);
            }
        }



        private static void PrintMoveNumber(int moveNumber, Player player)
        {
            Console.WriteLine("Move #" + moveNumber + "\nPlayer: " + player.Name + "\nWalls: " + player.Wall);
        }

        //Makes move
        private static void SetNextCellRowAndCol(Player player)
        {
            myBoard.MarkLegalMoves(player);
            Console.WriteLine("Enter the next row number");
            int nextRow = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the next column number");
            int nextCol = int.Parse(Console.ReadLine());

            //Checking move
            if (CheckCoordinates(new Cell(nextRow, nextCol), player))
            {
                SetNextCellRowAndCol(player);
            }
        }

        private static bool SetNextCell(Player player)
        {
            myBoard.MarkLegalMoves(player);
            Console.WriteLine("Enter next coordinate or press Enter to build the wall");
            try
            {
                int number = int.Parse(Console.ReadLine());

                //Checking move
                if (CheckCoordinates(new Cell(number), player))
                {
                    SetNextCell(player);
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

        //Checking whether the move is valid
        private static bool CheckCoordinates(Cell nextCell, Player player)
        {
            if(myBoard.isSave(nextCell.RowNumber, nextCell.ColNumber) &&
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


        private static void SetPlayerName()
        {
            Console.WriteLine("Enter player name:");
            String playerName1 = Console.ReadLine();

            player1.Name = playerName1;
            player2.Name = "Bot";
        }

        private static void SetPlayerNames()
        {
            Console.WriteLine("\nEnter the first player name:");
            String playerName1 = Console.ReadLine();

            Console.WriteLine("\nEnter the second player name:");
            String playerName2 = Console.ReadLine();

            player1.Name = playerName1;
            player2.Name = playerName2;
        }

        private static bool IsAWinner(Player player)
        {
            Console.WriteLine(player.Name + " " + player.Cell.RowNumber);
            if ((player.Id == 0 && player.Cell.ColNumber == 0) ||
                (player.Id == 1 && player.Cell.ColNumber == 8))
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

        private static void PrintBoard(Board myBoard)
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
