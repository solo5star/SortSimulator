using SortSimulator.Models.Algorithms;
using SortSimulator.Models;
using SortSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortSimulator.Views
{
    public partial class SortView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SortViewModel _viewModel;
        public SortViewModel ViewModel
        {
            get { return _viewModel; }
        }

        private SolidColorBrush ElementDefaultColor = Brushes.Gray;
        private SolidColorBrush ElementCompareColor = Brushes.Red;
        private SolidColorBrush ElementSwapColor = Brushes.LightGreen;
        private SolidColorBrush ElementCompleteColor = Brushes.Green;

        private NumericArrayModelEventArgs _latestArgs;

        private int _comparisonCount = 0;
        public int ComparisonCount
        {
            get { return _comparisonCount; }
            set
            {
                _comparisonCount = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ComparisonCount"));
            }
        }

        public SortView(LazyAlgorithm algorithm)
        {
            DataContext = this;

            InitializeComponent();

            
            m_SortName.Text = algorithm.GetType().Name;

            _viewModel = new SortViewModel(algorithm);

            _viewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs args1)
            {
                InitializeGrid();

                _viewModel.Array.OperationExecuted += delegate (object sender2, NumericArrayModelEventArgs args)
                {
                    if (args.Operation == "Reset")
                    {
                        InitializeGrid();
                    }
                    else if (args.Operation == "Compare")
                    {
                        ClearLatestArgs();

                        SetElementColor(args.Indexes[0], ElementCompareColor);
                        SetElementColor(args.Indexes[1], ElementCompareColor);

                        ComparisonCount = ComparisonCount + 1;

                        _latestArgs = args;
                    }
                    else if (args.Operation == "Swap")
                    {
                        ClearLatestArgs();

                        SetElementColor(args.Indexes[0], ElementSwapColor);
                        SetElementColor(args.Indexes[1], ElementSwapColor);
                        Swap(args.Indexes[0], args.Indexes[1]);

                        _latestArgs = args;
                    }
                    else if(args.Operation == "Complete")
                    {
                        ClearLatestArgs();

                        SetElementColor(args.Indexes[0], ElementCompleteColor);
                    }
                };
            };
        }

        private void InitializeGrid()
        {
            ComparisonCount = 0;

            m_SortGrid.Children.Clear();
            m_SortGrid.ColumnDefinitions.Clear();
            m_SortGrid.RowDefinitions.Clear();

            int length = _viewModel.Array.Length;

            for(int i = 0; i < length; i++)
            {
                m_SortGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                m_SortGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

                int value = _viewModel.Array[i];

                Rectangle rect = new Rectangle();
                rect.Fill = ElementDefaultColor;
                rect.Stroke = Brushes.Transparent;

                Grid.SetRow(rect, length - value);
                Grid.SetColumn(rect, i);
                Grid.SetRowSpan(rect, value);

                m_SortGrid.Children.Add(rect);
            }
        }

        public void Set(int index, int value)
        {
            var ui = GetElementByIndex(index);

            m_SortGrid.Children.Remove(ui);

            Grid.SetRow(ui, _viewModel.Array.Length - value);
            Grid.SetRowSpan(ui, value);

            m_SortGrid.Children.Add(ui);
        }

        public void Swap(int index1, int index2)
        {
            var ui1 = GetElementByIndex(index1);
            var ui2 = GetElementByIndex(index2);

            m_SortGrid.Children.Remove(ui1);
            m_SortGrid.Children.Remove(ui2);

            Grid.SetColumn(ui1, index2);
            Grid.SetColumn(ui2, index1);

            m_SortGrid.Children.Add(ui1);
            m_SortGrid.Children.Add(ui2);
        }

        private UIElement GetElementByIndex(int index)
        {
            return m_SortGrid.Children.Cast<UIElement>().First(e => Grid.GetColumn(e) == index);
        }

        private void SetElementColor(int index, SolidColorBrush brush)
        {
            var element = GetElementByIndex(index);
            Rectangle rect = (Rectangle)element;
            rect.Fill = brush;
        }

        private void ClearLatestArgs()
        {
            if(_latestArgs == null)
            {
                return;
            }
            foreach(int index in _latestArgs.Indexes)
            {
                SetElementColor(index, ElementDefaultColor);
            }

            _latestArgs = null;
        }
    }
}
