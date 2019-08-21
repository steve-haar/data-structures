namespace DataStructureRunner
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using DataStructures;
    using DataStructures.BinaryHeap;
    using DataStructures.BinarySearchTree;
    using DataStructures.DynamicArray;
    using DataStructures.HashTable;
    using DataStructures.LinkedList;
    using DataStructures.Queue;
    using DataStructures.Stack;
    using MoreLinq;

    public static class Program
    {
        public static void Main()
        {
            DataStructureRunner<int> runner = new DataStructureRunner<int>(GetSets<int>());
            runner.ExecuteTask(Search);
        }

        private static double Insertion(IDynamicSet<int> set)
        {
            Random random = new Random(0);
            int operations = 1000000;

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operations; i++)
            {
                set.Insert(random.Next(int.MinValue, int.MaxValue));
            }

            stopwatch.Stop();
            return operations / stopwatch.Elapsed.TotalSeconds;
        }

        private static double Deletion(IDynamicSet<int> set)
        {
            Random random = new Random(0);
            int operations = 1000000;

            System.Collections.Generic.IEnumerable<IDynamicSetNode<int>> nodes = Enumerable
                .Range(0, operations)
                    .Select(i => set.Insert(random.Next(int.MinValue, int.MaxValue)));

            System.Collections.Generic.IReadOnlyCollection<IDynamicSetNode<int>> shuffled = nodes.Shuffle().ToList();

            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (IDynamicSetNode<int> node in shuffled)
            {
                set.Delete(node);
            }

            stopwatch.Stop();
            return operations / stopwatch.Elapsed.TotalSeconds;
        }

        private static double Search(IDynamicSet<int> set)
        {
            Random random = new Random(0);
            int operations = 1000;

            for (int i = 0; i < 1000000; i++)
            {
                set.Insert(random.Next(int.MinValue, int.MaxValue));
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                set.Search(random.Next(int.MinValue, int.MaxValue));
            }

            stopwatch.Stop();
            return operations / stopwatch.Elapsed.TotalSeconds;
        }

        private static double Iteration(IDynamicSet<int> set)
        {
            Random random = new Random(0);
            int operations = 1000000;

            for (int i = 0; i < 100; i++)
            {
                set.Insert(random.Next(int.MinValue, int.MaxValue));
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operations; i++)
            {
                set.Count(e => e.Value % 2 == 0);
            }

            stopwatch.Stop();
            return operations / stopwatch.Elapsed.TotalSeconds;
        }

        private static System.Collections.Generic.IEnumerable<IDynamicSet<T>> GetSets<T>()
            where T : IComparable<T>
        {
            yield return new DynamicArray<T>();
            yield return new Stack<T>();
            yield return new Queue<T>();
            yield return new LinkedList<T>();
            yield return new HashTable<T>();
            yield return new MinBinaryHeap<T>();
            yield return new MaxBinaryHeap<T>();
            yield return new BinarySearchTree<T>();
        }
    }
}