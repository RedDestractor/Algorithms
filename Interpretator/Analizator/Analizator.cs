using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizator
{
    class Analizator
    {
        List<string> ErrorList = new List<string>();

        private Tuple<Dictionary<int, int>, Dictionary<int, int>> GetBracketsDivtionary(string line)
        {
            Dictionary<int, int> openBrackets = new Dictionary<int, int>();
            Dictionary<int, int> closeBrackets = new Dictionary<int, int>();

            var stackOpenBrackets = new Stack<int>();

            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '{')
                {
                    stackOpenBrackets.Push(i);
                }

                if (line[i] == '}')
                {
                    if (!stackOpenBrackets.Any())
                    {
                        ErrorList.Add($" Have no open brackets at {i} ");
                        continue;
                    }

                    var lastOpenBracket = stackOpenBrackets.Pop();

                    openBrackets[i] = lastOpenBracket;

                    closeBrackets[lastOpenBracket] = i;
                }
            }

            if (stackOpenBrackets.Any())
            {
                while (stackOpenBrackets.Any())
                {
                    ErrorList.Add($" Have no close brackets at {stackOpenBrackets.Pop()} ");
                }

                return null;
            }

            return Tuple.Create(openBrackets, closeBrackets);
        }

        public void Compilate(string line)
        {
            var dictinaries = GetBracketsDivtionary(line);

            if (dictinaries == null)
            {
                foreach (var error in ErrorList)
                {
                    Console.WriteLine(error);
                }
                return;
            }

            var openBrackets = dictinaries.Item1;
            var closeBrackets = dictinaries.Item2;

            CheckLetters(openBrackets, closeBrackets, line);

            if (!ErrorList.Any())
            {
                Console.WriteLine(" Compilation completed with no errors");
            }
            else
            {
                foreach (var error in ErrorList)
                {
                    Console.WriteLine(error);
                }
            }
        }

        private void CheckLetters(Dictionary<int, int> openBrackets, Dictionary<int, int> closeBrackets, string line)
        {
            CheckLetter(0, line.Length, line);

            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '{')
                {
                    var start = i;
                    var end = closeBrackets[i];

                    CheckLetter(start, end, line);
                }
            }
        }

        private void CheckLetter(int start, int end, string line)
        {
            var list = new List<char>();

            for (var k = start; k < end; k++)
            {
                if (!list.Contains(line[k]) && line[k] != '{' && line[k] != '}')
                {
                    list.Add(line[k]);
                }
                else if (line[k] != '{' && line[k] != '}')
                {
                    ErrorList.Add($" Already have symbol {line[k]} at {k} ");
                }
            }
        }

        static void Main()
        {
            var analizator = new Analizator();

            analizator.Compilate("a{{a}c");
        }
    }
}
