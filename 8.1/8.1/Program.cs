using System;
using System.IO;
namespace _8._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = InitArray();
            Console.WriteLine("Выберите метод поиска:\n1)Линейный\n2)Бинарный\n3)Интерполяционный\n4)Выход");
            string answer = Console.ReadLine();
            bool flag = true;
            while (flag)
            {               
                switch (answer)
                {
                    case "1":
                        CC();
                        Console.WriteLine("Введите элемент");
                        int num = int.Parse(Console.ReadLine());
                        CC();
                        CC();
                        TestLinearSearch(array, num, 0);
                        CR();
                        CC();
                        break;
                    case "2":
                        CC();
                        Console.WriteLine("Введите элемент");
                        int number = int.Parse(Console.ReadLine());
                        CC();
                        TestBinarySearch(array, number, 0);
                        CR();
                        CC();
                        break;
                    case "3":
                        CC();
                        Console.WriteLine("Введите элемент");
                        int element = int.Parse(Console.ReadLine());
                        CC();
                        TestInterpolationSearch(array, element, 0);
                        CR();
                        CC();
                        break;
                    case "4":
                        flag = false;
                        CC();
                        break;
                }
            }
        }
        static int [] InitArray()
        {
            FileStream file = new FileStream(@"d:\L7\7.2\sorted.dat", FileMode.Open);
            BinaryReader reader = new BinaryReader(file);
            int[] mas = new int[100000];
            for (int i=0; i<mas.Length; i++)
            {
                mas[i] = reader.ReadInt32();
            }
            return mas;
        }
        static void CC()
        {
            Console.Clear();
        }
        static void CR()
        {
            Console.ReadKey();
        }
        static int LinearSearch (int [] mas, int element, out int c)
        {
            int counter = 0;
            int pos = 0;
            bool flag = false;
            for (int i=0; i<mas.Length; i++)
            {
                counter++;
                if (mas[i] == element)
                {
                    flag = true;
                    pos = i;
                    break;
                }
            }
            c = counter;
            if (flag)
            {
                return pos;
            }
            else
            {
                return -1;
            }
        }
        static void TestLinearSearch (int [] array, int element, int counter)
        {
            DateTime start = DateTime.Now;
            int pos = LinearSearch(array, element, out counter);
            DateTime end = DateTime.Now;
            TimeSpan result = end.Subtract(start);
            if (pos != -1)
            {
                Console.WriteLine("Позиция искомого элемента: "+pos);
            }
            else
            {
                Console.WriteLine("Не найдено");
            }
            Console.WriteLine("Число сравнений: "+counter);
            Console.WriteLine($"Время работы: {result.Seconds}:{result.Milliseconds}");
        }
        static int BinarySearch(int [] array, int element, out int c)
        {
            int counter = 0;
            int startpos = 0;
            int endpos = array.Length - 1;
            while (endpos >= startpos)
            {
                int midpos = (startpos + endpos) / 2;
                counter++;
                if (array[midpos] == element)
                {
                    c = counter;
                    return midpos;
                }
                counter++;
                if (array[midpos] > element)
                {
                    endpos = midpos - 1;
                }
                else
                {
                    startpos = midpos + 1;
                }
            }
            c = counter;
            return -1;
        }
        static void TestBinarySearch (int [] array, int element, int counter)
        {
            DateTime start = DateTime.Now;
            int pos = BinarySearch(array, element, out counter);
            DateTime end = DateTime.Now;
            TimeSpan result = end.Subtract(start);
            if (pos != -1)
            {
                Console.WriteLine("Позиция искомого элемента: " + pos);
            }
            else
            {
                Console.WriteLine("Не найдено");
            }
            Console.WriteLine("Число сравнений: " + counter);
            Console.WriteLine($"Время работы: {result.Seconds}:{result.Milliseconds}");
        }
        static int IS(int [] a, int element, out int c)
        {
            int counter = 0;
            int low = 0;
            int mid;
            int high = a.Length - 1;
            try {
                while (low <= high)
                {
                    mid = low + (element - a[low]) * (high - low) / (a[high] - a[low]);
                    counter++;
                    if (a[mid] < element)
                        low = mid + 1;
                    else if (a[mid] > element)
                        high = mid - 1;
                    else if (element == a[mid])
                    {
                        c = counter;
                        return mid;
                    }
                }
                c = counter;
                return -1;
            }
            catch
            {
                c = counter;
                return -1;
            }
        }
        static void TestInterpolationSearch(int[] array, int element, int counter)
        {
            DateTime start = DateTime.Now;
            int pos= IS(array, element, out counter);
            DateTime end = DateTime.Now;
            TimeSpan result = end.Subtract(start);
            if (pos != -1)
            {
                Console.WriteLine("Позиция искомого элемента: " + pos);
            }
            else
            {
                Console.WriteLine("Не найдено");
            }
            Console.WriteLine("Число сравнений: " + counter);
            Console.WriteLine($"Время работы: {result.Seconds}:{result.Milliseconds}");
        }
    }
}
