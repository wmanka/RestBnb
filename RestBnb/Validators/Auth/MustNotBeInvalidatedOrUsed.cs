using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Helpers;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Auth
{
    public class MustNotBeInvalidatedOrUsed<T> : AsyncPropertyValidatorBase where T : IRequest<AuthResponse>
    {
        private readonly IServiceProvider _serviceProvider;
        public MustNotBeInvalidatedOrUsed(IServiceProvider serviceProvider) : base("Token has not expired yet.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var authServiceHelper = serviceScope.ServiceProvider.GetService<IAuthenticationServiceHelper>();
            var refreshTokensService = serviceScope.ServiceProvider.GetService<IRefreshTokensService>();

            var value = (T)context.PropertyValue;
            var token = (string)value.GetType().GetProperty("Token")?.GetValue(value, null);
            var refreshToken = (string)value.GetType().GetProperty("RefreshToken")?.GetValue(value, null);

            var validatedToken = authServiceHelper.GetPrincipalFromToken(token);

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await refreshTokensService.GetRefreshTokenByTokenAsync(refreshToken);

            return storedRefreshToken != null
                   && DateTime.UtcNow <= storedRefreshToken.ExpiryDate
                   && !storedRefreshToken.Invalidated
                   && !storedRefreshToken.Used
                   && storedRefreshToken.JwtId == jti;
        }
    }
}
