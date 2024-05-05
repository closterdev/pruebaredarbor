using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Ports;
using Microsoft.IdentityModel.Tokens;
using Shared.Common;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeAddOut> CreateEmployeeAsync(EmployeeAddIn employeeIn)
        {
            try
            {
                Employee employee = MapDTOToEntity(employeeIn);
                //await _employeeRepository.AddEmployeeAsync(employee);
                await _employeeRepository.AddEmployeeWithDapperAsync(employee);
                return new EmployeeAddOut { Message = "Empleado guardado correctamente.", Result = Result.Success };
            }
            catch (System.Exception ex)
            {
                return new EmployeeAddOut { Message = $"Ha ocurrido un error. {ex.Message}", Result = Result.Error };
            }
        }

        public async Task<EmployeeListOut> GetEmployeeAsync()
        {
            try
            {
                IEnumerable<Employee>? employeeList = await _employeeRepository.GetAllEmployeesAsync();

                return employeeList.IsNullOrEmpty()
                    ? new EmployeeListOut { Message = "No existen registros actualmente.", Result = Result.NoRecords }
                    : new EmployeeListOut { Message = "Lista generada correctamente.", Result = Result.Success, EmployeesList = employeeList };
            }
            catch (System.Exception ex)
            {
                return new EmployeeListOut { Message = $"Ha ocurrido un error. {ex.Message}", Result = Result.Error };
            }
        }

        private static Employee MapDTOToEntity(EmployeeAddIn employeeDTO)
        {
            var employee = new Employee
            {
                CompanyId = employeeDTO.CompanyId,
                CreatedOn = employeeDTO.CreatedOn,
                DeletedOn = employeeDTO.DeletedOn,
                Email = employeeDTO.Email,
                Fax = employeeDTO.Fax,
                Name = employeeDTO.Name,
                LastLogin = employeeDTO.LastLogin,
                Password = employeeDTO.Password,
                PortalId = employeeDTO.PortalId,
                RoleId = employeeDTO.RoleId,
                StatusId = employeeDTO.StatusId,
                Telephone = employeeDTO.Telephone,
                UpdatedOn = employeeDTO.UpdatedOn,
                Username = employeeDTO.Username
            };

            return employee;
        }
    }
}