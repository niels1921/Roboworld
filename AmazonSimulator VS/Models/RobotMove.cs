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

        public void StartTask(Robot r)
        {
            throw new NotImplementedException();
        }

        public bool Taskcomplete(Robot r)
        {
            throw new NotImplementedException();
        }
    }
}
