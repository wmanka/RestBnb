using MediatR;
using RestBnb.API.Application.Users.Responses;
using System;

namespace RestBnb.API.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<UserResponse>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }

        public UpdateUserCommand(int id)
        {
            Id = id;
        }
    }
}
