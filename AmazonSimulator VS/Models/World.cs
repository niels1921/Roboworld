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
        private List<Node> Punten = new List<Node>();


        public World() {
            //Robot r0 = CreateRobot(0, 0, 0);
            Robot r1 = CreateRobot(0, 0, 0);
            
            //Robot r2 = CreateRobot(0, 0, 0);
            //Robot r3 = CreateRobot(0, 0, 0);

            Lorry l = CreateLorry(0, 0, 0);
            Shelf s = CreateShelf(0, 0, 0);
            Product p = CreateProduct(0, 0, 0);

            r1.Move(2.0, 0, 4);
            l.Move(28.0, 0, 4);
            s.Move(28, 0, 28);
            p.Move(2, 0, 28);

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

            Nodes.Add_Nodes('A', new Dictionary<char, Node>() { { 'A', a }, { 'B', b }, { 'E', e } });
            Nodes.Add_Nodes('B', new Dictionary<char, Node>() { { 'B', b }, { 'A', a }, { 'C', c }, { 'G', g }, { 'H', h } });
            Nodes.Add_Nodes('C', new Dictionary<char, Node>() { { 'C', c }, { 'B', b }, { 'D', d } });
            Nodes.Add_Nodes('D', new Dictionary<char, Node>() { { 'D', d }, { 'A', a }, { 'C', c } });
            Nodes.Add_Nodes('E', new Dictionary<char, Node>() { { 'E', e }, { 'A', a }, { 'F', f } });
            Nodes.Add_Nodes('F', new Dictionary<char, Node>() { { 'F', f }, { 'D', d }, { 'E', e }, { 'H', h } });
            Nodes.Add_Nodes('G', new Dictionary<char, Node>() { { 'G', g }, { 'B', b }, { 'E', e } });
            Nodes.Add_Nodes('H', new Dictionary<char, Node>() { { 'H', h }, { 'B', b }, { 'F', f } });
            Nodes.CalculateDistance();

            //randomize deze node zet deze in de list voor de robot die je aanspreekt en laat hem zo deze nodes afwerken
            //match deze waardes met de id van de nodes(id zij nu char misschien toch int houden voor random numbergenerator)
            ///////////////////////////////////////////
            //Nodes.shortest_path('A', 'H');
            List<Node> reverseRoute = new List<Node>();

            foreach(char x in Nodes.shortest_path('A', 'H'))
            {
                Console.WriteLine(x);
                var punt = from point in Punten
                           where point.Id == x
                           select point;
                reverseRoute.Add(punt.Single());
            }
 
            foreach (Node i in reverseRoute.Reverse<Node>())
            {
                r1.Route.Add(i);
            }
        }

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