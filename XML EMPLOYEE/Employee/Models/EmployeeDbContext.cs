using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Employee.Models
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(): base("name=EmployeeDbContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeManager> EmployeeManagers { get; set; }
    }
}