using Domain.Entities;

namespace Domain.Ports
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
        Task AddEmployeeWithDapperAsync(Employee employee);
        Task<bool> UpdateEmployeeWithDapperAsync(Employee employee);
    }
}