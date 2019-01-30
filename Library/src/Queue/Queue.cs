namespace DataStructures.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using MoreLinq;

    public class Queue<T> : IDynamicSet<T>
        where T : IComparable<T>
    {
        private QueueNode<T>[] nodes = new QueueNode<T>[1];
        private int head = 0;
        private int tail = 0;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<IDynamicSetNode<T>> GetEnumerator()
        {
            for (int i = this.head; i < this.tail; i++)
            {
                yield return this.nodes[i];
            }
        }

        public int Count()
        {
            return this.tail >= this.head ?
                this.tail - this.head :
                this.nodes.Length - (this.head - this.tail);
        }

        public IDynamicSetNode<T> Insert(T value)
        {
            int nextTail = (this.tail + 1) % this.nodes.Length;
            if (nextTail == this.head)
            {
                this.ExpandCapacity();
            }

            QueueNode<T> node = new QueueNode<T>(value, this);
            this.nodes[this.tail] = node;
            this.tail = (this.tail + 1) % this.nodes.Length;

            return node;
        }

        public void Delete(IDynamicSetNode<T> node)
        {
            this.ValidateNodeBelongsToThisSet(node);

            QueueNode<T> sNode = node as QueueNode<T>;
            if (sNode == this.nodes[this.head])
            {
                sNode.RemoveFromList();
                this.head = (this.head + 1) % this.nodes.Length;
            }
            else
            {
                throw new InvalidOperationException("can only delete first node in the queue");
            }
        }

        public IDynamicSetNode<T> Search(T value)
        {
            return this.FirstOrDefault(nodes => nodes.Value.Equals(value));
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
            QueueNode<T> sNode = node as QueueNode<T>;
            EnsureArg.IsNotNull(sNode, nameof(node));
            EnsureArg.IsTrue(sNode.Set == this, nameof(node), opts => opts.WithMessage("node does not belong to this queue"));
        }

        private void ExpandCapacity()
        {
            QueueNode<T>[] newArray = new QueueNode<T>[this.nodes.Length * 2];
            this.nodes.CopyTo(newArray, 0);
            this.nodes = newArray;
        }
    }
}