﻿using System;
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

        public void printGraph()
        {
            var mainNode = 0;
            foreach(LinkedList<int> list in adjLists)
            {
                Console.WriteLine("\n" + mainNode + ":");
                mainNode++;
                LinkedListNode<int> node = list.First;
                if (node != null)
                {
                    Console.Write(node.Value);

                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        Console.Write(" " + node.Next.Value);
                        node = node.Next;
                    }
                }
            }
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

        public bool CheckMoveCorrectness()
        {
            bool result = GoingThroughGraph(80, 0);
            if (!result)
                return false;

            for (int i = 1; i < 81; i++)
            {
                result = GoingThroughGraph(0, i);
                if (!result)
                    return false;
            }
            return true;
        }

        public void PrintMoveCorrectness()
        {
            bool result = GoingThroughGraph(80, 0);
            if (result)
            {
                Console.WriteLine("\n0 " + result);
                for (int i = 1; i < 81; i++)
                {
                    result = GoingThroughGraph(0, i);
                    Console.WriteLine(i + " " + result);
                }
            }
            else
                Console.WriteLine("\n0 " + result);
        }

        public void BuildAWall(int a, int b, int c, int d)
        {
            adjLists[a].Find(b).Value = a;
            adjLists[b].Find(a).Value = b;

            adjLists[c].Find(d).Value = c;
            adjLists[d].Find(c).Value = d;

            if (CheckMoveCorrectness())
            {
                Console.WriteLine("Wall " + a + " " + b + " " + c + " " + d + " is ready");
            }
            else
            {
                adjLists[a].Find(a).Value = b;
                adjLists[b].Find(b).Value = a;

                adjLists[c].Find(c).Value = d;
                adjLists[d].Find(d).Value = c;

                Console.WriteLine("Wall " + a + " " + b + " " + c + " " + d + " can't be placed here");
            }
        }
    }
}