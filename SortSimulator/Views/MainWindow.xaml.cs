using SortSimulator.Models;
using SortSimulator.Models.Algorithms;
using SortSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SortSimulator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel Main
        {
            get => DataContext as MainWindowViewModel;
            set { DataContext = value; }
        }
        public List<SortView> SortViewList { get; } = new List<SortView>();

        private DispatcherTimer _ticker = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 10) };
        public DispatcherTimer Ticker { get => _ticker; set => _ticker = value; }

        private long CurrentMillis => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        private long PreviousMillis = 0;

        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();

            Main = new MainWindowViewModel();
            
            AddSortView(new SortView(new QuickSort()));
            AddSortView(new SortView(new InsertionSort()));
            AddSortView(new SortView(new BubbleSort()));
            AddSortView(new SortView(new HeapSort()));
            AddSortView(new SortView(new SelectionSort()));
            AddSortView(new SortView(new ShellSort()));
            AddSortView(new SortView(new CombSort()));
            AddSortView(new SortView(new CocktailSort()));

            UpdateSortViewGrid();

            Ticker.Tick += delegate (object sender, EventArgs args)
            {
                while (PreviousMillis > 0 && PreviousMillis + (1000 / m_SpeedSlider.Value) < CurrentMillis)
                {
                    Next();
                    PreviousMillis += (long)(1000 / m_SpeedSlider.Value);
                }
            };
        }

        public void RemoveAllSortView()
        {
            SortViewList.RemoveAll(_ => true);
        }

        public void AddSortView(SortView sortView)
        {
            SortViewList.Add(sortView);
            sortView.ViewModel.Array = Main.Array;
        }

        public void UpdateSortViewGrid()
        {
            m_Grid.Children.Clear();
            m_Grid.ColumnDefinitions.Clear();
            m_Grid.RowDefinitions.Clear();

            if(SortViewList.Count == 0)
            {
                return;
            }
            // □□□
            else if (SortViewList.Count <= 3)
            {
                for(int col = 0; col < SortViewList.Count; col++)
                {
                    m_Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                    Grid.SetRow(SortViewList[col], 0);
                    Grid.SetColumn(SortViewList[col], col);

                    m_Grid.Children.Add(SortViewList[col]);
                }
            }
            // □□
            // □□
            else if (SortViewList.Count <= 4)
            {
                for(int row = 0; row < 2; row++)
                {
                    m_Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }
                for(int col = 0; col < 2; col++)
                {
                    m_Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }

                for(int i = 0; i < SortViewList.Count; i++)
                {
                    Grid.SetRow(SortViewList[i], i / 2);
                    Grid.SetColumn(SortViewList[i], i % 2);

                    m_Grid.Children.Add(SortViewList[i]);
                }
            }

            // □□□
            // □□□
            else if (SortViewList.Count <= 6)
            {
                for(int row = 0; row < 2; row++)
                {
                    m_Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }
                for(int col = 0; col < 3; col++)
                {
                    m_Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }

                for(int i = 0; i < SortViewList.Count; i++)
                {
                    Grid.SetRow(SortViewList[i], i / 3);
                    Grid.SetColumn(SortViewList[i], i % 3);

                    m_Grid.Children.Add(SortViewList[i]);
                }
            }

            // □□□
            // □□□
            // □□□
            else if (SortViewList.Count <= 9)
            {
                for (int row = 0; row < 3; row++)
                {
                    m_Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }
                for (int col = 0; col < 3; col++)
                {
                    m_Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }

                for (int i = 0; i < SortViewList.Count; i++)
                {
                    Grid.SetRow(SortViewList[i], i / 3);
                    Grid.SetColumn(SortViewList[i], i % 3);

                    m_Grid.Children.Add(SortViewList[i]);
                }
            }
        }

        private void Click_AddSort(object sender, RoutedEventArgs args)
        {
            List<LazyAlgorithm> algorithms = AddSortViewDialog.OpenDialog();

            if (algorithms != null)
            {
                Stop();

                RemoveAllSortView();

                foreach (LazyAlgorithm algorithm in algorithms)
                {
                    AddSortView(new SortView(algorithm));
                }
            }

            UpdateSortViewGrid();
        }

        private void Click_ArraySettings(object sender, RoutedEventArgs args)
        {
            NumericArrayModel result = ArraySettingsDialog.OpenDialog();
            if(result != null)
            {
                Stop();

                Main.Array = result;

                SortViewList.ForEach(v => v.ViewModel.Array = Main.Array);
            }
        }

        private void Click_Next(object sender, RoutedEventArgs args)
        {
            if (IsRunning())
            {
                Stop();
            }
            Next();
        }

        private void Toggle_StartStop(object sender, RoutedEventArgs args)
        {
            if (IsRunning())
            {
                Stop();
            }
            else
            {
                Start();
            }
        }

        private void Click_Reset(object sender, RoutedEventArgs args)
        {
            Stop();

            SortViewList.ForEach(v => v.ViewModel.Array = Main.Array);
        }

        public bool IsRunning()
        {
            return Ticker.IsEnabled;
        }

        public void Stop()
        {
            Ticker.Stop();
            PreviousMillis = 0;
            m_StartStop.Content = "Start";
        }

        public void Start()
        {
            PreviousMillis = CurrentMillis - (long)(1000 / m_SpeedSlider.Value) - 1;
            Ticker.Start();
            m_StartStop.Content = "Stop";
        }

        public void Next()
        {
            foreach(SortView view in SortViewList)
            {
                view.ViewModel.AlgorithmEnumerator.MoveNext();
            }
        }
    }
}
