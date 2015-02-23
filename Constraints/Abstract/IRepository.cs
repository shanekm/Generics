using System;
using System.Linq;
namespace Constraints.Abstract
{
    // Generic repository
    // I Want to be able to access any repository, PersonRepository, CarsRepository etc
    // therefor I'm going to use abstraction for repository

    // When to apply Constraints to Interface? => avoid it
    //public interface IRepository<T> : IDisposable where T : class, IEntity
    
    // This is better
    // TAKE TWO - MAKING IRepository ReadOnly operations Covariant
    public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T>
    {
        // TAKE OUT METHODS IMPLEMENTED BY CONTRAVERIENCE
        //void Add(T newEntity);

        //void Delete(T entity);

        //int Commit();

        // TAKE OUT METHODS IMPLEMENTED BY COVARIENCE
        //T FindById(int id);

        //IQueryable<T> FindAll();
    }

    // TAKE TWO - COVARIANCE - ONLY WORKS WITH INTERFACES
    // allows for inherited types to be processed
    // ex. send in Employee but return Person
    // Should be able to pass in Person OR Employee - this only works when GETTING/IEnumerating
    // but NOT adding, it doesn't make sense to Add() Person as Employee or do some other processing
    // that Employee should not be doing
    // However enumerating is OK
    // 'out' keyword
    public interface IReadOnlyRepository<out T> : IDisposable
    {
        T FindById(int id);

        IQueryable<T> FindAll();
    }

    // TAKE THREE - CONTRAVARIANCE - INPUT MORE BROAD TO SPECIFIC
    public interface IWriteOnlyRepository<in T> : IDisposable
    {
        void Add(T newEntity);

        void Delete(T entity);

        int Commit();
    }
}
