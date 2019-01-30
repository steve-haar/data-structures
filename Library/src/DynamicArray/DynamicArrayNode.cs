namespace DataStructures.DynamicArray
{
    using System;
    using EnsureThat;

    public class DynamicArrayNode<T> : IDynamicSetNode<T>
        where T : IComparable<T>
    {
        internal DynamicArrayNode(T value, DynamicArray<T> set)
        {
            EnsureArg.IsNotNull(set, nameof(set));

            this.Value = value;
            this.Set = set;
        }

        public T Value { get; }

        internal DynamicArray<T> Set { get; private set; }

        internal void RemoveFromList()
        {
            this.Set = null;
        }
    }
}