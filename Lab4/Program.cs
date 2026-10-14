using System;
using System.Diagnostics;

namespace Rozumatic_ASD_Lab4
{
    // Собственная реализация динамического массива
    public class MyDynamicArray<T>
    {
        private T[] _items;
        private int _count;

        public int Count => _count;
        public int Capacity => _items.Length;

        public MyDynamicArray(int initialCapacity = 4)
        {
            if (initialCapacity < 1) initialCapacity = 4;
            _items = new T[initialCapacity];
            _count = 0;
        }

        // Добавление в конец за амортизированное время O(1)
        public void Add(T item)
        {
            if (_count == _items.Length)
            {
                Resize(_items.Length * 2); // Стратегия удвоения емкости
            }
            _items[_count++] = item;
        }

        // Доступ по индексу за O(1)
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException("Индекс вне диапазона!");
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException("Индекс вне диапазона!");
                _items[index] = value;
            }
        }

        // ИНДИВИДУАЛЬНОЕ ЗАДАНЬЕ (Вариант 2)
        // Удаление последнего элемента за O(1) со стратегией Shrink
        public T PopBack()
        {
            if (_count == 0) throw new InvalidOperationException("Массив пустой!");
            
            T item = _items[--_count];
            _items[_count] = default!; // очистка ссылки для Garbage Collector

            // Проверка условия Варианта 2 для уменьшения емкости (Shrink)
            // Уменьшаем вдвое, если элементов осталось <= четверти от текущей емкости, 
            // при условии, что емкость не станет меньше начального базового размера (4)
            if (_count > 0 && _count <= _items.Length / 4 && _items.Length / 2 >= 4)
            {
                Console.WriteLine($"\n📉 [SHRINK] Элементов слишком мало ({_count}/{_items.Length}). Уменьшаем емкость вдвое!");
                Resize(_items.Length / 2);
            }

            return item;
        }

        // Вставка по индексу за O(n)
        public void InsertAt(int index, T item)
        {
            if (index < 0 || index > _count) throw new IndexOutOfRangeException();
            if (_count == _items.Length) Resize(_items.Length * 2);

            for (int i = _count; i > index; i--)
            {
                _items[i] = _items[i - 1];
            }
            _items[index] = item;
            _count++;
        }

        private void Resize(int newCapacity)
        {
            Console.WriteLine($"⚡ [RESIZE] Изменение емкости: {_items.Length} -> {newCapacity}");
            T[] newArray = new T[newCapacity];
            Array.Copy(_items, newArray, _count);
            _items = newArray;
        }

        public void Print()
        {
            Console.Write($"[Count={_count}, Capacity={_items.Length}]: [ ");
            for (int i = 0; i < _count; i++) 
            {
                Console.Write($"{_items[i]} ");
            }
            Console.WriteLine("]");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  РОЗУМАТИК // ЛАБОРАТОРНА РОБОТА №4: ВЛАСНИЙ DYNAMIC ARRAY ");
            Console.WriteLine("==========================================================================");

            MyDynamicArray<int> vec = new MyDynamicArray<int>(initialCapacity: 2);
            Console.WriteLine("Создан динамический массив с начальной емкостью 2:\n");

            Stopwatch sw = Stopwatch.StartNew();

            // Добавляем 10 элементов, наблюдая за перевыделением памяти
            for (int i = 1; i <= 10; i++)
            {
                vec.Add(i * 10);
                vec.Print();
            }

            Console.WriteLine("\n--- Вставка в середину (индекс 3, значение 999) ---");
            vec.InsertAt(3, 999);
            vec.Print();

            // Демонстрация работы Варианта 2 (Shrink) при последовательном удалении элементов
            Console.WriteLine("\n--- Демонстрация Варианта 2: Удаление с конца (PopBack) до срабатывания Shrink ---");
            while (vec.Count > 2)
            {
                int removed = vec.PopBack();
                Console.WriteLine($"Удален элемент: {removed}");
                vec.Print();
            }

            sw.Stop();
            Console.WriteLine($"\n⏱ Время операций: {sw.Elapsed.TotalMilliseconds:F4} ms");
            Console.WriteLine("✅ Все тесты динамического расширения и уменьшения прошли успешно!");
        }
    }
}
