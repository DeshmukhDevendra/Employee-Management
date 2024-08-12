using EmployeeManagement.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> SaveEmployeeAsync(EmployeeDto employeeDto);
        Task<bool> UpdateAddressAsync(AddressDto addressDto);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByManagerAsync(int managerId);
        Task<IEnumerable<AddressDto>> GetAddressesByEmployeeAsync(int employeeId);
    }
}
