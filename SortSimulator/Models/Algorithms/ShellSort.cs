using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortSimulator.Models;

namespace SortSimulator.Models.Algorithms
{
    class ShellSort : LazyAlgorithm
    {
        public IEnumerator<object> GetEnumerator(NumericArrayModel array)
        {
            int gap = array.Length / 2;
            do
            {
                gap = gap / 2;
                if (gap % 2 == 0)
                {
                    gap++;
                }

                for (int i = 0; i < gap; i++)
                {
                    for(int j = i + gap; j < array.Length; j += gap)
                    {
                        for(int k = j - gap; k >= i; k -= gap)
                        {
                            bool b = array.IsGreaterThan(k, k + gap);
                            yield return null;

                            if (b)
                            {
                                array.Swap(k, k + gap);
                                yield return null;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            while (gap > 1);


                for (int i = 0; i < array.Length; i++)
            {
                array.Complete(i);
                yield return null;
            }
            yield break;
        }
    }
}
