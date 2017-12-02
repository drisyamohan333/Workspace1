using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.Models
{
    public class EmployeeManager
    {
        public int EmployeeManagerID { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Manager")]
        public string strManager { get; set; }
        //Navigational property
        public virtual ICollection<Employee> employees { get; set; }
    }
}