using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new[] { 1, 2, 4, 123, 4, 5, 6 };

            Sort.QuickSort(array);
        }
    }
}
