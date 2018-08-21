using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calling_Circles
{
    class Program
    {
        public static void Main()
        {
            var result = new CallingCircles(new ConsoleWrapper()).GetCallCircles();
        }
    }
}
