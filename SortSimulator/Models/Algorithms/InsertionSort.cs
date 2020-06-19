using SortSimulator.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortSimulator.Models.Algorithms
{
    public class InsertionSort : LazyAlgorithm
    {
        public IEnumerator<object> GetEnumerator(NumericArrayModel array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    bool b = array.IsGreaterThan(j - 1, j);
                    yield return null;
                    if (b)
                    {
                        array.Swap(j - 1, j);
                        yield return null;
                    }
                    else
                    {
                        break;
                    }
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
