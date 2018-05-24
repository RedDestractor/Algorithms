using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    public static class Sort
    {
        public static void QuickSort(int[] array)
        {
            Sorting(array, 0, array.Length - 1);
        }

        private static void Sorting(int[] array, int beginIndex, int endIndex)
        {
            if(beginIndex < endIndex)
            {
                var resultIndex = Partition(array, beginIndex, endIndex);
                Sorting(array, beginIndex, resultIndex - 1);
                Sorting(array, resultIndex + 1, endIndex);
            }
        }

        private static int Partition(int[] array, int indexBegin, int indexEnd)
        {
            var x = array[indexEnd];
            var i = indexBegin - 1;

            for(var j = indexBegin; j < indexEnd; j++)
            {
                if(array[j] <= x)
                {
                    i++;
                    Swap(ref array[i], ref array[j]);
                }
            }
            Swap(ref array[i + 1], ref array[indexEnd]);

            return i + 1;

        }

        private static void Swap(ref int x1, ref int x2)
        {
            var tmp = x1;
            x1 = x2;
            x2 = tmp;
        }
    }
}
