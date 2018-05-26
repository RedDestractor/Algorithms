using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    public class DescendingComparer<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T x, T y)
        {
            return y.CompareTo(x);
        }


    }

    public class PriorityQueue<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        SortedDictionary<TKey, Queue<TValue>> storage;

        public PriorityQueue()
        {
            storage = new SortedDictionary<TKey, Queue<TValue>>();
            Size = 0;
        }
        public PriorityQueue(IComparer<TKey> comparer)
        {
            storage = new SortedDictionary<TKey, Queue<TValue>>(comparer);
            Size = 0;
        }

        public int Size { get; private set; }
        public bool IsEmpty { get { return Size == 0; } }

        public void Enqueue(TKey key, TValue value)
        {
            if (!storage.ContainsKey(key))
            {
                storage.Add(key, new Queue<TValue>());
            }
            storage[key].Enqueue(value);
            Size++;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var key in storage.Keys)
            {
                foreach (var value in storage[key])
                {
                    yield return new KeyValuePair<TKey, TValue>(key, value);
                }
            }
        }

        public TValue Peek()
        {
            if (IsEmpty) throw new Exception("Please check that priorityQueue is not empty before peeking");

            foreach (var queue in storage.Values)
            {
                if (queue.Count > 0)
                    return queue.Peek();
            }

            return default(TValue);
        }

        public KeyValuePair<TKey, TValue> Dequeue()
        {
            if (IsEmpty)
                throw new NullReferenceException("Queue is empty");

            foreach (var key in storage.Keys)
            {
                if (storage[key].Count > 0)
                {
                    Size--;
                    var value = storage[key].Dequeue();
                    return new KeyValuePair<TKey, TValue>(key, value);  
                }
            }

            return default(KeyValuePair<TKey, TValue>);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
