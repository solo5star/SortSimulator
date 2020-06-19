using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortSimulator.Models;

namespace SortSimulator.Models.Algorithms
{
    class CombSort : LazyAlgorithm
    {
        public IEnumerator<object> GetEnumerator(NumericArrayModel array)
        {
            const float shrink = 1.3f;

            float gap = array.Length;
            bool swapped = true;

            while(gap > 1 || swapped)
            {
                if((gap /= shrink) < 1)
                {
                    gap = 1;
                }
                swapped = false;
                for(int j = 0; j < array.Length - gap; j++)
                {
                    bool b = array.IsGreaterThan(j, j + (int)gap);
                    yield return null;

                    if (b)
                    {
                        swapped = true;
                        array.Swap(j, j + (int)gap);
                        yield return null;
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
