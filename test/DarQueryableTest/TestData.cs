using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarQueryableTest
{
    public class TestData
    {
        public static List<Employee> GetList()
        {
            var list = new List<Employee>()
           {
               new Employee { Id = 1, Name = "Zhang", Birth = new DateTime(1980, 5, 3), Salary = 20000 },
               new Employee { Id = 2, Name = "Li", Birth = new DateTime(1970, 2, 11), Salary = 30000 },
               new Employee { Id = 3, Name = "Wang", Birth = new DateTime(1992, 9, 2), Salary = 8000 },
               new Employee { Id = 4, Name = "Zhao", Birth = new DateTime(1990, 3, 5), Salary = 12000 },
           };
            return list;
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public decimal Salary { get; set; }
    }
}
