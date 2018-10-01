using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotPickUp : IRobotTask
    {
        private Shelf Shelf;
        public RobotPickUp(Shelf s)
        {
            this.Shelf = s;
        }

        public void StartTask(Robot r)
        {
            r.AddShelf(Shelf);
        }

        public bool Taskcomplete(Robot r)
        {
            return true;
        }
    }
}
