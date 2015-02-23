namespace Reflection
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            // Will craete List`1 => says List<T> needs 1 T
            // ex. Dictionary`2 => says Dictionary<T, K> needs 2 things (T and K) => may need this syntax somewhere
            var employeeList = CreateCollection(typeof(List<Employee>));
            Console.WriteLine(employeeList.GetType().Name);
            var genericArguments = employeeList.GetType().GenericTypeArguments;

            foreach (var argument in genericArguments)
            {
                Console.WriteLine("[{0}]", argument.Name);
            }

            // TAKE TWO
            var employee = new Employee();
            var employeeType = typeof(Employee);
            var methodInfo = employeeType.GetMethod("Speak");
            methodInfo = methodInfo.MakeGenericMethod(typeof(DateTime));
            methodInfo.Invoke(employee, null);
        }

        private static object CreateCollection(Type type)
        {
            // Activator - will create a type with default constructor
            return Activator.CreateInstance(type);
        }
    }
}
