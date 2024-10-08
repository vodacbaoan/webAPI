using Core.Entities;
using MediatR;

namespace Business.Queries.Users
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}
