using System.Threading;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;

namespace WpfAppSort
{
    class DrawArray : DispatcherObject
    {
        /// <summary>
        /// Массив прямоугольников
        /// </summary>
        private Rectangle[] rectangles;
        private int heightMax = 400;
        private const int widthMax = 800;
        int[] array;

        public DrawArray(int height,  int[] array)
        {
            this.heightMax = height;
            this.array = array;
        }

        /// <summary>
        /// Перемесить прямоугольники в массиве прямоугольников
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void SwapRectangles(int index1, int index2)
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
            Thread.Sleep(50);
            Rectangle temp = rectangles[index1];
            rectangles[index1] = rectangles[index2];
            rectangles[index2] = temp;
        }

        /// <summary>
        /// Создания визуализации массива, через отрисовку прямоугольников
        /// </summary>
        /// <param name="array"></param>
        public Rectangle[] InitializeRectangles()
        {
            int widthRec = widthMax / array.Length;
            int width = widthRec >= 25 ? 25 : widthRec;

            int maxNumber = GetMax(array);
            int pixelsPerNumber = heightMax / maxNumber;
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
            }
            return rectangles;
        }

        /// <summary>
        /// Изменить цвет выбранных прямоугольников
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void SelectColor(int index1, int index2)
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
        public void DefauldColor(int index1, int index2)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                rectangles[index1].Fill = new SolidColorBrush(Colors.LightSlateGray);
                rectangles[index2].Fill = new SolidColorBrush(Colors.LightSlateGray);
            });
        }

        public void DefauldColor(int index1)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                rectangles[index1].Fill = new SolidColorBrush(Colors.LightSlateGray);
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
    }
}
