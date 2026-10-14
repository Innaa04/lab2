using System;
using System.Diagnostics;

namespace Rozumatic_ASD_Lab3
{
    class Program
    {
        // 1. Транспонирование квадратной матрицы In-place O(n^2)
        static void TransposeMatrix(int[,] matrix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int temp = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = temp;
                }
            }
        }

        // 2. Поиск седловой точки: min в строке и max в столбце
        static void FindSaddlePoints(int[,] matrix, int rows, int cols)
        {
            bool found = false;

            for (int i = 0; i < rows; i++)
            {
                // Находим минимум в текущей строке
                int minRowVal = matrix[i, 0];
                int minColIdx = 0;
                for (int j = 1; j < cols; j++)
                {
                    if (matrix[i, j] < minRowVal)
                    {
                        minRowVal = matrix[i, j];
                        minColIdx = j;
                    }
                }

                // Проверяем, является ли этот минимум максимальным в своем столбце minColIdx
                bool isSaddle = true;
                for (int k = 0; k < rows; k++)
                {
                    if (matrix[k, minColIdx] > minRowVal)
                    {
                        isSaddle = false;
                        break;
                    }
                }

                if (isSaddle)
                {
                    Console.WriteLine($"🎯 Найдена седловая точка: Значение = {minRowVal} на позиции [{i}, {minColIdx}]");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("ℹ️ В заданной матрице седловых точек нет.");
            }
        }

        // Вывод матрицы на экран
        static void PrintMatrix(int[,] matrix, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                Console.Write(" [ ");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j],4} ");
                }
                Console.WriteLine("]");
            }
        }

        // ИНДИВИДУАЛЬНОЕ ЗАДАНИЕ (Вариант 2)
        // Метод умножения двух прямоугольных матриц
        static int[,] MultiplyMatrices(int[,] matrixA, int rowsA, int colsA, int[,] matrixB, int rowsB, int colsB)
        {
            // По правилам математики, количество столбцов первой матрицы должно быть равно количеству строк второй
            if (colsA != rowsB)
            {
                Console.WriteLine("❌ Ошибка: Матрицы таких размерностей перемножить невозможно!");
                return null;
            }

            int[,] result = new int[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  РОЗУМАТИК // ЛАБОРАТОРНА РОБОТА №3: ДВОВИМІРНІ МАТРИЦІ  ");
            Console.WriteLine("==========================================================================");

            // Тестовая квадратная матрица 4х4 из кода преподавателя
            int[,] matrix = new int[,]
            {
                { 12, 14, 11, 15 },
                { 8,  10, 6,  9  },
                { 20, 22, 19, 25 },
                { 5,  7,  4,  6  }
            };

            int n = 4;
            Console.WriteLine("Початкова матриця 4x4:");
            PrintMatrix(matrix, n, n);

            Console.WriteLine("\n--- Пошук сідлових точок ---");
            FindSaddlePoints(matrix, n, n);

            Console.WriteLine("\n--- Транспонування матриці (In-place) ---");
            Stopwatch sw = Stopwatch.StartNew();
            TransposeMatrix(matrix, n);
            sw.Stop();

            PrintMatrix(matrix, n, n);
            Console.WriteLine($"\n⏱ Час транспонування: {sw.Elapsed.TotalMilliseconds:F4} ms");

            // Тестирование индивидуального задания (Вариант 2)
            Console.WriteLine("\n--- Індивідуальне завдання: Множення прямокутних матриць (Варіант 2) ---");
            
            // Матрица A размером 2х3
            int[,] matrixA = new int[,] 
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };
            
            // Матрица B размером 3х2
            int[,] matrixB = new int[,] 
            {
                { 7,  8 },
                { 9,  10 },
                { 11, 12 }
            };

            Console.WriteLine("Матриця A (2x3):");
            PrintMatrix(matrixA, 2, 3);

            Console.WriteLine("\nМатриця B (3x2):");
            PrintMatrix(matrixB, 3, 2);

            int[,] resultMatrix = MultiplyMatrices(matrixA, 2, 3, matrixB, 3, 2);

            if (resultMatrix != null)
            {
                Console.WriteLine("\nРезультат множення матриць A * B (2x2):");
                PrintMatrix(resultMatrix, 2, 2);
            }

            Console.WriteLine("\n✅ Роботу виконано успішно!");
        }
    }
}
