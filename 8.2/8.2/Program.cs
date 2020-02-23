using System;

namespace _8._2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Выберите метод поиска:\n1)Простой поиск подстроки\n2)Поиск Кнута-Морриса-Пратта\n3)Поиск Бойера-Мура\n4)Выход");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        CC();
                        Console.WriteLine("Введите строку, в которой будем искать подстроку");
                        string s1 = Console.ReadLine();
                        CC();
                        Console.WriteLine("Введите подстроку для поиска");
                        string s2 = Console.ReadLine();
                        CC();
                        TestSimpleSearch(s1, s2, 0);                        
                        CR();
                        CC();
                        break;
                    case "2":
                        CC();
                        Console.WriteLine("Введите строку, в которой будем искать подстроку");
                        string s3 = Console.ReadLine();
                        CC();
                        Console.WriteLine("Введите подстроку для поиска");
                        string s4 = Console.ReadLine();
                        CC();
                        TestKnutMorrisPrattSearch(s3, s4, 0);
                        CR();
                        CC();
                        break;
                    case "3":
                        CC();
                        Console.WriteLine("Введите строку, в которой будем искать подстроку");
                        string s5 = Console.ReadLine();
                        CC();
                        Console.WriteLine("Введите подстроку для поиска");
                        string s6 = Console.ReadLine();
                        CC();
                        TestBMSearch(s5, s6, 0);
                        CR();
                        CC();
                        break;
                    case "4":
                        flag = false;
                        CC();
                        break;
                    default:
                        CC();
                        break;
                }
            }
        }
        static int [] GetPrefix (string str)
        {
            int[] prefixarr = new int[str.Length];
            int j = 0;
            prefixarr[0] = 0;
            for (int i=1; i<str.Length; i++)
            {
                while (j > 0 && str[j] != str[i])
                {
                    j = prefixarr[j];
                }
                if (str[j] == str[i])
                {
                    j++;
                }
                prefixarr[i] = j;
            }
            return prefixarr;
        }
        static int KnutMorrisPrattSearch (string line, string toFind, out int c)
        {
            int counter = 0;
            int[] prefix = GetPrefix(toFind);
            int j = 0;
            try
            {
                for (int i = 1; j <= line.Length; i++)
                {
                    while (j > 0 && toFind[j] != line[i - 1])
                    {
                        j = prefix[j - 1];
                    }
                    counter++;
                    if (toFind[j] == line[i - 1])
                    {
                        j++;
                    }
                    counter++;
                    if (j == toFind.Length)
                    {
                        c = counter;
                        return i - toFind.Length;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                c = counter;
                return -1;
            }
            c = counter;
            return -1;
        }
        static int [] BadCharactersTable (string toFind)
        {
            int [] badcharacters = new int[256];
            for (int i=0; i<badcharacters.Length; i++)
            {
                badcharacters[i] = -1;
            }
            for (int i=0; i<toFind.Length-1; i++)
            {
                badcharacters[(int)toFind[i]] = i;
            }
            return badcharacters;
        }
        static int[] GoodSuffixTable(string toFind)
        {
            int[] suffixes = Suffixes(toFind);
            int[] goodsuffixes = new int[toFind.Length];
            for (int i=0; i<goodsuffixes.Length; i++)
            {
                goodsuffixes[i] = toFind.Length;
            }
            for (int i=toFind.Length-1; i>=0; i--)
            {
                if (suffixes[i] == i+1)
                {
                    for (int j=0; j<toFind.Length-i-1; j++)
                    {
                        if (goodsuffixes[j] == toFind.Length)
                        {
                            goodsuffixes[j] = toFind.Length - i - 1;
                        }
                    }
                }
            }
            for (int i=0; i<toFind.Length-2; i++)
            {
                goodsuffixes[toFind.Length - 1 - suffixes[i]] = toFind.Length - i - 1;
            }
            return goodsuffixes;
        }
        static int [] Suffixes (string toFind)
        {
            int[] suffixes = new int[toFind.Length];
            suffixes[toFind.Length - 1] = toFind.Length;
            int f = 0, g = toFind.Length - 1;
            for (int i=toFind.Length-2; i>=0; i--)
            {
                if (i > g && suffixes[i + toFind.Length - 1 - f] < i - g)
                {
                    suffixes[i] = suffixes[i + toFind.Length - 1 - f];
                }
                else if (i < g)
                {
                    g = i;
                }
                f = i;
                while (g >= 0 && toFind[g] == toFind[g + toFind.Length - 1 - f])
                {
                    g--;
                }
                suffixes[i] = f - g;
            }
            return suffixes;
        }
        static int BMSearch(string line, string toFind, out int c)
        {
            int counter = 0;
            if (toFind.Length > line.Length)
            {
                c = counter;
                return -1;
            }
            int[] badcharacters = BadCharactersTable(toFind);
            int[] goodsuffix = GoodSuffixTable(toFind);
            int pos = 0;
            while (pos <= line.Length - toFind.Length)
            {
                int i;
                for (i = toFind.Length - 1; i >= 0 && toFind[i] == line[i + pos]; i--) ;
                counter++;
                if (i < 0)
                {
                    c = counter;
                    return pos;
                }
                pos += Math.Max(i - badcharacters[(int)line[pos + i]], goodsuffix[i]);
            }
            c = counter;
            return -1;
        }
        static void TestKnutMorrisPrattSearch(string line, string toFind, int counter)
        {
            DateTime start = DateTime.Now;
            int pos= KnutMorrisPrattSearch(line, toFind, out counter);
            DateTime end = DateTime.Now;
            TimeSpan result = end.Subtract(start);
            if (pos !=-1)
            {
                Console.WriteLine("Индекс вхождения: "+pos);
            }
            else
            {
                Console.WriteLine("Не найдено");
            }
            Console.WriteLine("Число сравнений: "+counter);
            Console.WriteLine($"Время работы: {result.Seconds}:{result.Milliseconds}");
        }
        static void TestBMSearch(string line, string toFind, int counter)
        {
            DateTime start = DateTime.Now;
            int pos = BMSearch(line, toFind, out counter);
            DateTime end = DateTime.Now;
            TimeSpan result = end.Subtract(start);
            if (pos != -1)
            {
                Console.WriteLine("Индекс вхождения: " + pos);
            }
            else
            {
                Console.WriteLine("Не найдено");
            }
            Console.WriteLine("Число сравнений: " + counter);
            Console.WriteLine($"Время работы: {result.Seconds}:{result.Milliseconds}");
        }
        static void CC()
        {
            Console.Clear();
        }
        static void CR()
        {
            Console.ReadKey();
        }
        static int SimpleSearch(string line, string toFind, out int c)
        {
            int counter = 0;
            int pos = -1;
            while (pos < line.Length - toFind.Length)
            {
                counter++;
                pos++;
                int j = 0;
                while (j < toFind.Length && line[pos + j] == toFind[j])
                {
                    j++;
                }
                if (j == toFind.Length)
                {
                    c = counter;
                    return pos;
                }
            }
            c = counter;
            return -1;
        }
        static void TestSimpleSearch(string line, string toFind, int counter)
        {
            DateTime start = DateTime.Now;
            int pos = SimpleSearch(line, toFind, out counter);
            DateTime end = DateTime.Now;
            TimeSpan result = end.Subtract(start);
            if (pos != -1)
            {
                Console.WriteLine("Индекс вхождения: " + pos);
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
