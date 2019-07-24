using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilsYN;

namespace UtilsYN_TESTS
{
    [TestClass]
    public class UnionFindTests
    {
        private UnionFind<String> StringUnionFind;
        private UnionFind<char> CharUnionFind; // to test primitives specificallly
        private UnionFind<Stack<string>> StackUnionFind; // to test objects specifically

        [TestInitialize]
        public void Setup()
        {
            StringUnionFind = new UnionFind<string>();
            CharUnionFind = new UnionFind<char>();
            StackUnionFind = new UnionFind<Stack<string>>();
        }

        [TestMethod]
        public void AddedSingletonElementIsPresent()
        {
            StringUnionFind.AddElement("elem1");
            Assert.IsTrue(StringUnionFind.IsPresent("elem1"));
        }

        [TestMethod]
        public void FindWorksOnAddedSingleton()
        {
            StringUnionFind.AddElement("elem1");
            var x = StringUnionFind.Find("elem1"); // make sure no exception thrown
        }

        [TestMethod]
        public void UnaddedElemIsNotFound()
        {
            StringUnionFind.AddElement("elem1");
            Assert.IsFalse(StringUnionFind.IsPresent("elem2"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FindThrowsExceptionOnUnfoundElement()
        {
            StringUnionFind.AddElement("elem1");
            var x = StringUnionFind.Find("elem2");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FindOnTwoElementsThrowsExceptionIfOneIsNotFound()
        {
            StringUnionFind.AddElement("e1");
            StringUnionFind.AddElement("e2");
            StringUnionFind.Find("e3", "e1");
        }

        [TestMethod]
        public void AddedElementsAreAllPresent()
        {
            StringUnionFind.AddElements(new List<string> {"e1", "e2", "e3"});
            foreach (string s in new List<string> {"e1", "e2", "e3"})
            {
                Assert.IsTrue(StringUnionFind.IsPresent(s));
            }
        }

        [TestMethod]
        public void FindWorksOnAllAddedElements()
        {
            StringUnionFind.AddElements(new List<string> { "e1", "e2", "e3" });
            foreach (string s in new List<string> { "e1", "e2", "e3" })
            {
                var x = StringUnionFind.Find(s); // No exception
                Console.Write(x); // Make sure the compiler doesn't ignore the previous call because it isn't used
            }
        }

        [TestMethod]
        public void AddingTheSameStringTwiceDoesNotAddItAgain()
        {
            StringUnionFind.AddElement("elem1");
            var setId = StringUnionFind.Find("elem1");
            StringUnionFind.AddElement("elem1");
            Assert.AreEqual(setId, StringUnionFind.Find("elem1"));
        }

        [TestMethod]
        public void AddingTheSamePrimitiveTwiceDoesNotAddItAgain()
        {
            CharUnionFind.AddElement('a');
            var setId = CharUnionFind.Find('a');
            CharUnionFind.AddElement('a');
            Assert.AreEqual(setId, CharUnionFind.Find('a'));
        }

        [TestMethod]
        public void AddingTheSameObjectTwiceDoesNotAddItAgain()
        {
            Stack<string> stack = new Stack<string>();
            stack.Push("string");
            StackUnionFind.AddElement(stack);
            var setId = StackUnionFind.Find(stack);
            StackUnionFind.AddElement(stack);
            Assert.AreEqual(setId, StackUnionFind.Find(stack));
        }

        [TestMethod]
        public void TwoAddedStringsAreNotInTheSameSet()
        {
            StringUnionFind.AddElement("elem1");
            StringUnionFind.AddElement("elem2");
            Assert.IsFalse(StringUnionFind.Find("elem1", "elem2"));
        }

        [TestMethod]
        public void TwoAddedPrimitivesAreNotInTheSameSet()
        {
            CharUnionFind.AddElement('a');
            CharUnionFind.AddElement('b');
            Assert.IsFalse(CharUnionFind.Find('a', 'b'));
        }

        [TestMethod]
        public void TwoAddedObjectsAreNotInTheSameSet()
        {
            Stack<string> s1 = new Stack<string>(); 
            Stack<string> s2 = new Stack<string>();
            s1.Push("string");
            s2.Push("string");

            StackUnionFind.AddElement(s1);
            StackUnionFind.AddElement(s2);
            Assert.IsFalse(StackUnionFind.Find(s1, s2));
        }

        [TestMethod]
        public void UnionCombinesTwoStrings()
        {
            StringUnionFind.AddElement("elem1");
            StringUnionFind.AddElement("elem2");
            Assert.IsFalse(StringUnionFind.Find("elem1", "elem2"));
            StringUnionFind.Union("elem1", "elem2");
            Assert.IsTrue(StringUnionFind.Find("elem1", "elem2"));
        }

        [TestMethod]
        public void UnionCombinesTwoPrimitives()
        {
            CharUnionFind.AddElement('a');
            CharUnionFind.AddElement('b');
            Assert.IsFalse(CharUnionFind.Find('a', 'b'));
            CharUnionFind.Union('a', 'b');
            Assert.IsTrue(CharUnionFind.Find('a', 'b'));
        }

        [TestMethod]
        public void UnionCombinesTwoObjects()
        {
            Stack<string> s1 = new Stack<string>();
            Stack<string> s2 = new Stack<string>();
            s1.Push("s1");
            s2.Push("s2");

            StackUnionFind.AddElement(s1);
            StackUnionFind.AddElement(s2);
            Assert.IsFalse(StackUnionFind.Find(s1, s2));
            StackUnionFind.Union(s1, s2);
            Assert.IsTrue(StackUnionFind.Find(s1, s2));
        }

        [TestMethod]
        public void UnionAddsAndCombinesNewString()
        {
            StringUnionFind.AddElement("elem1");
            StringUnionFind.Union("elem1", "elem2", true);
            Assert.IsTrue(StringUnionFind.IsPresent("elem2"));
            Assert.IsTrue(StringUnionFind.Find("elem1", "elem2"));

            // Test the first argument also
            StringUnionFind.Union("elem3", "elem2", true);
            Assert.IsTrue(StringUnionFind.IsPresent("elem3"));
            Assert.IsTrue(StringUnionFind.Find("elem1", "elem3"));
        }

        [TestMethod]
        public void UnionAddsAndCombinesTwoNewStrings()
        {
            StringUnionFind.Union("elem1", "elem2", true);
            Assert.IsTrue(StringUnionFind.IsPresent("elem1"));
            Assert.IsTrue(StringUnionFind.IsPresent("elem2"));
            Assert.IsTrue(StringUnionFind.Find("elem1", "elem2"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UnionThrowsExceptionIfDontAddIsSelected()
        {
            StringUnionFind.AddElement("elem1");
            StringUnionFind.Union("elem2", "elem1");
        }

        [TestMethod]
        public void UnionAddsSmallerSetToLarger()
        {
            StringUnionFind.AddElements(new[] {"e1", "e2", "e3"});
            StringUnionFind.Union("e1", "e2");
            var biggerId = StringUnionFind.Find("e1");
            StringUnionFind.Union("e3", "e2");
            Assert.AreEqual(biggerId, StringUnionFind.Find("e3"));
        }

        [TestMethod]
        public void GetLargestSetCountIsZeroOnNew()
        {
            Assert.AreEqual(0, StringUnionFind.GetMaxSetCount());
        }

        [TestMethod]
        public void GetLargestSetCountIsOneAfterAddingSingletons()
        {
            StringUnionFind.AddElements(new[] { "e1", "e2", "e3" });
            Assert.AreEqual(1, StringUnionFind.GetMaxSetCount());
        }

        [TestMethod]
        public void GetLargestSetCountWorksAfterMultipleUnions()
        {
            StringUnionFind.AddElements(new[] {"e1", "e2", "e3", "e4", "e5", "e6", "e7", "e8"});
            StringUnionFind.Union("e1", "e5");
            StringUnionFind.Union("e4", "e6");
            StringUnionFind.Union("e7", "e8");
            StringUnionFind.Union("e8", "e5");
            Assert.AreEqual(4, StringUnionFind.GetMaxSetCount());
        }
    }
}
