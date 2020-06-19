using SortSimulator.Models.Algorithms;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SortSimulator.Views
{
    /// <summary>
    /// AddSortViewDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddSortViewDialog : Window
    {
        private bool NeedToReturn = true;

        public AddSortViewDialog()
        {
            InitializeComponent();
        }

        public static List<LazyAlgorithm> OpenDialog()
        {
            var dialog = new AddSortViewDialog();
            dialog.ShowDialog();

            if (!dialog.NeedToReturn)
            {
                return null;
            }

            var algorithms = new List<LazyAlgorithm>();

            if (dialog.m_BubbleSort.IsChecked == true) algorithms.Add(new BubbleSort());
            if (dialog.m_CocktailSort.IsChecked == true) algorithms.Add(new CocktailSort());
            if (dialog.m_CombSort.IsChecked == true) algorithms.Add(new CombSort());
            if (dialog.m_HeapSort.IsChecked == true) algorithms.Add(new HeapSort());
            if (dialog.m_InsertionSort.IsChecked == true) algorithms.Add(new InsertionSort());
            // if (dialog.m_MergeSort.IsChecked == true) algorithms.Add(new MergeSort());
            if (dialog.m_QuickSort.IsChecked == true) algorithms.Add(new QuickSort());
            if (dialog.m_SelectionSort.IsChecked == true) algorithms.Add(new SelectionSort());
            if (dialog.m_ShellSort.IsChecked == true) algorithms.Add(new ShellSort());

            return algorithms;
        }

        private void Click_OK(object sender, RoutedEventArgs args)
        {
            Close();
        }

        private void Click_Cancel(object sender, RoutedEventArgs args)
        {
            NeedToReturn = false;
            Close();
        }
    }
}
