using Business.Services;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Commands.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, User>
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginCommandHandler(IUserService userService, IPasswordHasher passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetAllUsersAsync();

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var loggedInUser = user.FirstOrDefault(u => u.Username == request.Username && u.PasswordHash == hashedPassword);
            return loggedInUser; 
        }

     
    }
}
