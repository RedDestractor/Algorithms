using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriorityQueue;

namespace HuffmanCoding
{
    public class HuffmanCoding
    {
        public Dictionary<char, string> huffmanCode;

        public HuffmanCoding(string input)
        {
            huffmanCode = GetCode(input);
        }

        private Dictionary<char, int> GetPriorityDictionary(string input)
        {
            var dictionary = new Dictionary<char, int>();

            foreach(var c in input)
            {
                if (!dictionary.ContainsKey(c))
                    dictionary.Add(c, 1);
                else dictionary[c] = dictionary[c] + 1;
            }

            return dictionary;
        }

        private PriorityQueue<int, Heap<char>> GetPriorirityHeapDictionary(string input)
        {
            var result = new PriorityQueue<int, Heap<char>>();
            var dictionary = GetPriorityDictionary(input);

            foreach (var pair in dictionary)
            {
                result.Enqueue(pair.Value, new Heap<char>(pair.Key));
            }

            return result;
        }

        public Heap<char> GetHuffmanHeap(string input)
        {
            var huffmanQueue = GetPriorirityHeapDictionary(input);

            while (huffmanQueue.Size != 1)
            {
                var firstHeap = huffmanQueue.Dequeue();
                var secondHeap = huffmanQueue.Dequeue();

                var resultHeap = new Heap<char>(firstHeap.Value, secondHeap.Value);
                huffmanQueue.Enqueue(firstHeap.Key + secondHeap.Key, resultHeap);
            }

            return huffmanQueue.Dequeue().Value;
        }

        public Dictionary<char, string> GetCode(string input, string code = "")
        {
            huffmanCode = new Dictionary<char, string>();
            var heap = GetHuffmanHeap(input);
            var current = heap.root;
            TraversalHeap(huffmanCode, current);
            return huffmanCode;
        }

        public void TraversalHeap(Dictionary<char, string> huffmanCode, Node<char> node, string code = "")
        {
            if(node.key != 0)
            {
                huffmanCode.Add(node.key, code);
                return;
            }
            TraversalHeap(huffmanCode, node.left, code + "0");
            TraversalHeap(huffmanCode, node.right, code + "1");
        }
    }

    public class Program
    {
        static void Main()
        {
            var huffman = new HuffmanCoding("beep boop beer!");

            var code = huffman.huffmanCode;

            foreach(var pair in code)
            {
                Console.WriteLine(pair);
            }
        }
    }
}
