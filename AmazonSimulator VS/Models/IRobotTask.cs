using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public interface IRobotTask
    {
        /// <summary>
        /// blauwdruk voor de startask methode voor alle taken van de robot
        /// </summary>
        /// <param name="r">robot</param>
        void StartTask(Robot r);
        /// <summary>
        /// blauwdruk voor de taskcomplete methode voor alle taken van de robot
        /// </summary>
        /// <param name="r">robot</param>
        /// <returns></returns>
        bool Taskcomplete(Robot r);
    }
}
