using EmployeeManagement.Business.Dtos;
using EmployeeManagement.Business.Services;
using EmployeeManagement.Repository.Interface;
using EmployeeManagement.Repository.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly Mock<ILogger<EmployeeService>> _loggerMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _loggerMock = new Mock<ILogger<EmployeeService>>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object, _addressRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task SaveEmployeeAsync_ShouldReturnTrue_WhenEmployeeIsSavedSuccessfully()
        {
            // Arrange
            var employeeDto = new EmployeeDto { FirstName = "John", LastName = "Doe", Designation = "Developer" };
            _employeeRepositoryMock.Setup(repo => repo.SaveEmployeeAsync(It.IsAny<Employee>())).ReturnsAsync(true);

            // Act
            var result = await _employeeService.SaveEmployeeAsync(employeeDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAddressAsync_ShouldReturnTrue_WhenAddressIsUpdatedSuccessfully()
        {
            // Arrange
            var addressDto = new AddressDto { Id = 1, City = "City", Area = "Area", PinCode = "12345" };
            var address = new Address { Id = 1, City = "OldCity", Area = "OldArea", PinCode = "54321" };
            _addressRepositoryMock.Setup(repo => repo.GetAddressByIdAsync(1)).ReturnsAsync(address);
            _addressRepositoryMock.Setup(repo => repo.UpdateAddressAsync(It.IsAny<Address>())).ReturnsAsync(true);

            // Act
            var result = await _employeeService.UpdateAddressAsync(addressDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetEmployeesByManagerAsync_ShouldReturnEmployeeDtos_WhenEmployeesExist()
        {
            // Arrange
            var employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Designation = "Developer", ReportsToId = 2 },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Doe", Designation = "Manager", ReportsToId = null }
        };
            _employeeRepositoryMock.Setup(repo => repo.GetEmployeesByManagerAsync(2)).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetEmployeesByManagerAsync(2);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAddressesByEmployeeAsync_ShouldReturnAddressDtos_WhenAddressesExist()
        {
            // Arrange
            var addresses = new List<Address>
        {
            new Address { Id = 1, City = "City1", Area = "Area1", PinCode = "12345", EmployeeId = 1 },
            new Address { Id = 2, City = "City2", Area = "Area2", PinCode = "54321", EmployeeId = 1 }
        };
            _addressRepositoryMock.Setup(repo => repo.GetAddressesByEmployeeAsync(1)).ReturnsAsync(addresses);

            // Act
            var result = await _employeeService.GetAddressesByEmployeeAsync(1);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task SaveEmployeeAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var employeeDto = new EmployeeDto { FirstName = "John", LastName = "Doe", Designation = "Developer" };
            _employeeRepositoryMock.Setup(repo => repo.SaveEmployeeAsync(It.IsAny<Employee>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _employeeService.SaveEmployeeAsync(employeeDto));

            // Assert
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error saving employee")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }

}
