using System;
using System.Collections.Generic;
using DataObjects;

namespace GenericCollections
{
    //ildasm
    //cw
    //ctor

    class Program
    {
        static void Main(string[] args)
        {
            // ARRAY
            //can't add items, have to do Array.Resize(ref array, new size 10)
            //Employee[] emp = new Employee[2];

            Employee[] employees = new Employee[2]
                                               {
                                                   new Employee { DepartmentId = 12, Name = "Scott" },
                                                   new Employee { DepartmentId = 9, Name = "Alex" }
                                               };

            // LIST
            // List allows adding, removing, inserting at index etc
            // List<T> => default capacity (4) when default capacity is not set, doubles each time if outgrows capacity
            var numbers = new List<int>();

            // QUEUE - FIFO
            Queue<Employee> queue = new Queue<Employee>();
            queue.Enqueue(new Employee { Name = "Chris" });
            queue.Enqueue(new Employee { Name = "Bob" });

            while (queue.Count > 0)
            {
                // store dequed employee
                var employee = queue.Dequeue();
                Console.WriteLine(employee.Name);

                // Check if queue contains some employees
                queue.Contains(employee);
            }

            // Copy queue to array (not reference but values copied)
            var array = queue.ToArray();

            // STACK - LIFO
            Stack<Employee> stack = new Stack<Employee>();
            stack.Push(new Employee { Name = "Chris" });
            stack.Push(new Employee { Name = "Bob" });


            while (stack.Count > 0)
            {
                // store poped employee
                var employee = stack.Pop();
                Console.WriteLine(employee.Name);

                // Check if stack contains some employees
                stack.Contains(employee);
            }

            // HASHSET - collection of uniqe numbers
            // Generic collection (Type Save) / different from HashTable which has no type safety
            // no order is set. 
            // if item already exists it will NOT be inserted (and will NOT throw exception)
            // BUT if you have two Employee objects they are different (new()), different references
            // If they're both referencing to one employee then it will NOT be added
            HashSet<int> set = new HashSet<int>();
            set.Add(1);
            set.Add(2);

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            // Intersect -> perform intersect of two sets (return values that are both in first and second set)
            // Union -> all items from both sets
            // SymetricExcept -> items in set one OR set two, BUT NOT both of the set (2 is in both, it will not be returned)

            // LINKED LIST
            // Each item knows about prev/next item
            // check for null if no next item or prev item
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddFirst(2);
            linkedList.AddFirst(3); // 3 before 2

            var first = linkedList.First;
            linkedList.AddAfter(first, 5);

            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }

            linkedList.RemoveFirst();

            // DICTIONARY
            // Orders info as inserting
            // TKey -> type of key
            // TValue -> type of value
            // Exception thrown when adding duplicate key
            var dict = new Dictionary<string, Employee>();
            dict.Add("Scott", new Employee { Name = "Scott" });
            dict.Add("Alex", new Employee { Name = "Alex" });

            // Find
            var scott = dict["Scott"];

            foreach (var item in dict)
            {
                Console.WriteLine("{0},{1}", item.Key, item.Value);
            }

            // Contains key
            dict.ContainsKey("someKey");
            dict.Remove("someKey");

            // SORTEDDICTIONARY
            // Storing data in a specific order
            // Will sort items as they are inserted
            // Optimized for inserts and removal by key
            var sortedDictionary = new SortedDictionary<string, Employee>();

            // SORTED LIST
            // Made for looping through things
            // Sorted list is optimized to use the least amount of memory and allows for itteration
            var sortedList = new SortedList<int, string>();
            sortedList.Add(3, "emp3");
            sortedList.Add(1, "emp3");

            // SORTED SET
            // Keeps everything in numerical order
            var sortedSet = new SortedSet<int>();
            sortedSet.Add(3);
            sortedSet.Add(1);
            var enumerator = set.GetEnumerator();
            enumerator.MoveNext();

            // SUMMARY / STRENGTHS
            // List<T> -> a growing array
            // Queue<T>, Stack<T> -> FIFO and LIFO
            // HashSet<T> -> unique items only
            // LinkedList<T> -> Flexible inserts
            // Dictionary<TKey, TValue> -> Quick lookup by key
            // SortedSet<T> -> Sorted & unique
            // SortedList<TKey, TValue> -> Sorted & memory efficient
            // SortedDictionary<Tkey, TValue> -> Sorted, fast inserts and removals
            // Concurrent Collections -> Thread safe, Multiple writers and readers
            // Immutable Collections -> Thread safe, modifications produce new collections (new list everytime you create it)

            // NAME / PURPOSE / IMPLEMENTED BY
            // IEnumerable<T> => iterate only
            // IList<T> => access by index => List<T>, SortedList<T>
            // ICollection<T> => add,remove,search => List<T>, Dictionary<K,V>, HashSet<T>
            // IDictionary<K,V> => access by key => Dictionary<K,V>
            // IReadOnlyCollection<T> => countable collection => List<T>, Dictionary<K,V>
            // ISet => set based operations => HashSet<T>
            // IComparer<T>, IEqualityComparer<T> => compare objects
        }
    }
}
