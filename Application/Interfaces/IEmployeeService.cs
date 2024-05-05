using Application.Dtos;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeAddOut> CreateEmployeeAsync(EmployeeAddIn employeeIn);
        Task<EmployeeListOut> GetEmployeeAsync();
        Task<EmployeeIdOut> GetEmployeeIdAsync(int employeeId);
        Task<EmployeeItemOut> PutEmployeeAsync(int employeeId, EmployeeItemIn employee);
    }
}