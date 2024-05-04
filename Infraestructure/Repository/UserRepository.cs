using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiContext _context;

        public UserRepository(ApiContext context)
        {
            _context = context;
        }

        //public async Task<User?> GetUserByIdAsync(int id)
        //{
        //    return await _context.Users.FindAsync(id);
        //}

        public async Task<User?> GetUserByKeyAsync(string username, string password)
        {
            return await _context.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        }
    }
}