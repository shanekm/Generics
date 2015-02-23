namespace Constraints
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Constraints.Abstract;

    class Program
    {
        static void Main(string[] args)
        {
            // Using Repository Pattern
            using (IRepository<Employee> empRepository = new SQLRepository<Employee>())
            {
                empRepository.Add(new Employee{ Id = 1, Name = "John" });
                empRepository.Commit();

                // Variance => made IReadOnlyRepository() => Covariance
                DumpPeople(empRepository);

                // Variance => made IWriteOnlyRepository() => Contravariance
                AddManagers(empRepository);
            }
        }

        // Should be able to pass in Person OR Employee - this only works when GETTING/IEnumerating
        // but NOT adding, it doesn't make sense to Add() Person as Employee or do some other processing
        // that Employee should not be doing
        // However enumerating is OK
        // 'out' keyword

        // Covariance => treat Employee as Person
        public static void DumpPeople(IReadOnlyRepository<Employee> empRepository)
        {
            var emps = empRepository.FindAll();
            foreach (var employee in emps)
            {
                Console.WriteLine(employee);
            }
        }

        // Contravariance => treat Employee as Manager
        public static void AddManagers(IRepository<Employee> empRepository)
        {
            empRepository.Add(new Manager());
            empRepository.Commit();
        }
    }
}
