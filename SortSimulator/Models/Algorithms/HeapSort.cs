using SortSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortSimulator.Models.Algorithms
{
    class HeapSort : LazyAlgorithm
    {
        public IEnumerator<object> GetEnumerator(NumericArrayModel array)
        {
            int n = array.Length;

            // Build max heap
            for(int i = n / 2 - 1; i >= 0; i--)
            {
                var e = Heapify(array, n, i);
                while (e.MoveNext()) yield return null;
            }

            for(int i = n - 1; i >= 0; i--)
            {
                array.Swap(0, i);

                var e = Heapify(array, i, 0);
                while (e.MoveNext()) yield return null;
            }



            for (int i = 0; i < array.Length; i++)
            {
                array.Complete(i);
                yield return null;
            }
            yield break;
        }

        private IEnumerator<object> Heapify(NumericArrayModel array, int n, int i)
        {
            // Find largest among root, left child and right child
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            bool b;
            
            if (l < n)
            {
                b = array.IsGreaterThan(l, largest);
                yield return null;
                if (b)
                {
                    largest = l;
                }
            }
            
            if(r < n)
            {
                b = array.IsGreaterThan(r, largest);
                yield return null;
                if (b)
                {
                    largest = r;
                }
            }

            if(largest != i)
            {
                array.Swap(i, largest);
                yield return null;

                var e = Heapify(array, n, largest);
                while (e.MoveNext()) yield return null;
            }
            yield break;
        }
    }
}
