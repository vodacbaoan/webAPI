using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserService
    {    
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User newUser);
        Task UpdateUserAsync(int id, User updatedUser);
        Task DeleteUserAsync(int id);
    }
}
