namespace Start
{
    using System;
    using DataObjects;
    using DataObjects.Abstract;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // TAKE ONE
            var buffer = new CircularBuffer<double>();
            //ProcessInput(buffer);

            // TAKE TWO
            // Some scenarios where extension of buffer is needed
            // Avoid coding agains a concrete class, instead code against an interface
            var bufferInterface = new CircularBufferByInterface<double>();
            ProcessBufferByInterface(bufferInterface);

            // And I can use different Buffer without changing client code
            var queueBuffer = new QueueBuffer<double>();
            ProcessBufferByInterface(queueBuffer);

            // TAKE THREE
            // Enumerating a buffer - IEnumerable has been implemented
            // Now i'm able to use foreach
            foreach (var item in queueBuffer)
            {
                Console.WriteLine(item);
            }

            // TAKE FOUR - ADDING <TOutput> METHOD 
            // TOutput will return different TOutput type than Buffer<T> type
            var asInts = queueBuffer.AsEnumerableOf<int>();
            ProcessInputByInterface(queueBuffer);
            foreach (var item in queueBuffer)
            {
                Console.WriteLine(item);
            }

            // TAKE FIVE - ADDING EXTENSION METHOD that returns <TOutput)
            var asInts2 = queueBuffer.AsEnumerableOfExtension<double, int>();
            ProcessInputByInterface(queueBuffer);

            // TAKE SIX - ADDING ANOTHER EXTENSION (VOID) method
            queueBuffer.Dump();
        }

        // Passing concrete type
        private static void ProcessBuffer(CircularBuffer<double> buffer)
        {
            var sum = 0.0;
            Console.WriteLine("Buffer: ");
            
            while (!buffer.IsEmpty)
            {
                sum += buffer.Read();
            }

            Console.WriteLine(sum);
        }

        private static void ProcessInput(CircularBuffer<double> buffer)
        {
            while (true)
            {
                var value = 0.0;
                var input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    buffer.Write(value);
                    continue;
                }
                break;
            }
        }

        // Interface implementation
        // Allows for swapping out any buffer, not concrete type anymore - flexible
        // Doing this I can use any type of buffer, a circular buffer, maximized buffer etc
        // This says: I don't care what you pass me, as longs as it's IBuffer of <T>
        private static void ProcessBufferByInterface(IBuffer<double> buffer)
        {
            // I'm able to execute methods specified in IBuffer (contract)
            var sum = 0.0;
            Console.WriteLine("Buffer: ");

            while (!buffer.IsEmpty)
            {
                sum += buffer.Read();
            }

            Console.WriteLine(sum);
        }

        private static void ProcessInputByInterface(IBuffer<double> buffer)
        {
            while (true)
            {
                var value = 0.0;
                var input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    buffer.Write(value);
                    continue;
                }
                break;
            }
        }
    }
}
