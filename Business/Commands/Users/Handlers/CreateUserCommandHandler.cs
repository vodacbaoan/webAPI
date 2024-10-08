using Core.Interfaces;
using Core.Entities;
using MediatR;

namespace Business.Commands.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
               
                Name = request.Name,
                Email = request.Email
            }; 

            await _userService.CreateUserAsync(user);
            return user;
        }

    }
}
