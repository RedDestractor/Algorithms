namespace HuffmanCoding
{
    public class Heap<T>
    {
        public Node<T> root;
        public T Key { get => root.key; private set { } }        

        public Heap(Heap<T> firstHeap, Heap<T> secondHeap)
        {
            root = new Node<T>();
            root.left = firstHeap.root;
            root.right = secondHeap.root;
        }

        public Heap(T key)
        {
            root = new Node<T>();
            root.key = key;
        }        
    }

    public class Node<T>
    {
        public Node<T> left;
        public Node<T> right;
        public T key;
    }
}