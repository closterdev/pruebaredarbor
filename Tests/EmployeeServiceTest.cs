using Moq;
using Application.Services;
using Domain.Ports;
using Application.Dtos;
using Domain.Entities;
using Shared.Common;
using AutoMapper;

namespace Tests
{
    public class EmployeeServiceTest
    {
        private readonly EmployeeService _employeeService;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public EmployeeServiceTest()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateEmployeeAsync_Success()
        {
            // Arrange
            EmployeeAddIn employeeToAdd = new()
            {
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeletedOn = DateTime.Now,
                Email = "test1@test.test.tmp",
                Fax = "000.000.000",
                Name = "test1",
                LastLogin = DateTime.Now,
                Password = "test",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                UpdatedOn = DateTime.Now,
                Username = "test1"
            };

            // Act
            var result = await _employeeService.CreateEmployeeAsync(employeeToAdd);

            // Assert
            Assert.Equal(Result.Success, result.Result);
            Assert.Equal("Empleado guardado correctamente.", result.Message);
        }

        [Fact]
        public async Task CreateEmployeeAsync_Exception()
        {
            // Arrange
            _employeeRepositoryMock.Setup(repo => repo.AddEmployeeWithDapperAsync(It.IsAny<Employee>()))
                                   .ThrowsAsync(new System.Exception("Simulated error"));

            EmployeeAddIn employeeToAdd = new()
            {
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeletedOn = DateTime.Now,
                Email = "test1@test.test.tmp",
                Fax = "000.000.000",
                Name = "test1",
                LastLogin = DateTime.Now,
                Password = "test",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                UpdatedOn = DateTime.Now,
                Username = "test1"
            };

            // Act
            var result = await _employeeService.CreateEmployeeAsync(employeeToAdd);

            // Assert
            Assert.Equal(Result.Error, result.Result);
            Assert.Contains("Ha ocurrido un error.", result.Message);
        }

        [Fact]
        public async Task GetEmployeeAsync_Success()
        {
            // Arrange
            Employee employee1 = new()
            {
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeletedOn = DateTime.Now,
                Email = "test1@test.test.tmp",
                Fax = "000.000.000",
                Name = "test1",
                LastLogin = DateTime.Now,
                Password = "test",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                UpdatedOn = DateTime.Now,
                Username = "test1"
            };

            Employee employee2 = new()
            {
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeletedOn = DateTime.Now,
                Email = "test1@test.test.tmp",
                Fax = "000.000.000",
                Name = "test1",
                LastLogin = DateTime.Now,
                Password = "test",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                UpdatedOn = DateTime.Now,
                Username = "test1"
            };

            List<Employee> employeesList = new() { employee1, employee2 };

            _employeeRepositoryMock.Setup(repo => repo.GetAllEmployeesAsync())
                                   .ReturnsAsync(employeesList);

            // Act
            var result = await _employeeService.GetEmployeeAsync();

            // Assert
            Assert.Equal(Result.Success, result.Result);
            Assert.Equal("Lista generada correctamente.", result.Message);
            Assert.Equal(employeesList, result.EmployeesList);
        }

        [Fact]
        public async Task GetEmployeeAsync_NoRecords()
        {
            // Arrange
            _employeeRepositoryMock.Setup(repo => repo.GetAllEmployeesAsync())
                                   .ReturnsAsync(new List<Employee>());

            // Act
            var result = await _employeeService.GetEmployeeAsync();

            // Assert
            Assert.Equal(Result.NoRecords, result.Result);
            Assert.Equal("No existen registros actualmente.", result.Message);
            Assert.Null(result.EmployeesList);
        }

        [Fact]
        public async Task GetEmployeeAsync_Exception()
        {
            // Arrange
            _employeeRepositoryMock.Setup(repo => repo.GetAllEmployeesAsync())
                                   .ThrowsAsync(new System.Exception("Simulated error"));

            // Act
            var result = await _employeeService.GetEmployeeAsync();

            // Assert
            Assert.Equal(Result.Error, result.Result);
            Assert.Contains("Ha ocurrido un error.", result.Message);
            Assert.Null(result.EmployeesList);
        }

        [Fact]
        public async Task GetEmployeeIdAsync_Exists()
        {
            // Arrange
            int employeeId = 1;
            Employee existingEmployee = new()
            {
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeletedOn = DateTime.Now,
                Email = "test1@test.test.tmp",
                Fax = "000.000.000",
                Name = "test1",
                LastLogin = DateTime.Now,
                Password = "test",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                UpdatedOn = DateTime.Now,
                Username = "test1"
            };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeByIdAsync(employeeId))
                                   .ReturnsAsync(existingEmployee);

            // Act
            var result = await _employeeService.GetEmployeeIdAsync(employeeId);

            // Assert
            Assert.Equal(Result.Success, result.Result);
            Assert.Equal("Se consulto el empleado correctamente.", result.Message);
            Assert.Equal(existingEmployee, result.EmployeeItem);
        }

