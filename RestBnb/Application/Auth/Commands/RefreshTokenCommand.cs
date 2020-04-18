using MediatR;
using RestBnb.Core.Entities;

namespace RestBnb.API.Application.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public RefreshTokenCommand(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
