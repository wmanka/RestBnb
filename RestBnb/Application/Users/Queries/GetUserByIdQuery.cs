using MediatR;
using RestBnb.API.Application.Users.Responses;

namespace RestBnb.API.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
