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
        double moveX, moveZ;

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

            foreach (Node x in this.Route)
            {
                
                if (Math.Round(this._z) < x.Z)
                {
                    this.Move(this._x, this._y, this._z += 0.1);
                    Console.WriteLine(this._z);
                }
                else if (Math.Round(this._z) > x.Z)
                {
                    this.Move(this._x, this._y, this._z -= 0.1);
                    Console.WriteLine(this._z);
                }
                else if (this._x < x.X)
                {
                    this.Move(this._x += 0.10, this._y, this._z);
                    Console.WriteLine("x= " + this._x);
                }
                else if (this._x > x.X)
                {
                    this.Move(this._x -= 0.10, this._y, this._z);
                    Console.WriteLine("x= " + this._x);
                }
            }

            /*if (this._x <= X)
            {
                this.Move(this._x += 0.1, this._y, this._z);
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