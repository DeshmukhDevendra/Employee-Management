using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string PinCode { get; set; }
        public int EmployeeId { get; set; }
    }

}
