using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calling_Circles
{
    internal class CallingCircles
    {
        private readonly IConsole _consoleWrapper;

        public CallingCircles(IConsole console)
        {
            _consoleWrapper = console;
        }

        public string GetCallCircles()
        {
            var data = GetDataFromConsole();
            var graph = GetGraphFromData(data);
            var resultRow = new StringBuilder();

            foreach (var list in new TarjanAlgorithm(graph).SccList)
            {
                if (list.Count > 1)
                {
                    foreach (var vertex in list)
                    {
                        resultRow.Append($"{vertex.Name}, ");
                    }
                    resultRow.Remove(resultRow.Length - 2, 2);
                    resultRow.AppendLine();
                }                
            }

            return resultRow.ToString();
        }

        private IEnumerable<(string vertexName, string neiborName)> GetDataFromConsole()
        {
            while (true)
            {
                var input = _consoleWrapper.ReadLine();

                if (input == "")
                {
                    break;
                }

                var vertexName = input.Split(' ')[0];
                var neighborName = input.Split(' ')[1];

                yield return (vertexName, neighborName);
            }            
        }

        private IEnumerable<Vertex> GetGraphFromData(IEnumerable<(string vertexName, string neiborName)> valuePair)
        {
            var vertexDictionary = new Dictionary<string, Vertex>();
            var idCount = 1;

            foreach(var (vertexName, neighborName) in valuePair)
            { 
                Vertex vertex;
                Vertex neighborVertex;

                if (!vertexDictionary.ContainsKey(vertexName))
                {
                    vertex = new Vertex() { Id = idCount++, Name = vertexName };
                    vertexDictionary[vertexName] = vertex;
                }
                else
                {
                    vertex = vertexDictionary[vertexName];
                }

                if (!vertexDictionary.ContainsKey(neighborName))
                {
                    neighborVertex = new Vertex() {Id = idCount++, Name = neighborName};
                    vertexDictionary[neighborName] = neighborVertex;
                }
                else
                {
                    neighborVertex = vertexDictionary[neighborName];
                }

                vertex.Neighbors.Add(neighborVertex);
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