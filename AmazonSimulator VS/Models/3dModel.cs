using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Models
{
    public abstract class _3DModel
    {
        //public abstract double _x { get; set; }
        //public abstract double _y { get; set; }
        //public abstract double _z { get; set; }

        public abstract void Move(double x, double y, double z);

        public abstract void Rotate(double rotationX, double rotationY, double rotationZ);

        public abstract bool Update(int tick);
    }
}
