using SortSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortSimulator.Models.Algorithms
{
    public interface LazyAlgorithm
    {
        IEnumerator<object> GetEnumerator(NumericArrayModel array);
    }
}
