namespace IoC
{
    using Reflection;

    public interface ILogger
    {
        // log message etc.
    }

    public class SqlServerLogger : ILogger
    {
        // log message etc.
    }

    public interface IRepository<T>
    {
        
    }

    public class SqlRepository<T> : IRepository<T>
    {
        public SqlRepository(ILogger logger)
        {
            
        }
    }

    public class InvoiceService
    {
        public InvoiceService(IRepository<Employee> repository, ILogger logger)
        {
            
        }
    }
}
