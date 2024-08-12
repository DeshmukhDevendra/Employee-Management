using EmployeeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesByManagerAsync(int managerId);
        Task<bool> SaveEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
    }
}
