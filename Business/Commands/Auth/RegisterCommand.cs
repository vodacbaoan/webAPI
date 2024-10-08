using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Commands.Auth
{
    public class RegisterCommand : IRequest<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public RegisterCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
