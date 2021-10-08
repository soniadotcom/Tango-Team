using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    public class Player
    {
        public int Id { get; set; }
        public Cell Cell { get; set; }
        public String Name { get; set; }
        public int Wall { get; set; }
        public Player(int id, Cell cell)
        {
            Id = id;
            Cell = cell;
            Wall = 5;
        }
    }
}
