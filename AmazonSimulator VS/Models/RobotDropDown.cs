using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotDropDown : IRobotTask
    {
        private Node node;
        private Shelf shelf;
        public RobotDropDown(Node n)
        {
            this.node = n;
        }
        public void StartTask(Robot r)
        {
            this.shelf = r.ReturnShelf();
            r.RemoveShelf();
            node.Shelf = this.shelf;

        }

        public bool Taskcomplete(Robot r)
        {
            return Math.Round(r.x, 2) == node.X && Math.Round(r.z, 2) == node.Z;
        }
    }
}
