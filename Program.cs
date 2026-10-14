using System;
using System.Diagnostics;

namespace Rozumatic_ASD_Lab2
{
    class Program
    {
        // 1. Пошук мінімуму, максимуму та їх індексів за один прохід O(n)
        static void FindExtremes(int[] arr, out int minVal, out int minIdx, out int maxVal, out int maxIdx)
        {
            minVal = arr[0]; minIdx = 0;
            maxVal = arr[0]; maxIdx = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < minVal)
                {
                    minVal = arr[i];
                    minIdx = i;
                }
                if (arr[i] > maxVal)
                {
                    maxVal = arr[i];
                    maxIdx = i;
                }
            }
        }

        // 2. In-place реверс масиву двома вказівниками за O(n/2) операцій і O(1) пам'яті
        static void ReverseInPlace(int[] arr)
        {
            int left = 0;
            int right = arr.Length - 1;
            while (left < right)
            {
                int temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;
                left++;
                right--;
            }
        }

        // 3. Фільтрація елементів: выбір лише додатних парних чисел
        static int[] FilterPositiveEven(int[] arr)
        {
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0 && arr[i] % 2 == 0) count++;
            }

            int[] result = new int[count];
            int idx = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0 && arr[i] % 2 == 0)
                    result[idx++] = arr[i];
            }

            return result;
        }

        // ІНДИВІДУАЛЬНА МОДИФІКАЦІЯ (Варіант 2)
        // Допоміжний метод для реверсу певної ділянки масиву від індексу left до right
        static void ReverseRange(int[] arr, int left, int right)
        {
            while (left < right)
            {
                int temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;
                left++;
                right--;
            }
        }

        // Головний метод циклічного зсуву праворуч за допомогою алгоритму потрійного реверсу
        static void RotateRight(int[] arr, int k)
        {
            if (arr == null || arr.Length == 0) return;
            
            // Нормалізуємо k на випадок, якщо зсув більший за довжину масиву
            k = k % arr.Length;
            if (k == 0) return;

            // 1. Реверс всього масиву
            ReverseRange(arr, 0, arr.Length - 1);
            // 2. Реверс перших k елементів
            ReverseRange(arr, 0, k - 1);
            // 3. Реверс решти елементів від k до кінця
            ReverseRange(arr, k, arr.Length - 1);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  РОЗУМАТИК // ЛАБОРАТОРНА РОБОТА №2: АЛГОРИТМИ НАД МАСИВАМИ  ");
            Console.WriteLine("==========================================================================");

            int[] numbers = { 42, -7, 18, 99, 0, -15, 24, 88, 3, -2, 76 };
            Console.WriteLine("Вихідний масив: [ " + string.Join(", ", numbers) + " ]\n");

            Stopwatch sw = Stopwatch.StartNew();

            // 1. Екстремуми
            FindExtremes(numbers, out int minVal, out int minIdx, out int maxVal, out int maxIdx);
            Console.WriteLine($"🔍 Мінімум: {minVal} (індекс {minIdx})");
            Console.WriteLine($"🔍 Максимум: {maxVal} (індекс {maxIdx})");

            // 2. Фільтрація
            int[] filtered = FilterPositiveEven(numbers);
            Console.WriteLine($"\n✨ Додатні парні числа: [ " + string.Join(", ", filtered) + " ]");

            // Виклик індивідуального завдання (Варіант 2)
            int k = 3; // Кількість позицій для зсуву праворуч
            RotateRight(numbers, k);
            Console.WriteLine($"\n↩️ Масив після циклічного зсуву праворуч на {k} (Варіант 2): [ " + string.Join(", ", numbers) + " ]");

            // 3. In-place реверс (виконується над уже зсунутим масивом)
            ReverseInPlace(numbers);
            Console.WriteLine($"\n🔄 Масив після реверсу In-place: [ " + string.Join(", ", numbers) + " ]");

            sw.Stop();
            Console.WriteLine($"\n⏱ Загальний час виконання: {sw.Elapsed.TotalMilliseconds:F4} ms");
            Console.WriteLine("✅ Тестування завершено успішно!");
        }
    }
}
