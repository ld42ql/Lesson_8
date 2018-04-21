using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WpfAppSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int heightMax = 400;
        private const int widthMax = 800;

        private string sortType = "BULB";
        private int[] numbers;

        /// <summary>
        /// Массив прямоугольников
        /// </summary>
        private Rectangle[] rectangles;
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
            GetSortType();
            sortingThread = new Thread(delegate ()
            {
                Sorting();
            });
            sortingThread.Start();
            InitButton.IsEnabled = true;
        }

        private void Sorting()
        {
            if (sortType.Equals("BULB")) numbers = BulbSort(numbers);
            if (sortType.Equals("INSERTION")) numbers = InsertionSort(numbers);
            if (sortType.Equals("SHAKER")) numbers = ShakerSort(numbers);
            if (sortType.Equals("SHELL")) numbers = ShellsSort(numbers);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                RunSortingButton.IsEnabled = true;
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

            numbers = GenericArray.UniqueVales(Convert.ToInt32(CoutTxt.Text));
            InitializeRectangles(numbers);
            RunSortingButton.IsEnabled = true;
        }
        
        private int[] BulbSort(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    SelectColor(i, j);
                    if (input[j] < input[i])
                    {
                        Swap(input, j, i);
                    }
                    Pause(input.Length * 2);
                    DefauldColor(i, j);
                }
            }
            return input;
        }

        public int[] InsertionSort(int[] array)
        {
            int[] result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int j = i;
                while (j > 0 && result[j - 1] > array[i])
                {
                    SelectColor(i, j - 1);
                    result[j] = result[j - 1];
                    SwapRectangles(j, j - 1);
                    Pause(array.Length * 2 / 3);
                    DefauldColor(i, j - 1);
                    j--;
                }
                result[j] = array[i];
            }
            return result;
        }

        private int[] ShakerSort(int[] myint)
        {
            int beg, end;
            int count = 0;
            for (int i = 0; i < myint.Length / 2; i++)
            {
                beg = 0;
                end = myint.Length - 1;
                do
                {
                    SelectColor(beg, beg + 1);
                    count += 2;
                    if (myint[beg] > myint[beg + 1])
                        Swap(myint, beg, beg + 1);
                    Pause(myint.Length * 2);
                    DefauldColor(beg, beg + 1);
                    beg++;
                    SelectColor(end, end - 1);
                    if (myint[end - 1] > myint[end])
                        Swap(myint, end - 1, end);
                    Pause(myint.Length * 2);
                    DefauldColor(end, end - 1);
                    end--;
                }
                while (beg <= end);
            }
            return myint;
        }

        public int[] ShellsSort(int[] array)
        {
            int n = array.Length;
            int j, temp;
            int k = n / 2;
            while (k > 0)
            {
                for (int i = k; i < n; i++)
                {
                    temp = array[i];

                    for (j = i; j >= k; j -= k)
                    {
                        SelectColor(j, j - k);
                        if (temp < array[j - k])
                        {
                            array[j] = array[j - k];
                            SwapRectangles(j, j - k);
                            Pause(array.Length);
                            DefauldColor(j, j - k);
                        }
                        else
                        {
                            DefauldColor(j, j - k);
                            break;
                        }
                    }
                    array[j] = temp;
                    Pause(array.Length);
                }
                k /= 2;
            }
            return array;
        }

        /// <summary>
        /// Переместить числа в массиве и запустить метод перемещения в массиве прямоугольников
        /// </summary>
        /// <param name="myint"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Swap(int[] myint, int i, int j)
        {
            int temp;
            temp = myint[i];
            myint[i] = myint[j];
            myint[j] = temp;
            Pause(20);
            SwapRectangles(i, j);
        }

        /// <summary>
        /// Перемесить прямоугольники в массиве прямоугольников
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private void SwapRectangles(int index1, int index2)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                int leftFirst = (int)rectangles[index1].Margin.Left;
                int leftSecond = (int)rectangles[index2].Margin.Left;
                int topFirst = (int)rectangles[index1].Margin.Top;
                int topSecond = (int)rectangles[index2].Margin.Top;
                rectangles[index1].Margin = new Thickness(leftSecond, topFirst, 0, 0);
                rectangles[index2].Margin = new Thickness(leftFirst, topSecond, 0, 0);
            });
            Rectangle temp = rectangles[index1];
            rectangles[index1] = rectangles[index2];
            rectangles[index2] = temp;
        }

        /// <summary>
        /// Создания визуализации массива, через отрисовку прямоугольников
        /// </summary>
        /// <param name="array"></param>
        private void InitializeRectangles(int[] array)
        {
            int widthRec = widthMax / array.Length;
            int width = widthRec >= 25 ? 25 : widthRec;

            int maxNumber = GetMax(array);
            int pixelsPerNumber = heightMax / maxNumber;
            DrawingCanvas.Children.Clear();
            rectangles = new Rectangle[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int height = array[i] * pixelsPerNumber;
                int top = heightMax - height;

                int left = widthRec * i;
                Rectangle rectangle = new Rectangle();
                SolidColorBrush myBrush = new SolidColorBrush(Colors.LightSlateGray);
                rectangle.Fill = myBrush;
                rectangle.Height = height;
                rectangle.Width = width;
                rectangle.Margin = new Thickness(left, top, 0, 0);
                rectangles[i] = rectangle;
                DrawingCanvas.Children.Add(rectangle);
            }
        }

        /// <summary>
        /// Изменить цвет выбранных прямоугольников
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private void SelectColor(int index1, int index2)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                rectangles[index1].Fill = new SolidColorBrush(Colors.Red);
                rectangles[index2].Fill = new SolidColorBrush(Colors.Red);
            });
        }

        /// <summary>
        /// Вернуть цвет выбранным прямоугольникам
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private void DefauldColor(int index1, int index2)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                rectangles[index1].Fill = new SolidColorBrush(Colors.LightSlateGray);
                rectangles[index2].Fill = new SolidColorBrush(Colors.LightSlateGray);
            });
        }

        
        /// <summary>
        /// Нахождение максимального числа в массиве
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private int GetMax(int[] array)
        {
            int temp = int.MinValue;
            
            foreach (var item in array)
            {
                if (item > temp)
                {
                    temp = item;
                }
            }
            return temp;
        }

        private void Pause(int value)
        {
            Thread.Sleep(1000 / value);
        }
    }
}
