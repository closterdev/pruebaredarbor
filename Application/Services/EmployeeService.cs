using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Ports;
using Microsoft.IdentityModel.Tokens;
using Shared.Common;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeAddOut> CreateEmployeeAsync(EmployeeAddIn employeeIn)
        {
            try
            {
                Employee employee = MapDTOToEntity(employeeIn);
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

        public async Task<EmployeeIdOut> GetEmployeeIdAsync(int employeeId)
        {
            try
            {
                Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

                return employee != null
                    ? new EmployeeIdOut { Message = "Se consulto el empleado correctamente.", Result = Result.Success, EmployeeItem = employee }
                    : new EmployeeIdOut { Message = "No se encontro el empleado.", Result = Result.NoRecords };
            }
            catch (System.Exception ex)
            {
                return new EmployeeIdOut { Message = $"Ha ocurrido un error. {ex.Message}", Result = Result.Error };
            }
        }

        public async Task<EmployeeItemOut> PutEmployeeAsync(int employeeId, EmployeeItemIn employee)
        {
            try
            {
                Employee employeeUp = MapDTOToEntity(employee);
                bool updateOut = await _employeeRepository.UpdateEmployeeWithDapperAsync(employeeUp, employeeId);

                return updateOut
                    ? new EmployeeItemOut { Message = "Empleado actualizado correctamente.", Result = Result.Success }
                    : new EmployeeItemOut { Message = "No se encontro el empleado.", Result = Result.NoRecords };
            }
            catch (System.Exception ex)
            {
                return new EmployeeItemOut { Message = $"Ha ocurrido un error. {ex.Message}", Result = Result.Error };
            }
        }

        public async Task<EmployeeItemOut> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                bool deleteOut = await _employeeRepository.DeleteEmployeeWithDapperAsync(employeeId);

                return deleteOut
                    ? new EmployeeItemOut { Message = "Empleado eliminado correctamente.", Result = Result.Success }
                    : new EmployeeItemOut { Message = "No se encontro el empleado.", Result = Result.NoRecords };
            }
            catch (System.Exception ex)
            {
                return new EmployeeItemOut { Message = $"Ha ocurrido un error. {ex.Message}", Result = Result.Error };
            }
        }

        private Employee MapDTOToEntity<T>(T employeeDTO)
        {
            return _mapper.Map<T, Employee>(employeeDTO);
        }
    }
}