using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortSimulator.Models;

namespace SortSimulator.Models.Algorithms
{
    class QuickSort : LazyAlgorithm
    {
        public IEnumerator<object> GetEnumerator(NumericArrayModel array)
        {
            IEnumerator<object> sort = Sort(array, 0, array.Length - 1);
            while(sort.MoveNext()) { yield return null; }


            for (int i = 0; i < array.Length; i++)
            {
                array.Complete(i);
                yield return null;
            }
            yield break;
        }

        private IEnumerator<object> Sort(NumericArrayModel array, int left, int right)
        {
            if(left < right)
            {
                int pivot = left;

                int i = left;

                for(int j = left + 1; j <= right; j++)
                {
                    bool b = array.IsGreaterThan(pivot, j);
                    yield return null;
                    if (b)
                    {
                        i++;
                        array.Swap(j, i);
                        yield return null;
                    }
                }
                array.Swap(left, i);
                yield return null;

                pivot = i;

                IEnumerator<object> sort1 = Sort(array, left, pivot - 1);
                while (sort1.MoveNext()) { yield return null; }
                IEnumerator<object> sort2 = Sort(array, pivot + 1, right);
                while (sort2.MoveNext()) { yield return null; }
            }
            yield break;
        }
    }
}
