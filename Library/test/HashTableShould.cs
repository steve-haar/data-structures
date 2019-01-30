namespace DataStructures.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using DataStructures.HashTable;
    using Xunit;

    public class HashTableShould : DynamicSetShould<HashTable<int>>
    {
        [Fact]
        public void FindNodeWithDuplicate2()
        {
            IDynamicSetNode<int> node1 = this.Set.Insert(1);
            IDynamicSetNode<int> node2 = this.Set.Insert(2);
            IDynamicSetNode<int> node3 = this.Set.Insert(3);
            IDynamicSetNode<int> node4 = this.Set.Insert(5);

            this.Set.Delete(node4);

            Assert.Null(this.Set.Search(5));
        }
    }
}