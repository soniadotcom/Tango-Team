using System;

namespace ConsoleChessApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            View.ExplainRules();
            StartMenu();
        }

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

                    if (Player.IsAWinner(players[0], myBoard))
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

                    if(gamemode == "Bot")
                    {
                        Bot.BotMakesMove(players[1], myBoard, graph);
                    }
                    else
                    {
                        Player.PlayerMakesMove(players[1], myBoard, graph);
                    }

                    if (Player.IsAWinner(players[1], myBoard))
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
    }
}
