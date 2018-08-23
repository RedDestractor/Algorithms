using System;
using System.Collections.Generic;

namespace Calling_Circles
{
    //https://en.wikipedia.org/wiki/Tarjan's_strongly_connected_components_algorithm
    public class TarjanAlgorithm
    {
        public readonly List<List<Vertex>> SccList;

        private readonly Stack<Vertex> _stack;
        private int _index;

        public TarjanAlgorithm(IEnumerable<Vertex> graphInput)
        {
            SccList = new List<List<Vertex>>();
            _stack = new Stack<Vertex>();

            foreach (var vertex in graphInput)
            {
                if (vertex.Index < 0)
                {
                    StrongConnect(vertex);
                }
            }
        }

        private void StrongConnect(Vertex vertex)
        {
            vertex.Index = _index;
            vertex.Lowlink = _index;
            _index++;
            _stack.Push(vertex);

            foreach (var neighbor in vertex.Neighbors)
            {
                if (neighbor.Index < 0)
                {
                    StrongConnect(neighbor);
                    vertex.Lowlink = Math.Min(vertex.Lowlink, neighbor.Lowlink);
                }
                else if (_stack.Contains(neighbor))
                {
                    vertex.Lowlink = Math.Min(vertex.Lowlink, neighbor.Index);
                }
            }

            if (vertex.Lowlink == vertex.Index)
            {
                var currentScc = new List<Vertex>();
                Vertex current;
                do
                {
                    current = _stack.Pop();
                    currentScc.Add(current);
                } while (current != vertex);

                SccList.Add(currentScc);
            }
        }
    }
}
