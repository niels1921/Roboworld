using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Models
{
    public class Lorry : _3DModel , IUpdatable
    {
        private List<Node> Route = new List<Node>();

        private double DeltaX = 0;

        private int shelfnr;

        public Lorry(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("lorry", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.Move(x, y, z);
        }

        public int Shelfnr()
        {
            return shelfnr;
        }

        public void SetShelfnr(int nr)
        {
            this.shelfnr = nr;
        }

        public override bool Update(int tick)
        {

            if (this.Route.Count() >= 0)
            {
                if (this.Route.Count() != 0)
                {
                    if (Math.Round(DeltaX) == 0)
                    {
                        DeltaX = Math.Abs(this.Route[0].X) - Math.Abs(this.x);
                        DeltaX = Math.Abs(DeltaX);
                        this.Route.RemoveAt(0);
                    }                   
                }
                if (Math.Round(DeltaX) > 0)
                {
                    this.Move(this.x + 0.1, this.y, this.z);
                    DeltaX -= 0.1;
                }
            }
            return base.Update(tick);
        }

        public override string getType()
        {
            type = this.type;
            return type;
        }

        public void VrachtwagenRoute(List<Node> route)
        {
            Route = route;
        }

        public List<Node> GetRoute()
        {
            return this.Route;
        }
    }
}

