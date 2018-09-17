using System;
using System.Threading;

// Написать приложение, считающее в раздельных потоках:
// a.факториал числа N, которое вводится с клавиатуры;
// b.сумму целых чисел до N, которое также вводится с клавиатуры.

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                int n;
                int n2;
                int resultFactorial = 0;
                int resultSum = 0;
                Console.WriteLine("Enter N to get factorial:");
                n = int.TryParse(Console.ReadLine(), out n) == true ? n : throw new Exception("Enter integer value");
                Console.WriteLine("Enter N to get sum of numbers to N");
                n2 = int.TryParse(Console.ReadLine(), out n2) == true ? n2 : throw new Exception("Enter ingeger value");

                var threadFactorial = new Thread(new ThreadStart(() => resultFactorial = GetFactorial(n))) { Name = "Factorial"};
                threadFactorial.Start(); // Запускаем первый поток, который считает факториал

                var threadSum = new Thread(new ThreadStart(() => resultSum = GetSumToN(n))) { Name = "Sum" };
                threadSum.Start(); // Запускаем второй поток, который считает сумму

                Thread.Sleep(500); // Притормаживаем основной поток, чтоб успели выполниться вторичные потоки

                Console.WriteLine(resultFactorial);
                Console.WriteLine(resultSum);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static int GetFactorial(int i)
        {
            if (i == 1) return 1;
            return GetFactorial(i - 1) * i;
        }

        private static int GetSumToN(int i)
        {
            if (i == 1) return 1;
            return GetSumToN(i - 1) + i;
        }
    }
}