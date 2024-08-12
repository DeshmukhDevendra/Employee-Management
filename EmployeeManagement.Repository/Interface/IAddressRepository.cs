using EmployeeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository.Interface
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressByIdAsync(int id);
        Task<IEnumerable<Address>> GetAddressesByEmployeeAsync(int employeeId);
        Task<bool> SaveAddressAsync(Address address);
        Task<bool> UpdateAddressAsync(Address address);
    }
}
