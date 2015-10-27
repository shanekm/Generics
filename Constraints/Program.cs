﻿using Constraints.Repository;

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
                empRepository.Add(new Employee { Id = 1, Name = "John" });
                empRepository.Commit();

                // Variance => made IReadOnlyRepository() => Covariance
                DumpPeople(empRepository);

                // Variance => made IWriteOnlyRepository() => Contravariance
                AddManagers(empRepository);

                // Manager is child class of Employee
                empRepository.Add(new Manager { Id = 10, Name = "person" });
            }


            // Example 2
            // T and TKey implementation of RepositoryWithKey
            // Can NOT use ints since T must be of IEntity type
            // IRepositoryWithKey<int, Person> repoWithKey = new RepositoryWithKey<int, Person>();


            IRepositoryWithKey<EntityImplementation, Employee> repoWithKeyEmployee = new RepositoryWithKey<EntityImplementation, Employee>();
            repoWithKeyEmployee.AddItem(new EntityImplementation(1), new Employee { Name = "John" });
            repoWithKeyEmployee.AddItem(new EntityImplementation(2), new Employee { Name = "Steve" });
            repoWithKeyEmployee.AddItem(new EntityImplementation(3), new Employee { Name = "Bob" });
            Console.WriteLine(repoWithKeyEmployee.GetItem(new EntityImplementation(2)).Name);

            // Manager should also work
            IRepositoryWithKey<EntityImplementation, Manager> repoWithKeyManager = new RepositoryWithKey<EntityImplementation, Manager>();
            repoWithKeyManager.AddItem(new EntityImplementation(1), new Manager{ Name = "Lisa" });
            Console.WriteLine(repoWithKeyEmployee.GetItem(new EntityImplementation(1)).Name);
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
