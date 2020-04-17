using MediatR;
using RestBnb.API.Application.Users.Responses;

namespace RestBnb.API.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<UserResponse>
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
