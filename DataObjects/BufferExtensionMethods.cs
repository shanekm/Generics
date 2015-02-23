namespace DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using DataObjects.Abstract;

    public static class BufferExtensionMethods
    {
        // TAKE ONE - CONVERTING USING TYPEDESCRIPTOR/ RETURNS TOUTPUT
        // Extending IBuffer interface with generic Extension
        // Extend IBuffer<T> (this IBuffer<T>)
        // Returns IEnumerable of TOutput
        // Takes in T and TOutput specifications
        public static IEnumerable<TOutput> AsEnumerableOfExtension<T, TOutput>(this IBuffer<T> buffer)
        {
            // Need to conver T into TOutput we want
            // So we're using TypeDescriptor to figure out what type T is
            var converter = TypeDescriptor.GetConverter(typeof(T));
            foreach (var item in buffer)
            {
                var result = converter.ConvertTo(item, typeof(TOutput));
                yield return (TOutput)result;
            }
        }
        
        // TAKE TWO - CONVERTING USING CONVERTER/ RETURNS TOUTPUT
        // Using Converter
        // Instead of using TypeDescriptor
        public static IEnumerable<TOutput> AsEnumerableOfExtensionUsingConverter<T, TOutput>(this IBuffer<T> buffer, Converter<T, TOutput> converter)
        {
            foreach (var item in buffer)
            {
                TOutput result = converter(item);
                yield return result;
            }
        }

        // Output to console extension method
        // No need to pass TOutput since we are not returning anything
        public static void Dump<T>(this IBuffer<T> buffer)
        {
            foreach (var item in buffer)
            {
                Console.WriteLine(item);
            }
        }

        // Using Deleage - Action<T>
        // I'm sending in a pointer to a method 
        // Output to console extension method
        // No need to pass TOutput since we are not returning anything
        public static void DumpActionDelegate<T>(this IBuffer<T> buffer, Action<T> print)
        {
            foreach (var item in buffer)
            {
                print(item);
            }
        }

    }
}
