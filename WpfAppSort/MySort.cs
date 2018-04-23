using System.Threading;
using System.Windows.Shapes;

namespace WpfAppSort
{
    /// <summary>
    /// Класс с сортировками
    /// </summary>
    class MySort
    {
        DrawArray draw;

        /// <summary>
        /// Массив с визуализацией массива в виде прямоугольников
        /// </summary>
        public Rectangle[] Rectangles { get => draw.InitializeRectangles(); }

        private int[] array;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="count">Количество в массиве</param>
        public MySort(int count)
        {
            this.array = GenericArray.UniqueVales(count);
            draw = new DrawArray(400, array);
        }

        /// <summary>
        /// Пузырьковая сортировка
        /// </summary>
        /// <returns></returns>
        public void BulbSort()
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    draw.SelectColor(i, j);
                    if (array[j] < array[i])
                    {
                        Swap(array, j, i);
                    }
                  
                    draw.DefauldColor(i, j);
                }
            }
        }

        /// <summary>
        /// Сортировка вставками
        /// </summary>
        /// <returns></returns>
        public void InsertionSort()
        {
            int[] result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int j = i;
                while (j > 0 && result[j - 1] > array[i])
                {
                    draw.SelectColor(i, j - 1);
                    result[j] = result[j - 1];
                    draw.SwapRectangles(j, j - 1);
                   
                    draw.DefauldColor(i, j - 1);
                    draw.DefauldColor(j);
                    j--;
                }
                result[j] = array[i];
            }
        }

        /// <summary>
        /// Сортировка смешиванием
        /// </summary>
        /// <returns></returns>
        public void ShakerSort()
        {
            int beg, end;
            int count = 0;
            for (int i = 0; i < array.Length / 2; i++)
            {
                beg = 0;
                end = array.Length - 1;
                do
                {
                    draw.SelectColor(beg, beg + 1);
                    count += 2;
                    if (array[beg] > array[beg + 1])
                        Swap(array, beg, beg + 1);
                    
                    draw.DefauldColor(beg, beg + 1);
                    beg++;
                    draw.SelectColor(end, end - 1);
                    if (array[end - 1] > array[end])
                        Swap(array, end - 1, end);
                   
                    draw.DefauldColor(end, end - 1);
                    end--;
                }
                while (beg <= end);
            }
        }

        /// <summary>
        /// Сортировка Шелла
        /// </summary>
        /// <returns></returns>
        public void ShellsSort()
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
                        draw.SelectColor(j, j - k);
                        if (temp < array[j - k])
                        {
                            array[j] = array[j - k];
                            draw.SwapRectangles(j, j - k);
                            draw.DefauldColor(j, j - k);
                        }
                        else
                        {
                            draw.DefauldColor(j, j - k);
                            break;
                        }
                    }
                    array[j] = temp;
                }
                k /= 2;
            }
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
            draw.SwapRectangles(i, j);
        }
    }
}
