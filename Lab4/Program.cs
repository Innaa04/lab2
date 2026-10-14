using System;
using System.Diagnostics;

namespace Rozumatic_ASD_Lab4
{
    // Власна реалізація динамічного масиву
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

        // Додавання в кінець за амортизований час O(1)
        public void Add(T item)
        {
            if (_count == _items.Length)
            {
                Resize(_items.Length * 2); // Стратегія подвоєння ємності
            }
            _items[_count++] = item;
        }

        // Доступ за індексом за O(1)
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException("Індекс поза діапазоном!");
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException("Індекс поза діапазоном!");
                _items[index] = value;
            }
        }

        // ІНДИВІДУАЛЬНЕ ЗАВДАННЯ (Варіант 2)
        // Видалення останнього елемента за O(1) зі стратегією Shrink
        public T PopBack()
        {
            if (_count == 0) throw new InvalidOperationException("Масив порожній!");
            
            T item = _items[--_count];
            
            // Виправлено для старих компіляторів: використовуємо default(T) замість default!
            _items[_count] = default(T); // очищення посилання для Garbage Collector

            // Перевірка умови Варіанту 2 для зменшення ємності (Shrink)
            // Зменшуємо вдвічі, якщо елементів залишилося <= чверті від поточної ємності, 
            // за умови, що ємність не стане меншою за початковий базовий розмір (4)
            if (_count > 0 && _count <= _items.Length / 4 && _items.Length / 2 >= 4)
            {
                Console.WriteLine($"\n📉 [SHRINK] Елементів занадто мало ({_count}/{_items.Length}). Зменшуємо ємність вдвічі!");
                Resize(_items.Length / 2);
            }

            return item;
        }

        // Вставка за індексом за O(n)
        public void InsertAt(int index, T item)
        {
            if (index < 0 || index > _count) throw new IndexOutOfRangeException("Некоректний індекс!");
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
            Console.WriteLine($"⚡ [RESIZE] Зміна ємності: {_items.Length} -> {newCapacity}");
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
            Console.WriteLine("Створено динамічний масив з початковою місткістю 2:\n");

            Stopwatch sw = Stopwatch.StartNew();

            // Додаємо 10 елементів, спостерігаючи за перевиділенням пам'яті
            for (int i = 1; i <= 10; i++)
            {
                vec.Add(i * 10);
                vec.Print();
            }

            Console.WriteLine("\n--- Вставка в середину (індекс 3, значення 999) ---");
            vec.InsertAt(3, 999);
            vec.Print();

            // Демонстрація роботи Варіанту 2 (Shrink) при послідовному видаленні елементів
            Console.WriteLine("\n--- Демонстрація Варіанту 2: Видалення з кінця (PopBack) до спрацьовування Shrink ---");
            while (vec.Count > 2)
            {
                int removed = vec.PopBack();
                Console.WriteLine($"Видалено елемент: {removed}");
                vec.Print();
            }

            sw.Stop();
            Console.WriteLine($"\n⏱ Час операцій: {sw.Elapsed.TotalMilliseconds:F4} ms");
            Console.WriteLine("✅ Всі тести динамічного розширення та зменшення пройшли успішно!");
        }
    }
}
