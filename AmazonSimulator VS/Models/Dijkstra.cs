using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Dijkstra
    {
        Dictionary<char, Dictionary<char, Node>> tijdelijk = new Dictionary<char, Dictionary<char, Node>>();
        Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();
        double OriginX, OriginZ;

        public void Add_Nodes(char name, Dictionary<char, Node> randen)
        {
            tijdelijk[name] = randen;
        }

        public void CalculateDistance()
        {
            foreach (KeyValuePair<char, Dictionary<Char, Node>> node in tijdelijk)
            {
                Dictionary<char, int> vert = new Dictionary<char, int>();
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

        public List<char> shortest_path(char start, char finish)

        {

            var previous = new Dictionary<char, char>();

            var distances = new Dictionary<char, int>();

            var nodes = new List<char>();



            List<char> path = null;



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

                    path = new List<char>();

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
