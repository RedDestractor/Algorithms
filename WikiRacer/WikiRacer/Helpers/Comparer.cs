using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiRacer.Helpers
{
    public class QueueComparer<T> : IComparer<T>
    {
        private Func<T, T, int> comparer;
        public QueueComparer(Func<T, T, int> comparer)
        {
            this.comparer = comparer;
        }
        public static IComparer<T> Create(Func<T, T, int> comparer)
        {
            return new QueueComparer<T>(comparer);
        }
        public int Compare(T x, T y)
        {
            return comparer(x, y);
        }
    }
}
