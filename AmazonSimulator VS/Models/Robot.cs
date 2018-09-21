using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : _3DModel, IUpdatable
    {
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        private double _rX = 0;
        private double _rY = 0;
        private double _rZ = 0;

        public string type { get; }
        public Guid guid { get; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }
        public List<Node> Route { get; set; }

        public bool needsUpdate = true;
        
        private double DeltaX, DeltaZ;

        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            this.type = "robot";
            this.guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;
        }

        public override void Move(double x, double y, double z)
        {
            this._x = x;
            this._y = y;
            this._z = z;

            needsUpdate = true;
        }

        public override void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            needsUpdate = true;
        }

        public override bool Update(int tick)
        {
            if (this.Route.Count() >= 0)
            {
                if (this.Route.Count() != 0)
                {
                    if (Math.Round(DeltaX) == 0 && Math.Round(DeltaZ) == 0)
                    {
                        DeltaX = this.Route[0].X - this._x;
                        DeltaZ = this.Route[0].Z - this._z;
                        this.Route.RemoveAt(0);
                    }
                }
                if (Math.Round(DeltaZ) > 0)
                {
                    this.Move(this._x, this._y, this._z += 0.1);
                    DeltaZ -= 0.1;
                    Console.WriteLine(DeltaZ);
                }
                else if (Math.Round(DeltaZ) < 0)
                {
                    this.Move(this._x, this._y, this._z -= 0.1);
                    DeltaZ += 0.1;
                }
                else if (Math.Round(DeltaX) > 0)
                {
                    this.Move(this._x += 0.1, this._y, this._z);
                    DeltaX -= 0.1;
                    Console.WriteLine(DeltaX);
                }
                else if (Math.Round(DeltaX) < 0)
                {
                    this.Move(this._x -= 0.1, this._y, this._z);
                    DeltaX += 0.1;
                }
            }
            /*
            for (int i = this.Route.Count(); i >= 0; i--)
            {
                if (this.Route.Count() != 0)
                {
                    while (Math.Round(this.Route[0].Z) >= this._z)
                    {
                        this.Move(this._x, this._y, this._z += 1);
                    }
                    while (this.Route[0].X >= this._x)
                    {
                        this.Move(this._x += 1, this._y, this._z);
                    }
                    this.Route.RemoveAt(0);
                }
            }*/
            //eerst y gelijk stellen. vervolgens x gelijkstellen daarna weer x dan weer y of iig zoiets
            // a --> c --> stelling --> hoogte b --> b eindpunt
            if (needsUpdate)
            {
                needsUpdate = false;
                return true;
            }
            return false;
        }
    }
}