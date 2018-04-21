using System;

namespace WpfAppSort
{
    static class GenericArray
    {

       static Random random = new Random();

        /// <summary>
        /// Метод для генерации массива с случайном порядком размещения
        /// </summary>
        /// <param name="count">Количество элементов</param>
        /// <returns></returns>
        static public int[] UniqueVales(int count)
        {
            int[] array = new int[count];

            for (int i = 1; i <= count; i++)
            {
                int index = random.Next(0, count - 1);

                while (array[index] != 0)
                {
                    index = random.Next(0, count);
                }
                array[index] = i;
            }

            return array;
        }

        /// <summary>
        /// Метод для генерации массива с случайном набором чисел от 0 до 100
        /// </summary>
        /// <param name="count">Количество элементов</param>
        /// <returns></returns>
        static public int[] RandomValues(int count)
        {
            int[] array = new int[count];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, 100);
            }

            return array;
        }
    }
}
