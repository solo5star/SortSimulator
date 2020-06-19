using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortSimulator.Models;

namespace SortSimulator.Models.Algorithms
{
    class BubbleSort : LazyAlgorithm
    {
        public IEnumerator<object> GetEnumerator(NumericArrayModel array)
        {
            for(int i = array.Length - 1; i >= 0; i--)
            {
                for(int j = 0; j < i; j++)
                {
                    bool b = array.IsGreaterThan(j, j + 1);
                    yield return null;
                    if(b)
                    {
                        array.Swap(j, j + 1);
                        yield return null;
                    }
                }
            }

            for(int i = 0; i < array.Length; i++)
            {
                array.Complete(i);
                yield return null;
            }
            yield break;
        }
    }
}
