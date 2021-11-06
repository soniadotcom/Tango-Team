using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Board myBoard = new Board(9);

            IPlayer winner = new Player();

            Graph graph = new Graph(81);

            bool gameInProgress = true;

            int moveNumber = 1;

            Player player1 = new Player(0, new Cell(8, 4), '■');
            Bot player2 = new Bot(1, new Cell(0, 4), 'O');



            Board.PutPlayerOnBoard(player1, myBoard);
            Board.PutPlayerOnBoard(player2, myBoard);

            IPlayer[] players = new IPlayer[2];

            if (Input.ChooseColor()) // Якщо введено BLACK
            {
                player1.Name = "White";
                player2.Name = "Black";
                players[0] = player1;
                players[1] = player2;
            }
            else
            {
                player1.Name = "White";
                player2.Name = "Black";
                players[0] = player2;
                players[1] = player1;
            }

            

            while (gameInProgress)
            {
                //Console.Clear();

                if (moveNumber % 2 == 0)
                {
                    //View.PrintMoveNumber(moveNumber, players[0]);
                    myBoard.MarkLegalMoves(players[0]);
                    View.PrintBoard(myBoard, players[0], players[1]);

                    String input = Console.ReadLine();

                    players[0].MakeNewMove(players[0], myBoard, graph, input);

                    if (Board.IsAWinner(players[0], myBoard))
                    {
                        winner = players[0];
                        break;
                    }
                }
                else
                {
                    //View.PrintMoveNumber(moveNumber, players[1]);
                    myBoard.MarkLegalMoves(players[1]);
                    View.PrintBoard(myBoard, players[0], players[1]);

                    String input = Console.ReadLine();

                    players[1].MakeNewMove(players[1], myBoard, graph, input);

                    if (Board.IsAWinner(players[1], myBoard))
                    {
                        winner = players[1];
                        break;
                    }
                }

                moveNumber++;
            }

            //View.ExplainRules();
            //StartMenu();
        }

        /*

        public static void StartMenu()
        {
            View.ChooseGameMode();
            String gameMode = Input.InputGameMode();

            StartTheGame(gameMode);
        }

        private static void StartTheGame(String gamemode)
        {
            Board myBoard = new Board(9);

            Player winner = new Player();

            Graph graph = new Graph(81);

            bool gameInProgress = true;

            int moveNumber = 1;

            String[] playerNames;

            if (gamemode == "Bot")
            {
                playerNames = Input.InputPlayerName();
            }
            else
            {
                playerNames = Input.InputPlayerNames();
            }

            Player[] players = Player.SetPlayerNames(playerNames);

            Board.PutPlayerOnBoard(players[0], myBoard);
            Board.PutPlayerOnBoard(players[1], myBoard);

            while (gameInProgress)
            {
                Console.Clear();

                if (moveNumber % 2 == 1)
                {
                    View.PrintMoveNumber(moveNumber, players[0]);
                    myBoard.MarkLegalMoves(players[0]);
                    View.PrintBoard(myBoard, players[0], players[1]);

                    Player.PlayerMakesMove(players[0], myBoard, graph);

                    if (IPlayer.IsAWinner(players[0], myBoard))
                    {
                        winner = players[0];
                        break;
                    }
                }
                else
                {
                    View.PrintMoveNumber(moveNumber, players[1]);
                    myBoard.MarkLegalMoves(players[1]);
                    View.PrintBoard(myBoard, players[0], players[1]);

                    if (gamemode == "Bot")
                    {
                        Bot.BotMakesMove(players[1], myBoard, graph);
                    }
                    else
                    {
                        Player.PlayerMakesMove(players[1], myBoard, graph);
                    }

                    if (IPlayer.IsAWinner(players[1], myBoard))
                    {
                        winner = players[1];
                        break;
                    }
                }

                moveNumber++;
            }

            View.GameOver(moveNumber, winner, myBoard, players[0], players[1]);

            if (Input.PlayAgain())
            {
                Console.Clear();
                Program.StartMenu();
            }
        }
        */
    }
}
