using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Commands.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, User>
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler(IUserService userService, IPasswordHasher passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            
            var hashedPassword = _passwordHasher.HashPassword(request.Password);

        
            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                Role = "testRole"
               
            };

            
            await _userService.CreateUserAsync(newUser);

            return newUser;
        }

       
        
    }
}
