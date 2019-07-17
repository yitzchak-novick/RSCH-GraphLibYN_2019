using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class UnionFind<T>
    {
        private int nextId = 0;
        private Dictionary<int, HashSet<T>> elementSets = new Dictionary<int, HashSet<T>>();
        private Dictionary<T, int> elementIds = new Dictionary<T, int>();
        private int maxSetCount = 0;

        public void AddElement(T elem)
        {
            if (!IsPresent(elem))
            {
                int currId = nextId++;
                HashSet<T> newSet = new HashSet<T>();
                newSet.Add(elem);
                elementSets.Add(currId, newSet);
                elementIds.Add(elem, currId);

                if (maxSetCount == 0)
                    maxSetCount = 1;
            }
        }

        public void AddElements(IEnumerable<T> elems)
        {
            foreach (var elem in elems)
            {
                AddElement(elem);
            }
        }

        public void Union(T elem1, T elem2, bool addIfNotPresent = false)
        {
            if (addIfNotPresent)
            {
                if (!IsPresent(elem1))
                    AddElement(elem1);
                if (!IsPresent(elem2))
                    AddElement(elem2);
            }
            else
            {
                if (!IsPresent(elem1))
                    throw new Exception("Requested element in Union is not present " + elem1);
                if (!IsPresent(elem2))
                    throw new Exception("Requested element in Union is not present " + elem2);
            }

            if (elementIds[elem1] == elementIds[elem2])
                return;

            int smallerId, largerId;
            if (elementSets[elementIds[elem1]].Count < elementSets[elementIds[elem2]].Count)
            {
                smallerId = elementIds[elem1];
                largerId = elementIds[elem2];
            }
            else
            {
                smallerId = elementIds[elem2];
                largerId = elementIds[elem1];
            }

            foreach (var elem in elementSets[smallerId])
            {
                elementSets[largerId].Add(elem);
                elementIds[elem] = largerId;
            }

            elementSets.Remove(smallerId);

            if (elementSets[largerId].Count > maxSetCount)
                maxSetCount = elementSets[largerId].Count;

        }

        public bool IsPresent(T elem)
        {
            return elementIds.ContainsKey(elem);
        }

        public int Find(T elem)
        {
            if (!IsPresent(elem))
                throw new Exception("Requested element is not present in UnionFind collection");
            return elementIds[elem];
        }

        public bool Find(T elem1, T elem2)
        {
            return Find(elem1) == Find(elem2);
        }

        public int GetMaxSetCount()
        {
            return maxSetCount;
        }
    }
}
