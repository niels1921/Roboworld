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
            Robot r = CreateRobot(0,0,0);
            Lorry l = CreateLorry(0, 0, 0);
            Shelf s = CreateShelf(0, 0, 0);
            Product p = CreateProduct(0, 0, 0);


            r.Move(4.6, 0, 13);
            l.Move(4.6, 0, 8);
            s.Move(8.6, 0, 13);
            p.Move(8.6, 0, 8);

            Node a = new Node(){Id = 'A', X = 0, Y = 0, Z = 0};
            Punten.Add(a);
            // afstand aanpassen
            Nodes.add_vertex('A', new Dictionary<char, int>() { { 'B', 7 }, { 'C', 8 } });
            Nodes.add_vertex('B', new Dictionary<char, int>() { { 'A', 7 }, { 'F', 2 } });
            Nodes.add_vertex('C', new Dictionary<char, int>() { { 'A', 8 }, { 'F', 6 }, { 'G', 4 } });
            Nodes.add_vertex('D', new Dictionary<char, int>() { { 'F', 8 } });
            Nodes.add_vertex('E', new Dictionary<char, int>() { { 'H', 1 } });
            Nodes.add_vertex('F', new Dictionary<char, int>() { { 'B', 2 }, { 'C', 6 }, { 'D', 8 }, { 'G', 9 }, { 'H', 3 } });
            Nodes.add_vertex('G', new Dictionary<char, int>() { { 'C', 4 }, { 'F', 9 } });
            Nodes.add_vertex('H', new Dictionary<char, int>() { { 'E', 1 }, { 'F', 3 } });
            //randomize deze node zet deze in de list voor de robot die je aanspreekt en laat hem zo deze nodes afwerken
            //match deze waardes met de id van de nodes(id zij nu char misschien toch int houden voor random numbergenerator)
            Nodes.shortest_path('A', 'H').ForEach(x => Console.WriteLine(x));

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