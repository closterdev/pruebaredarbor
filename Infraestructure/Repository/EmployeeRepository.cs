using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infraestructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApiContext _context;
        private readonly IConfiguration _configuration;

        public EmployeeRepository(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("No se pudo guardar el empleado: " + ex.Message);
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
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

        public async Task AddEmployeeWithDapperAsync(Employee employee)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync("INSERT INTO Employees " +
                        "(CompanyId, CreatedOn, Email, Fax, [Name], Lastlogin, [Password], PortalId, RoleId, StatusId, Telephone, Username, IsDeleted) " +
                        "VALUES (@CompanyId, @CreatedOn, @Email, @Fax, @Name, @Lastlogin, @Password, @PortalId, @RoleId, @StatusId, @Telephone, @Username, @IsDeleted)",
                        new
                        {
                            employee.CompanyId,
                            employee.CreatedOn,
                            employee.Email,
                            employee.Fax,
                            employee.Name,
                            employee.LastLogin,
                            employee.Password,
                            employee.PortalId,
                            employee.RoleId,
                            employee.StatusId,
                            employee.Telephone,
                            employee.Username,
                            employee.IsDeleted
                        });
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("Error al agregar el empleado: " + ex.Message);
            }
        }

        public async Task<bool> UpdateEmployeeWithDapperAsync(Employee employee, int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var employeeIdExists = await connection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM Employees WHERE EmployeeId = @EmployeeId",
                    new { EmployeeId = id });

                if (employeeIdExists == 0)
                {
                    return false;
                }

                await connection.ExecuteAsync("UPDATE Employees SET " +
                "CompanyId = @CompanyId, Email = @Email, " +
                "Fax = @Fax, [Name] = @Name, [Password] = @Password, " +
                "PortalId = @PortalId, RoleId = @RoleId, StatusId = @StatusId, " +
                "Telephone = @Telephone, UpdatedOn = @UpdatedOn, Username = @Username " +
                "WHERE EmployeeId = @EmployeeId",
                    new
                    {
                        EmployeeId = id,
                        employee.CompanyId,
                        employee.Email,
                        employee.Fax,
                        employee.Name,
                        employee.Password,
                        employee.PortalId,
                        employee.RoleId,
                        employee.StatusId,
                        employee.Telephone,
                        UpdatedOn = System.DateTime.Now,
                        employee.Username
                    });

                return true;
            }
        }

        public async Task<bool> DeleteEmployeeWithDapperAsync(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var employeeIdExists = await connection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM Employees WHERE EmployeeId = @EmployeeId",
                    new { EmployeeId = id });

                if (employeeIdExists == 0)
                {
                    return false;
                }

                await connection.ExecuteAsync("UPDATE Employees SET IsDeleted = @IsDeleted, DeletedOn = @DeletedOn WHERE EmployeeId = @Id",
                    new {
                        Id = id,
                        DeletedOn = System.DateTime.Now,
                        IsDeleted = true
                    });

                return true;
            }
        }
    }
}