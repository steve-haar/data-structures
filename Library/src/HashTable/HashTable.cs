namespace DataStructures.HashTable
{
    using System;
    using System.Collections;
    using System.Linq;
    using DataStructures.LinkedList;
    using EnsureThat;
    using MoreLinq;

    public class HashTable<T> : IDynamicSet<T>
        where T : IComparable<T>
    {
        private int length = 0;
        private LinkedList<HashTableNode<T>>[] nodes = new LinkedList<HashTableNode<T>>[1];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public System.Collections.Generic.IEnumerator<IDynamicSetNode<T>> GetEnumerator()
        {
            foreach (var linkedList in this.nodes)
            {
                if (linkedList != null)
                {
                    foreach (var node in linkedList)
                    {
                        yield return node.Value;
                    }
                }
            }
        }

        public int Count()
        {
            return this.length;
        }

        public IDynamicSetNode<T> Insert(T value)
        {
            if (this.length == this.nodes.Length)
            {
                this.ExpandCapacity();
            }

            HashTableNode<T> node = new HashTableNode<T>(value, this);
            this.InsertNode(node);
            this.length++;
            return node;
        }

        public void Delete(IDynamicSetNode<T> node)
        {
            this.ValidateNodeBelongsToThisSet(node);

            HashTableNode<T> sNode = node as HashTableNode<T>;
            this.RemoveNode(sNode);
            sNode.RemoveFromList();
            this.length--;
        }

        public IDynamicSetNode<T> Search(T value)
        {
            int index = this.Hash(value);
            var linkedList = this.nodes[index];
            if (linkedList == null)
            {
                return null;
            }
            else
            {
                return linkedList.FirstOrDefault(node => node.Value.Value.Equals(value))?.Value;
            }
        }

        public IDynamicSetNode<T> Minimum()
        {
            return this.MinBy(i => i.Value).FirstOrDefault();
        }

        public IDynamicSetNode<T> Maximum()
        {
            return this.MaxBy(i => i.Value).FirstOrDefault();
        }

        private void ValidateNodeBelongsToThisSet(IDynamicSetNode<T> node)
        {
            HashTableNode<T> sNode = node as HashTableNode<T>;
            EnsureArg.IsNotNull(sNode, nameof(node));
            EnsureArg.IsTrue(sNode.Set == this, nameof(node), opts => opts.WithMessage("node does not belong to this table"));
        }

        private void ExpandCapacity()
        {
            var oldArray = this.nodes;
            this.nodes = new LinkedList<HashTableNode<T>>[this.nodes.Length * 2];

            foreach (LinkedList<HashTableNode<T>> linkedList in oldArray)
            {
                if (linkedList != null)
                {
                    foreach (var node in linkedList)
                    {
                        this.InsertNode(node.Value);
                    }
                }
            }
        }

        private int Hash(T value)
        {
            return Math.Abs(value.GetHashCode()) % this.nodes.Length;
        }

        private void InsertNode(HashTableNode<T> node)
        {
            int index = this.Hash(node.Value);
            this.nodes[index] = this.nodes[index] ?? new LinkedList<HashTableNode<T>>();
            var linkedList = this.nodes[index];
            var linkedListNode = linkedList.Insert(node) as LinkedListNode<HashTableNode<T>>;
            node.LinkedListNode = linkedListNode;
        }

        private void RemoveNode(HashTableNode<T> node)
        {
            int index = this.Hash(node.Value);
            var linkedList = this.nodes[index];
            linkedList.Delete(node.LinkedListNode);
        }
    }
}