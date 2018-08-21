using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calling_Circles
{
    //https://en.wikipedia.org/wiki/Tarjan's_strongly_connected_components_algorithm
    public class TarjanAlgorithm
    {
        public List<List<Vertex>> sccList;

        private Stack<Vertex> stack;
        private int index;

        public TarjanAlgorithm(IEnumerable<Vertex> graphInput)
        {
            sccList = new List<List<Vertex>>();
            stack = new Stack<Vertex>();

            foreach (Vertex vertex in graphInput)
            {
                if (vertex.index < 0)
                {
                    StrongConnect(vertex);
                }
            }
        }

        private void StrongConnect(Vertex vertex)
        {
            vertex.index = index;
            vertex.lowlink = index;
            index++;
            stack.Push(vertex);

            foreach (Vertex neighbor in vertex.neighbors)
            {
                if (neighbor.index < 0)
                {
                    StrongConnect(neighbor);
                    vertex.lowlink = Math.Min(vertex.lowlink, neighbor.lowlink);
                }
                else if (stack.Contains(neighbor))
                {
                    vertex.lowlink = Math.Min(vertex.lowlink, neighbor.index);
                }
            }

            if (vertex.lowlink == vertex.index)
            {
                List<Vertex> currentScc = new List<Vertex>();
                Vertex current;
                do
                {
                    current = stack.Pop();
                    currentScc.Add(current);
                } while (current != vertex);

                sccList.Add(currentScc);
            }
        }
    }
}
