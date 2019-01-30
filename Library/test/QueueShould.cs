namespace DataStructures.Tests
{
    using System;
    using System.Linq;
    using DataStructures.Queue;
    using Xunit;

    public class QueueShould : DynamicSetShould<Queue<int>>
    {
        [Fact]
        public override void HaveProperCountAfterDeletion()
        {
            for (int i = 0; i < 3; i++)
            {
                IDynamicSetNode<int>[] nodes = Enumerable.Range(1, 20)
                            .Select(node => this.Set.Insert(node))
                            .ToArray();

                for (int j = 0; j < nodes.Length; j++)
                {
                    this.Set.Delete(nodes[j]);
                    Assert.Equal(expected: nodes.Length - 1 - j, actual: this.Set.Count());
                }
            }
        }

        [Fact]
        public void ThrowExceptionWhenDeletingNotFirstNode()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(1);
            IDynamicSetNode<int> node2 = this.Set.Insert(2);
            IDynamicSetNode<int> node3 = this.Set.Insert(3);

            Assert.Throws<InvalidOperationException>(() => this.Set.Delete(node2));
            Assert.Throws<InvalidOperationException>(() => this.Set.Delete(node3));
        }

        [Fact]
        public override void RemoveNodeOnDeletion()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(1);
            IDynamicSetNode<int> node2 = this.Set.Insert(2);
            IDynamicSetNode<int> node3 = this.Set.Insert(3);

            this.Set.Delete(node1);

            Assert.Contains(expected: node2, collection: this.Set);
            Assert.Contains(expected: node3, collection: this.Set);
            Assert.DoesNotContain(expected: node1, collection: this.Set);
        }
    }
}