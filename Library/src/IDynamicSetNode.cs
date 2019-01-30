namespace DataStructures
{
    using System;

    public interface IDynamicSetNode<T>
        where T : IComparable<T>
    {
        T Value { get; }
    }
}