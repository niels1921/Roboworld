using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotPickUp : IRobotTask
    {
        /// <summary>
        /// de shelf die moet worden opgepakt
        /// </summary>
        private Shelf shelf;
        /// <summary>
        /// de node waar de shelf moet worden opgehaald
        /// </summary>
        private Node ShelfNode;
        /// <summary>
        /// geeft de shelf en de node door
        /// </summary>
        /// <param name="n">node</param>
        public RobotPickUp(Node n)
        {
            this.shelf = n.Shelf;
            this.ShelfNode = n;
        }
        /// <summary>
        /// start de taak voor de robot
        /// </summary>
        /// <param name="r">robot</param>
        public void StartTask(Robot r)
        {            
            r.AddShelf(this.shelf);
        }
        /// <summary>
        /// geeft door wanneer de taak compleet is
        /// </summary>
        /// <param name="r">robot</param>
        /// <returns>bool</returns>
        public bool Taskcomplete(Robot r)
        {
            ShelfNode.Shelf = null;
            double xwaarde = Math.Round(r.x, 2);
            double zwaarde = Math.Round(r.z, 2);
            return xwaarde == Math.Round(shelf.x, 2) && zwaarde == Math.Round(shelf.z, 2);
        }
    }
}
