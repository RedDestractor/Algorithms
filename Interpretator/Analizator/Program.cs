using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Задача:
//Реализовать статический анализатор для языка программирования.
//Возможности языка:
//1) Можно объявлять переменные(переменные состоят строго из одной буквы латинского алфавита). Для объявления нужно просто написать имя этой переменной.Повторное объявление переменной в той же области видимости - это ошибка. Область видимости переменных такая же как в C#.
//2) Можно использовать блоки.Они задают область видимости.

//Статический анализатор должен вывести список ошибок или "Нет ошибок".
//Возможные ошибки:
//1) Переменная a уже объявлена
//2) Неожиданный символ %
//3) Лишняя }
//4) Не хватает }
//В идеале ещё показывать номер символа, где найдена ошибка.

//Примеры:
//a{ b}
//c -> Нет ошибок
//a{}c -> Нет ошибок
//{ a }
//b{a} -> Нет ошибок
//a{a}c -> Переменная a уже объявлена, номер символа: 2
//a{#}c -> Неожиданный символ #, номер символа: 2
//a{bc -> Не хватает }
//ab}c -> Лишняя }, номер символа: 2

namespace Analizator
{
    class Program
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
                    while(stackOpenBrackets.Any())
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

                if(dictinaries == null)
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

                if(!ErrorList.Any())
                {
                    Console.WriteLine(" Compilation completed with no errors");
                }
                else
                {
                    foreach(var error in ErrorList)
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
        }
        
        
        static void Main()
        {
            var analizator = new Analizator();

            analizator.Compilate("a{{a}c");

        }

    }
}
