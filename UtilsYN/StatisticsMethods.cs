using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsYN
{
    public static class StatisticsMethods
    {
        public static SortedList<double, double> GetCurvedDataPoints(SortedList<double, double> origValues, int numBins)
        {
            SortedList<double, double> results = new SortedList<double, double>();
            var keys = origValues.Keys.ToList();
            var values = origValues.Values.ToList();

            var inc = (keys[keys.Count - 1] - keys[0]) / (numBins - 1);

            var prevIndex = 0;
            var currIndex = 0;
            results.Add(keys[0], values[0]);

            var currResultKey = keys[0];

            for (int i = 0; i < numBins - 2; i++)
            {
                currResultKey = currResultKey + inc;
                while (!(keys[prevIndex] <= currResultKey && keys[currIndex] >= currResultKey))
                {
                    prevIndex = currIndex;
                    currIndex++;
                }

                var prevActualKey = keys[prevIndex];
                var currActualKey = keys[currIndex];
                var prevActualValue = values[prevIndex];
                var currActualValue = values[currIndex];

                var currResultValue =
                    (currResultKey - prevActualKey) / (currActualKey - prevActualKey) *
                    (currActualValue - prevActualValue) + prevActualValue;

                results.Add(currResultKey, currResultValue);
            }

            results.Add(keys[keys.Count - 1], values[values.Count - 1]);

            return results;
        }
    }
}
