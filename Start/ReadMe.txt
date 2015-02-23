// GENERICS
Generic types allow code reuse with type safety
- defer type specification to the client
- internal algorithms remain the same

// Generic class - anytime you have <T> at the end of class name
// <T> - type parameter
public class CircularBuffer<T>
{
	private T[] _buffer;
}

// Usage
// buffer1.GetType() == buffer2.GetType(); // true
// buffer1.GetType() == buffer3.GetType(); // false
var buffer1 = new CircularBuffer<double>();
var buffer2 = new CircularBuffer<double>();
var buffer3 = new CircularBuffer<string>();


-COLLECTIONS- SUMMARY / STRENGTHS
// List<T> -> a growing array
// Queue<T>, Stack<T> -> FIFO and LIFO
// Hashset<T> -> unique items only
// LinkedList<T> -> Flexible inserts
// Dictionary<TKey, TValue> -> Quick lookup by key
// SortedSet<T> -> Sorted & unique
// SortedList<TKey, TValue> -> Sorted & memory efficient
// SortedDictionary<Tkey, TValue> -> Sorted, fast inserts and removals
// Concurrent Collections -> Multiple writers and readers
// Immutable Collections -> Thread safe, modifications produce new collections


// NAME / PURPOSE / IMPLEMENTED BY
// IEnumerable<T> => iterate only
// IList<T> => access by index => List<T>, SortedList<T>, => iterate, modify, sort
// ICollection<T> => add,remove,search => List<T>, Dictionary<K,V>, HashSet<T> => iterate, modify
// IDictionary<K,V> => access by key => Dictionary<K,V>
// IReadOnlyCollection<T> => countable collection => List<T>, Dictionary<K,V>
// ISet => set based operations => HashSet<T>
// IComparer<T>, IEqualityComparer<T> => compare objects

// DELEGATES
Action => returns nothing, takes in 0 - 16 parameters
Func<double, string> => last type is the return type. (takes in double, return string)
Converter<TypeIn, TypeOut>

// CONSTRAINTS
- to be alble to access objects method, or interface methods etc, or abstract class
- to access constructor (if needed)


PROGRAM
1. Collection Types
2. Interface Implementation => swapping Buffers, QueueBuffer or CircularBuffer
3. Inheritance => overriding Methods for a specific Buffer type
4. IEnumerable => implementing IEnumerable, GetEnumerator, yield keyword
5. IComparer => IComparable, IComparer, sorting by key, sorting with HashSet, SortedDictionary
6. Building Custom Collection => creating custom collection, inheriting Dictionary
7. Extension Methods with Generics => creating extension method with T
8. Action<out>, Func<in,out> => action (returns void, up to 16 params), func (returns type = last param, takes in 16 params)
9. Converter<T, K> => from to converting and using typeOf
10. Events with Generic type => building custom EventArgs, subsribing to events
11. Constraints => building repository pattern, where clause, new(), inheritance
12. Variance => Covariance => allows for enumerating inherited types, building IReadOnlyRepository, DumpPeople()
13. Variance => Contravarience => allows for base class to be sent to more specific child class (send in Manager to Employee => manager is child class of Employee)
14. IoC => building IoC container, resolving types with default constructor, resolving types with constructor with params, resolving Generic types
15. Enum =>  building extension method for Enums, converts strings to enums strongly typed
16. Math => can't do math on generics +/ etc. Must use overloaded methods
17. Inheritance for Generic types => storing Item<int>, Item<double> in List<Item>, this is doable using inheritance

