using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using RestBnb.API.Helpers;
using RestBnb.Core.Entities;
using System;
using System.Linq;

namespace RestBnb.API.Validators.Auth
{
    public class MustBeExpired<T> : PropertyValidator where T : IRequest<AuthResponse>
    {
        private readonly IServiceProvider _serviceProvider;
        public MustBeExpired(IServiceProvider serviceProvider) : base("Token has not expired yet.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var authServiceHelper = serviceScope.ServiceProvider.GetService<IAuthenticationServiceHelper>();

            var value = (T)context.PropertyValue;
            var token = (string)value.GetType().GetProperty("Token")?.GetValue(value, null);

            var validatedToken = authServiceHelper.GetPrincipalFromToken(token);

            var expiryDateUnix = long.Parse(validatedToken
                .Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Exp)
                .Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            return expiryDateTimeUtc <= DateTime.UtcNow;
        }
    }
}
