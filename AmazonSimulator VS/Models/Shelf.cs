using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Shelf : _3DModel, IUpdatable
{
        public List<Node> Route { get; set; }

        public Node Node { get; set; }

        public Shelf(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("shelf", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.Move(x, y, z);
        }

        public override bool Update(int tick)
        {
            return base.Update(tick);
        }

        public override string getType()
        {
            type = this.type;

            return type;
        }
    }
}

