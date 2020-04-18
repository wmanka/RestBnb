using MediatR;
using RestBnb.API.Application.Auth.Commands;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Auth.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RefreshTokenAsync(request.Token, request.RefreshToken);
        }
    }
}
