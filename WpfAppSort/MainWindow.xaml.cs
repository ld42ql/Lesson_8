using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WpfAppSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        private string sortType = "BULB";

        MySort sort;

        private Thread sortingThread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            if (sortingThread != null)
            {
                sortingThread.Abort();
            }
            this.Close();
        }

        private void RunSorting(object sender, MouseButtonEventArgs e)
        {
            RunSortingButton.IsEnabled = false;
            InitButton.IsEnabled = false;
            GetSortType();
            sortingThread = new Thread(delegate ()
            {
                Sorting();
            });
            sortingThread.Start();
        }

        private void Sorting()
        {
            if (sortType.Equals("BULB")) sort.BulbSort();
            if (sortType.Equals("INSERTION"))  sort.InsertionSort();
            if (sortType.Equals("SHAKER"))  sort.ShakerSort();
            if (sortType.Equals("SHELL"))  sort.ShellsSort();
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                InitButton.IsEnabled = true;
            });
        }

        private void GetSortType()
        {
            InitButton.IsEnabled = false;
            if (FirstRadioButton.IsChecked == true) sortType = "BULB";
            if (SecondRadioButton.IsChecked == true) sortType = "INSERTION";
            if (ThirdRadioButton.IsChecked == true) sortType = "SHAKER";
            if (FotingRadioButton.IsChecked == true) sortType = "SHELL";
        }

        private void InitializeButton(object sender, MouseButtonEventArgs e)
        {
            sort = new MySort(Convert.ToInt32(CoutTxt.Text));

            DrawingCanvas.Children.Clear();

            foreach (var item in sort.Rectangles)
            {
                DrawingCanvas.Children.Add(item);
            }

            RunSortingButton.IsEnabled = true;
        }
    }
}
