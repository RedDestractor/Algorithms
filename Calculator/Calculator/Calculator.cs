using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Calculator.App
{
    public class CalculatorManager
    {
        public string[] AllowedOperations
        {
            get
            {
                return new string[] { "+", "-", "*", "/", ")", "(" };
            }
        }

        public double GetCalculation(string input)
        {
            var reverseNotationStack = GetReverseNotation(input);

            Stack<double> digitsStack = new Stack<double>();

            try
            {
                while (reverseNotationStack.Count != 0)
                {
                    var current = reverseNotationStack.Pop();

                    if (double.TryParse(current, out double digit))
                    {
                        digitsStack.Push(digit);
                    }
                    else if (digitsStack.Count >= 2)
                    {
                        var rightOperand = Math.Round(digitsStack.Pop(), 5);
                        var leftOperand = Math.Round(digitsStack.Pop(), 5);
                        var result = OperandsOperation(current, leftOperand, rightOperand);
                        digitsStack.Push(result);
                    }
                }
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }

            return digitsStack.Pop();
        }

        private Stack<string> GetReverseNotation(string input)
        {
            var inputArray = SplitInputArray(input);

            Stack<string> operationStack = new Stack<string>();
            Stack<string> outputStack = new Stack<string>();

            foreach (var c in inputArray)
            {
                if (double.TryParse(c, out double digit))
                {
                    outputStack.Push(c);
                }
                else if (operationStack.Count == 0)
                {
                    operationStack.Push(c);
                }
                else if (priorityTable[c] == Priority.Wild)
                {
                    operationStack.Push(c);
                }
                else if (priorityTable[c] == Priority.Immediate)
                {
                    while (operationStack.Count > 0 &&
                           priorityTable[operationStack.Peek()] != Priority.Wild)
                    {
                        outputStack.Push(operationStack.Pop());
                    }
                    operationStack.Pop();
                }
                else if (priorityTable[c] > priorityTable[operationStack.Peek()])
                {
                    operationStack.Push(c);
                }
                else if (priorityTable[c] <= priorityTable[operationStack.Peek()])
                {
                    while (operationStack.Count > 0 &&
                           priorityTable[c] <= priorityTable[operationStack.Peek()] &&
                           priorityTable[operationStack.Peek()] != Priority.Wild)
                    {
                        outputStack.Push(operationStack.Pop());
                    }
                    operationStack.Push(c);
                }
            }
            while (operationStack.Count > 0)
            {
                outputStack.Push(operationStack.Pop());
            }

            var result = new Stack<string>(outputStack);

            return result;
        }

        private double OperandsOperation(string current, double leftOperand, double rightOperand)
        {
            switch (current)
            {
                case "*":
                    return leftOperand * rightOperand;
                case "/":
                    return leftOperand / rightOperand;
                case "+":
                    return leftOperand + rightOperand;
                case "-":
                    return leftOperand - rightOperand;
                default:
                    return 0;
            }
        }

        private string[] SplitInputArray(string input)
        {
            var result = new List<string>();
            var currentNumber = "";

            foreach (var c in input)
            {
                if (Char.IsDigit(c) || c == ',')
                {
                    currentNumber += c;
                    continue;
                }
                else if (priorityTable.ContainsKey(c.ToString()))
                {
                    if (!String.IsNullOrEmpty(currentNumber))
                        result.Add(currentNumber);
                    result.Add(c.ToString());
                    currentNumber = "";
                }
                else if (c == ' ')
                {
                    continue;
                }
                else
                {
                    throw new ArgumentException(nameof(SplitInputArray));
                }
            }
            if (!String.IsNullOrEmpty(currentNumber))
                result.Add(currentNumber);

            return result.ToArray();
        }

        private Dictionary<string, Priority> priorityTable = new Dictionary<string, Priority>
        {
            { ")", Priority.Immediate },
            { "*", Priority.High },
            { "/", Priority.High },
            { "+", Priority.Low },
            { "-", Priority.Low},
            { "(", Priority.Wild },
        };
    }

    public enum Priority
    {
        Wild,
        Low,
        High,
        Immediate,
    }
}