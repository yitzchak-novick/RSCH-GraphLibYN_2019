using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KattisAPlusB
{
    class Program
    {
        public class ValueList
        {
            public int[] array = new int[100001];

            public int this[int i] => array[i + 50000];

            public void inc(int i) => array[i + 50000]++;
        }
        static void Main(string[] args)
        {
            ValueList counts = new ValueList();
            int numVals = int.Parse(Console.ReadLine());
            string[] line = Console.ReadLine().Split(' ');
            for (int i = 0; i < numVals; i++)
            {
                int val = int.Parse(line[i]);
                counts.inc(val);
            }

            long answer = 0;

            // positive and positive
            for (int i = 1; i <= 25000; i++)
            {
                // deal with duplicates of the number itself
                answer += counts[i] * (counts[i] - 1) * counts[i + i];
                // deal with zero plus the number
                answer += 2 * counts[0] + counts[i] + (counts[i] - 1);
                // deal with all other numbers to which this can be added
                for (int j = i + 1; j <= 50000 - i; j++)
                    answer += 2 * counts[i] * counts[j] * counts[i + j];
            }

            // negative and negative
            for (int i = -1; i >= -25000; i--)
            {
                // deal with duplicates of the number itself
                answer += counts[i] * (counts[i] - 1) * counts[i + i];
                // deal with zero plus the number
                answer += 2 * counts[0] + counts[i] + (counts[i] - 1);
                // deal with all other numbers to which this can be added
                for (int j = i - 1; j >= -50000 - i; j--)
                    answer += 2 * counts[i] * counts[j] * counts[i + j];
            }

            // zero plus zero
            answer += counts[0] * (counts[0] - 1) * (counts[0] - 2);

          // positive and negative
            for (int i = 1; i <= 50000; i++)
            for (int j = -1; j >= -50000; j--)
            {
                int result = counts[i + j];
                answer += 2 * counts[i] * counts[j] * result;
            }

            Console.WriteLine(answer);
            Console.ReadKey();
        }
    }
}
