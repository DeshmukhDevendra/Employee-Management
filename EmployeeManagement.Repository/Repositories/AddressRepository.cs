using EmployeeManagement.Repository.Data;
using EmployeeManagement.Repository.Interface;
using EmployeeManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Repository.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AddressRepository> _logger;

        public AddressRepository(ApplicationDbContext context, ILogger<AddressRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            try
            {
                return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving address with Id {id}");
                throw;
            }
        }

        public async Task<IEnumerable<Address>> GetAddressesByEmployeeAsync(int employeeId)
        {
            try
            {
                return await _context.Addresses.Where(a => a.EmployeeId == employeeId)
                                               .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving addresses for employee with Id {employeeId}");
                throw;
            }
        }

        public async Task<bool> SaveAddressAsync(Address address)
        {
            try
            {
                _context.Addresses.Add(address);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving address");
                throw;
            }
        }

        public async Task<bool> UpdateAddressAsync(Address address)
        {
            try
            {
                _context.Addresses.Update(address);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating address");
                throw;
            }
        }
    }

}
