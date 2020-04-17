using FluentValidation;
using MediatR;
using RestBnb.API.Application.Users.Responses;
using System;

namespace RestBnb.API.Validators.Users
{
    public static class UserCommandValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TElement> MustBeOwnedByCurrentUser<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<UserResponse>
        {
            return ruleBuilder.SetValidator(new MustBeOwnedByCurrentUser<TElement>(serviceProvider));
        }

        public static IRuleBuilderOptions<T, TElement> MustExist<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<UserResponse>
        {
            return ruleBuilder.SetValidator(new MustExist<TElement>(serviceProvider));
        }
    }
}
