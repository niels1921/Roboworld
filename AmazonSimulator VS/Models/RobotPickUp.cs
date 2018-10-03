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

        public RobotPickUp(Shelf s, Node n)
        {
            this.shelf = s;
            this.ShelfNode = n;
        }

        public void StartTask(Robot r)
        {
            if (r.x == shelf.x && r.z == shelf.z && r.ShelfStatus() == true)
                r.RemoveShelf();
            else
                r.AddShelf(this.shelf);
        }

        public bool Taskcomplete(Robot r)
        {
            foreach (Node n in Manager.Punten)
            {
                if (n.Id.Length == 4)
                {
                    if (Math.Round(r.x, 2) == n.X && Math.Round(r.z, 2) == n.Z && Manager.TruckDelivery == false)
                    {
                        Manager.TruckReadyList[0].Shelf = ShelfNode.Shelf;
                        
                        ShelfNode.Shelf = null;
                    }                   
                }
                else if(n.Id.Length == 1)
                {
                    if (Math.Round(r.x, 2) == n.X && Math.Round(r.z, 2) == n.Z && Manager.TruckDelivery == true)
                    {
                        n.Shelf = worldmanager.GetDockShelf().Shelf;
                        worldmanager.RemoveDockShelf();
                    }
                }
            }

            double xwaarde = Math.Round(r.x, 2);
            double zwaarde = Math.Round(r.z, 2);
            return xwaarde == Math.Round(shelf.x, 2) && zwaarde == Math.Round(shelf.z, 2);
        }
    }
}
