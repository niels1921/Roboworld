using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Models;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private Manager WorldManager = new Manager();
        private List<_3DModel> worldObjects = new List<_3DModel>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        private Lorry vrachtwagen;
        private List<Node> Punten = Manager.Punten;

        public World() {
            WorldManager.AddNodes();
            //Robot r0 = CreateRobot(0, 0, 0);          
            //Robot r1 = CreateRobot(0, 0, 0);
            //Robot r2 = CreateRobot(0, 0, 0);
            Robot r3 = CreateRobot(0, 0, 0);
            vrachtwagen = CreateLorry(0, 0, 0);
            //Product p = CreateProduct(0, 0, 0);

            //r0.Move(2, 0, 1);
            //r1.Move(2, 0, 2);
            //r2.Move(2, 0, 3);
            r3.Move(2, 0, 4);
            
            vrachtwagen.Move(0, 0, -2);

            //p.Move(2, 0, 28);

            foreach(var punt in Punten)
            {
                if(punt.Id.Length == 1)
                {
                    Shelf s = CreateShelf(0, 0, 0);
                    punt.Shelf = s;
                    s.Move(punt.X, 0, punt.Z);
                }
            }
        }

        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot(x,y,z,0,0,0);
            List<Node> Route = new List<Node>();
            r.Route = Route;
            worldObjects.Add(r);
            WorldManager.Addrobot(r);
            return r;
        }

        private Product CreateProduct(double x, double y, double z)
        {
            Product p = new Product(x, y, z, 0, 0, 0);
            worldObjects.Add(p);
            return p;
        }

        private Lorry CreateLorry(double x, double y, double z)
        {
            Lorry l = new Lorry(x, y, z, 0, 0, 0);
            worldObjects.Add(l);
            WorldManager.AddTruck(l);
            return l;
        }

        private Shelf CreateShelf(double x, double y, double z)
        {
            Shelf s = new Shelf(x, y, z, 0, 0, 0);
            worldObjects.Add(s);
            WorldManager.AddShelf(s);
            return s;
        }

        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer)) {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c) {
            for(int i = 0; i < this.observers.Count; i++) {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs) {
            foreach(_3DModel m3d in worldObjects) {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }

        public bool Update(int tick)
        {
            for(int i = 0; i < worldObjects.Count; i++) {
                _3DModel u = worldObjects[i];

                if (vrachtwagen.GetRoute().Count == 0 && vrachtwagen.x < 16)
                {
                    foreach (string l in WorldManager.ReturnNodes().shortest_path("VA", "VB"))
                    {
                        var punt = from point in Punten
                                   where point.Id == l
                                   select point;
                        vrachtwagen.AddRoute(punt.Single());
                    }                   
                }
                if(Math.Round(vrachtwagen.x, 1) == 20)
                {
                    WorldManager.AssignRobot();
                    
                }
                if (vrachtwagen.x > 35)               
                    vrachtwagen.Move(0, 0, -2);
                
                    
                if (u is IUpdatable) {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

                    if(needsCommand) {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
            }

            return true;
        }
    }

    internal class Unsubscriber<Command> : IDisposable
    {
        private List<IObserver<Command>> _observers;
        private IObserver<Command> _observer;

        internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose() 
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}