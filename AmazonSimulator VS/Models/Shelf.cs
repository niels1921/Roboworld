using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Shelf : _3DModel, IUpdatable
    {
        /// <summary>
        /// maakt de shelf aan
        /// </summary>
        /// <param name="x">x waarde van de shelf</param>
        /// <param name="y">y waarde van de shelf</param>
        /// <param name="z">z waarde van de shelf</param>
        /// <param name="rotationX">rotatie x van de shelf</param>
        /// <param name="rotationY">rotatie y van de shelf</param>
        /// <param name="rotationZ">rotatie z van de shelf</param>
        public Shelf(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("shelf", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.Move(x, y, z);
        }
        /// <summary>
        /// aantal updates per tick
        /// </summary>
        /// <param name="tick">aantal beeld updates per minuut</param>
        /// <returns>update</returns>
        public override bool Update(int tick)
        {
            return base.Update(tick);
        }
        /// <summary>
        /// haalt het type van dit 3d model
        /// </summary>
        /// <returns>string</returns>
        public override string getType()
        {
            type = this.type;

            return type;
        }
    }
}

