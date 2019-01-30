namespace DataStructures.DynamicArray
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using MoreLinq;

    public class DynamicArray<T> : IDynamicSet<T>
        where T : IComparable<T>
    {
        private int size = 0;
        private DynamicArrayNode<T>[] nodes = new DynamicArrayNode<T>[1];

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

            DynamicArrayNode<T> node = new DynamicArrayNode<T>(value, this);
            this.nodes[this.size] = node;
            this.size++;

            return node;
        }

        public void Delete(IDynamicSetNode<T> node)
        {
            this.ValidateNodeBelongsToThisSet(node);

            DynamicArrayNode<T> sNode = node as DynamicArrayNode<T>;
            for (int i = 0; i < this.size; i++)
            {
                if (this.nodes[i].Equals(sNode))
                {
                    for (int j = i; j < this.size - 1; j++)
                    {
                        this.nodes[j] = this.nodes[j + 1];
                    }

                    sNode.RemoveFromList();
                    this.size--;
                    break;
                }
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
            DynamicArrayNode<T> sNode = node as DynamicArrayNode<T>;
            EnsureArg.IsNotNull(sNode, nameof(node));
            EnsureArg.IsTrue(sNode.Set == this, nameof(node), opts => opts.WithMessage("node does not belong to this array"));
        }

        private void ExpandCapacity()
        {
            DynamicArrayNode<T>[] newArray = new DynamicArrayNode<T>[this.nodes.Length * 2];
            this.nodes.CopyTo(newArray, 0);
            this.nodes = newArray;
        }
    }
}