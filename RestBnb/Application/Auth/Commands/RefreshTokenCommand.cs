using MediatR;
using RestBnb.API.Application.Auth.Responses;

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
