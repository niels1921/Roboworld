using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Robot : _3DModel, IUpdatable
    {
        public List<Node> Route { get; set; }
        private Shelf Shelf;
        private List<IRobotTask> tasks = new List<IRobotTask>();
        Manager RobotManager = new Manager();

        private double DeltaX, DeltaZ;

        public Robot (double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
        {
            this.Move(this.x, this.y, this.z);
        }


        public override bool Update(int tick)
        {
            if(tasks.Count() != 0)
            {
                if (tasks.First().Taskcomplete(this))
                {
                    if(this.Route.Count() != 0)
                        this.Route.RemoveAt(0);
                    tasks.RemoveAt(0);
                    if(tasks.Count != 0)
                    {
                        tasks.First().StartTask(this);
                        //////////////RobotManager.AssignRobot();
                    }
                    
                }
            }


            if (this.Route.Count() >= 0)
            {
                if (this.Route.Count() != 0)
                {
                    if (Math.Round(DeltaX , 1) == 0 && Math.Round(DeltaZ, 1) == 0)
                    {
                        DeltaX = this.Route[0].X - this.x;
                        DeltaZ = this.Route[0].Z - this.z;
                        if (Math.Round(DeltaX, 1) != 0)
                        {
                            if (DeltaX > this.x)
                            {
                                this.Rotate(this.rotationX, this.rotationY + (0.5 * Math.PI), this.rotationZ);
                            }
                            else if (DeltaX < this.x)
                            {
                                this.Rotate(this.rotationX, this.rotationY + (-0.5 * Math.PI), this.rotationZ);
                            }
                        }
                        else if (DeltaZ != 0)
                        {
                            if (this.rotationY > 1.5 && DeltaZ > this.z)
                            {
                                this.Rotate(this.rotationX, this.rotationY + (-0.5 * Math.PI), this.rotationZ);
                            }
                            else if (this.rotationY < 1.5 && DeltaZ < this.z)
                            {
                                this.Rotate(this.rotationX, this.rotationY + (0.5 * Math.PI), this.rotationZ);
                            }
                        }
                        if(this.Route.Count() != 1)
                            this.Route.RemoveAt(0);
                    }
                }

                if (Math.Round(DeltaZ, 2) > 0)
                {
                    this.Move(this.x, this.y, this.z + 0.1);
                    if(Shelf != null)
                        this.Shelf.Move(this.x, this.y, this.z);
                    DeltaZ -= 0.1;
                }
                else if (Math.Round(DeltaZ, 2) < 0)
                {
                    this.Move(this.x, this.y, this.z - 0.1);
                    if (Shelf != null)
                        this.Shelf.Move(this.x, this.y, this.z);
                    DeltaZ += 0.1;
                }
                else if (Math.Round(DeltaX, 2) > 0)
                {
                    this.Move(this.x + 0.1, this.y, this.z);
                    if (Shelf != null)
                        this.Shelf.Move(this.x, this.y, this.z);
                    DeltaX -= 0.1;
                }
                else if (Math.Round(DeltaX, 2) < 0)
                {
                    this.Move(this.x - 0.1, this.y, this.z);
                    if (Shelf != null)
                        this.Shelf.Move(this.x, this.y, this.z);
                    DeltaX += 0.1;
                }
            }

            return base.Update(tick);
        }

        public override string getType()
        {
            type = this.type;
      
            return type;
        }

        public void MoveOverPath(List<Node> route)
        {
            this.Route = route;
        }

        public void AddTask(IRobotTask taak)
        {
            tasks.Add(taak);
        }

        public int TaskCount()
        {
            int aantal = tasks.Count();
            return aantal;
        }

        public void AddShelf(Shelf shelf)
        {
            this.Shelf = shelf;
        }

        public void RemoveShelf()
        {
            this.Shelf = null;
        }
    }
}