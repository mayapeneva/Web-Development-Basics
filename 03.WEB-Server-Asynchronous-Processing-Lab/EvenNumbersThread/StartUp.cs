namespace EvenNumbersThread
{
    using System;
    using System.Linq;
    using System.Threading;

    public class StartUp
    {
        public static void Main()
        {
            var range = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var thread = new Thread(() => PrintEvenNumbersInRange(range[0], range[1]));
            thread.Start();
            thread.Join();
            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbersInRange(int start, int end)
        {
            var numbers = Enumerable.Range(start, end - start + 1);
            var result = numbers.Where(n => n % 2 == 0);

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }
    }
}