namespace DataStructures
{
    using System;
    using System.Collections.Generic;

    public interface IDynamicSet<T> : IEnumerable<IDynamicSetNode<T>>
        where T : IComparable<T>
    {
        int Count();

        IDynamicSetNode<T> Insert(T value);

        void Delete(IDynamicSetNode<T> node);

        IDynamicSetNode<T> Search(T value);

        IDynamicSetNode<T> Minimum();

        IDynamicSetNode<T> Maximum();
    }
}