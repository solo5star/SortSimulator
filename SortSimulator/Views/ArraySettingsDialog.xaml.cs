using SortSimulator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SortSimulator.Views
{
    public partial class ArraySettingsDialog : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ArrayStatusEntry> _arrayTypeEntries = new ObservableCollection<ArrayStatusEntry>()
        {
            new ArrayStatusEntry(){ Name = "Random", Status = NumericArrayStatus.Random },
            new ArrayStatusEntry("Sorted", NumericArrayStatus.Sorted),
            new ArrayStatusEntry("AlmostSorted", NumericArrayStatus.AlmostSorted),
            new ArrayStatusEntry("FewUnique", NumericArrayStatus.FewUnique),
            new ArrayStatusEntry("Reversed", NumericArrayStatus.Reversed)
        };

        public ObservableCollection<ArrayStatusEntry> ArrayTypeEntries
        {
            get { return _arrayTypeEntries; }
            set { _arrayTypeEntries = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrayTypeEntries")); }
        }

        private ArrayStatusEntry _selectedArrayTypeEntry = new ArrayStatusEntry() { Name = "Random", Status = NumericArrayStatus.Random };
        public ArrayStatusEntry SelectedArrayTypeEntry
        {
            get { return _selectedArrayTypeEntry; }
            set { _selectedArrayTypeEntry = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedArrayTypeEntry")); }
        }


        private int? _length = 50;
        public int? Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public ArraySettingsDialog()
        {
            DataContext = this;

            InitializeComponent();
        }

        public static NumericArrayModel OpenDialog()
        {
            var dialog = new ArraySettingsDialog();
            dialog.ShowDialog();
            return dialog.Length == null ? null : new NumericArrayModel((int)dialog.Length, dialog.SelectedArrayTypeEntry.Status);
        }

        private void Click_OK(object sender, RoutedEventArgs args)
        {
            string txt = m_Length.Text;
            if (int.TryParse(txt, out int output) && output > 0)
            {
                Length = output;
                Close();
            }
            else
            {
                MessageBox.Show("Array Length 값은 자연수만 입력할 수 있습니다.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Click_Cancel(object sender, RoutedEventArgs args)
        {
            Length = null;
            Close();
        }
    }

    public class ArraySettingsResult
    {
        public int Length;
        public string Status;
    }

    public class ArrayStatusEntry
    {
        public string Name { get; set; }
        public NumericArrayStatus Status { get; set; }

        public ArrayStatusEntry()
        {

        }

        public ArrayStatusEntry(string name, NumericArrayStatus status)
        {
            Name = name;
            Status = status;
        }

        public override string ToString()
        {
            return Name ?? "Unknown";
        }
    }
}
