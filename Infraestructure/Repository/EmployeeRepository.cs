using Dapper;
using Domain.Entities;
using Domain.Ports;
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

        public async Task AddEmployeeWithDapperAsync(Employee employ)
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
                            employ.CompanyId,
                            employ.CreatedOn,
                            employ.Email,
                            employ.Fax,
                            employ.Name,
                            employ.LastLogin,
                            employ.Password,
                            employ.PortalId,
                            employ.RoleId,
                            employ.StatusId,
                            employ.Telephone,
                            employ.Username,
                            employ.IsDeleted
                        });
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("Error al agregar el empleado: " + ex.Message);
            }
        }

        public async Task UpdateEmployeeWithDapperAsync(Employee employ, int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("UPDATE Employees SET " +
                "CompanyId = @CompanyId, Email = @Email, " +
                "Fax = @Fax, [Name] = @Name, Lastlogin = @Lastlogin, [Password] = @Password, " +
                "PortalId = @PortalId, RoleId = @RoleId, StatusId = @StatusId, " +
                "Telephone = @Telephone, UpdatedOn = @UpdatedOn, Username = @Username " +
                "WHERE EmployeeId = @EmployeeId",
                    new
                    {
                        employ.EmployeeId,
                        employ.CompanyId,
                        employ.Email,
                        employ.Fax,
                        employ.Name,
                        employ.LastLogin,
                        employ.Password,
                        employ.PortalId,
                        employ.RoleId,
                        employ.StatusId,
                        employ.Telephone,
                        employ.UpdatedOn,
                        employ.Username
                    });
            }
        }

        public async Task DeleteEmployeeWithDapperAsync(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("DELETE FROM Employees WHERE EmployeeId = @Id", new { Id = id });
            }
        }
    }
}