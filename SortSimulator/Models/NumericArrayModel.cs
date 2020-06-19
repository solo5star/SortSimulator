using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortSimulator.Models
{
    public class NumericArrayModel : ICloneable
    {
        private int[] _array;

        public NumericArrayModel(int size) : this(size, NumericArrayStatus.Random) { }

        public NumericArrayModel(int size, NumericArrayStatus status)
        {
            Random rnd;

            switch (status)
            {
                case NumericArrayStatus.Random:
                    rnd = new Random();
                    _array = Enumerable.Range(1, size).OrderBy(r => rnd.Next()).ToArray();
                    break;

                case NumericArrayStatus.FewUnique:
                    _array = new int[size];
                    int splitCount = 4;
                    float term = size / (float)splitCount;
                    for(int i = 1; i <= size; i++)
                    {
                        _array[i - 1] = (int)(Math.Ceiling(i / term) * term);
                    }

                    rnd = new Random();
                    _array = _array.OrderBy(r => rnd.Next()).ToArray();
                    break;

                case NumericArrayStatus.Reversed:
                    _array = Enumerable.Range(1, size).Reverse().ToArray();
                    break;

                case NumericArrayStatus.AlmostSorted:
                    rnd = new Random();
                    int rndIndex = rnd.Next(1, size);
                    _array = Enumerable.Range(1, size).OrderBy(r => rndIndex == r ? rnd.Next(1, size) + 0.5 : r).ToArray();
                    break;

                case NumericArrayStatus.Sorted:
                default:
                    _array = Enumerable.Range(1, size).ToArray();
                    break;
            }
        }

        public int this[int index]
        {
            get {
                return _array[index];
            }
        }

        public int Length
        {
            get { return _array.Length; }
        }

        public void Shuffle()
        {
            RaiseOperationExecuted("Reset");

            Random rnd = new Random();
            _array = _array.OrderBy(r => rnd.Next()).ToArray();
        }

        public void Reset()
        {
            RaiseOperationExecuted("Reset");

            _array = Enumerable.Range(1, _array.Length).ToArray();
        }

        public bool IsGreaterThan(int index1, int index2)
        {
            RaiseOperationExecuted("Compare", index1, index2);

            return _array[index1] > _array[index2];
        }

        public bool IsEqualOrGreaterThan(int index1, int index2)
        {
            RaiseOperationExecuted("Compare", index1, index2);

            return _array[index1] >= _array[index2];
        }

        public bool IsSmallerThan(int index1, int index2)
        {
            RaiseOperationExecuted("Compare", index1, index2);

            return _array[index1] < _array[index2];
        }

        public bool IsEqualOrSmallerThan(int index1, int index2)
        {
            RaiseOperationExecuted("Compare", index1, index2);

            return _array[index1] <= _array[index2];
        }

        public void Swap(int index1, int index2)
        {
            if(index1 == index2)
            {
                return;
            }
            RaiseOperationExecuted("Swap", index1, index2);

            int temp = _array[index1];
            _array[index1] = _array[index2];
            _array[index2] = temp;
        }

        public void Complete(int index)
        {
            RaiseOperationExecuted("Complete", index);
        }



        public event EventHandler<NumericArrayModelEventArgs> OperationExecuted;

        private void RaiseOperationExecuted(string operation, params int[] indexes)
        {
            EventHandler<NumericArrayModelEventArgs> handler = OperationExecuted;

            handler?.Invoke(this, new NumericArrayModelEventArgs(operation, indexes.Length == 0 ? Enumerable.Range(0, _array.Length).ToArray() : indexes));
        }

        public object Clone()
        {
            NumericArrayModel array = (NumericArrayModel)MemberwiseClone();
            array._array = (int[])_array.Clone();
            return array;
        }
    }

    public class NumericArrayModelEventArgs : EventArgs
    {
        public string Operation;
        public int[] Indexes;

        public NumericArrayModelEventArgs(string operation, params int[] indexes)
        {
            Operation = operation;
            Indexes = indexes;
        }
    }

    public enum NumericArrayStatus
    {
        Sorted,
        Random,
        FewUnique,
        Reversed,
        AlmostSorted
    }
}
