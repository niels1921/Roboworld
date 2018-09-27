using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Node
    {
        public string Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        //public bool Occupied { get; set; }


        public static List<Node> Punten = new List<Node>()
        {
            new Node() { Id = "A", X = 2, Y = 0, Z = 4 },
            new Node() { Id = "B", X = 28, Y = 0, Z = 4 },
            new Node() { Id = "C", X = 28, Y = 0, Z = 28 },
            new Node() { Id = "D", X = 2, Y = 0, Z = 28 },
            new Node() { Id = "E", X = 2, Y = 0, Z = 8 },
            new Node() { Id = "F", X = 2, Y = 0, Z = 20 },
            new Node() { Id = "G", X = 14, Y = 0, Z = 8 },
            new Node() { Id = "H", X = 14, Y = 0, Z = 20 },
        };

    }
}
