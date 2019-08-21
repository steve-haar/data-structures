namespace DataStructures.BinarySearchTree
{
    using System;
    using EnsureThat;

    public class BinarySearchTreeNode<T> : IDynamicSetNode<T>
        where T : IComparable<T>
    {
        internal BinarySearchTreeNode(
            T value,
            BinarySearchTreeNode<T> parent,
            BinarySearchTree<T> set)
        {
            EnsureArg.IsNotNull(set, nameof(parent));
            EnsureArg.IsNotNull(set, nameof(set));

            this.Value = value;
            this.Parent = parent;
            this.Set = set;
        }

        public T Value { get; }

        internal BinarySearchTree<T> Set { get; private set; }

        internal BinarySearchTreeNode<T> Parent { get; set; }

        internal BinarySearchTreeNode<T> Left { get; set; }

        internal BinarySearchTreeNode<T> Right { get; set; }

        internal void RemoveFromList()
        {
            this.Set = null;
        }
    }
}