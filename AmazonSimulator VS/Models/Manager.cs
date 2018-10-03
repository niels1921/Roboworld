using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controllers;

namespace Models
{
    public class Manager
    {
        private List<Robot> RobotList = new List<Robot>();
        private List<Shelf> ShelfList = new List<Shelf>();
        private List<Node> AvailableShelfs = new List<Node>();
        private List<Node> AvailableDockNodes = new List<Node>();
        private Lorry Truck;
        private Dijkstra Nodes = new Dijkstra();
        private bool StorageEmpty = false;
        public static List<Node> Punten = new List<Node>()
        {
            //Hoek nodes
            new Node() { Id = "HA", X = 2, Y = 0, Z = 4 }, //0
            new Node() { Id = "HB", X = 28, Y = 0, Z = 4 }, //1
            new Node() { Id = "HC", X = 28, Y = 0, Z = 28 }, //2
            new Node() { Id = "HD", X = 2, Y = 0, Z = 28 }, //3
            //Main path nodes
            new Node() { Id = "PA", X = 2, Y = 0, Z = 8 }, //4
            new Node() { Id = "PB", X = 2, Y = 0, Z = 20 }, //5
            new Node() { Id = "PC", X = 28, Y = 0, Z = 20 }, //6
            new Node() { Id = "PD", X = 28, Y = 0, Z = 8 }, //7
            //path nodes connected aan shelfs van path A
            new Node() { Id = "PAA", X = 5, Y = 0, Z = 8 }, //8
            new Node() { Id = "PAB", X = 7.5, Y = 0, Z = 8 }, //9
            new Node() { Id = "PAC", X = 10, Y = 0, Z = 8 }, //10
            new Node() { Id = "PAD", X = 13.5, Y = 0, Z = 8 }, //11
            new Node() { Id = "PAE", X = 16, Y = 0, Z = 8 }, //12
            new Node() { Id = "PAF", X = 18.5, Y = 0, Z = 8 }, //13
            //path nodes connected aan shelfs van path B
            new Node() { Id = "PBG", X = 5, Y = 0, Z = 20 }, //14
            new Node() { Id = "PBH", X = 7.5, Y = 0, Z = 20 }, //15
            new Node() { Id = "PBI", X = 10, Y = 0, Z = 20 }, //16
            new Node() { Id = "PBJ", X = 13.5, Y = 0, Z = 20 }, //17
            new Node() { Id = "PBK", X = 16, Y = 0, Z = 20 }, //18
            new Node() { Id = "PBL", X = 18.5, Y = 0, Z = 20 }, //19
            //Shelf nodes path A
            new Node() { Id = "A", X = 5, Y = 0, Z = 10 }, //20
            new Node() { Id = "B", X = 7.5, Y = 0, Z = 10 }, //21
            new Node() { Id = "C", X = 10, Y = 0, Z = 10 }, //22
            new Node() { Id = "D", X = 13.5, Y = 0, Z = 10 }, //23
            new Node() { Id = "E", X = 16, Y = 0, Z = 10 }, //24
            new Node() { Id = "F", X = 18.5, Y = 0, Z = 10 }, //25
            //Shelf nodes path B
            new Node() { Id = "G", X = 5, Y = 0, Z = 22 }, //26
            new Node() { Id = "H", X = 7.5, Y = 0, Z = 22 }, //27
            new Node() { Id = "I", X = 10, Y = 0, Z = 22 }, //28
            new Node() { Id = "J", X = 13.5, Y = 0, Z = 22 }, //29
            new Node() { Id = "K", X = 16, Y = 0, Z = 22 }, //30
            new Node() { Id = "L", X = 18.5, Y = 0, Z = 22 }, //31
            //Vrachtwagen nodes
            new Node() { Id = "VA", X = 0, Y = 0, Z = -2}, //32
            new Node() { Id = "VB", X = 20.5, Y = 0, Z = -2}, //33
            new Node() { Id = "VC", X = 36, Y = 0, Z = -2}, //34
            //loading dock nodes
            new Node() { Id = "LDA",  X = 23, Y = 0, Z = 3}, //35
            new Node() { Id = "LDB",  X = 18, Y = 0, Z = 3}, //36
            new Node() { Id = "LDC",  X = 12, Y = 0, Z = 3}, //37
            new Node() { Id = "LDD",  X = 7,  Y = 0, Z = 3}, //38

            new Node() { Id = "LDSA", X = 23, Y = 0, Z = 2}, //39
            new Node() { Id = "LDSB", X = 18, Y = 0, Z = 2}, //40
            new Node() { Id = "LDSC", X = 12, Y = 0, Z = 2}, //41
            new Node() { Id = "LDSD", X = 7,  Y = 0, Z = 2} //42
        };

