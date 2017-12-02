using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.Models
{
    public class Employee
    {
        public long ID { get; set; }
        public string Employee_Name { get; set; }

        public double Salary { get; set; }

        public virtual EmployeeManager employeemanager { get; set; }

        public int EmployeeManagerID { get; set; }
    }
}