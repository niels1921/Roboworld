using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Dijkstra
    {
        Dictionary<string, Dictionary<string, Node>> tijdelijk = new Dictionary<string, Dictionary<string, Node>>();
        Dictionary<string, Dictionary<string, int>> vertices = new Dictionary<string, Dictionary<string, int>>();
        double OriginX, OriginZ;

        public void Add_Nodes(string name, Dictionary<string, Node> randen)
        {
            tijdelijk[name] = randen;
        }

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

        public List<string> shortest_path(string start, string finish)
        {

            var previous = new Dictionary<string, string>();

            var distances = new Dictionary<string, int>();

            var nodes = new List<string>();



            List<string> path = null;



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

                    path = new List<string>();

                    while (previous.ContainsKey(smallest))

                    {

                        path.Add(smallest);

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

            path.Reverse();

            return path;
        }
    }
}
