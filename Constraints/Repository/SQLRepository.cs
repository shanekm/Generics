namespace Constraints.Abstract
{
    // Better to be generic because now I can send anything, Employee or Car etc
    // I can have CSVRepo sending in T objects and doing it's saving
    // I can ave SQLRepo sending in T object and inserting/saving in SQL db
    // where T => only OBJECT specific methods are available
    // IEntity => all objects T are implementing IEntity
    // where T : class => is a reference type
    // where T : struct => is value type
    // where T : Person => T must inherit Person
    // where T : new() => always last contraint. says type T has a constructor
    public class SQLRepository<T> : IRepository<T> where T : class, IEntity
    {
        public void Add(T newEntity)
        {
            // Now I can access methods and properties of IEntity
            if (newEntity.IsValid())
            {
                // do stuff
            }
            
            throw new System.NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public T FindById(int id)
        {
            // default keyword
            // creates an instance of T
            // Can NOT do = new T() => because compiler doesn't know if it can create an object
            T entity = default(T);

            // TAKE TWO => added where new();
            // now I can used new() instead of default(T);
            //T entity2 = new T();

            return entity;
        }

        public System.Linq.IQueryable<T> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public int Commit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
