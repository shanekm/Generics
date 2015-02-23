
namespace DataObjects.Abstract
{
    using System.Collections.Generic;

    // Creating Interface
    // Client code will only know to use IBuffer and we can easily swap out different buffers that implement this interface (IBuffer)
    // In order to access methods() in concrete type AND have an interface specified-
    // -so that for the client I can pass in different implementations
    // I need to specify <T> in my interface ie (returning <double><T> etc)

    // TAKE TWO 
    // Implement IEnumerable<T>
    // Implementing IEnumerable will require implmeneting IEnumerabor<T> GetEnumerator() AND IEnumerator GetEnumerator()
    // All buffers implementing IBuffer will be able enumerate
    public interface IBuffer<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }

        // Want strongly type so use T
        void Write(T value);

        // In order to be able to access methods returning some Type
        T Read();

        // New specs calls for inspection of buffer data BUT as String
        // we have to implement a new method
        // that will accept and return any type of buffer contents
        // so Buffer<double> may return content in strings, ints etc.
        // Methods can be Generic Typed other T type than class Type
        IEnumerable<TOutput> AsEnumerableOf<TOutput>();
    }
}
