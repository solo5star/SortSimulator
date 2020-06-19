using SortSimulator.Models.Algorithms;
using SortSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SortSimulator.ViewModels
{
    public class SortViewModel : ObservableObject
    {
        public LazyAlgorithm Algorithm;
        public IEnumerator<object> AlgorithmEnumerator;

        private NumericArrayModel _array;
        public NumericArrayModel Array
        {
            get
            {
                return _array;
            }
            set
            {
                _array = value;
                AlgorithmEnumerator = Algorithm.GetEnumerator(_array);

                RaisePropertyChangedEvent("Array");
            }
        }

        public SortViewModel(LazyAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        public bool Next()
        {
            return AlgorithmEnumerator.MoveNext();
        }
    }
}
