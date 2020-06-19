using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortSimulator.Models;

namespace SortSimulator.Models.Algorithms
{
    class SelectionSort : LazyAlgorithm
    {
        public IEnumerator<object> GetEnumerator(NumericArrayModel array)
        {
            for(int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for(int j = i; j < array.Length; j++)
                {
                    bool b = array.IsGreaterThan(minIndex, j);
                    yield return null;
                    if (b)
                    {
                        minIndex = j;
                    }
                }
                if(minIndex != i)
                {
                    array.Swap(minIndex, i);
                    yield return null;
                }
            }


            for (int i = 0; i < array.Length; i++)
            {
                array.Complete(i);
                yield return null;
            }
            yield break;
        }
    }
}
