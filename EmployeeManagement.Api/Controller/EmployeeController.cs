using EmployeeManagement.Business.Dtos;
using EmployeeManagement.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var result = await _employeeService.SaveEmployeeAsync(employeeDto);
                if (result)
                    return Ok();
                else
                    return StatusCode(500, "An error occurred while saving the employee.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SaveEmployee");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("address/update")]
        public async Task<IActionResult> UpdateAddress(AddressDto addressDto)
        {
            try
            {
                var result = await _employeeService.UpdateAddressAsync(addressDto);
                if (result)
                    return Ok();
                else
                    return NotFound("Address not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in UpdateAddress");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("manager/{managerId}/employees")]
        public async Task<IActionResult> GetEmployeesByManager(int managerId)
        {
            try
            {
                var employees = await _employeeService.GetEmployeesByManagerAsync(managerId);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetEmployeesByManager for managerId {managerId}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{employeeId}/addresses")]
        public async Task<IActionResult> GetAddressesByEmployee(int employeeId)
        {
            try
            {
                var addresses = await _employeeService.GetAddressesByEmployeeAsync(employeeId);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetAddressesByEmployee for employeeId {employeeId}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }

}
