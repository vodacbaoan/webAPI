using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserUnitOfWork
    {
        IUserRepository Users { get; }
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string user);
        Task<User> CreateUserAsync(User newUser);
        Task UpdateUserAsync(int id, User updatedUser);
        Task DeleteUserAsync(int id);
        Task<int> CompleteAsync();
    }
}
