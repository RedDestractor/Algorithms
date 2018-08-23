using System;

namespace Calling_Circles
{
    static class Program
    {
        public static void Main()
        {
            var result = new CallingCircles(new ConsoleWrapper()).GetCallCircles();

            Console.WriteLine(result);
        }
    }
}
