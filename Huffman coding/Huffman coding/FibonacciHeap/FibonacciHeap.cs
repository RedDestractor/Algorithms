using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FibonacciHeap
{
    public class FibonacciHeap<T>
        where T : IComparable
    {
        public class Node<T>
            where T: IComparable
        {
            public T Key;
            public Node<T> Parent;
            public Node<T> Child;
            public Node<T> Left;
            public Node<T> Right;
            public T Degree;
            public Boolean Mark;
        }

        private int Size;
        private Node<T> Min;

        public T MinKey
        {
            get { return Min.Key; }
        }

        public void Insert(T x)
        {
            var node = new Node<T>();
            node.Key = x;

            if(Size == 0)
            {
                Min = node;
                Min.Left = node;
                Min.Right = node;
            }
            else
            {
                var prevRight = Min.Right;
                Min.Right = node;
                node.Left = Min;
                node.Right = prevRight;
                prevRight.Left = node;
            }
            if(node.Key.CompareTo(Min.Key) < 0)
            {
                Min = node;
            }
            Size++;
        }

        public void UnionLists(Node<T> firstNode, Node<T> secondNode)
        {
            var left = firstNode.Left;
            var right = secondNode.Right;
            secondNode.Right = firstNode;
            firstNode.Left = secondNode;
            left.Right = right;
            right.Left = left;
        }

        
    }
}
