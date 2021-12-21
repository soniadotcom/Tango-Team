using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleChessApp
{
    public class Graph
    {
        public int VerticesNumber { get; set; }
        public LinkedList<int>[] adjLists { get; set; }

        public Graph (int vertices)
        {
            VerticesNumber = vertices;
            adjLists = new LinkedList<int>[vertices];

            for (int i = 0; i < vertices; i++)
            {
                adjLists[i] = new LinkedList<int>();
            }

            addAllEdges();
        }

        public void addAllEdges()
        {
            for (int i = 0; i < 81; i++)
            {
                if (i - 9 >= 0)
                {
                    addEdge(i, i - 9);
                }
                if (i + 1 < 81 && i % 9 != 8)
                {
                    addEdge(i, i + 1);
                }
                if (i + 9 < 81)
                {
                    addEdge(i, i + 9);
                }
                if (i - 1 >= 0 && i % 9 != 0)
                {
                    addEdge(i, i - 1);
                }
            }
        }

        public void addEdge (int src, int dest)
        {
            adjLists[src].AddLast(dest);
        }

        public bool GoingThroughGraph(int s, int t)
        {

            ArrayList queue = new ArrayList();

            queue.Add(s);

            bool[] visited = new bool[81];
            for (int i = 0; i < 81; i++)
            {
                visited[i] = false;
            }

            visited[s] = true;

            
            while (queue.Count > 0)
            {

                int v = (int)queue[0];
                queue.RemoveAt(0);

                foreach (int neighbor in adjLists[v])
                {
                    if (!visited[neighbor])
                    {
                        queue.Add(neighbor);
                        visited[neighbor] = true;
                        if (neighbor == t)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckMoveCorrectness(IPlayer player, IPlayer opponent)
        {

            int numberWhite = player.Cell.Number;
            int numberBlack = opponent.Cell.Number;


            if(player.Id == 1)
            {
                numberBlack = new Cell(player.Cell.RowNumber, player.Cell.ColNumber).Number;
                numberWhite = new Cell(opponent.Cell.RowNumber, opponent.Cell.ColNumber).Number;
                
            } else
            {
                numberWhite = new Cell(player.Cell.RowNumber, player.Cell.ColNumber).Number;
                numberBlack = new Cell(opponent.Cell.RowNumber, opponent.Cell.ColNumber).Number;
            }

            bool black = false;
            bool white = false;

            for(int i = 0; i < 9; i++)
            {
                if (GoingThroughGraph(numberWhite, i)) {
                    white = true;
                }
            }

            for (int i = 80; i > 71; i--)
            {
                if (GoingThroughGraph(numberBlack, i))
                {
                    black = true;
                }
            }

            return white && black;
        }


        public bool BuildAWall(int a, int b, IPlayer player, IPlayer opponent, Board myBoard)
        {
            int c = 0, d = 0;
            if (b - a == 9)
            {
                c = a + 1;
                d = b + 1;
            }
            else if (b - a == 1)
            {
                c = a + 9;
                d = b + 9;
            }
            else
            {
                //Console.WriteLine("Coordinates are wrong");
                return false;
            }

            Cell aCell = new Cell(a);

            if (a >= 0 && b >= 0 && c >= 0 && d >= 0)
            {
                if (a < 72 && a % 9 != 8)
                {
                    if (adjLists[a].Contains(c) && adjLists[c].Contains(a) &&
                        adjLists[b].Contains(d) && adjLists[d].Contains(b))
                    {
                        if (myBoard.theGrid[aCell.RowNumber, aCell.ColNumber].HorizontalWall != 1 &&
                            myBoard.theGrid[aCell.RowNumber, aCell.ColNumber].VerticalWall != 1)
                        {
                            adjLists[a].Find(c).Value = a;
                            adjLists[c].Find(a).Value = c;

                            adjLists[b].Find(d).Value = b;
                            adjLists[d].Find(b).Value = d;
                        } 
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            if (CheckMoveCorrectness(player, opponent))
            {
                //Console.WriteLine("Wall " + a + " " + b + " is ready");
                return true;
            }
            else
            {
                adjLists[a].Find(a).Value = c;
                adjLists[c].Find(c).Value = a;

                adjLists[b].Find(b).Value = d;
                adjLists[d].Find(d).Value = b;

                //Console.WriteLine("Wall " + a + " " + b + " can't be placed here");
                return false;
            }
        }
    }
}