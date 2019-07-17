using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /*
     * Extension Methods, mostly related to randomization, a few others.
     * Many of these do not have any unit testing, but I have tested them
     * by other means and they seem to be correct.
     */
    public static class ExtensionMethods
    {
        /// <summary>
        /// Choose a subset from a set where each element has some weight that determines its likelihood of being selected.
        /// </summary>
        /// <typeparam name="T">The type of the set</typeparam>
        /// <param name="origset">The original set</param>
        /// <param name="subsetSize">How large the size to be selected should be</param>
        /// <param name="WeightOfElem">A function that will create a weight for each element</param>
        /// <param name="replace">True if it should be possible to select an element more than once</param>
        /// <returns>The subset of selected elements</returns>
        public static IEnumerable<T> ChooseBiasedSubset<T>(this IEnumerable<T> origset, int subsetSize,
            Func<T, double> WeightOfElem, bool replace = false)
        {
            List<T> Subset = new List<T>();

            var setItems = origset.Select(e => new {weight = WeightOfElem(e), elem = e}).ToList();

            double sumOfAllVals = setItems.Sum(i => i.weight);

            Random random = TSRandom.NextRandom();

            for (int i = 0; i < subsetSize; i++)
            {
                double curr = 0.0;
                double selectedVal = random.NextDouble() * sumOfAllVals;
                foreach (var item in setItems)
                {
                    if (curr <= selectedVal && curr + item.weight > selectedVal)
                    {
                        Subset.Add(item.elem);
                        if (!replace)
                        {
                            setItems.Remove(item);
                            sumOfAllVals -= item.weight;
                        }
                        break;
                    }
                    curr += item.weight;
                }
            }
            return Subset;
        }

        public static T ChooseBiasedElement<T>(this IEnumerable<T> set, Func<T, double> WeightOfElem) =>
            ChooseBiasedSubset(set, 1, WeightOfElem).First();

        public static T ChooseRandomElement<T>(this IEnumerable<T> set)
        {
            var arr = set.ToArray();
            return arr[TSRandom.Next(0, arr.Length)];
        }

        public static IEnumerable<T> ChooseRandomSubset<T>(this IEnumerable<T> set, int size, bool replace = false)
        {
            if (replace)
            {
                var arr = set.ToArray();
                return Enumerable.Range(0, size).Select(i => arr[TSRandom.Next(0, arr.Length)]);
            }

            return set.RandomlyShuffled().Take(size);
        }

        /// <summary>
        /// Get a copy of the IEnumerable back in a random order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        // Easier to just use OrderBy(e => random.Next()) but this is linear so a slight savings,
        // usually should just do the n log n version for simplicity.
        public static IEnumerable<T> RandomlyShuffled<T>(this IEnumerable<T> collection)
        {
            Random rand = TSRandom.NextRandom();

            var array = collection.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                int randIndex = rand.Next(i, array.Length);
                T tmp = array[randIndex];
                array[randIndex] = array[i];
                array[i] = tmp;
            }

            return array;
        }

        public static double LogGMean(this IEnumerable<double> set) => set.Average(d => Math.Log(d));
        public static double LogGMean(this IEnumerable<int> set) => set.Select(i => (double) i).LogGMean();
        public static double LogGMean(this IEnumerable<decimal> set) => set.Select(d => (double) d).LogGMean();
        public static double LogGMean<T>(this IEnumerable<T> set, Func<T, double> f) => set.Select(f).LogGMean();

        public static double GMean(this IEnumerable<double> set) => Math.Exp(set.LogGMean());
        public static double GMean(this IEnumerable<int> set) => Math.Exp(set.LogGMean());
        public static double GMean(this IEnumerable<decimal> set) => Math.Exp(set.LogGMean());
        public static double GMean<T>(this IEnumerable<T> set, Func<T, double> f) => set.Select(f).GMean();


        public static double DoubleBetween(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }

        public static HashSet<T> CreateAnonHashSet<T>(this IEnumerable<T> set)
        {
            return new HashSet<T>(set);
        }

        public static Stack<T> CreateEmptyAnonStack<T>(this IEnumerable<T> set)
        {
            return new Stack<T>();
        }

        public static Stack<T> CreateFullAnonStack<T>(this IEnumerable<T> set)
        {
            Stack<T> s = new Stack<T>();
            foreach (var v in set)
                s.Push(v);
            return s;
        }
    }
}
