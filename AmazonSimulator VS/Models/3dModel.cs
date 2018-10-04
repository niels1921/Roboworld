using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Models
{
    public abstract class _3DModel
    {
        /// <summary>
        /// private x waarde van de 3d modellen
        /// </summary>
        private double _x = 0;
        /// <summary>
        /// private y waarde van de 3d modellen
        /// </summary>
        private double _y = 0;
        /// <summary>
        /// private z waarde van de 3d modellen
        /// </summary>
        private double _z = 0;
        /// <summary>
        /// private rotatie x waarde van de 3d modellen
        /// </summary>
        private double _rX = 0;
        /// <summary>
        /// private rotatie y waarde van de 3d modellen
        /// </summary>
        private double _rY = 0;
        /// <summary>
        /// private rotatie z waarde van de 3d modellen
        /// </summary>
        private double _rZ = 0;
        /// <summary>
        /// type van het 3d model
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// unique id van de 3d modellen
        /// </summary>
        public Guid guid { get; set; }
        /// <summary>
        /// public x waarde van het 3d model 
        /// </summary>
        public double x { get { return _x; } }
        /// <summary>
        /// public y waarde van het 3d model 
        /// </summary>
        public double y { get { return _y; } }
        /// <summary>
        /// public z waarde van het 3d model 
        /// </summary>
        public double z { get { return _z; } }
        /// <summary>
        /// public rotatie x waarde van het 3d model 
        /// </summary>
        public double rotationX { get { return _rX; } }
        /// <summary>
        /// public rotatie y waarde van het 3d model 
        /// </summary>
        public double rotationY { get { return _rY; } }
        /// <summary>
        /// public rotatie z waarde van het 3d model 
        /// </summary>
        public double rotationZ { get { return _rZ; } }
        /// <summary>
        /// bool die aangeeft dat alle modellen updates nodig hebben
        /// </summary>
        public bool needsUpdate = true;
        /// <summary>
        /// Blauwdruk voor het maken van alle 3d modellen
        /// </summary>
        /// <param name="type">type van het 3d model</param>
        /// <param name="x">x waarde van het 3d model</param>
        /// <param name="y">y waarde van het 3d model</param>
        /// <param name="z">z waarde van het 3d model</param>
        /// <param name="rotationX">x waarde van het 3d model</param>
        /// <param name="rotationY">y waarde van het 3d model</param>
        /// <param name="rotationZ">z waarde van het 3d model</param>
        public _3DModel(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            this.type = type;
            this.guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;
        }
        /// <summary>
        /// Blauwdruk voor de move functie voor alle 3d modellen
        /// </summary>
        /// <param name="x">x waarde van het 3d model die geupdate wordt per tick</param>
        /// <param name="y">y waarde van het 3d model die geupdate wordt per tick</param>
        /// <param name="z">z waarde van het 3d model die geupdate wordt per tick</param>
        public virtual void Move(double x, double y, double z)
        {
            this._x = x;
            this._y = y;
            this._z = z;

            needsUpdate = true;
        }
        /// <summary>
        /// Blauwdruk voor de rotatie functie voor alle 3d modellen
        /// </summary>
        /// <param name="rotationX">rotatie x waarde van het 3d model die geupdate wordt per tick</param>
        /// <param name="rotationY">rotatie y waarde van het 3d model die geupdate wordt per tick</param>
        /// <param name="rotationZ">rotatie z waarde van het 3d model die geupdate wordt per tick</param>
        public virtual void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            needsUpdate = true;
        }
        /// <summary>
        /// Blauwdruk voor de update functie voor alle 3d modellen
        /// </summary>
        /// <param name="tick">aantal scherm updates per minuut</param>
        /// <returns>bool</returns>
        public virtual bool Update(int tick)
        {
            if (needsUpdate)
            {
                needsUpdate = false;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Blauwdruk voor de gettype functie voor alle 3d modellen
        /// </summary>
        /// <returns>string</returns>
        public abstract string getType();
    }
}
