using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleChessApp
{
    public class AIBot : IPlayer
    {
        public int Id { get; set; }
        public Cell Cell { get; set; }
        public String Name { get; set; }
        public int Wall { get; set; }
        public char Symbol { get; set; }

        public AIBot()
        {
        }

        public AIBot(int id, Cell cell, char symbol)
        {
            Id = id;
            Cell = cell;
            Symbol = symbol;
            Wall = 10;
        }

        public bool MakeNewMove(IPlayer player, IPlayer opponent, Board myBoard, Graph graph)
        {

            findBestMove(player, opponent, myBoard, graph);

            return true;
        }



        private Cell findBestMove(IPlayer player, IPlayer opponent, Board myBoard, Graph graph)
        {
            int bestValue = -1000;
            int value;
            List<Cell> possibleMoves;
            Cell playerCell;
            Cell bestCell = null;

            Cell prevPlayerCell = player.Cell;

            possibleMoves = myBoard.MarkLegalMoves(player);

            foreach (Cell cell in possibleMoves)
            {

                playerCell = player.Cell;
                ChangeAIBotPosition(player, cell, myBoard);

                value = minimax(player, opponent, myBoard, 0, false, -1000, 1000);

                ChangeAIBotPosition(player, playerCell, myBoard);

                if(value > bestValue)
                {
                    bestCell = cell;
                    bestValue = value;
                }


            }

            //Console.WriteLine(bestValue);

            if (player.Wall > 0 && bestValue < 0)
            {
                if(BuildAIWall(player, opponent, myBoard, graph)) // If we builded the correct wall
                {
                    ChangeAIBotPosition(player, new Cell(prevPlayerCell.RowNumber, prevPlayerCell.ColNumber), myBoard);

                    return bestCell;
                }
            }

            ChangeAIBotPosition(player, bestCell, myBoard);

            PrintBestMove(bestCell, prevPlayerCell);

            return bestCell;
        }

        


        private int minimax(IPlayer player, IPlayer opponent, Board myBoard, int depth, bool isMax, int alpha, int beta)
        {

            int bestValue;
            int value;
            List<Cell> possibleMoves;
            Cell playerCell;
            Cell opponentCell;

            int winner = evaluateScore(player, opponent, myBoard);

            int returnedScore = ReturnScore(player, winner, depth, isMax);
            if (returnedScore != 666) 
            {
                return returnedScore; // if SOMEBODY wins and depth <= 14
            }

            if (isMax)
            {
                bestValue = -1000;
                possibleMoves = myBoard.MarkLegalMoves(player);
                foreach (Cell cell in possibleMoves)
                {

                    playerCell = new Cell(player.Cell.RowNumber, player.Cell.ColNumber);
                    ChangeAIBotPosition(player, cell, myBoard);

                    value = minimax(player, opponent, myBoard, depth + 1, false, alpha, beta);
                    bestValue = Math.Max(bestValue, value);

                    ChangeAIBotPosition(player, playerCell, myBoard);

                    alpha = Math.Max(alpha, bestValue);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return bestValue;
            }
            else
            {
                bestValue = 1000;
                possibleMoves = myBoard.MarkLegalMoves(opponent);
                foreach (Cell cell in possibleMoves)
                {
                    opponentCell = new Cell(opponent.Cell.RowNumber, opponent.Cell.ColNumber);
                    ChangeAIBotPosition(opponent, cell, myBoard);

                    value = minimax(player, opponent, myBoard, depth + 1, true, alpha, beta);
                    bestValue = Math.Min(bestValue, value);

                    ChangeAIBotPosition(opponent, opponentCell, myBoard);

                    beta = Math.Min(beta, bestValue);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return bestValue;
            }
        }

        private int ReturnScore(IPlayer player, int winner, int depth, bool isMax)
        {
            if (winner == 10) // AIBot wins
            {
                return depth;
            }
            else if (winner == -10)  // Opponent wins
            {
                return -depth;
            }

            if (depth > 14)
            {
                if (player.Id == 0)
                {
                    if (isMax)
                    {
                        return -100;
                    }
                    else
                    {
                        return 100;
                    }
                }
                else
                {
                    if (isMax)
                    {
                        return 100;
                    }
                    else
                    {
                        return -100;
                    }
                }
            }
            return 666; //NOBODY wins and depth <= 14
        }

        private int evaluateScore(IPlayer player, IPlayer opponent, Board board)
        {
            if (Board.IsAWinner(player, board))
            {
                return 10; // IF AIBot wins
            }
            else if (Board.IsAWinner(opponent, board))
            {
                return -10; // If opponent wins
            }
                return 0;
        }


        private bool BuildAIWall(IPlayer player, IPlayer opponent, Board myBoard, Graph graph)
        {
            if (opponent.Id == 0)
            {
                int opponentNumber = 9 * opponent.Cell.ColNumber + opponent.Cell.RowNumber;
                int wall1 = opponentNumber - 9;
                int wall2 = wall1 + 1;
                if (graph.BuildAWall(wall1, wall2, player, opponent, myBoard))
                {
                    player.Wall--;
                    myBoard.DisplayWall(wall1, wall2);

                    PrintBestWall(wall1, wall2);

                    return true;
                } else
                {
                    wall2 = opponentNumber - 9;
                    wall1 = wall2 - 1;
                    if (graph.BuildAWall(wall1, wall2, player, opponent, myBoard))
                    {
                        player.Wall--;
                        myBoard.DisplayWall(wall1, wall2);

                        PrintBestWall(wall1, wall2);

                        return true;
                    }
                }
            }
            else
            {
                int opponentNumber = 9 * opponent.Cell.ColNumber + opponent.Cell.RowNumber;
                int wall1 = opponentNumber;
                int wall2 = wall1 + 1;
                if (graph.BuildAWall(wall1, wall2, player, opponent, myBoard))
                {
                    player.Wall--;
                    myBoard.DisplayWall(wall1, wall2);

                    PrintBestWall(wall1, wall2);

                    return true;
                }
                else
                {
                    wall2 = opponentNumber;
                    wall1 = wall2 - 1;
                    if (graph.BuildAWall(wall1, wall2, player, opponent, myBoard))
                    {
                        player.Wall--;
                        myBoard.DisplayWall(wall1, wall2);

                        PrintBestWall(wall1, wall2);

                        return true;
                    }
                }
            }
            return false;
        }

        // Changes AIBot Position
        private void ChangeAIBotPosition(IPlayer player, Cell nextCell, Board myBoard)
        {
            myBoard.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = false;
            player.Cell = new Cell(nextCell.RowNumber, nextCell.ColNumber);
            myBoard.theGrid[player.Cell.RowNumber, player.Cell.ColNumber].CurrentlyOccupied = true;
        }



        private void PrintBestMove(Cell cell, Cell prevPlayerCell)
        {

            String command;
            if (cell.RowNumber == prevPlayerCell.ColNumber)
            {
                if (cell.ColNumber + 1 == prevPlayerCell.RowNumber || cell.ColNumber - 1 == prevPlayerCell.RowNumber)
                {
                    command = "move";
                } else
                {
                    command = "jump";
                }
            }
            else if (cell.ColNumber == prevPlayerCell.RowNumber)
            {
                if (cell.RowNumber + 1 == prevPlayerCell.ColNumber || cell.RowNumber - 1 == prevPlayerCell.ColNumber)
                {
                    command = "move";
                } else
                {
                    command = "jump";
                }
            } else
            {
                command = "jump";
            }

            switch (command)
            {
                case "move":
                    Console.WriteLine("move " + cell.Symbol1 + cell.Symbol2);
                    break;
                case "jump":

                    Console.WriteLine("jump " + cell.Symbol1 + cell.Symbol2);
                    break;
                default:
                    break;
            }
        }


        private void PrintBestWall(int wall1, int wall2)
        {
            int col = wall1 % 9;
            int row = (wall1 - col) / 9;
            Cell wall = new Cell(col, row);

            String[] letters = { "S", "T", "U", "V", "W", "X", "Y", "Z"};
            String letter = letters.GetValue(col).ToString();
            
            int number = wall.Symbol2;
            String rotation;
            if (wall2 - wall1 == 1)
            {
                rotation = "h";
            }
            else
            {
                rotation = "v";
            }
            Console.WriteLine("wall " + letter + number + rotation);
        }
    }
}
