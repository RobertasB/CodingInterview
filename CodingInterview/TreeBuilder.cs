using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingInterview
{
    /// <summary>
    /// Converts flat list to tree structure.
    /// </summary>
    /// <typeparam name="T">Node Value type</typeparam>
    public class TreeBuilder<T>
    {
        private readonly Dictionary<int, Node<T>> _lookup = new Dictionary<int, Node<T>>();

        /// <summary>
        /// Builds tree structure.
        /// </summary>
        /// <returns>List of top level nodes.</returns>
        public List<Node<T>> BuildTree()
        {
            return _lookup.Values.Where(x => x.Parent == null).ToList();
        }

        /// <summary>
        /// Adds nodes range.
        /// </summary>
        /// <typeparam name="I">Item type</typeparam>
        /// <param name="items">Items collection</param>
        /// <param name="id">Function to obtain Item Id</param>
        /// <param name="value">Function to obtain Item Value</param>
        /// <param name="parentId">Function to obtain Item ParentId</param>
        public void AddNodes<I>(IEnumerable<I> items, Func<I, int> id, Func<I, T> value, Func<I, int?> parentId)
        {
            foreach (var item in items)
            {
                AddNode(id(item), value(item), parentId(item));
            }
        }

        /// <summary>
        /// Adds single node.
        /// </summary>
        /// <param name="id">Node Id</param>
        /// <param name="value">Node Value</param>
        /// <param name="parentId">Node ParentId</param>
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