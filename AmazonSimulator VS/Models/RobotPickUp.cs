using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotPickUp : IRobotTask
    {
        private Shelf shelf;
        private Node RandomNode;
        private List<Node> nodes = new List<Node>();

        public RobotPickUp(Shelf s, Node n, List<Node> ln)
        {
            this.shelf = s;
            this.RandomNode = n;
            this.nodes = ln;
        }

        public void StartTask(Robot r)
        {
            if (r.x == shelf.x && r.z == shelf.z)
                r.RemoveShelf();
            else
                r.AddShelf(this.shelf);
        }

        public bool Taskcomplete(Robot r)
        {
            if (Math.Round(r.x, 2) == nodes[0].X && Math.Round(r.z, 2) == nodes[0].Z)
            {
                nodes[0].Shelf = RandomNode.Shelf;
                RandomNode.Shelf = null;
            }
            double xwaarde = Math.Round(r.x, 2);
            double zwaarde = Math.Round(r.z, 2);
            return xwaarde == Math.Round(shelf.x, 2) && zwaarde == Math.Round(shelf.z, 2);
        }       
    }
}
