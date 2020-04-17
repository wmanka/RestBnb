

using System;
using FluentValidation;
using MediatR;
using RestBnb.API.Application.Properties.Responses;

namespace RestBnb.API.Validators.Properties
{
    public static class PropertyCommandValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TElement> MustExist<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<PropertyResponse>
        {
            return ruleBuilder.SetValidator(new MustExist<TElement>(serviceProvider));
        }

        public static IRuleBuilderOptions<T, TElement> MustBeOwnedByCurrentUser<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<PropertyResponse>
        {
            return ruleBuilder.SetValidator(new MustBeOwnedByCurrentUser<TElement>(serviceProvider));
        }
    }
}
