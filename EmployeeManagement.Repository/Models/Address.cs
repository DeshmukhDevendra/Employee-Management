using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string PinCode { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
