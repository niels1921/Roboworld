using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : _3DModel, IUpdatable
    {
        /// <summary>
        /// De route van de robot
        /// </summary>
        public List<Node> Route { get; set; }
        /// <summary>
        /// De shelf die op de robot staat
        /// </summary>
        private Shelf Shelf;
        /// <summary>
        /// Als de robot taken heeft is deze bool true
        /// </summary>
        public bool RobotBusy { get; set; }
        /// <summary>
        /// De lijst met taken van de robot
        /// </summary>
        private List<IRobotTask> tasks = new List<IRobotTask>();
        /// <summary>
        /// Doubles die aftellen wanneer de robot beweegt
        /// </summary>
        private double DeltaX, DeltaZ;
        /// <summary>
        /// Maakt de robot aan en zet deze op de juiste positie.
        /// </summary>
        /// <param name="x">x waarde van de robot</param>
        /// <param name="y">y waarde van de robot</param>
        /// <param name="z">z waarde van de robot</param>
        /// <param name="rotationX">x rotatie waarde van de robot</param>
        /// <param name="rotationY">y rotatie waarde van de robot</param>
        /// <param name="rotationZ">z rotatie waarde van de robot</param>
        public Robot (double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.Move(this.x, this.y, this.z);
        }

        /// <summary>
        /// Zorgt ervoor dat de robot draait en moved tevens kijkt deze functie of de robot taken heeft. Zo ja wordt er gekeken of de taak af is en start de volgende totdat de taaklist leeg is. 
        /// </summary>
        /// <param name="tick">aantal beeld updates per minuut</param>
        /// <returns>update tick</returns>
        public override bool Update(int tick)
        {
            if (tasks.Count() != 0)
            {
                if (tasks.First().Taskcomplete(this))
                {
                    if (this.Route.Count() != 0)
                        this.Route.RemoveAt(0);
                    tasks.RemoveAt(0);
                    if (tasks.Count != 0)
                    {
                        tasks.First().StartTask(this);
                    }

                }
            }
            else
                this.RobotBusy = false;


            if (this.Route.Count() >= 0)
            {
                if (this.Route.Count() != 0)
                {
                    if (Math.Round(DeltaX , 1) == 0 && Math.Round(DeltaZ, 1) == 0)
                    {
                        DeltaX = this.Route[0].X - this.x;
                        DeltaZ = this.Route[0].Z - this.z;
                        DeltaX = Math.Round(DeltaX, 2);
                        DeltaZ = Math.Round(DeltaZ, 2);

                        if (this.Route.First().X > Math.Round(this.x, 2))
                        {
                            this.Rotate(this.rotationX, 0 + (Math.PI / 2), this.rotationZ);
                        }
                        else if (this.Route.First().X < Math.Round(this.x, 2))
                        {
                            this.Rotate(this.rotationX, 0 - (Math.PI / 2), this.rotationZ);
                        }
                        else if (this.Route.First().Z > Math.Round(this.z, 2))
                        {
                            this.Rotate(this.rotationX, 0, this.rotationZ);
                        }
                        else if (this.Route.First().Z < Math.Round(this.z,  2))
                        {
                            this.Rotate(this.rotationX, 0 + Math.PI, this.rotationZ);
                        }                         

                        if (this.Route.Count() != 1)
                            this.Route.RemoveAt(0);
                    }
                }

                if (Math.Round(DeltaZ, 2) > 0)
                {
                    this.Move(this.x, this.y, this.z + 0.1);
                    if(Shelf != null)
                        this.Shelf.Move(this.x, 0.2, this.z);
                    DeltaZ -= 0.1;
                }
                else if (Math.Round(DeltaZ, 2) < 0)
                {
                    this.Move(this.x, this.y, this.z - 0.1);
                    if (Shelf != null)
                        this.Shelf.Move(this.x, 0.2, this.z);
                    DeltaZ += 0.1;
                }
                else if (Math.Round(DeltaX, 2) > 0)
                {
                    this.Move(this.x + 0.1, this.y, this.z);
                    if (Shelf != null)
                        this.Shelf.Move(this.x, 0.2, this.z);
                    DeltaX -= 0.1;
                }
                else if (Math.Round(DeltaX, 2) < 0)
                {
                    this.Move(this.x - 0.1, this.y, this.z);
                    if (Shelf != null)
                        this.Shelf.Move(this.x, 0.2, this.z);
                    DeltaX += 0.1;
                }
            }
            if (Math.Round(this.x) == 2 && Math.Round(this.z) == 4 && TaskCount() == 0)
               this.RobotBusy = false;
            return base.Update(tick);
        }
        /// <summary>
        /// returned the type in string vorm
        /// </summary>
        /// <returns>sting type</returns>
        public override string getType()
        {
            type = this.type;
      
            return type;
        }
        /// <summary>
        /// Geeft de route door aan de robot.
        /// </summary>
        /// <param name="route">route van de robot</param>
        public void MoveOverPath(List<Node> route)
        {
            this.Route = route;
        }
        /// <summary>
        /// Voegt een taak aan de tasklist van de robot toe
        /// </summary>
        /// <param name="taak">taak die aan de tasklist moet worden toegevoegd</param>
        public void AddTask(IRobotTask taak)
        {
            tasks.Add(taak);
        }
        /// <summary>
        /// Telt alle taken
        /// </summary>
        /// <returns>task count</returns>
        public int TaskCount()
        {
            int aantal = tasks.Count();
            return aantal;
        }
        /// <summary>
        /// Voegt een shelf toe aan de robot
        /// </summary>
        /// <param name="shelf">shelf voor de robot</param>
        public void AddShelf(Shelf shelf)
        {
            this.Shelf = shelf;
        }
        /// <summary>
        /// Haalt de shelf van de robot af
        /// </summary>
        public void RemoveShelf()
        {
            this.Shelf = null;
        }
        /// <summary>
        /// Geeft de shelf toe die bij de robot hoort
        /// </summary>
        /// <returns>shelf</returns>
        public Shelf ReturnShelf()
        {
            return this.Shelf;
        }
        /// <summary>
        /// Kijkt of er een shelf op de robot staat
        /// </summary>
        /// <returns>bool</returns>
        public bool ShelfStatus()
        {
            if (this.Shelf != null)
                return true;
            else
                return false;
        }
    }
}