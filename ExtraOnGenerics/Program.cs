namespace ExtraOnGenerics
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            // TAKE ONE
            var input = "Step1";
            var value = input.ParseEnum<Steps>();
            Console.WriteLine(value);

            // TAKE TWO - adding numbers
            // there is now way to do +, or division etc on generics
            // only way to do it is to have several overloaded methods
            var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            //var result = SampledAverage(numbers); // can't do math


            // TAKE THREE - adding items to list of different types
            // use inheritance
            // I'm able to use different types now
            var list = new List<Item>();
            list.Add(new Item<int>());
            list.Add(new Item<double>());
        }

        public static double SampledAverage(double[] numbers)
        {
            var count = 0;
            var sum = 0.0;
            for (int i = 0; i < numbers.Length; i += 2)
            {
                sum += numbers[i];
                count += 1;
            }

            return sum / count;
        }
    }

    // TAKE THREE
    public class Item<T> : Item
    { }

    public class Item { }

    public enum Steps
    {
        Step1,
        Step2,
        Step3
    }

    public static class StringExtensions
    {
        // TAKE TWO - how to force extension method to allow for enums only?
        // added struct => however this still allows for input.ParseEnum<int>() which will error out. BUT int is a struct 
        // so it will compile fine
        public static TEnum ParseEnum<TEnum>(this string value) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        // TAKE TWO - MATH
        // there is now way to do +, or division etc on generics
        // only way to do it is to have several overloaded methods
    }

}
