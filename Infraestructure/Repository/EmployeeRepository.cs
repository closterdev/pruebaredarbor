using Domain.Entity;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApiContext _context;

        public EmployeeRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task AddEmployeeAsync(Employee employ)
        {
            try
            {
                _context.Employees.Add(employ);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("No se pudo guardar el empleado: " + ex.Message);
            }
        }

        public async Task UpdateEmployeeAsync(Employee employ)
        {
            _context.Entry(employ).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employ = await _context.Employees.FindAsync(id);
            if (employ != null)
            {
                _context.Employees.Remove(employ);
                await _context.SaveChangesAsync();
            }
        }
    }
}