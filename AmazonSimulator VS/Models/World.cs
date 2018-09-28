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
        private Robot robot1;
        private List<Node> Punten = Manager.Punten;

        public World() {
            WorldManager.AddNodes();
            //Robot r0 = CreateRobot(0, 0, 0);

            robot1 = CreateRobot(0, 0, 0);
            //Robot r2 = CreateRobot(0, 0, 0);
            //Robot r3 = CreateRobot(0, 0, 0);

            vrachtwagen = CreateLorry(0, 0, 0);
            
            Product p = CreateProduct(0, 0, 0);

            robot1.Move(2.0, 0, 4);
            vrachtwagen.Move(0, 0, -2);

            p.Move(2, 0, 28);

            foreach(var punt in Punten)
            {
                if(punt.Id.Length == 1)
                {
                    Shelf s = CreateShelf(0, 0, 0);
                    s.Move(punt.X, 2.2, punt.Z);
                }
            }
            //randomize deze node zet deze in de list voor de robot die je aanspreekt en laat hem zo deze nodes afwerken
            //match deze waardes met de id van de nodes(id zij nu char misschien toch int houden voor random numbergenerator)

            
            

            //if(Robot.ended == true)
            //{
            //    foreach (char x in Nodes.shortest_path('H', 'B'))
            //    {
            //        Console.WriteLine(x);
            //        var punt = from point in Punten
            //                   where point.Id == x
            //                   select point;
            //        r1.Route.Add(punt.Single());
            //    }
            //}
        }

        //private void AssignRoute()
        //{
        //    foreach(var worldObject in worldObjects)
        //    {
        //        if(worldObject.getType() == "robot")
        //        {
        //            Random rnd = new Random();//whileloop omheen met voorwaarde z niet dit of dat en shelf is niet empty
        //            int random = rnd.Next(0, Punten.Count() + 1);
        //            char punt1 = Punten[random].Id;
        //            if (route.Count() == 0 && Robot.shelfStatus == false)//&& robot bevat shelf als die 0 is dan heenreis als die teruggaat is shelf vol
        //            {
        //                foreach (char x in Nodes.shortest_path('A', punt1))
        //                {
        //                    Console.WriteLine(x);
        //                    var punt = from point in Punten
        //                               where point.Id == x
        //                               select point;
        //                    //r1.Route.Add(punt.Single());
        //                }
        //            }
        //            else if(route.Count() == 0 && Robot.shelfStatus == true)
        //            {//////////////////////////////////////////////////////////////////////////////////////////////////////
        //                foreach (char x in Nodes.shortest_path(punt1, 'B'))
        //                {
        //                    Console.WriteLine(x);
        //                    var punt = from point in Punten
        //                               where point.Id == x
        //                               select point;
        //                    //r1.Route.Add(punt.Single());
        //                }
        //            }
        //        }
        //    }
        //}

        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot(x,y,z,0,0,0);
            List<Node> route = new List<Node>();
            r.Route = route;
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
                    foreach (string l in WorldManager.returnNodes().shortest_path("VA", "VB"))
                    {
                        var punt = from point in Punten
                                   where point.Id == l
                                   select point;
                        vrachtwagen.AddRoute(punt.Single());
                    }
                    
                }
                if(Math.Round(vrachtwagen.x, 1) == 20.5)
                {
                    WorldManager.AssignRobot();
                }
                //if (Math.Round(vrachtwagen.x) == 20 && robot1.Route.Count() == 0)
                //{
                //    foreach (string x in Nodes.shortest_path("A", "H"))
                //    {
                //        Console.WriteLine(x);
                //        var punt = from point in Punten
                //                   where point.Id == x
                //                   select point;
                //        robot1.Route.Add(punt.Single());
                //    }
                //}

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