﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Bot : IPlayer
    {
        public int Id { get; set; }
        public Cell Cell { get; set; }
        public String Name { get; set; }
        public int Wall { get; set; }
        public char Symbol { get; set; }

        public Bot()
        {
        }

        public Bot(int id, Cell cell, char symbol)
        {
            Id = id;
            Cell = cell;
            Symbol = symbol;
            Wall = 10;
        }

        public static void PlayerMakesMove(IPlayer player, Board myBoard, Graph graph)
        {
            Random random = new System.Random();

            if (random.Next(2) >= 0.5 && player.Wall > 0)
                SetNextWall(player, myBoard, graph);
            else
                SetNextCell(player, myBoard);
        }

        private static bool SetNextCell(IPlayer player, Board myBoard)
        {
            List<Cell> legalMovesList = myBoard.MarkLegalMoves(player);

            Random random = new System.Random();

            int randomIndex = random.Next(legalMovesList.Count);

            Cell randomCell = new Cell(legalMovesList[randomIndex].RowNumber, legalMovesList[randomIndex].ColNumber);

            if (IPlayer.CheckCoordinates(player, randomCell, myBoard))
            {
                SetNextCell(player, myBoard);
                return true;
            }
            else
                return false;
        }


        private static void SetNextWall(IPlayer player, Board myBoard, Graph graph)
        {
            Random random = new System.Random();

            int wall1 = random.Next(72);
            int wall2 = 0;

            if (random.Next(2) > 0.5)
            {
                wall2 = wall1 + 9;
            }
            else if (wall1 % 8 != 0)
            {
                wall2 = wall1 + 1;
            }
            else
            {
                wall1--;
                wall2 = wall1 + 1;
            }

            if (graph.BuildAWall(wall1, wall2)) // If the wall doesn't breaks the rules, we add it to the board
            {
                player.Wall--;
                myBoard.DisplayWall(wall1, wall2);
            }
            else // Or trying to build the wall again
            {
                SetNextWall(player, myBoard, graph);
            }
        }

        public static void BotMakesMove(IPlayer player, Board myBoard, Graph graph)
        {
            Random random = new System.Random();

            if (random.Next(2) >= 0.5 && player.Wall > 0)
                SetNextWall(player, myBoard, graph);
            else
                SetNextCell(player, myBoard);
        }


        public bool MakeNewMove(IPlayer player, Board myBoard, Graph graph, String input)
        {
            Random random = new System.Random();

            if (random.Next(2) >= 0.5 && player.Wall > 0)
            {
                SetNextWall(player, myBoard, graph);
            }
            else
            {
                SetNextCell(player, myBoard);
            }
            return true;
        }
    }
}
