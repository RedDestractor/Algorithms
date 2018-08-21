using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calling_Circles
{
    class CallingCircles
    {
        IConsole consoleWrapper;

        public CallingCircles(IConsole console)
        {
            consoleWrapper = console;
        }

        public string GetCallCircles()
        {
            var graph = GetGraphFromConsole();
            var tarjan = new TarjanAlgorithm(graph);
            var resultRow = new StringBuilder();

            foreach (var list in tarjan.sccList)
            {
                if (list.Count > 1)
                {
                    foreach (var vertex in list)
                    {
                        resultRow.Append($"{vertex.name}, ");
                    }
                    resultRow.Remove(resultRow.Length - 2, 2);
                    resultRow.AppendLine();
                }                
            }
            consoleWrapper.WriteLine(resultRow.ToString());
            return resultRow.ToString();
        }

        private List<Vertex> GetGraphFromConsole()
        {
            var graph = new List<Vertex>();
            var idCount = 1;
            string input = null;

            while (true)
            {
                input = consoleWrapper.ReadLine();

                if(input == "")
                {
                    break;
                }

                var vertexName = input.Split(' ')[0];
                var neighborName = input.Split(' ')[1];
                var vertex = graph.Find(x => x.name == vertexName);
                var neighborVertex = graph.Find(x => x.name == neighborName);

                if (vertex == null)
                {
                    vertex = new Vertex() { id = idCount++, name = vertexName };
                    graph.Add(vertex);
                }
                if (neighborVertex == null)
                {
                    neighborVertex = new Vertex() { id = idCount++, name = neighborName };
                    graph.Add(neighborVertex);
                }

                vertex.neighbors.Add(neighborVertex);
            }

            return graph;
        }
    }  
}