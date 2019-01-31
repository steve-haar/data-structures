namespace DataStructures.BinaryHeap
{
    using System;
    using EnsureThat;

    public class BinaryHeapNode<T> : IDynamicSetNode<T>
        where T : IComparable<T>
    {
        internal BinaryHeapNode(T value, BinaryHeap<T> set)
        {
            EnsureArg.IsNotNull(set, nameof(set));

            this.Value = value;
            this.Set = set;
        }

        public T Value { get; }

        public BinaryHeap<T> Set { get; private set; }

        internal void RemoveFromList()
        {
            this.Set = null;
        }
    }
}