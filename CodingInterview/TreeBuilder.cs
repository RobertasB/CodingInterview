using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingInterview
{
    public class TreeBuilder<T>
    {
        private readonly Dictionary<int, Node<T>> _lookup = new Dictionary<int, Node<T>>();

        public List<Node<T>> BuildTree()
        {
            return _lookup.Values.Where(x => x.Parent == null).ToList();
        }

        public void AddNodes<I>(IEnumerable<I> items, Func<I, int> getId, Func<I, T> getValue, Func<I, int?> getPrentId)
        {
            foreach (var item in items)
            {
                var value = getValue(item);
                var id = getId(item);
                var parentId = getPrentId(item);

                AddNode(id, value, parentId);
            }
        }

        public void AddNode(int id, T value, int? parentId)
        {
            Node<T> node;
            var nodeExists = _lookup.TryGetValue(id, out node);
            if (nodeExists)
            {
                node.Value = value;
            }
            else
            {
                node = new Node<T>(value);
                _lookup[id] = node;
            }

            SetParentNode(node, parentId);
        }

        private void SetParentNode(Node<T> node, int? parentId)
        {
            if (!parentId.HasValue)
            {
                return;
            }

            Node<T> parentNode;
            var parentNodeExists = _lookup.TryGetValue(parentId.Value, out parentNode);
            if (!parentNodeExists)
            {
                parentNode = new Node<T>();
                _lookup[parentId.Value] = parentNode;
            }

            node.Parent = parentNode;
            parentNode.Children.Add(node);
        }
    }
}