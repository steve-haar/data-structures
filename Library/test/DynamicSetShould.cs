namespace DataStructures.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public abstract class DynamicSetShould<TSet>
        where TSet : IDynamicSet<int>, new()
    {
        protected TSet Set { get; } = new TSet();

        #region Count
        [Fact]
        public void HaveCountOf0WhenConstructed()
        {
            Assert.Equal(expected: 0, actual: this.Set.Count());
        }

        [Fact]
        public void HaveProperCountAfterInsert()
        {
            for (int i = 1; i <= 20; i++)
            {
                this.Set.Insert(i);
                Assert.Equal(expected: i, actual: this.Set.Count());
            }
        }

        [Fact]
        public virtual void HaveProperCountAfterDeletion()
        {
            IDynamicSetNode<int>[] nodes = Enumerable.Range(1, 20)
                .Select(i => this.Set.Insert(i))
                .ToArray();

            for (int i = nodes.Length - 1; i >= 0; i--)
            {
                this.Set.Delete(nodes[i]);
                Assert.Equal(expected: i, actual: this.Set.Count());
            }
        }
        #endregion

        #region Insert
        [Fact]
        public void InsertMultipleDistinctNodesWithTheSameValue()
        {
            IEnumerable<IDynamicSetNode<int>> nodes = Enumerable.Range(1, 20)
                .Select(i => this.Set.Insert(1));

            Assert.Equal(expected: 20, actual: nodes.Distinct().Count());
        }
        #endregion

        #region Delete
        [Fact]
        public void ThrowExceptionWhenDeletingNullNode()
        {
            Assert.Throws<ArgumentNullException>(() => this.Set.Delete(null));
        }

        [Fact]
        public void ThrowExceptionWhenDeletingNonExistingNode()
        {
            IDynamicSetNode<int> node = this.Set.Insert(1);
            this.Set.Delete(node);

            Assert.Throws<ArgumentException>(() => this.Set.Delete(node));
        }

        [Fact]
        public void ThrowExceptionWhenDeletingNodeFromOtherSet()
        {
            TSet set = new TSet();
            IDynamicSetNode<int> node = set.Insert(1);
            this.Set.Insert(1);

            Assert.Throws<ArgumentException>(() => this.Set.Delete(node));
        }

        [Fact]
        public virtual void RemoveNodeOnDeletion()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(1);
            IDynamicSetNode<int> node2 = this.Set.Insert(2);
            IDynamicSetNode<int> node3 = this.Set.Insert(3);

            this.Set.Delete(node2);

            Assert.Contains(expected: node1, collection: this.Set);
            Assert.Contains(expected: node3, collection: this.Set);
            Assert.DoesNotContain(expected: node2, collection: this.Set);
        }
        #endregion

        #region Search
        [Fact]
        public void NotFindNonExistentNode()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(1);
            IDynamicSetNode<int> node2 = this.Set.Insert(2);
            IDynamicSetNode<int> node3 = this.Set.Insert(3);

            Assert.Null(this.Set.Search(4));
        }

        [Fact]
        public void FindExistentNode()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(1);
            IDynamicSetNode<int> node2 = this.Set.Insert(2);
            IDynamicSetNode<int> node3 = this.Set.Insert(3);

            Assert.Equal(expected: node2, actual: this.Set.Search(2));
        }

        [Fact]
        public void FindNodeWithDuplicate()
        {
            IReadOnlyCollection<IDynamicSetNode<int>> nodes = Enumerable.Range(1, 20)
                .Select(i => this.Set.Insert(1))
                .ToList();

            Assert.Contains(this.Set.Search(1), nodes);
        }
        #endregion

        #region Minimum
        [Fact]
        public void NotFindMinimumWhenEmpty()
        {
            Assert.Null(this.Set.Minimum());
        }

        [Fact]
        public void FindMinimum()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(2);
            IDynamicSetNode<int> node2 = this.Set.Insert(4);
            IDynamicSetNode<int> node3 = this.Set.Insert(1);
            IDynamicSetNode<int> node4 = this.Set.Insert(3);

            Assert.Equal(expected: node3, actual: this.Set.Minimum());
        }
        #endregion

        #region Maximum
        [Fact]
        public void NotFindMaximumWhenEmpty()
        {
            Assert.Null(this.Set.Maximum());
        }

        [Fact]
        public void FindMaximum()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(2);
            IDynamicSetNode<int> node2 = this.Set.Insert(4);
            IDynamicSetNode<int> node3 = this.Set.Insert(1);
            IDynamicSetNode<int> node4 = this.Set.Insert(3);

            Assert.Equal(expected: node2, actual: this.Set.Maximum());
        }
        #endregion

        #region Iterate
        [Fact]
        public void IterateThroughNodes()
        {
            IDynamicSetNode<int>[] nodes = new IDynamicSetNode<int>[]
            {
            this.Set.Insert(1),
            this.Set.Insert(2),
            this.Set.Insert(3)
            };

            AssertCollectionsEquivalent(expected: nodes, actual: AsWeakEnumerable(this.Set).Cast<IDynamicSetNode<int>>());
        }
        #endregion

        private static IEnumerable AsWeakEnumerable(IEnumerable source)
        {
            foreach (object o in source)
            {
                yield return o;
            }
        }

        private static void AssertCollectionsEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.Equal(
                expected: expected.OrderBy(i => i.GetHashCode()),
                actual: actual.OrderBy(i => i.GetHashCode()));
        }
    }
}