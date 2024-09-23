using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;


namespace WebApplication1.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(int id);
        User CreateUser(User newUser);
        void UpdateUser(int id, User updatedUser);
        void DeleteUser(int id);
    }

    public class UserService : IUserService
    {
        

        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.User.ToList(); 
        }

        public User GetUser(int id)
        {
            return _dbContext.User.FirstOrDefault(u => u.Id == id); 
        }

        public User CreateUser(User newUser)
        {
            _dbContext.User.Add(newUser);  
            _dbContext.SaveChanges();  
            return newUser;
        }

        public void UpdateUser(int id, User updatedUser)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                _dbContext.SaveChanges(); 
            }
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _dbContext.User.Remove(user); 
                _dbContext.SaveChanges(); 
            }
        }
    }
}
