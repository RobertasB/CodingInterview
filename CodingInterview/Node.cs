using System.Collections.Generic;

namespace CodingInterview
{
    public class Node<T>
    {
        public Node()
        {
            Children = new List<Node<T>>();
        }

        public Node(T value) : this()
        {
            Value = value;
        }

        public T Value { get; set; }

        public Node<T> Parent { get; set; }

        public List<Node<T>> Children { get; set; }
    }
}