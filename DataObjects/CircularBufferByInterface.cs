namespace DataObjects
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    using DataObjects.Abstract;

    public class CircularBufferByInterface<T> : IBuffer<T>
    {
        private T[] buffer;

        private int start;

        private int end;

        public CircularBufferByInterface()
            : this(capacity: 10)
        {
        }

        public CircularBufferByInterface(int capacity)
        {
            buffer = new T[capacity + 1];
            start = 0;
            end = 0;
        }

        public void Write(T value)
        {
            buffer[end] = value;
            end = (end + 1) % buffer.Length;
            if (end == start)
            {
                start = (start + 1) % buffer.Length;
            }
        }

        public T Read()
        {
            T result = buffer[start];
            start = (start + 1) % buffer.Length;
            return result;
        }

        public int Capacity
        {
            get { return buffer.Length; }
        }

        public bool IsEmpty
        {
            get { return this.end == this.start; }
        }

        public bool IsFull
        {
            get
            {
                return (end + 1) % buffer.Length == start;
            }
        }

        // Implementation of IEnumerator<T>
        // allows foreach
        // GetEnumerator => returns object that can be used to iterate through the collection
        // Returns Generic enumerator
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in buffer)
            {
                // ... other code
                // do some other stuff
                // "yield" keyword => will return each item (return then come back here and return next)
                yield return item;
            }
        }

        // Returns IEnumerator, not generic type
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        // New methods returns Enumerable of TOutput
        // ex. Strings for logger (contents of buffer)
        // NOW every IBuffer concrete type will be able return content of the buffer
        // with a specified type
        public IEnumerable<TOutput> AsEnumerableOf<TOutput>()
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
    }
}