        public void AddNodes()
        {
            //hoeken
            Nodes.Add_Nodes("HA", new Dictionary<string, Node>() { { "HA", Punten[0] }, { "LDD", Punten[38] }, { "PA", Punten[4] } });
            Nodes.Add_Nodes("HB", new Dictionary<string, Node>() { { "HB", Punten[1] }, { "LDA", Punten[35] }, { "PD", Punten[7] } });
            Nodes.Add_Nodes("HC", new Dictionary<string, Node>() { { "HC", Punten[2] }, { "PC", Punten[6] }, { "HD", Punten[3] } });
            Nodes.Add_Nodes("HD", new Dictionary<string, Node>() { { "HD", Punten[3] }, { "PB", Punten[5] }, { "HC", Punten[2] } });
            //loading dock
            Nodes.Add_Nodes("LDA", new Dictionary<string, Node>() { { "LDA", Punten[35] }, { "HB", Punten[1]   }, { "LDB", Punten[36] }, { "LDSA", Punten[39] } });
            Nodes.Add_Nodes("LDB", new Dictionary<string, Node>() { { "LDB", Punten[36] }, { "LDA", Punten[35] }, { "LDC", Punten[37] }, { "LDSB", Punten[40] }, });
            Nodes.Add_Nodes("LDC", new Dictionary<string, Node>() { { "LDC", Punten[37] }, { "LDB", Punten[36] }, { "LDD", Punten[38] }, { "LDSC", Punten[41] }, });
            Nodes.Add_Nodes("LDD", new Dictionary<string, Node>() { { "LDD", Punten[38] }, { "LDC", Punten[37] }, { "HA", Punten[0]   }, { "LDSD", Punten[42] }, });
            //shelfs loading dock
            Nodes.Add_Nodes("LDSA", new Dictionary<string, Node>() { { "LDSA", Punten[39] }, { "LDA", Punten[35] } });
            Nodes.Add_Nodes("LDSB", new Dictionary<string, Node>() { { "LDSB", Punten[40] }, { "LDB", Punten[36] } });
            Nodes.Add_Nodes("LDSC", new Dictionary<string, Node>() { { "LDSC", Punten[41] }, { "LDC", Punten[37] } });
            Nodes.Add_Nodes("LDSD", new Dictionary<string, Node>() { { "LDSD", Punten[42] }, { "LDD", Punten[38] } });
            //main path
            Nodes.Add_Nodes("PA", new Dictionary<string, Node>() { { "PA", Punten[4] }, { "HA", Punten[0] }, { "PB", Punten[5] }, { "PAA", Punten[8] } });
            Nodes.Add_Nodes("PB", new Dictionary<string, Node>() { { "PB", Punten[5] }, { "HD", Punten[3] }, { "PA", Punten[4] }, { "PBG", Punten[14] } });
            Nodes.Add_Nodes("PC", new Dictionary<string, Node>() { { "PC", Punten[6] }, { "HC", Punten[2] }, { "PD", Punten[7] }, { "PBL", Punten[19] } });
            Nodes.Add_Nodes("PD", new Dictionary<string, Node>() { { "PD", Punten[7] }, { "HB", Punten[1] }, { "PC", Punten[6] }, { "PAF", Punten[13] } });
            //path A nodes
            Nodes.Add_Nodes("PAA", new Dictionary<string, Node>() { { "PAA", Punten[8] }, { "PA", Punten[4] }, { "A", Punten[20] }, { "PAB", Punten[9] } });
            Nodes.Add_Nodes("PAB", new Dictionary<string, Node>() { { "PAB", Punten[9] }, { "PAA", Punten[8] }, { "B", Punten[21] }, { "PAC", Punten[10] } });
            Nodes.Add_Nodes("PAC", new Dictionary<string, Node>() { { "PAC", Punten[10] }, { "PAB", Punten[9] }, { "C", Punten[22] }, { "PAD", Punten[11] } });
            Nodes.Add_Nodes("PAD", new Dictionary<string, Node>() { { "PAD", Punten[11] }, { "PAC", Punten[10] }, { "D", Punten[23] }, { "PAE", Punten[12] } });
            Nodes.Add_Nodes("PAE", new Dictionary<string, Node>() { { "PAE", Punten[12] }, { "PAD", Punten[11] }, { "E", Punten[24] }, { "PAF", Punten[13] } });
            Nodes.Add_Nodes("PAF", new Dictionary<string, Node>() { { "PAF", Punten[13] }, { "PAE", Punten[12] }, { "F", Punten[25] }, { "PD", Punten[7] } });
            //path B nodes
            Nodes.Add_Nodes("PBG", new Dictionary<string, Node>() { { "PBG", Punten[14] }, { "PB", Punten[5] }, { "G", Punten[26] }, { "PBH", Punten[15] } });
            Nodes.Add_Nodes("PBH", new Dictionary<string, Node>() { { "PBH", Punten[15] }, { "PBG", Punten[14] }, { "H", Punten[27] }, { "PBI", Punten[16] } });
            Nodes.Add_Nodes("PBI", new Dictionary<string, Node>() { { "PBI", Punten[16] }, { "PBH", Punten[15] }, { "I", Punten[28] }, { "PBJ", Punten[17] } });
            Nodes.Add_Nodes("PBJ", new Dictionary<string, Node>() { { "PBJ", Punten[17] }, { "PBK", Punten[18] }, { "J", Punten[29] }, { "PBI", Punten[16] } });
            Nodes.Add_Nodes("PBK", new Dictionary<string, Node>() { { "PBK", Punten[18] }, { "PBJ", Punten[17] }, { "K", Punten[30] }, { "PBL", Punten[19] } });            
            Nodes.Add_Nodes("PBL", new Dictionary<string, Node>() { { "PBL", Punten[19] }, { "PBK", Punten[18] }, { "L", Punten[31] }, { "PC", Punten[6] } });
            //Shelf nodes A path
            Nodes.Add_Nodes("A", new Dictionary<string, Node>() { { "A", Punten[20] }, { "PAA", Punten[8] } });
            Nodes.Add_Nodes("B", new Dictionary<string, Node>() { { "B", Punten[21] }, { "PAB", Punten[9] } });
            Nodes.Add_Nodes("C", new Dictionary<string, Node>() { { "C", Punten[22] }, { "PAC", Punten[10] } });
            Nodes.Add_Nodes("D", new Dictionary<string, Node>() { { "D", Punten[23] }, { "PAD", Punten[11] } });
            Nodes.Add_Nodes("E", new Dictionary<string, Node>() { { "E", Punten[24] }, { "PAE", Punten[12] } });
            Nodes.Add_Nodes("F", new Dictionary<string, Node>() { { "F", Punten[25] }, { "PAF", Punten[13] } });
            //Shelf nodes B path
            Nodes.Add_Nodes("G", new Dictionary<string, Node>() { { "G", Punten[26] }, { "PBG", Punten[14] } });
            Nodes.Add_Nodes("H", new Dictionary<string, Node>() { { "H", Punten[27] }, { "PBH", Punten[15] } });
            Nodes.Add_Nodes("I", new Dictionary<string, Node>() { { "I", Punten[28] }, { "PBI", Punten[16] } });
            Nodes.Add_Nodes("J", new Dictionary<string, Node>() { { "J", Punten[29] }, { "PBJ", Punten[17] } });
            Nodes.Add_Nodes("K", new Dictionary<string, Node>() { { "K", Punten[30] }, { "PBK", Punten[18] } });
            Nodes.Add_Nodes("L", new Dictionary<string, Node>() { { "L", Punten[31] }, { "PBL", Punten[19] } });
            //Vrachtwagen nodes
            Nodes.Add_Nodes("VA", new Dictionary<string, Node>() { { "VA", Punten[8] }, { "VB", Punten[9] } });
            Nodes.Add_Nodes("VB", new Dictionary<string, Node>() { { "VB", Punten[9] }, { "VC", Punten[10] } });
            Nodes.Add_Nodes("VC", new Dictionary<string, Node>() { { "VC", Punten[10] }, { "VB", Punten[9] } });
            Nodes.CalculateDistance();
        }

