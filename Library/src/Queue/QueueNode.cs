namespace DataStructures.Queue
{
    using System;
    using EnsureThat;

    public class QueueNode<T> : IDynamicSetNode<T>
        where T : IComparable<T>
    {
        internal QueueNode(T value, Queue<T> set)
        {
            EnsureArg.IsNotNull(set, nameof(set));

            this.Value = value;
            this.Set = set;
        }

        public T Value { get; }

        internal Queue<T> Set { get; private set; }

        internal void RemoveFromList()
        {
            this.Set = null;
        }
    }
}