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

            IPlayer[] players = new IPlayer[2];

            if (Input.ChooseColor()) // Якщо введено BLACK
            {
                players[0] = new Player(0, new Cell(8, 4), '■');
                players[1] = new AIBot(1, new Cell(0, 4), 'O');
            }
            else
            {
                players[0] = new AIBot(0, new Cell(8, 4), '■');
                players[1] = new Player(1, new Cell(0, 4), 'O');
            }
            players[0].Name = "White";
            players[1].Name = "Black";


            Board.PutPlayerOnBoard(players[0], myBoard);
            Board.PutPlayerOnBoard(players[1], myBoard);


            while (gameInProgress)
            {
                //Console.Clear();

                if (moveNumber % 2 == 1)
                {
                    //View.PrintMoveNumber(moveNumber, players[0]);
                    myBoard.MarkLegalMoves(players[0]);
                    //View.PrintBoard(myBoard, players[0], players[1]);

                    players[0].MakeNewMove(players[0], players[1], myBoard, graph);

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
                    //View.PrintBoard(myBoard, players[0], players[1]);

                    players[1].MakeNewMove(players[1], players[0], myBoard, graph);

                    if (Board.IsAWinner(players[1], myBoard))
                    {
                        winner = players[1];
                        break;
                    }
                }

                moveNumber++;
            }
        }
    }
}
