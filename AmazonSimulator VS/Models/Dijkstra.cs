using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Dijkstra
    {
        /// <summary>
        /// Dictionary met alle gelinkte nodes
        /// </summary>
        Dictionary<string, Dictionary<string, Node>> tijdelijk = new Dictionary<string, Dictionary<string, Node>>();
        /// <summary>
        /// Dictionary met alle gelinkte nodes en afstanden
        /// </summary>
        Dictionary<string, Dictionary<string, int>> vertices = new Dictionary<string, Dictionary<string, int>>();
        /// <summary>
        /// De x en z waarde van de start node. Vanaf hier worden alle afstanden tot de start node berekend.
        /// </summary>
        double OriginX, OriginZ;
        /// <summary>
        /// Voegt de node met alle connecties toe
        /// </summary>
        /// <param name="name">node id</param>
        /// <param name="randen">nodes die aan de start node verbonden zijn</param>
        public void Add_Nodes(string name, Dictionary<string, Node> randen)
        {
            tijdelijk[name] = randen;
        }
        /// <summary>
        /// Berekent de afstanden tot de startnode
        /// </summary>
        public void CalculateDistance()
        {
            foreach (KeyValuePair<string, Dictionary<string, Node>> node in tijdelijk)
            {
                Dictionary<string, int> vert = new Dictionary<string, int>();
                foreach (var iets in node.Value)
                {
                    if (node.Key == iets.Key)
                    {
                        OriginX = iets.Value.X;
                        OriginZ = iets.Value.Z;
                    }
                    else
                    {
                        int g = (int)iets.Value.X - (int)OriginX;
                        if (g < 0)
                            g = g * -1;
                        int h = (int)iets.Value.Z - (int)OriginZ;
                        if (h < 0)
                            h = h * -1;
                        int TotaalAfstand = g + h;
                        vert.Add(iets.Key, TotaalAfstand);                   
                    }
                }
                vertices.Add(node.Key, vert);
            }
        }
        /// <summary>
        /// Berekent het kortste pad van start tot finish node
        /// </summary>
        /// <param name="start">start node</param>
        /// <param name="finish">finish node</param>
        /// <returns>list<node></returns>
        public List<Node> shortest_path(string start, string finish)
        {

            var previous = new Dictionary<string, string>();

            var distances = new Dictionary<string, int>();

            var nodes = new List<string>();



            List<Node> node = null;


            foreach (var vertex in vertices)

            {

                if (vertex.Key == start)

                {

                    distances[vertex.Key] = 0;

                }

                else

                {

                    distances[vertex.Key] = int.MaxValue;

                }



                nodes.Add(vertex.Key);

            }



            while (nodes.Count != 0)

            {

                nodes.Sort((x, y) => distances[x] - distances[y]);



                var smallest = nodes[0];

                nodes.Remove(smallest);



                if (smallest == finish)

                {

                    node = new List<Node>();
                    while (previous.ContainsKey(smallest))

                    {
                        Manager manager = new Manager();
                        foreach(Node n in manager.points())
                        {
                            if (smallest == n.Id)
                                node.Add(n);
                        }

                        smallest = previous[smallest];

                    }



                    break;

                }



                if (distances[smallest] == int.MaxValue)

                {

                    break;

                }

                foreach (var neighbor in vertices[smallest])

                {

                    var alt = distances[smallest] + neighbor.Value;

                    if (alt < distances[neighbor.Key])

                    {

                        distances[neighbor.Key] = alt;

                        previous[neighbor.Key] = smallest;

                    }

                }

            }        
            node.Reverse();
            return node;
        }
    }
}
