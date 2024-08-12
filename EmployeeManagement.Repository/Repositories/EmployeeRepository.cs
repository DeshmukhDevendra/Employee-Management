using EmployeeManagement.Repository.Data;
using EmployeeManagement.Repository.Interface;
using EmployeeManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Repository.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(ApplicationDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                return await _context.Employees.Include(e => e.Addresses)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving employee with Id {id}");
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByManagerAsync(int managerId)
        {
            try
            {
                return await _context.Employees.Where(e => e.ReportsToId == managerId)
                                               .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving employees reporting to manager with Id {managerId}");
                throw;
            }
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving employee");
                throw;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee");
                throw;
            }
        }
    }
}
