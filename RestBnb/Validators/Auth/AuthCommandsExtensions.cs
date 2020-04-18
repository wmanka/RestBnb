using FluentValidation;
using MediatR;
using RestBnb.Core.Entities;
using System;

namespace RestBnb.API.Validators.Auth
{
    public static class AuthCommandsExtensions
    {
        public static IRuleBuilderOptions<T, TElement> MustBeUnique<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<AuthResponse>
        {
            return ruleBuilder.SetValidator(new MustBeUnique<TElement>(serviceProvider));
        }

        public static IRuleBuilderOptions<T, TElement> MustProvideMatchingPassword<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<AuthResponse>
        {
            return ruleBuilder.SetValidator(new PasswordMustMatch<TElement>(serviceProvider));
        }

        public static IRuleBuilderOptions<T, TElement> MustExist<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<AuthResponse>
        {
            return ruleBuilder.SetValidator(new MustExist<TElement>(serviceProvider));
        }

        public static IRuleBuilderOptions<T, TElement> TokenMustBeExpired<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<AuthResponse>
        {
            return ruleBuilder.SetValidator(new MustBeExpired<TElement>(serviceProvider));
        }

        public static IRuleBuilderOptions<T, TElement> TokenMustNotBeInvalidatedOrUsed<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<AuthResponse>
        {
            return ruleBuilder.SetValidator(new MustNotBeInvalidatedOrUsed<TElement>(serviceProvider));
        }
    }
}
