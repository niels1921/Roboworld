using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotPickUp : IRobotTask
    {
        private Shelf shelf;
        private Node ShelfNode;
        private Manager worldmanager = new Manager();

        public RobotPickUp(Node n)
        {
            this.shelf = n.Shelf;
            this.ShelfNode = n;
        }

        public void StartTask(Robot r)
        {            
            r.AddShelf(this.shelf);
        }

        public bool Taskcomplete(Robot r)
        {
            ShelfNode.Shelf = null;
            double xwaarde = Math.Round(r.x, 2);
            double zwaarde = Math.Round(r.z, 2);
            return xwaarde == Math.Round(shelf.x, 2) && zwaarde == Math.Round(shelf.z, 2);
        }
    }
}
