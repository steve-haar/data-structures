namespace DataStructures.Tests
{
    using System;
    using DataStructures.Stack;
    using Xunit;

    public class StackShould : DynamicSetShould<Stack<int>>
    {
        [Fact]
        public void ThrowExceptionWhenDeletingNotLastNode()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(1);
            IDynamicSetNode<int> node2 = this.Set.Insert(2);
            IDynamicSetNode<int> node3 = this.Set.Insert(3);

            Assert.Throws<InvalidOperationException>(() => this.Set.Delete(node1));
            Assert.Throws<InvalidOperationException>(() => this.Set.Delete(node2));
        }

        [Fact]
        public override void RemoveNodeOnDeletion()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(1);
            IDynamicSetNode<int> node2 = this.Set.Insert(2);
            IDynamicSetNode<int> node3 = this.Set.Insert(3);

            this.Set.Delete(node3);

            Assert.Contains(expected: node1, collection: this.Set);
            Assert.Contains(expected: node2, collection: this.Set);
            Assert.DoesNotContain(expected: node3, collection: this.Set);
        }
    }
}