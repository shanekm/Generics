namespace Constraints.Abstract
{
    // SEE SQLREPOSITORY CLASS FOR BETTER DOCUMENTATION
    // Better to be generic because now I can send anything, Employee or Car etc
    // I can have CSVRepo sending in T objects and doing it's saving
    // I can ave SQLRepo sending in T object and inserting/saving in SQL db
    // where T => only OBJECT specific methods are available
    public class CSVRepository<T> : IRepository<T> where T : class
    {
        public void Add(T newEntity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public T FindById(int id)
        {
            throw new System.NotImplementedException();
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
