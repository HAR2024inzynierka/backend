using Workshop.Core.Entities;

namespace Workshop.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<bool> EmailExistsAsync(string email);
        Task<List<User>> GetAllUsersAsync();
    }
}
