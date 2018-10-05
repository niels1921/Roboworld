using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Models;

namespace Models
{
    public class World : IObservable<Command>, IUpdatable
    {
        /// <summary>
        /// link met de worldmanager wordt aangemaakt
        /// </summary>
        private Manager WorldManager = new Manager();
        /// <summary>
        /// lijst van alle 3d modellen in de wereld
        /// </summary>
        private List<_3DModel> worldObjects = new List<_3DModel>();
        /// <summary>
        /// lijst van alle observers 
        /// </summary>
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        /// <summary>
        /// de vrachtwagen in de wereld
        /// </summary>
        private Lorry vrachtwagen;
        /// <summary>
        /// bool die aangeeft of er moet worden bijgevuld
        /// </summary>
        private bool StorageEmpty = false;
        /// <summary>
        /// methode die de wereld opzet
        /// </summary>
        public World()
        {
            WorldManager.AddNodes();
            Robot r0 = CreateRobot(0, 0, 0);
            Robot r1 = CreateRobot(0, 0, 0);
            Robot r2 = CreateRobot(0, 0, 0);
            Robot r3 = CreateRobot(0, 0, 0);
            vrachtwagen = CreateLorry(0, 0, 0);

            r0.Move(2, 2, 1);
            r1.Move(2, 2, 2);
            r2.Move(2, 2, 3);
            r3.Move(2, 2, 4);

            vrachtwagen.Move(0, 0, -2);

            foreach (var punt in WorldManager.points())
            {
                if (punt.Id.Length == 1)
                {
                    Shelf s = CreateShelf(0, 0, 0);
                    punt.Shelf = s;
                    WorldManager.AddShelf(punt);
                    s.Move(punt.X, 0, punt.Z);
                    punt.ShelfStatus = true;
                }
                if (punt.Id.Length == 4)
                    punt.ShelfStatus = false;
            }
        }
        /// <summary>
        /// maakt de robot aan
        /// </summary>
        /// <param name="x">de x waarde waar de robot gezet moet worden</param>
        /// <param name="y">de y waarde waar de robot gezet moet worden</param>
        /// <param name="z">de z waarde waar de robot gezet moet worden</param>
        /// <returns>robot</returns>
        private Robot CreateRobot(double x, double y, double z)
        {
            Robot r = new Robot(x, y, z, 0, 0, 0);
            List<Node> Route = new List<Node>();
            r.Route = Route;
            worldObjects.Add(r);
            WorldManager.Addrobot(r);
            return r;
        }
        /// <summary>
        /// maakt de producten aan.........................................
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private Product CreateProduct(double x, double y, double z)
        {
            Product p = new Product(x, y, z, 0, 0, 0);
            worldObjects.Add(p);
            return p;
        }
        /// <summary>
        /// maakt de vrachtwagen aan
        /// </summary>
        /// <param name="x">de x waarde waar de vrachtwagen gezet moet worden</param>
        /// <param name="y">de y waarde waar de vrachtwagen gezet moet worden</param>
        /// <param name="z">de z waarde waar de vrachtwagen gezet moet worden</param>
        /// <returns></returns>
        private Lorry CreateLorry(double x, double y, double z)
        {
            Lorry l = new Lorry(x, y, z, 0, 0, 0);
            worldObjects.Add(l);
            WorldManager.AddTruck(l);
            return l;
        }
        /// <summary>
        /// maakt de shelf aan
        /// </summary>
        /// <param name="x">de x waarde waar de shelf gezet moet worden</param>
        /// <param name="y">de y waarde waar de shelf gezet moet worden</param>
        /// <param name="z">de z waarde waar de shelf gezet moet worden</param>
        /// <returns></returns>
        private Shelf CreateShelf(double x, double y, double z)
        {
            Shelf s = new Shelf(x, y, z, 0, 0, 0);
            worldObjects.Add(s);
            return s;
        }

        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c)
        {
            for (int i = 0; i < this.observers.Count; i++)
            {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs)
        {
            foreach (_3DModel m3d in worldObjects)
            {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }
        /// <summary>
        /// update alle 3d modellen in de wereld
        /// ook hier wordt de route voor vrachtwagen bepaald
        /// als laatste wordt gekeken of het magazijn moet worden bijgevuld 
        /// </summary>
        /// <param name="tick">aantal beeld updates per minuut</param>
        /// <returns>bool</returns>
        public bool Update(int tick)
        {
            for (int i = 0; i < worldObjects.Count; i++)
            {
                _3DModel u = worldObjects[i];

                if (vrachtwagen.GetRoute().Count == 0 && vrachtwagen.x < 16)
                {
                    vrachtwagen.VrachtwagenRoute(WorldManager.ReturnNodes().shortest_path("VA", "VB"));
                }
                if (Math.Round(vrachtwagen.x, 1) == 20)
                {
                    if (WorldManager.AvailableRobots() == 4)
                    {
                        if (WorldManager.Shelfs().Count() == 12)
                            WorldManager.AssignRobot();
                        else if (WorldManager.Shelfs().Count() == 8)
                        {
                            StorageEmpty = true;
                            WorldManager.SetFillStorage(true);
                            WorldManager.FillStorage();
                        }
                    }
                    else if (WorldManager.DockNodes().Count() == 4 && WorldManager.GetFillStorage() == false)
                    {
                        vrachtwagen.VrachtwagenRoute(WorldManager.ReturnNodes().shortest_path("VB", "VC"));
                        foreach (Node n in WorldManager.points())
                        {
                            if (n.Id.Length == 4)
                            {
                                n.Shelf.Move(0, 1000, 0);

                                n.ShelfStatus = false;
                            }
                        }
                    }

                }
                if (vrachtwagen.x > 39)
                {  
                    StorageEmpty = false;
                    vrachtwagen.Move(0, 0, -2);
                }

                if (u is IUpdatable)
                {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

                    if (needsCommand)
                    {
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