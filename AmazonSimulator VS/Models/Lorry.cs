using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Models
{
    public class Lorry : _3DModel , IUpdatable
    {
        /// <summary>
        /// De route van de vrachtwagen
        /// </summary>
        private List<Node> Route = new List<Node>();
        /// <summary>
        /// Double die aftelt wanneer de vrachtwagen beweegt
        /// </summary>
        private double DeltaX = 0;
        /// <summary>
        /// Maakt de vrachtwagen aan en zet de zet de vrachtwagen op de juiste plaats
        /// </summary>
        /// <param name="x">x waarde van de vrachtwagen</param>
        /// <param name="y">y waarde van de vrachtwagen</param>
        /// <param name="z">z waarde van de vrachtwagen</param>
        /// <param name="rotationX">rotatie x waarde van de vrachtwagen</param>
        /// <param name="rotationY">rotatie y waarde van de vrachtwagen</param>
        /// <param name="rotationZ">rotatie z waarde van de vrachtwagen</param>
        public Lorry(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("lorry", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.Move(x, y, z);
        }
        /// <summary>
        /// Beweegt de robot
        /// </summary>
        /// <param name="tick">aantal beeld updates per minuut</param>
        /// <returns>update tick</returns>
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
        /// <summary>
        /// Geeft de type in string terug
        /// </summary>
        /// <returns>string</returns>
        public override string getType()
        {
            type = this.type;
            return type;
        }
        /// <summary>
        /// Geeft de route aan de vrachtwagen
        /// </summary>
        /// <param name="route">vrachtwagen route</param>
        public void VrachtwagenRoute(List<Node> route)
        {
            Route = route;
        }
        /// <summary>
        /// Geeft de route terug
        /// </summary>
        /// <returns>node route</returns>
        public List<Node> GetRoute()
        {
            return this.Route;
        }
    }
}

