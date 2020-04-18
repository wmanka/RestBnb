using MediatR;
using RestBnb.API.Application.Auth.Responses;

namespace RestBnb.API.Application.Auth.Commands
{
    public class UserRegistrationCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserRegistrationCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
