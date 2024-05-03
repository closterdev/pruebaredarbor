using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

    }
}