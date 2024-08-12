using EmployeeManagement.Business.Dtos;
using EmployeeManagement.Business.Interfaces;
using EmployeeManagement.Repository.Interface;
using EmployeeManagement.Repository.Models;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, IAddressRepository addressRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<bool> SaveEmployeeAsync(EmployeeDto employeeDto)
        {
            try
            {
                var employee = new Employee
                {
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Designation = employeeDto.Designation,
                    ReportsToId = employeeDto.ReportsToId
                };

                foreach (var addressDto in employeeDto.Addresses)
                {
                    var address = new Address
                    {
                        City = addressDto.City,
                        Area = addressDto.Area,
                        PinCode = addressDto.PinCode,
                        Employee = employee
                    };

                    employee.Addresses.Add(address);
                }

                return await _employeeRepository.SaveEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving employee");
                throw;
            }
        }

        public async Task<bool> UpdateAddressAsync(AddressDto addressDto)
        {
            try
            {
                var address = await _addressRepository.GetAddressByIdAsync(addressDto.Id);
                if (address == null)
                {
                    _logger.LogWarning($"Address with Id {addressDto.Id} not found");
                    return false;
                }

                address.City = addressDto.City;
                address.Area = addressDto.Area;
                address.PinCode = addressDto.PinCode;

                return await _addressRepository.UpdateAddressAsync(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating address with Id {addressDto.Id}");
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByManagerAsync(int managerId)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesByManagerAsync(managerId);
                return employees.Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Designation = e.Designation,
                    ReportsToId = e.ReportsToId
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving employees for manager with Id {managerId}");
                throw;
            }
        }

        public async Task<IEnumerable<AddressDto>> GetAddressesByEmployeeAsync(int employeeId)
        {
            try
            {
                var addresses = await _addressRepository.GetAddressesByEmployeeAsync(employeeId);
                return addresses.Select(a => new AddressDto
                {
                    Id = a.Id,
                    City = a.City,
                    Area = a.Area,
                    PinCode = a.PinCode
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving addresses for employee with Id {employeeId}");
                throw;
            }
        }
    }

}
