using System;
using System.Collections.Generic;
using GraphUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static GraphUtils.DisintegrationTracker;

namespace GraphUtils_TESTS
{
    [TestClass]
    public class DisintegrationTrackerTests
    {
        /*
        private static readonly double TOLERANCE = 0.000001;

        private DisintegrationTracker tracker;

        [TestInitialize]
        public void Setup()
        {
            tracker = new DisintegrationTracker();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void AddingAnInoculationValueThatIsLessThanPreviousThrowsException()
        {
            tracker.AddStep(3, 12, 7, new Tuple<string, string>[] { });
            tracker.AddStep(2, 13, 17, new Tuple<string, string>[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void AddingAnInterviewValueThatIsLessThanPreviousThrowsException()
        {
            tracker.AddStep(3, 12, 7, new Tuple<string, string>[] { });
            tracker.AddStep(5, 10, 17, new Tuple<string, string>[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void AddingAVerticesInterviewedValueThatIsLessThanPreviousThrowsException()
        {
            tracker.AddStep(3, 12, 7, new Tuple<string, string>[] { });
            tracker.AddStep(5, 13, 5, new Tuple<string, string>[] { });
        }

        // This method will simulate a disintegration, then other methods can test the results with
        // different ranges, costs, etc.
        private void SimulateADisintegration()
        {
            #region EXPLANATION

            /*
            * Here is the disintegration being simulated:
            *
            *  Graph:
            *
            *  0
            *  1
            *  2
            *  3
            *  4
            *  5
            *  6
            *  7
            *  8
            *  9
            *  10
            *  0 2
            *  0 4
            *  0 5
            *  1 4
            *  1 5
            *  2 3
            *  2 4
            *  4 5
            *  8 4
            *  8 6
            *  6 0
            *  1 8
            *  7 3
            *  9 3
            *  10 9
            *  10 7
            *  6 10
            *  7 0
            *  1 6
            *  10 8
            *
            *  [] indicate what we will assume is the costs are in the form of: [Inoculations, Interviews, Vertices Interviewed]
            *
            *  [0, 0 , 0]: Max Comp: 11
            *  1) Remove 7: [1, 2, 1] (7, 10) (7, 3) (7, 0) Max: 10
            *  2) Remove 8: [2, 3, 2] (8, 10) (8, 6) (8, 1) (8, 4) Max: 9
            *  3) Remove 0: [3, 5, 2] (0, 6) (0, 5) (0, 4) (0, 2) Max: 8
            *  4) Remove 6: [4, 7, 4] (6, 10) (6, 1) Max: 7
            *  5) Remove 3: [5, 8, 5] (3, 9) (3, 2) Max: 4
            *  6) Remove 4: [6, 8, 5] (4, 1) (4, 2) (4, 5) Max: 2
            *  7) Remove 2: [6, 8, 5] Max: 2
            *  8) Remove 1: [7, 8, 6] (1, 5) Max: 2
            *  9) Remove 5: [7, 8, 6] Max: 2
            *  10) Remove 9: [8, 9, 8] (9, 10) Max: 1
            *  11) Remove 10: [8, 10, 9] Max: 1
            /* END COMMENT HERE

            #endregion

            tracker.AddStep(1, 2, 1, new List<Tuple<string, string>>
            {
                new Tuple<string, string>("7", "10"),
                new Tuple<string, string>("7", "3"),
                new Tuple<string, string>("7", "0")
            });
            tracker.AddStep(2, 3, 2, new List<Tuple<string, string>>
            {
                new Tuple<string, string>("8", "10"),
                new Tuple<string, string>("8", "6"),
                new Tuple<string, string>("8", "1"),
                new Tuple<string, string>("8", "4")
            });
            tracker.AddStep(3, 5, 2, new List<Tuple<string, string>>
            {
                new Tuple<string, string>("0", "6"),
                new Tuple<string, string>("0", "5"),
                new Tuple<string, string>("0", "4"),
                new Tuple<string, string>("0", "2")
            });
            tracker.AddStep(4, 7, 4, new List<Tuple<string, string>>
            {
                new Tuple<string, string>("6", "10"),
                new Tuple<string, string>("6", "1")
            });
            tracker.AddStep(5, 8, 5, new List<Tuple<string, string>>
            {
                new Tuple<string, string>("3", "9"),
                new Tuple<string, string>("3", "2")
            });
            tracker.AddStep(5, 8, 5, new List<Tuple<string, string>>
            {
                new Tuple<string, string>("3", "9"),
                new Tuple<string, string>("3", "2")
            });
            tracker.AddStep(6, 8, 5, new List<Tuple<string, string>>
            {
                new Tuple<string, string>("4", "1"),
                new Tuple<string, string>("4", "2"),
                new Tuple<string, string>("4", "5")
            });
            tracker.AddStep(6, 8, 5, new List<Tuple<string, string>>
            {
            });
            tracker.AddStep(7, 8, 6, new List<Tuple<string, string>>
            {
                new Tuple<string, string>("1", "5")
            });
            tracker.AddStep(7, 8, 6, new List<Tuple<string, string>> { });
            tracker.AddStep(8, 9, 8, new List<Tuple<string, string>>
            {
                new Tuple<string, string>("9", "10")
            });
            tracker.AddStep(8, 10, 9, new List<Tuple<string, string>> { });
        }

        [TestMethod]
        public void MinKVPWorks()
        {
            SimulateADisintegration();
            KeyValuePair<int, int> MinKvp;

            MinKvp = tracker.GetMinKeyValuePair(COSTTYPE.TotalInoculations);
            Assert.AreEqual(0, MinKvp.Key);
            Assert.AreEqual(11, MinKvp.Value);
            MinKvp = tracker.GetMinKeyValuePair(COSTTYPE.TotalInterviews);
            Assert.AreEqual(0, MinKvp.Key);
            Assert.AreEqual(11, MinKvp.Value);
            MinKvp = tracker.GetMinKeyValuePair(COSTTYPE.TotalVerticesInterviewed);
            Assert.AreEqual(0, MinKvp.Key);
            Assert.AreEqual(11, MinKvp.Value);
        }

        [TestMethod]
        public void MaxKVPWorks()
        {
            SimulateADisintegration();
            KeyValuePair<int, int> MaxKvp;

            MaxKvp = tracker.GetMaxKeyValuePair(COSTTYPE.TotalInoculations);
            Assert.AreEqual(8, MaxKvp.Key);
            Assert.AreEqual(1, MaxKvp.Value);
            MaxKvp = tracker.GetMaxKeyValuePair(COSTTYPE.TotalInterviews);
            Assert.AreEqual(10, MaxKvp.Key);
            Assert.AreEqual(1, MaxKvp.Value);
            MaxKvp = tracker.GetMaxKeyValuePair(COSTTYPE.TotalVerticesInterviewed);
            Assert.AreEqual(9, MaxKvp.Key);
            Assert.AreEqual(1, MaxKvp.Value);
        }

        [TestMethod]
        public void SimpleDisintegrationWorksForInoculations()
        {
            SimulateADisintegration();
            var results = tracker.GetDisintegrationResults((mn, mx) => mx - mn + 1, (i, j, k) => i);
            using (var iterator = results.GetEnumerator())
            {
                var kvp = iterator.Current;
                Assert.AreEqual(0.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(11.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(1.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(10.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(2.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(9.0, kvp.Value, TOLERANCE);


                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(3.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(8.0, kvp.Value, TOLERANCE);


                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(4.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(7.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(5.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(4.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(6.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(2.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(7.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(2.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(8.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(1.0, kvp.Value, TOLERANCE);

                Assert.IsFalse(iterator.MoveNext());
            }
        }

        [TestMethod]
        public void SimpleDisintegrationWorksForInoculationsWith7Bins()
        {
            SimulateADisintegration();
            var results = tracker.GetDisintegrationResults((mn, mx) => 7, (i, j, k) => i);
            using (var iterator = results.GetEnumerator())
            {
                var kvp = iterator.Current;
                Assert.AreEqual(0.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(11.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(1.33333333333333, kvp.Key, TOLERANCE);
                Assert.AreEqual(9.66666666666667, kvp.Value, TOLERANCE);


                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(2.66666666666667, kvp.Key, TOLERANCE);
                Assert.AreEqual(8.33333333333333, kvp.Value, TOLERANCE);


                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(4.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(7.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(5.33333333333333, kvp.Key, TOLERANCE);
                Assert.AreEqual(3.33333333333333, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(6.66666666666667, kvp.Key, TOLERANCE);
                Assert.AreEqual(2.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(8.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(1.0, kvp.Value, TOLERANCE);

                Assert.IsFalse(iterator.MoveNext());
            }
        }

        [TestMethod]
        public void SimpleDisintegrationWorksForInoculationsPlusHalfVerticesInterviewedWith8Bins()
        {
            SimulateADisintegration();
            var results = tracker.GetDisintegrationResults((mn, mx) => mx - mn, (i, j, k) => i + 0.5 * k);
            using (var iterator = results.GetEnumerator())
            {
                var kvp = iterator.Current;
                Assert.AreEqual(0.0, kvp.Key, TOLERANCE);
                Assert.AreEqual(11.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(1.785714286, kvp.Key, TOLERANCE);
                Assert.AreEqual(9.80952381, kvp.Value, TOLERANCE);


                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(3.571428571, kvp.Key, TOLERANCE);
                Assert.AreEqual(8.428571429, kvp.Value, TOLERANCE);


                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(5.357142857, kvp.Key, TOLERANCE);
                Assert.AreEqual(7.321428571, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(7.142857143, kvp.Key, TOLERANCE);
                Assert.AreEqual(4.714285714, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(8.928571429, kvp.Key, TOLERANCE);
                Assert.AreEqual(2.0, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(10.71428571, kvp.Key, TOLERANCE);
                Assert.AreEqual(1.642857143, kvp.Value, TOLERANCE);

                iterator.MoveNext();
                kvp = iterator.Current;
                Assert.AreEqual(12.5, kvp.Key, TOLERANCE);
                Assert.AreEqual(1.0, kvp.Value, TOLERANCE);

                Assert.IsFalse(iterator.MoveNext());
            }
        }
        */
    }
}
