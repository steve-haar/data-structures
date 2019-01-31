namespace DataStructures.BinaryHeap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using MoreLinq;

    public class BinaryHeap<T> : IDynamicSet<T>
        where T : IComparable<T>
    {
        private int size = 0;
        private BinaryHeapNode<T>[] nodes = new BinaryHeapNode<T>[1];
        private Func<T, T, int> comparison;

        public BinaryHeap(Func<T, T, int> comparison)
        {
            this.comparison = comparison;
        }

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

            int currentIndex = this.size;
            BinaryHeapNode<T> node = new BinaryHeapNode<T>(value, this);
            this.size++;

            while (currentIndex > 0)
            {
                int parentIndex = currentIndex >> 1;
                if (this.comparison(node.Value, this.nodes[parentIndex].Value) > 0)
                {
                    this.nodes[currentIndex] = this.nodes[parentIndex];
                    currentIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }

            this.nodes[currentIndex] = node;
            return node;
        }

        public void Delete(IDynamicSetNode<T> node)
        {
            this.ValidateNodeBelongsToThisSet(node);

            BinaryHeapNode<T> sNode = node as BinaryHeapNode<T>;
            sNode.RemoveFromList();
            int currentIndex = Array.IndexOf(this.nodes, sNode);
            sNode = this.nodes[this.size - 1];
            this.size--;

            while (currentIndex < this.size)
            {
                int? maxIndex = this.GetIndexOfMaxChild(currentIndex);
                if (maxIndex.HasValue && this.comparison(this.nodes[maxIndex.Value].Value, sNode.Value) > 0)
                {
                    this.nodes[currentIndex] = this.nodes[maxIndex.Value];
                    currentIndex = maxIndex.Value;
                }
                else
                {
                    break;
                }
            }

            this.nodes[currentIndex] = sNode;
        }

        public IDynamicSetNode<T> Search(T value)
        {
            return this.FirstOrDefault(nodes => nodes.Value.Equals(value));
        }

        public virtual IDynamicSetNode<T> Minimum()
        {
            return this.MinBy(i => i.Value).FirstOrDefault();
        }

        public virtual IDynamicSetNode<T> Maximum()
        {
            return this.MaxBy(i => i.Value).FirstOrDefault();
        }

        private void ValidateNodeBelongsToThisSet(IDynamicSetNode<T> node)
        {
            BinaryHeapNode<T> sNode = node as BinaryHeapNode<T>;
            EnsureArg.IsNotNull(sNode, nameof(node));
            EnsureArg.IsTrue(sNode.Set == this, nameof(node), opts => opts.WithMessage("node does not belong to this array"));
        }

        private void ExpandCapacity()
        {
            BinaryHeapNode<T>[] newArray = new BinaryHeapNode<T>[this.nodes.Length * 2];
            this.nodes.CopyTo(newArray, 0);
            this.nodes = newArray;
        }

        private int? GetIndexOfMaxChild(int currentIndex)
        {
            int leftIndex = (currentIndex * 2) + 1;
            int rightIndex = leftIndex + 1;
            BinaryHeapNode<T> leftNode = leftIndex < this.size ? this.nodes[leftIndex] : null;
            BinaryHeapNode<T> rightNode = rightIndex < this.size ? this.nodes[rightIndex] : null;

            if (leftNode == null)
            {
                return null;
            }
            else if (rightNode == null)
            {
                return leftIndex;
            }
            else
            {
                return this.comparison(leftNode.Value, rightNode.Value) > 0 ? leftIndex : rightIndex;
            }
        }
    }
}