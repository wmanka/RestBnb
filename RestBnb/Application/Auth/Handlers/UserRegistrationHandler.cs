using MediatR;
using RestBnb.API.Application.Auth.Commands;
using RestBnb.API.Application.Auth.Responses;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Auth.Handlers
{
    public class UserRegistrationHandler : IRequestHandler<UserRegistrationCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public UserRegistrationHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request.Email, request.Password);
        }
    }
}
