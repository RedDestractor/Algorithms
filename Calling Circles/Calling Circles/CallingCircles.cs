using System;
using System.Collections;
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

        private IEnumerable<Vertex> GetGraphFromConsole()
        {
            var vertexDictionary = new Dictionary<string, Vertex>();
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
                Vertex vertex;
                Vertex neighborVertex;

                if (!vertexDictionary.ContainsKey(vertexName))
                {
                    vertex = new Vertex() { id = idCount++, name = vertexName };
                    vertexDictionary[vertexName] = vertex;
                }
                else
                {
                    vertex = vertexDictionary[vertexName];
                }
                if (!vertexDictionary.ContainsKey(neighborName))
                {
                    neighborVertex = new Vertex() { id = idCount++, name = neighborName };
                    vertexDictionary[neighborName] = neighborVertex;
                }
                else
                {
                    neighborVertex = vertexDictionary[neighborName];
                }

                vertex.neighbors.Add(neighborVertex);
            }

            return vertexDictionary.Select(x => x.Value);
        }
    }

    public class ConsoleWrapper : IConsole
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}