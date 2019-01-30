namespace DataStructures.Stack
{
    using System;
    using EnsureThat;

    public class StackNode<T> : IDynamicSetNode<T>
        where T : IComparable<T>
    {
        internal StackNode(T value, Stack<T> set)
        {
            EnsureArg.IsNotNull(set, nameof(set));

            this.Value = value;
            this.Set = set;
        }

        public T Value { get; }

        internal Stack<T> Set { get; private set; }

        internal void RemoveFromList()
        {
            this.Set = null;
        }
    }
}