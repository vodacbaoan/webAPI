using Core.Entities;
using Core.Interfaces;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public UserService(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();
            return newUser;
        }

        public async Task UpdateUserAsync(int id, User updatedUser)
        {
            var existingUser = await _unitOfWork.Users.GetByIdAsync(id);
            if (existingUser != null)
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Email = updatedUser.Email;
                await _unitOfWork.CompleteAsync(); 
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user != null)
            {
                await _unitOfWork.Users.RemoveAsync(user);
                await _unitOfWork.CompleteAsync();
            }
        }

        //private readonly IGenericRepository<User> _repository;

        //public UserService(IGenericRepository<User> repository)
        //{
        //    _repository = repository;
        //}

        //public IEnumerable<User> GetAllUsers()
        //{
        //    return _repository.GetAll();
        //}

        //public User GetUserById(int id)
        //{
        //    return _repository.GetById(id);
        //}

        //public User CreateUser(User newUser)
        //{
        //    _repository.Add(newUser);
        //    _repository.Save();
        //    return newUser; 
        //}

        //public void UpdateUser(int id, User updatedUser)
        //{
        //    var existingUser = _repository.GetById(id);
        //    if (existingUser != null)
        //    {
        //        existingUser.Name = updatedUser.Name;
        //        existingUser.Email = updatedUser.Email;
        //        _repository.Save();
        //    }
        //}

        //public void DeleteUser(int id)
        //{
        //    var user = _repository.GetById(id);
        //    if (user != null)
        //    {
        //        _repository.Remove(user);
        //        _repository.Save();
        //    }
        //}

        //public async Task<IEnumerable<User>> GetAllUsersAsync()
        //{
        //    return await _repository.GetAllAsync();
        //}

        //public async Task<User?> GetUserByIdAsync(int id)
        //{
        //    return await _repository.GetByIdAsync(id);
        //}

        //public async Task<User> CreateUserAsync(User newUser)
        //{
        //    await _repository.AddAsync(newUser);
        //    await _repository.SaveAsync(); 
        //    return newUser;
        //}

        //public async Task UpdateUserAsync(int id, User updatedUser)
        //{
        //    var existingUser = await _repository.GetByIdAsync(id);
        //    if (existingUser != null)
        //    {
        //        existingUser.Name = updatedUser.Name;
        //        existingUser.Email = updatedUser.Email;
        //        await _repository.SaveAsync();
        //    }
        //}

        //public async Task DeleteUserAsync(int id)
        //{
        //    var user = await _repository.GetByIdAsync(id);
        //    if (user != null)
        //    {
        //        await _repository.RemoveAsync(user);
        //        await _repository.SaveAsync(); 
        //    }
        //}


    }
}

