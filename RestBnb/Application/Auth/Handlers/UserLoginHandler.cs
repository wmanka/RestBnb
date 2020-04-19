using MediatR;
using RestBnb.API.Application.Auth.Commands;
using RestBnb.API.Application.Auth.Responses;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Auth.Handlers
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public UserLoginHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.Email, request.Password);
        }
    }
}
