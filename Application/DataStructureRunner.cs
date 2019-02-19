namespace DataStructureRunner
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using DataStructures;

    public class DataStructureRunner<T>
        where T : IComparable<T>
    {
        private readonly IReadOnlyCollection<IDynamicSet<T>> sets;

        public DataStructureRunner(IEnumerable<IDynamicSet<T>> sets)
        {
            this.sets = sets.ToList();
        }

        public void ExecuteTask(Func<IDynamicSet<T>, double> action)
        {
            foreach (var set in this.sets)
            {
                double? speed = null;
                try
                {
                    speed = action(set);
                }
                catch
                {
                }

                string name = set.GetType().Name;
                string result = speed.HasValue ? speed.ToString() : "n/a";
                Console.WriteLine($"{name}: {result}");
            }
        }
    }
}