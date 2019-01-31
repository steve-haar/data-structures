namespace DataStructures.BinaryHeap
{
    using System;
    using System.Linq;

    public class MinBinaryHeap<T> : BinaryHeap<T>
        where T : IComparable<T>
    {
        public MinBinaryHeap()
            : base((T a, T b) => b.CompareTo(a))
        {
        }

        public override IDynamicSetNode<T> Minimum()
        {
            return this.FirstOrDefault();
        }
    }
}