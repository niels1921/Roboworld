using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotDropDown : IRobotTask
    {
        /// <summary>
        /// Node waar de shelf moet worden neergezet
        /// </summary>
        private Node node;
        /// <summary>
        /// De shelf die moet worden neergezet
        /// </summary>
        private Shelf shelf;
        /// <summary>
        /// Geeft de node door waar de shelf moet worden neergezet
        /// </summary>
        /// <param name="n">node</param>
        public RobotDropDown(Node n)
        {
            this.node = n;
        }
        /// <summary>
        /// Start de taak voor de robot en verwijdert de shelf van de robot
        /// </summary>
        /// <param name="r">robot</param>
        public void StartTask(Robot r)
        {
            this.shelf = r.ReturnShelf();
            r.RemoveShelf();
            node.Shelf = this.shelf;

        }
        /// <summary>
        /// Wanneer de robot de node waar de shelf moet staan bereikt
        /// </summary>
        /// <param name="r">robot</param>
        /// <returns>bool</returns>
        public bool Taskcomplete(Robot r)
        {
            return Math.Round(r.x, 2) == node.X && Math.Round(r.z, 2) == node.Z;
        }
    }
}
