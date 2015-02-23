using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using IoC;

namespace UnitTests
{
    [TestClass]
    public class IoCTests
    {
        // TAKE ONE - RESOLVING
        [TestMethod]
        public void Can_Resolve_Types()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();

            var logger = ioc.Resolve<ILogger>();

            // Is type of the logger correct?
            Assert.AreEqual(typeof(SqlServerLogger), logger.GetType());
        }

        // TAKE TWO - RESOLVING SqlRepository (which has logger in it instance)
        [TestMethod]
        public void Can_Resolve_Types_Without_Default_Ctor()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            var repository = ioc.Resolve<IRepository<Employee>>();

            // Is type of the logger correct?
            Assert.AreEqual(typeof(SqlRepository<Employee>), repository.GetType());
        }

        // TAKE THREE - resolving concrete type
        // Service has two interfaces (ILogger and IRepository)
        [TestMethod]
        public void Can_Resolve_Concrete_Type()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            var service = ioc.Resolve<InvoiceService>();

            Assert.IsNotNull(service);
        }

        // TAKE FOUR - resolving service 
        // Must be able to resolve IRepository<Employee>, OR IRepository<Customer> etc, Use Generics
        // Service has two interfaces (ILogger and IRepository)
        [TestMethod]
        public void Can_Resolve_Concrete_Type_With_Generics()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For(typeof(IRepository<>)).Use(typeof(SqlRepository<>));
            // Don't want to be doing this
            //ioc.For<IRepository<Customer>>().Use<SqlRepository<Customer>>();
            //ioc.For<IRepository<Car>>().Use<SqlRepository<Car>>();

            var service = ioc.Resolve<InvoiceService>();

            Assert.IsNotNull(service);
        }
    }
}
