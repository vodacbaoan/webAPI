using Core.Interfaces;
using MediatR;

namespace Business.Commands.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.Id);

            if (user == null)
            {
                return false;
            }

            await _userService.DeleteUserAsync(request.Id);
            return true;
        }
    
    }
}
