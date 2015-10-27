using System;

namespace GenericMethod
{
    internal class Program
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = b;
            b = a;
            a = temp;
        }

        private static void Main(string[] args)
        {
            int a = 10;
            int b = 20;
            Console.WriteLine("beforeSwap: a:{0}, b:{1}", a, b);
            Swap<int>(ref a, ref b);
            Console.WriteLine("afterSwap: a:{0}, b:{1}", a, b);
        }
    }
}