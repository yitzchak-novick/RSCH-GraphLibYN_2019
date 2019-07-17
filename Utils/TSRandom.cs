using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /*
     * ThreadSafe Random class. Multiple threads can get random numbers without interfering with each other.
     * The method NextRandom gives a new Random object that is seeded by the Random object in this class.
     * The other static methods give random ints and doubles. But the methods that give values should be used
     * sparingly, they lock on every call. Wherever possible, it'd be better to use NextRandom to get a new
     * Random object and then use that object without locks to generate multiple values.
     * 
     */
    public class TSRandom
    {
        private static Random random = new Random();
        private static object RandomLock = new object();

        /// <summary>
        /// Gives a new Random object that is seeded by a random number, WARNING: Locks on every call
        /// </summary>
        /// <returns>A new Random object</returns>
        public static Random NextRandom()
        {
            Random r;
            lock (RandomLock)
                r = new Random(random.Next());
            return r;
        }

        /// <summary>
        /// Returns a random integer, WARNING: Locks on every call
        /// </summary>
        /// <returns>A random int</returns>
        public static int Next() => NextRandom().Next();
        /// <summary>
        /// Returns a random integer, WARNING: Locks on every call
        /// </summary>
        /// <param name="maxValue">The upper limit (exclusive)</param>
        /// <returns>A random integer</returns>
        public static int Next(int maxValue) => NextRandom().Next(maxValue);
        /// <summary>
        /// Returns a random integer, WARNING: Locks on every call
        /// </summary>
        /// <param name="minValue">The lower limit (inclusive)</param>
        /// <param name="maxValue">The upper limit (exclusive)</param>
        /// <returns>A random integer in the specified range</returns>
        public static int Next(int minValue, int maxValue) => NextRandom().Next(minValue, maxValue);

        /// <summary>
        /// Returns a random double, WARNING: locks on every call
        /// </summary>
        /// <returns>A random double in the range of 0 - 1 (0 inclusive, 1 exclusive)</returns>
        public static double NextDouble() => NextRandom().NextDouble();
        /// <summary>
        /// Returns a random double in the specified range WARNING: Locks on every call
        /// </summary>
        /// <param name="min">The lower limit (inclusive)</param>
        /// <param name="max">The upper limit (exclusive)</param>
        /// <returns>A random double in the specified range</returns>
        public static double DoubleBetween(double min, double max) => NextRandom().DoubleBetween(min, max);
    }
}
