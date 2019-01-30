namespace DataStructures.LinkedList
{
    using System;
    using EnsureThat;

    public class LinkedListNode<T> : IDynamicSetNode<T>
        where T : IComparable<T>
    {
        internal LinkedListNode(
            T value,
            LinkedList<T> set,
            LinkedListNode<T> predecessor,
            LinkedListNode<T> successor)
        {
            EnsureArg.IsNotNull(set, nameof(set));
            EnsureArg.IsNotNull(predecessor, nameof(predecessor));
            EnsureArg.IsNotNull(successor, nameof(successor));

            this.Value = value;
            this.Set = set;
            this.Predecessor = predecessor;
            this.Successor = successor;
        }

        private LinkedListNode(
            T value,
            LinkedList<T> set)
        {
            EnsureArg.IsNotNull(set, nameof(set));

            this.Value = value;
            this.Set = set;
            this.Predecessor = this;
            this.Successor = this;
        }

        public T Value { get; }

        internal LinkedList<T> Set { get; private set; }

        internal LinkedListNode<T> Predecessor { get; set; }

        internal LinkedListNode<T> Successor { get; set; }

        internal static LinkedListNode<T> GetSentinelNode(T value, LinkedList<T> list)
        {
            return new LinkedListNode<T>(value, list);
        }

        internal void RemoveFromList()
        {
            this.Set = null;
        }
    }
}