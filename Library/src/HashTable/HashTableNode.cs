namespace DataStructures.HashTable
{
    using System;
    using DataStructures.LinkedList;
    using EnsureThat;

    public class HashTableNode<T> : IDynamicSetNode<T>, IComparable<HashTableNode<T>>
        where T : IComparable<T>
    {
        internal HashTableNode(T value, HashTable<T> set)
        {
            EnsureArg.IsNotNull(set, nameof(set));

            this.Value = value;
            this.Set = set;
        }

        public T Value { get; }

        internal HashTable<T> Set { get; private set; }

        internal LinkedListNode<HashTableNode<T>> LinkedListNode { get; set; }

        public int CompareTo(HashTableNode<T> other)
        {
            return this.Value.CompareTo(other.Value);
        }

        internal void RemoveFromList()
        {
            this.Set = null;
            this.LinkedListNode = null;
        }
    }
}