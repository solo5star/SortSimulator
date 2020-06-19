using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortSimulator.Models;

namespace SortSimulator.Models.Algorithms
{
    class CocktailSort : LazyAlgorithm
    {
        public IEnumerator<object> GetEnumerator(NumericArrayModel array)
        {
            int i;
            int left;
            int shift = -1;
            int right = array.Length - 1;

            while((left = shift) < right)
            {
                for(i = left + 1; i < right; i++)
                {
                    bool b = array.IsGreaterThan(i, i + 1);
                    yield return null;

                    if (b)
                    {
                        array.Swap(i, i + 1);
                        yield return null;

                        shift = i;
                    }
                }
                
                for(i = (right = shift) - 1; i > left; i--)
                {
                    bool b = array.IsGreaterThan(i, i + 1);
                    yield return null;

                    if (b)
                    {
                        array.Swap(i, i + 1);
                        yield return null;

                        shift = i;
                    }
                }
            }


            for (int j = 0; j < array.Length; j++)
            {
                array.Complete(j);
                yield return null;
            }
            yield break;
        }
    }
}
