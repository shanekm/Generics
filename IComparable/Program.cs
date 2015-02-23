namespace Comparable
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    using DataObjects;

    class Program
    {
        static void Main(string[] args)
        {
            // SortedDictionary => sorts by KEY (so if you put sales first) it will show 
            // Engineering first because of SortedDictionary (unline Dictionary<K,V>)
            // What if you want to compare items so that Dani does NOT show up twice
            // in Saled department
            var departments = new SortedDictionary<string, HashSet<Employee>>();

            // TAKE ONE
            // HashSet<T> => Constructor allows only for IEqualityComparer
            // Specifying EmployeeCmparer doesn't allow for duplicates
            departments.Add("Sales", new HashSet<Employee>(new EmployeeComparer()));
            departments["Sales"].Add(new Employee { Name = "Joy" });
            departments["Sales"].Add(new Employee { Name = "Dani" });

            // Duplicate => Without (EmployeeCmparer())
            // Specifying EmployeeCmparer doesn't allow for duplicates => duplicates no longer exists when running the code
            departments["Sales"].Add(new Employee { Name = "Dani" });

            departments.Add("Engineering", new HashSet<Employee>(new EmployeeComparer()));
            departments["Engineering"].Add(new Employee { Name = "Scott" });
            departments["Engineering"].Add(new Employee { Name = "Alex" });
            departments["Engineering"].Add(new Employee { Name = "Dani" });

            foreach (var department in departments)
            {
                Console.WriteLine(department.Key);
                foreach (var employee in department.Value)
                {
                    Console.WriteLine("\t" + employee.Name);
                }
            }


            // TAKE TWO
            // Specifying EmployeeCmparer doesn't allow for duplicates
            // SortedSet<T> => Constructor allows only for IComparer
            var departments2 = new SortedDictionary<string, SortedSet<Employee>>();
    
            // Making it a SortedSet will sort Employees
            departments2.Add("Sales", new SortedSet<Employee>(new EmployeeComparer()));
            departments2["Sales"].Add(new Employee { Name = "Joy" });
            departments2["Sales"].Add(new Employee { Name = "Dani" });

            // Duplicate => Without (EmployeeCmparer())
            // Specifying EmployeeCmparer doesn't allow for duplicates => duplicates no longer exists when running the code
            departments2["Sales"].Add(new Employee { Name = "Dani" });

            departments2.Add("Engineering", new SortedSet<Employee>(new EmployeeComparer()));
            departments2["Engineering"].Add(new Employee { Name = "Scott" });
            departments2["Engineering"].Add(new Employee { Name = "Alex" });
            departments2["Engineering"].Add(new Employee { Name = "Dani" });

            foreach (var department in departments2)
            {
                Console.WriteLine(department.Key);
                foreach (var employee in department.Value)
                {
                    Console.WriteLine("\t" + employee.Name);
                }
            }

            // TAKE THREE
            // Using Custom Collection - DepartmentCollection
            var departments3 = new DepartmentCollection();

            // Making it a SortedSet will sort Employees
            departments3.Add("Sales", new Employee { Name = "Joy" });
            departments3.Add("Sales", new Employee { Name = "Dani" });
            departments3.Add("Sales", new Employee { Name = "Dani" });

            // notice Syntax - still good
            departments3.Add("Engineering", new Employee { Name = "Scott" })
                .Add("Engineering", new Employee { Name = "Alex" })
                .Add("Engineering", new Employee { Name = "Dani" });

            foreach (var department in departments3)
            {
                Console.WriteLine(department.Key);
                foreach (var employee in department.Value)
                {
                    Console.WriteLine("\t" + employee.Name);
                }
            }

            Console.ReadKey();
        }

        // IEqualityComparer => check if objects are equal (for adding new ones -> duplicates)
        // IComparer => sort by Name
        // To make sure you only have one Dani (Employee) per Department
        // Create a Comparer that will let HashSet know how to compare
        // if an Employee object already exists
        public class EmployeeComparer : IEqualityComparer<Employee>, IComparer<Employee>
        {
            public bool Equals(Employee x, Employee y)
            {
                // Compare by Name
                return String.Equals(x.Name, y.Name);
            }

            // This makes sure that Two Employees with the same name
            // will generate the same Hash code
            public int GetHashCode(Employee obj)
            {
                return obj.Name.GetHashCode();
            }

            // IComparer implementation
            // Return integer
            // return < 0 => first object is less than second object
            // return > 0 => first object is greater than second object
            // return = 0 => first and second object are the same
            public int Compare(Employee x, Employee y)
            {
                return String.CompareOrdinal(x.Name, y.Name);
            }
        }

        // TAKE THREE - CUSTOM COLLECTION
        // Building custom to model departments
        // easier to use than having Dicitonary of HashSets etc
        public class DepartmentCollection : SortedDictionary<string, SortedSet<Employee>>
        {
            // Could be void here
            // but we want to return itself
            public DepartmentCollection Add(string departmentName, Employee employee)
            {
                if (!this.ContainsKey(departmentName))
                {
                    this.Add(departmentName, new SortedSet<Employee>(new EmployeeComparer()));
                }

                this[departmentName].Add(employee);

                // Return itself
                return this;
            }
        }

    }
}
