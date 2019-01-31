namespace DataStructures.BinaryHeap
{
    using System;
    using System.Linq;

    public class MaxBinaryHeap<T> : BinaryHeap<T>
        where T : IComparable<T>
    {
        public MaxBinaryHeap()
            : base((T a, T b) => a.CompareTo(b))
        {
        }

        public override IDynamicSetNode<T> Maximum()
        {
            return this.FirstOrDefault();
        }
    }
}