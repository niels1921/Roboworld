using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotMove : IRobotTask
    {
        private bool StartupComplete = false;
        private bool Complete = false;

        private List<Node> Path;

        public RobotMove(List<Node> path)
        {
            this.Path = path;
        }

        public void StartTask(Robot r)
        {
            r.MoveOverPath(this.Path);
        }

        public bool Taskcomplete(Robot r)
        {
            //return false;
            double xwaarde = Math.Round(r.x, 2);
            double zwaarde = Math.Round(r.z, 2);
           return xwaarde == Path.Last().X && zwaarde == Path.Last().Y;
        }
    }
}
