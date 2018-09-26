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
            return r.x == Path.Last().X && r.z == Path.Last().Y;
        }
    }
}
