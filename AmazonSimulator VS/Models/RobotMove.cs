using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class RobotMove : IRobotTask
    {
        /// <summary>
        /// de route die de robot moet volgen
        /// </summary>
        private List<Node> Path;
        /// <summary>
        /// geeft de route door aan de robot
        /// </summary>
        /// <param name="path">de route van de robot</param>
        public RobotMove(List<Node> path)
        {
            this.Path = path;
        }
        /// <summary>
        /// start de taak van de robot
        /// </summary>
        /// <param name="r">robot</param>
        public void StartTask(Robot r)
        {
            r.MoveOverPath(this.Path);
        }
        /// <summary>
        /// eindigt de taak als het doel bereikt is
        /// </summary>
        /// <param name="r">robot</param>
        /// <returns>bool</returns>
        public bool Taskcomplete(Robot r)
        {
            double xwaarde = Math.Round(r.x, 2);
            double zwaarde = Math.Round(r.z, 2);
           return xwaarde == Path.Last().X && zwaarde == Path.Last().Z;
        }
    }
}
