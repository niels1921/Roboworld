﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Shelf : _3DModel, IUpdatable
{
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        private double _rX = 0;
        private double _rY = 0;
        private double _rZ = 0;

        public string type { get; set; }
        public Guid guid { get; set; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }
        public List<Node> Route { get; set; }

        public Node Node { get; set; }

        public Shelf(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            this.type = "shelf";
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
            if (needsUpdate)
            {
                needsUpdate = false;
                return true;
            }
            return false;
        }

        public override string getType()
        {
            type = this.type;

            return type;
        }
    }
}

