namespace DataStructures.BinarySearchTree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using MoreLinq;

    public class BinarySearchTree<T> : IDynamicSet<T>
        where T : IComparable<T>
    {
        private BinarySearchTreeNode<T> rootNode;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<IDynamicSetNode<T>> GetEnumerator()
        {
            foreach (IDynamicSetNode<T> node in Walk(this.rootNode))
            {
                yield return node;
            }
        }

        public int Count()
        {
            int count = 0;
            foreach (IDynamicSetNode<T> node in Walk(this.rootNode))
            {
                count++;
            }

            return count;
        }

        public IDynamicSetNode<T> Insert(T value)
        {
            BinarySearchTreeNode<T> previousNode = this.rootNode;
            BinarySearchTreeNode<T> node = this.rootNode;

            while (node != null)
            {
                previousNode = node;
                node = value.CompareTo(node.Value) > 0 ? node.Right : node.Left;
            }

            BinarySearchTreeNode<T> newNode = new BinarySearchTreeNode<T>(value, previousNode, this);

            if (previousNode == null)
            {
                this.rootNode = newNode;
            }
            else if (newNode.Value.CompareTo(previousNode.Value) > 0)
            {
                previousNode.Right = newNode;
            }
            else
            {
                previousNode.Left = newNode;
            }

            return newNode;
        }

        public void Delete(IDynamicSetNode<T> node)
        {
            this.ValidateNodeBelongsToThisSet(node);

            BinarySearchTreeNode<T> sNode = node as BinarySearchTreeNode<T>;
            sNode.RemoveFromList();

            if (sNode.Left == null)
            {
                this.Transplant(sNode, sNode.Right);
            }
            else if (sNode.Right == null)
            {
                this.Transplant(sNode, sNode.Left);
            }
            else
            {
                BinarySearchTreeNode<T> predecessor = Minimum(sNode.Right);
                if (predecessor.Parent != sNode)
                {
                    this.Transplant(predecessor, predecessor.Right);
                    predecessor.Right = sNode.Right;
                    predecessor.Right.Parent = predecessor;
                }

                this.Transplant(sNode, predecessor);
                predecessor.Left = sNode.Left;
                predecessor.Left.Parent = predecessor;
            }
        }

        public IDynamicSetNode<T> Search(T value)
        {
            BinarySearchTreeNode<T> node = this.rootNode;

            while (node != null && node.Value.Equals(value) == false)
            {
                node = value.CompareTo(node.Value) > 0 ? node.Right : node.Left;
            }

            return node;
        }

        public IDynamicSetNode<T> Minimum()
        {
            return Minimum(this.rootNode);
        }

        public IDynamicSetNode<T> Maximum()
        {
            return Maximum(this.rootNode);
        }

        private static BinarySearchTreeNode<T> Minimum(BinarySearchTreeNode<T> node)
        {
            while (node?.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        private static BinarySearchTreeNode<T> Maximum(BinarySearchTreeNode<T> node)
        {
            while (node?.Right != null)
            {
                node = node.Right;
            }

            return node;
        }

        private static IEnumerable<BinarySearchTreeNode<T>> Walk(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return Array.Empty<BinarySearchTreeNode<T>>();
            }

            return Walk(node.Left)
                .Concat(new BinarySearchTreeNode<T>[] { node })
                .Concat(Walk(node.Right));
        }

        private void Transplant(
            BinarySearchTreeNode<T> u,
            BinarySearchTreeNode<T> v)
        {
            if (u.Parent == null)
            {
                this.rootNode = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }

            if (v != null)
            {
                v.Parent = u.Parent;
            }
        }

        private void ValidateNodeBelongsToThisSet(IDynamicSetNode<T> node)
        {
            BinarySearchTreeNode<T> sNode = node as BinarySearchTreeNode<T>;
            EnsureArg.IsNotNull(sNode, nameof(node));
            EnsureArg.IsTrue(sNode.Set == this, nameof(node), opts => opts.WithMessage("node does not belong to this tree"));
        }
    }
}