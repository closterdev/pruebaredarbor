using Domain.Entities;

namespace Domain.Ports
{
    public interface IUserRepository
    {
        //Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByKeyAsync(string username, string password);
    }
}