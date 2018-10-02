using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotPickUp : IRobotTask
    {
        private Shelf shelf;

        public RobotPickUp(Shelf s)
        {
            this.shelf = s;
        }

        public void StartTask(Robot r)
        {
            r.AddShelf(this.shelf);

        }

        public bool Taskcomplete(Robot r)
        {
            double xwaarde = Math.Round(r.x, 2);
            double zwaarde = Math.Round(r.z, 2);
            return xwaarde == Math.Round(shelf.x, 2) && zwaarde == Math.Round(shelf.z, 2);
        }

        
    }
}
