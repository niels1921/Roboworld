using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Models;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<_3DModel> worldObjects = new List<_3DModel>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        private Dijkstra Nodes = new Dijkstra();
        private Lorry vrachtwagen;
        public static List<Node> Punten = new List<Node>()
        {
            new Node() { Id = "A", X = 2, Y = 0, Z = 4 },
            new Node() { Id = "B", X = 28, Y = 0, Z = 4 },
            new Node() { Id = "C", X = 28, Y = 0, Z = 28 },
            new Node() { Id = "D", X = 2, Y = 0, Z = 28 },
            new Node() { Id = "E", X = 2, Y = 0, Z = 8 },
            new Node() { Id = "F", X = 2, Y = 0, Z = 20 },
            new Node() { Id = "G", X = 14, Y = 0, Z = 8 },
            new Node() { Id = "H", X = 14, Y = 0, Z = 20 },
            new Node() { Id = "VA", X = 0, Y = 0, Z = -6},
            new Node() { Id = "VB", X = 20.5, Y = 0, Z = -6},
            new Node() { Id = "VC", X = 8, Y = 0, Z = -6}
        };

        public World() {
            //Robot r0 = CreateRobot(0, 0, 0);
            Robot r1 = CreateRobot(0, 0, 0);
            
            //Robot r2 = CreateRobot(0, 0, 0);
            //Robot r3 = CreateRobot(0, 0, 0);

            vrachtwagen = CreateLorry(0, 0, 0);
            Shelf s = CreateShelf(0, 0, 0);
            Product p = CreateProduct(0, 0, 0);

            r1.Move(2.0, 0, 4);
            vrachtwagen.Move(0, 0, -2);
            s.Move(28, 0, 28);
            p.Move(2, 0, 28);

            Nodes.Add_Nodes("A", new Dictionary<string, Node>() { { "A", Punten[0] }, { "B", Punten[1] }, { "E", Punten[4] } });
            Nodes.Add_Nodes("B", new Dictionary<string, Node>() { { "B", Punten[1] }, { "A", Punten[0] }, { "C", Punten[2] }, { "G", Punten[6] }, { "H", Punten[7] } });
            Nodes.Add_Nodes("C", new Dictionary<string, Node>() { { "C", Punten[2] }, { "B", Punten[1] }, { "D", Punten[3] } });
            Nodes.Add_Nodes("D", new Dictionary<string, Node>() { { "D", Punten[3] }, { "A", Punten[0] }, { "C", Punten[2] } });
            Nodes.Add_Nodes("E", new Dictionary<string, Node>() { { "E", Punten[4] }, { "A", Punten[0] }, { "F", Punten[5] } });
            Nodes.Add_Nodes("F", new Dictionary<string, Node>() { { "F", Punten[5] }, { "D", Punten[3] }, { "E", Punten[4] }, { "H", Punten[7] } });
            Nodes.Add_Nodes("G", new Dictionary<string, Node>() { { "G", Punten[6] }, { "B", Punten[1] }, { "E", Punten[4] } });
            Nodes.Add_Nodes("H", new Dictionary<string, Node>() { { "H", Punten[7] }, { "B", Punten[1] }, { "F", Punten[5] } });
            Nodes.Add_Nodes("VA", new Dictionary<string, Node>() { { "VA", Punten[8] }, { "VB", Punten[9] } });
            Nodes.Add_Nodes("VB", new Dictionary<string, Node>() { { "VB", Punten[9] }, { "VC", Punten[10] } });
            Nodes.Add_Nodes("VC", new Dictionary<string, Node>() { { "VC", Punten[10] }, { "VB", Punten[9] } });
            Nodes.CalculateDistance();

            //randomize deze node zet deze in de list voor de robot die je aanspreekt en laat hem zo deze nodes afwerken
            //match deze waardes met de id van de nodes(id zij nu char misschien toch int houden voor random numbergenerator)

            foreach(string x in Nodes.shortest_path("A", "H"))
            {
                Console.WriteLine(x);
                var punt = from point in Punten
                           where point.Id == x
                           select point;
                r1.Route.Add(punt.Single());
            }
            

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

                if (vrachtwagen.GetRoute().Count == 0)
                {
                    foreach (string l in Nodes.shortest_path("VA", "VB"))
                    {
                        var punt = from point in Punten
                                   where point.Id == l
                                   select point;
                        vrachtwagen.AddRoute(punt.Single());
                    }
                }

                if(u is IUpdatable) {
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