        [Fact]
        public async Task GetEmployeeIdAsync_NotExists()
        {
            // Arrange
            int employeeId = 2;
            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeByIdAsync(employeeId))
                                   .ReturnsAsync((Employee)null);

            // Act
            var result = await _employeeService.GetEmployeeIdAsync(employeeId);

            // Assert
            Assert.Equal(Result.NoRecords, result.Result);
            Assert.Equal("No se encontro el empleado.", result.Message);
            Assert.Null(result.EmployeeItem);
        }

        [Fact]
        public async Task GetEmployeeIdAsync_Exception()
        {
            // Arrange
            int employeeId = 3;
            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<int>()))
                                   .ThrowsAsync(new System.Exception("Simulated error"));


            // Act
            var result = await _employeeService.GetEmployeeIdAsync(employeeId);

            // Assert
            Assert.Equal(Result.Error, result.Result);
            Assert.Contains("Ha ocurrido un error.", result.Message);
            Assert.Null(result.EmployeeItem);
        }

        [Fact]
        public async Task PutEmployeeAsync_UpdatedSuccessfully()
        {
            // Arrange
            var employeeId = 1;
            var employeeIn = new EmployeeItemIn
            {
                CompanyId = 1,
                Email = "test1@test.test.tmp",
                Fax = "000.000.000",
                Name = "test1",
                Password = "test",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                Username = "test1"
            };

            var updatedSuccessfully = true;
            _employeeRepositoryMock.Setup(repo => repo.UpdateEmployeeWithDapperAsync(It.IsAny<Employee>(), employeeId))
                                   .ReturnsAsync(updatedSuccessfully);

            // Act
            var result = await _employeeService.PutEmployeeAsync(employeeId, employeeIn);

            // Assert
            Assert.Equal(Result.Success, result.Result);
            Assert.Equal("Empleado actualizado correctamente.", result.Message);
        }

        [Fact]
        public async Task PutEmployeeAsync_NotFound()
        {
            // Arrange
            var employeeId = 2;
            var employeeIn = new EmployeeItemIn {
                CompanyId = 1,
                Email = "test1@test.test.tmp",
                Fax = "000.000.000",
                Name = "test1",
                Password = "test",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                Username = "test1"
            };

            var updatedSuccessfully = false;
            _employeeRepositoryMock.Setup(repo => repo.UpdateEmployeeWithDapperAsync(It.IsAny<Employee>(), employeeId))
                                   .ReturnsAsync(updatedSuccessfully);

            // Act
            var result = await _employeeService.PutEmployeeAsync(employeeId, employeeIn);

            // Assert
            Assert.Equal(Result.NoRecords, result.Result);
            Assert.Equal("No se encontro el empleado.", result.Message);
        }

        [Fact]
        public async Task PutEmployeeAsync_Exception()
        {
            // Arrange
            var employeeId = 9;
            var employeeIn = new EmployeeItemIn {
                CompanyId = 1,
                Email = "test1@test.test.tmp",
                Fax = "000.000.000",
                Name = "test1",
                Password = "test",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                Username = "test1"
            };

            _employeeRepositoryMock.Setup(repo => repo.UpdateEmployeeWithDapperAsync(It.IsAny<Employee>(), It.IsAny<int>()))
                                   .ThrowsAsync(new System.Exception("Simulated error"));


            // Act
            var result = await _employeeService.PutEmployeeAsync(employeeId, employeeIn);

            // Assert
            Assert.Equal(Result.Error, result.Result);
            Assert.Contains("Ha ocurrido un error.", result.Message);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_DeletedSuccessfully()
        {
            // Arrange
            var employeeId = 1;
            var deletedSuccessfully = true;
            _employeeRepositoryMock.Setup(repo => repo.DeleteEmployeeWithDapperAsync(employeeId))
                                   .ReturnsAsync(deletedSuccessfully);

            // Act
            var result = await _employeeService.DeleteEmployeeAsync(employeeId);

            // Assert
            Assert.Equal(Result.Success, result.Result);
            Assert.Equal("Empleado eliminado correctamente.", result.Message);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_NotFound()
        {
            // Arrange
            var employeeId = 22;
            var deletedSuccessfully = false;
            _employeeRepositoryMock.Setup(repo => repo.DeleteEmployeeWithDapperAsync(employeeId))
                                   .ReturnsAsync(deletedSuccessfully);

            // Act
            var result = await _employeeService.DeleteEmployeeAsync(employeeId);

            // Assert
            Assert.Equal(Result.NoRecords, result.Result);
            Assert.Equal("No se encontro el empleado.", result.Message);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_Exception()
        {
            // Arrange
            var employeeId = 8;
            _employeeRepositoryMock.Setup(repo => repo.DeleteEmployeeWithDapperAsync(It.IsAny<int>()))
                                   .ThrowsAsync(new System.Exception("Simulated error"));


            // Act
            var result = await _employeeService.DeleteEmployeeAsync(employeeId);

            // Assert
            Assert.Equal(Result.Error, result.Result);
            Assert.Contains("Ha ocurrido un error.", result.Message);
        }
    }
}