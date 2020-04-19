using MediatR;
using RestBnb.API.Application.Users.Commands;
using RestBnb.API.Application.Users.Responses;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Users.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, UserResponse>
    {
        private readonly IUsersService _usersService;

        public DeleteUserHandler(
            IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<UserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _usersService.DeleteUserAsync(request.Id);

            return null;
        }
    }
}
