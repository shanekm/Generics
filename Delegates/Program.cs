namespace Delegates
{
    using System;
    using DataObjects;

    class Program
    {
        static void Main(string[] args)
        {
            // DELEGATE => pointer to an action/method()

            // ACTION<T>
            // Action => returns nothing, takes 0-16 parameters (in this case a double object)
            // TAKE ONE
            // Pointer to a method ConsoleWrite
            Action<double> print = ConsoleWrite;
            print(2.2);

            // Extension Method taking in Action<T> => sending a pointer to ConsoleWrite Method
            var queueBuffer = new QueueBuffer<double>();
            queueBuffer.DumpActionDelegate<double>(print);

            // TAKE TWO - ANONYMOUS DELEGATE METHOD
            // Where implementation is anonymous and not an actual Method()
            Action<double> printAnon = delegate(double data)
                { Console.Write(data); };

            var queueBuffer2 = new QueueBuffer<double>();
            queueBuffer2.DumpActionDelegate<double>(printAnon);

            // TAKE THREE - LAMBDA EXPRESSIONS
            var queueBuffer3 = new QueueBuffer<double>();
            queueBuffer3.DumpActionDelegate(d => ConsoleWrite(d));

            // Empty Lambda
            Action doNothing = () => { };


            // FUNC<T, K>
            // Func => returns one param, takes 0-16 parameters (in this case a double object) 
            // Last param is the return type
            // ie. Func<double, string> takes in double, returns a string
            // TAKE ONE
            // Squere a number
            Func<double, double> square = d => d * d;

            // Add two doubles, return a double
            Func<double, double, double> add = (x, y) => x + y;

            // TAKE FOUR
            // CONVERTER
            // Takes in one type and returns another
            Converter<double, string> converter = d => d.ToString();

            // Using Converter
            var queueBufferConverter = new QueueBuffer<double>();

            Converter<double, DateTime> converterDate = d => new DateTime(2010, 1, 1).AddDays(d);
            queueBufferConverter.AsEnumerableOfExtensionUsingConverter<double, DateTime>(converterDate);

            // TAKE FIVE
            // Adding event handler
            var queueWithEvent = new QueueBuffer<double>();

            // Subscribe to event and specify the method that will fire when event occurs
            queueWithEvent.ItemDiscarded += QueueWithEventOnItemDiscarded;
        }

        // Method for implementation "what item is being discarded"?
        private static void QueueWithEventOnItemDiscarded(object sender, ItemDiscardedEventArgs<double> e)
        {
            Console.WriteLine("Buffer Full. Discarding {0}, new Item is {1}", e.ITemDiscarded, e.NewItem);
        }

        static void ConsoleWrite(double data)
        {
            Console.Write(data);
        }
    }
}
