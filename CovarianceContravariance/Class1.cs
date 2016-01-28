using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CovarianceContravariance
{
    public class Animal{ public string Name { get; set; }}
    public class Horse : Animal{ public int Age { get; set; }}

    interface IInvariant<T>
    {
        // This interface can not be implicitly cast AT ALL Used for non-readonly collections
        //IList<T> GetList { get; }
        // Used when T is used as both argument *and* return type
        void hide(T t);
        T pull();
    }

    // Covariant - in - Write only. Can change from a class to a class derived from it
    // implicitly cast to MORE DERIVED. T could be base => sub
    public interface IContravariant<in T>
    {
        void Add(T t);
    }

    // Contravariant - out - Read only. Can change from a class to one of its base classes
    // implicitly cast to LESS DERIVED. T could be sub => base
    public interface ICovariant<out T>
    {
        T GetBase();
    }

    internal sealed class Concrete<T> : ICovariant<T>, IContravariant<T>, IInvariant<T>
    {
        T[] a = new T[10];
        int index = 0;

        public void Add(T t)
        {
            a[index] = t;
            index++;
        }

        public T GetBase()
        {
            return default(T);
        }

        public void hide(T t)
        {
            T d = t;
        }

        public T pull()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class Program
    {
        private static void PrintAnimals(IEnumerable<Animal> animals)
        {
            foreach (var animal in animals)
            {
                Console.WriteLine(animal.Name);
            }    
        }

        private static void Main(string[] args)
        {
            //IInvariant<Horse> i1a = new Concrete<Animal>();   // Error
            //IInvariant<Animal> i1b = new Concrete<Horse>();   // Error
            IInvariant<Horse> i1 = new Concrete<Horse>();       // Ok - same types
            //IInvariant<Animal> ia = i1;                      // Error
            IInvariant<Animal> ia = new Concrete<Animal>();
            ia.hide(new Horse());                            
            ia.hide(new Animal());

            ICovariant<Animal> i2 = new Concrete<Horse>(); // even tho defined as Animal, it can receive Horses
            i2.GetBase(); // covariance -> horse going into animal - cast to LESS DERIVED

            IContravariant<Horse> i3 = new Concrete<Animal>(); // even tho defined as Horse, it can recive Animal
            i3.Add(new Horse()); // contravariance -> horse going into animal - cast to MORE DERIVED


            // IEnumerable<out T> - Ienumerable is convarient
            IEnumerable<Animal> horses = new List<Animal>{ new Horse()};
            PrintAnimals(horses); // OK - horse is animal - covarience - to less Derived - Compiler converts IEnum<Horse> => IEnum<Animal>



        }
    }
}