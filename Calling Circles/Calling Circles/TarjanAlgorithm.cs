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

            foreach (Vertex v in graphInput)
            {
                if (v.index < 0)
                {
                    StrongConnect(v);
                }
            }
        }

        private void StrongConnect(Vertex v)
        {
            v.index = index;
            v.lowlink = index;
            index++;
            stack.Push(v);

            foreach (Vertex neighbor in v.neighbors)
            {
                if (neighbor.index < 0)
                {
                    StrongConnect(neighbor);
                    v.lowlink = Math.Min(v.lowlink, neighbor.lowlink);
                }
                else if (stack.Contains(neighbor))
                {
                    v.lowlink = Math.Min(v.lowlink, neighbor.index);
                }
            }

            if (v.lowlink == v.index)
            {
                List<Vertex> currentScc = new List<Vertex>();
                Vertex w;
                do
                {
                    w = stack.Pop();
                    currentScc.Add(w);
                } while (w != v);

                sccList.Add(currentScc);
            }
        }
    }
}
