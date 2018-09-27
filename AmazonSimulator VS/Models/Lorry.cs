﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Models
{
    public class Lorry : _3DModel , IUpdatable
    {
        private List<Node> Route = new List<Node>();

        public Lorry(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("lorry", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.Move(x, y, z);
        }

        public override bool Update(int tick)
        {
            if (this.Route != null)
            {

                if (Math.Round(this.x) != this.Route[0].X)
                {
                    this.Move(this.x + 0.1, this.y, this.z);

                }
                else
                {
                    this.Route.RemoveAt(0);
                }
            }


            return base.Update(tick);
        }

        public override string getType()
        {
            type = this.type;
            return type;
        }

        public void AddRoute(Node route)
        {
            this.Route.Add(route);
        }

        public List<Node> GetRoute()
        {
            return this.Route;
        }
    }
}

