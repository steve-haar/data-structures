namespace DataStructures.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using MoreLinq;

    public class Stack<T> : IDynamicSet<T>
        where T : IComparable<T>
    {
        private int size = 0;
        private StackNode<T>[] nodes = new StackNode<T>[1];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<IDynamicSetNode<T>> GetEnumerator()
        {
            for (int i = 0; i < this.size; i++)
            {
                yield return this.nodes[i];
            }
        }

        public int Count()
        {
            return this.size;
        }

        public IDynamicSetNode<T> Insert(T value)
        {
            if (this.size == this.nodes.Length)
            {
                this.ExpandCapacity();
            }

            StackNode<T> node = new StackNode<T>(value, this);
            this.nodes[this.size] = node;
            this.size++;

            return node;
        }

        public void Delete(IDynamicSetNode<T> node)
        {
            this.ValidateNodeBelongsToThisSet(node);

            StackNode<T> sNode = node as StackNode<T>;
            if (sNode == this.nodes[this.size - 1])
            {
                sNode.RemoveFromList();
                this.size--;
            }
            else
            {
                throw new InvalidOperationException("can only delete top node on the stack");
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
            StackNode<T> sNode = node as StackNode<T>;
            EnsureArg.IsNotNull(sNode, nameof(node));
            EnsureArg.IsTrue(sNode.Set == this, nameof(node), opts => opts.WithMessage("node does not belong to this stack"));
        }

        private void ExpandCapacity()
        {
            StackNode<T>[] newArray = new StackNode<T>[this.nodes.Length * 2];
            this.nodes.CopyTo(newArray, 0);
            this.nodes = newArray;
        }
    }
}