using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public int? ReportsToId { get; set; }
        public Employee ReportsTo { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
    }
}