        public Dijkstra ReturnNodes()
        {
            return Nodes;
        }

        public void CheckForAvailableShelfNodes()
        {
            var AvailableNodes = from node in Punten
                            where node.Shelf != null && node.Id.Length == 1
                            select node;
            
            AvailableShelfs = AvailableNodes.ToList();            
        }

        public void CheckForAvailableDockNodes()
        {
            var AvailableDock = from node in Punten
                                where node.Shelf == null && node.Id.Length == 4
                                select node;
            AvailableDockNodes = AvailableDock.ToList();
        }

        public void AssignRobot()
        {
            CheckForAvailableShelfNodes();
            CheckForAvailableDockNodes();
            if (AvailableShelfs.Count() == 8)
            {
                StorageEmpty = true;
                FillStorage();
            }
            else
            {
                if (AvailableDockNodes.Count() == 0)
                {
                    foreach (string x in Nodes.shortest_path("VB", "VC"))
                    {
                        var punt = from point in Punten
                                   where point.Id == x
                                   select point;
                        Truck.AddRoute(punt.Single());
                    }
                    foreach (Node n in Punten)
                    {
                        if (n.Id.Length == 4)
                        {
                            n.Shelf.Move(0, 1000, 0);
                            n.Shelf = null;
                        }
                    }
                }
                else
                {
                    int i = 0;
                    foreach (Robot r in RobotList)
                    {
                        if (r.TaskCount() == 0 && i < 4 && AvailableDockNodes.Count() == 4)
                        {
                            CheckForAvailableShelfNodes();

                            List<Node> RobotRouteHeenweg = new List<Node>();
                            List<Node> RobotRouteTerugweg = new List<Node>();
                            List<Node> RobotRouteStartPositie = new List<Node>();
                            List<Node> RobotStoreShelf = new List<Node>();
                            Random rnd = new Random();
                            int random = rnd.Next(0, AvailableShelfs.Count() - 1);
                            Console.WriteLine();
                            Node punt1 = AvailableShelfs[random];
                            foreach (string x in Nodes.shortest_path("HA", punt1.Id))
                            {
                                Console.WriteLine(x);
                                var punt = from point in Punten
                                           where point.Id == x
                                           select point;
                                RobotRouteHeenweg.Add(punt.Single());
                            }
                            RobotMove move = new RobotMove(RobotRouteHeenweg);
                            r.AddTask(move);

                            RobotPickUp pickup = new RobotPickUp(punt1.Shelf, punt1, AvailableDockNodes);
                            r.AddTask(pickup);
                            Console.WriteLine();
                            foreach (string x in Nodes.shortest_path(punt1.Id, "HB"))
                            {
                                Console.WriteLine(x);
                                var punt = from point in Punten
                                           where point.Id == x
                                           select point;
                                RobotRouteTerugweg.Add(punt.Single());
                            }
                            RobotMove terugweg = new RobotMove(RobotRouteTerugweg);
                            r.AddTask(terugweg);

                            foreach (string x in Nodes.shortest_path("HB", AvailableDockNodes[i].Id))
                            {
                                Console.WriteLine(x);
                                var punt = from point in Punten
                                           where point.Id == x
                                           select point;
                                RobotStoreShelf.Add(punt.Single());
                            }
                            RobotMove storeshelf = new RobotMove(RobotStoreShelf);
                            r.AddTask(storeshelf);

                            RobotPickUp dropdown = new RobotPickUp(punt1.Shelf, punt1, AvailableDockNodes);
                            r.AddTask(dropdown);
                            Console.WriteLine();
                            foreach (string x in Nodes.shortest_path(AvailableDockNodes[i].Id, "HA"))
                            {
                                Console.WriteLine(x);
                                var punt = from point in Punten
                                           where point.Id == x
                                           select point;
                                RobotRouteStartPositie.Add(punt.Single());
                            }
                            RobotMove startpositie = new RobotMove(RobotRouteStartPositie);
                            r.AddTask(startpositie);
                            move.StartTask(r);
                            AvailableDockNodes.Count();
                            i++;
                        }
                    }
                }
            }
        }

        public void FillStorage()
        {
            //doe iets
        }

        public void Addrobot(Robot robot)
        {
            RobotList.Add(robot);
        }

        public void AddShelf(Shelf shelf)
        {
            ShelfList.Add(shelf);
        }

        public List<Robot> Robots()
        {
            return RobotList;
        }

        public List<Shelf> Shelfs()
        {
            return ShelfList;
        }

        public void AddTruck(Lorry l)
        {
            Truck = l;
        }
        public bool GetStorageStatus()
        {
            if (StorageEmpty == false)
                return false;
            else
                return true;
        }
    }
}
