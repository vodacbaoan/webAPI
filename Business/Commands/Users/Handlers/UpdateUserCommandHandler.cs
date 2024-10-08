using Core.Interfaces;
using MediatR;

namespace Business.Commands.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userService.GetUserByIdAsync(request.Id);
            if (existingUser == null)
            {
                return false; 
            }

            existingUser.Name = request.Name;
            existingUser.Email = request.Email;

            await _userService.UpdateUserAsync(request.Id,existingUser);
            return true;
        }
    }
}
