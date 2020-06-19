using SortSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortSimulator.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private NumericArrayModel _array = new NumericArrayModel(50);
        public NumericArrayModel Array
        {
            get { return (NumericArrayModel)_array.Clone(); }
            set
            {
                _array = value;

                RaisePropertyChangedEvent("Array");
            }
        }
    }
}
