using MediatR;
using RestBnb.API.Application.Auth.Responses;

namespace RestBnb.API.Application.Auth.Commands
{
    public class UserLoginCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserLoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
