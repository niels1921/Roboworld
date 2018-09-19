using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<_3DModel> worldObjects = new List<_3DModel>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        private Dijkstra Nodes = new Dijkstra();
        private List<Node> Punten = new List<Node>();

        public World() {
            Robot r1 = CreateRobot(0, 0, 0);
            Lorry l = CreateLorry(0, 0, 0);
            Shelf s = CreateShelf(0, 0, 0);
            Product p = CreateProduct(0, 0, 0);

            AddNodes();

            r1.Move(2.0, 0, 4);
            l.Move(28.0, 0, 4);
            s.Move(28, 0, 28);
            p.Move(2, 0, 28);

            // afstand aanpassen
            Nodes.add_vertex('A', new Dictionary<char, int>() { { 'B', 24 },{ 'E', 4 } });
            Nodes.add_vertex('B', new Dictionary<char, int>() { { 'A', 24 }, { 'C', 24}, { 'G', 18 }, { 'H', 30} });
            Nodes.add_vertex('C', new Dictionary<char, int>() { { 'B', 24 }, { 'D', 26 }});
            Nodes.add_vertex('D', new Dictionary<char, int>() { { 'A', 24 }, { 'C', 26} });
            Nodes.add_vertex('E', new Dictionary<char, int>() { { 'A', 4 }, { 'F', 12 } });
            Nodes.add_vertex('F', new Dictionary<char, int>() { { 'E', 12 }, { 'D', 8 }, { 'H', 12} });
            Nodes.add_vertex('G', new Dictionary<char, int>() { { 'B', 18 }, { 'E', 2 } });
            Nodes.add_vertex('H', new Dictionary<char, int>() { { 'F', 12 }, { 'B', 30 } });
            //randomize deze node zet deze in de list voor de robot die je aanspreekt en laat hem zo deze nodes afwerken
            //match deze waardes met de id van de nodes(id zij nu char misschien toch int houden voor random numbergenerator)
            ///////////////////////////////////////////
            Nodes.shortest_path('A', 'H');
            List<Node> route = new List<Node>();
            r1.Route = route;
            foreach(char x in Nodes.shortest_path('A', 'H'))
            {
                var b = from point in Punten
                        where point.Id == x
                        select point;
                r1.Route.Add(b.Single());
                r1.Route.Reverse();
            }
            foreach (var x in r1.Route)
            {
                Console.WriteLine(x.Id);
            }
            //Nodes.shortest_path('A', 'H').ForEach(x => Console.WriteLine(x));

        }

        private void AddNodes()
        {
            Node a = new Node() { Id = 'A', X = 2, Y = 0, Z = 4 };
            Punten.Add(a);
            Node b = new Node() { Id = 'B', X = 28, Y = 0, Z = 4 };
            Punten.Add(b);
            Node c = new Node() { Id = 'C', X = 28, Y = 0, Z = 28 };
            Punten.Add(c);
            Node d = new Node() { Id = 'D', X = 2, Y = 0, Z = 28 };
            Punten.Add(d);
            Node e = new Node() { Id = 'E', X = 2, Y = 0, Z = 8 };
            Punten.Add(e);
            Node f = new Node() { Id = 'F', X = 2, Y = 0, Z = 20 };
            Punten.Add(f);
            Node g = new Node() { Id = 'G', X = 14, Y = 0, Z = 8 };
            Punten.Add(g);
            Node h = new Node() { Id = 'H', X = 14, Y = 0, Z = 20 };
            Punten.Add(h);
        }



        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot(x,y,z,0,0,0);
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