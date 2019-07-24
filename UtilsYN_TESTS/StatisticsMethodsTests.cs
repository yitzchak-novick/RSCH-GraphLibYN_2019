using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stats =  UtilsYN.StatisticsMethods;

namespace UtilsYN_TESTS
{
    [TestClass]
    public class StatisticsMethodsTests
    {
        private static readonly double TOLERANCE = 0.000001;

        private SortedList<double, double> sampleValues_01;

        [TestInitialize]
        public void Setup()
        {
            sampleValues_01 = new SortedList<double, double>();
            sampleValues_01.Add(0.0, 11.0);
            sampleValues_01.Add(1.0, 10.0);
            sampleValues_01.Add(2.0, 9.0);
            sampleValues_01.Add(3.0, 8.0);
            sampleValues_01.Add(4.0, 7.0);
            sampleValues_01.Add(5.0, 4.0);
            sampleValues_01.Add(6.0, 2.0);
            sampleValues_01.Add(7.0, 2.0);
            sampleValues_01.Add(8.0, 1.0);
        }

        [TestMethod]
        public void SameBinsAsInput()
        {
            var results = Stats.GetCurvedDataPoints(sampleValues_01, 9);
            var iterator = results.GetEnumerator();
            iterator.MoveNext();

            var current = iterator.Current;
            Assert.AreEqual(0.0, current.Key, TOLERANCE);
            Assert.AreEqual(11.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(1.0, current.Key, TOLERANCE);
            Assert.AreEqual(10.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(2.0, current.Key, TOLERANCE);
            Assert.AreEqual(9.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(3.0, current.Key, TOLERANCE);
            Assert.AreEqual(8.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(4.0, current.Key, TOLERANCE);
            Assert.AreEqual(7.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(5.0, current.Key, TOLERANCE);
            Assert.AreEqual(4.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(6.0, current.Key, TOLERANCE);
            Assert.AreEqual(2.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(7.0, current.Key, TOLERANCE);
            Assert.AreEqual(2.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(8.0, current.Key, TOLERANCE);
            Assert.AreEqual(1.0, current.Value, TOLERANCE);

            Assert.IsFalse(iterator.MoveNext());
        }

        [TestMethod]
        public void SevenBinsFromNine()
        {
            var results = Stats.GetCurvedDataPoints(sampleValues_01, 7);
            var iterator = results.GetEnumerator();
            iterator.MoveNext();

            var current = iterator.Current;
            Assert.AreEqual(0.0, current.Key, TOLERANCE);
            Assert.AreEqual(11.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(1.3333333333333, current.Key, TOLERANCE);
            Assert.AreEqual(9.6666666666667, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(2.6666666666667, current.Key, TOLERANCE);
            Assert.AreEqual(8.3333333333333, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(4.0, current.Key, TOLERANCE);
            Assert.AreEqual(7.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(5.33333333333333, current.Key, TOLERANCE);
            Assert.AreEqual(3.33333333333333, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(6.66666666666667, current.Key, TOLERANCE);
            Assert.AreEqual(2.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(8.0, current.Key, TOLERANCE);
            Assert.AreEqual(1.0, current.Value, TOLERANCE);

            Assert.IsFalse(iterator.MoveNext());
        }

        [TestMethod]
        public void TwelveBinsFromNine()
        {
            var results = Stats.GetCurvedDataPoints(sampleValues_01, 12);
            var iterator = results.GetEnumerator();
            iterator.MoveNext();

            var current = iterator.Current;
            Assert.AreEqual(0.0, current.Key, TOLERANCE);
            Assert.AreEqual(11.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(0.7272727272727, current.Key, TOLERANCE);
            Assert.AreEqual(10.2727272727272, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(1.454545454545455, current.Key, TOLERANCE);
            Assert.AreEqual(9.545454545454545, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(2.181818181818182, current.Key, TOLERANCE);
            Assert.AreEqual(8.818181818181818, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(2.909090909090909, current.Key, TOLERANCE);
            Assert.AreEqual(8.090909090909091, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(3.636363636363636, current.Key, TOLERANCE);
            Assert.AreEqual(7.363636363636364, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(4.363636363636364, current.Key, TOLERANCE);
            Assert.AreEqual(5.909090909090909, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(5.090909090909091, current.Key, TOLERANCE);
            Assert.AreEqual(3.818181818181818, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(5.818181818181819, current.Key, TOLERANCE);
            Assert.AreEqual(2.363636363636364, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(6.545454545454545, current.Key, TOLERANCE);
            Assert.AreEqual(2.0, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(7.272727272727273, current.Key, TOLERANCE);
            Assert.AreEqual(1.727272727272727, current.Value, TOLERANCE);

            iterator.MoveNext();
            current = iterator.Current;
            Assert.AreEqual(8.0, current.Key, TOLERANCE);
            Assert.AreEqual(1.0, current.Value, TOLERANCE);

            Assert.IsFalse(iterator.MoveNext());
        }
    }
}
