namespace DataStructures.LinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EnsureThat;
    using MoreLinq;

    public class LinkedList<T> : IDynamicSet<T>
        where T : IComparable<T>
    {
        private LinkedListNode<T> sentinelNode;

        public LinkedList()
        {
            this.sentinelNode = this.GetSentinelNode();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<IDynamicSetNode<T>> GetEnumerator()
        {
            LinkedListNode<T> node = this.sentinelNode.Successor;
            while (node != this.sentinelNode)
            {
                yield return node;
                node = node.Successor;
            }
        }

        public int Count()
        {
            int count = 0;
            LinkedListNode<T> node = this.sentinelNode.Successor;
            while (node != this.sentinelNode)
            {
                count++;
                node = node.Successor;
            }

            return count;
        }

        public IDynamicSetNode<T> Insert(T value)
        {
            LinkedListNode<T> lastNode = this.sentinelNode.Predecessor;
            LinkedListNode<T> newNode = new LinkedListNode<T>(
                value: value,
                set: this,
                predecessor: lastNode,
                successor: this.sentinelNode);

            this.sentinelNode.Predecessor = lastNode.Successor = newNode;

            return newNode;
        }

        public void Delete(IDynamicSetNode<T> node)
        {
            this.ValidateNodeBelongsToThisSet(node);

            LinkedListNode<T> sNode = node as LinkedListNode<T>;
            sNode.Predecessor.Successor = sNode.Successor;
            sNode.Successor.Predecessor = sNode.Predecessor;
            sNode.RemoveFromList();
        }

        public IDynamicSetNode<T> Search(T value)
        {
            LinkedListNode<T> node = this.sentinelNode.Successor;
            while (node != this.sentinelNode)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }

                node = node.Successor;
            }

            return null;
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
            LinkedListNode<T> sNode = node as LinkedListNode<T>;
            EnsureArg.IsNotNull(sNode, nameof(node));
            EnsureArg.IsTrue(sNode.Set == this, nameof(node), opts => opts.WithMessage("node does not belong to this list"));
        }

        private LinkedListNode<T> GetSentinelNode()
        {
            return LinkedListNode<T>.GetSentinelNode(default(T), this);
        }
    }
}