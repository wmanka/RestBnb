using FluentValidation;
using MediatR;
using RestBnb.Core.Contracts.V1.Responses;
using System;

namespace RestBnb.API.Validators.Bookings
{
    public static class BookingCommandValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TElement> MustLastAtLeastOneDay<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
            where TElement : IRequest<BookingResponse>
        {
            return ruleBuilder.SetValidator(new MustLastAtLeastOneDay<TElement>());
        }

        public static IRuleBuilderOptions<T, TElement> MustExist<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : class
        {
            return ruleBuilder.SetValidator(new MustExist<TElement>(serviceProvider));
        }

        public static IRuleBuilderOptions<T, TElement> MustBeAvailable<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<BookingResponse>
        {
            return ruleBuilder.SetValidator(new MustBeAvailable<TElement>(serviceProvider));
        }

        public static IRuleBuilderOptions<T, TElement> MustBeOwnedByCurrentUser<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<BookingResponse>
        {
            return ruleBuilder.SetValidator(new MustBeOwnedByCurrentUser<TElement>(serviceProvider));
        }

        public static IRuleBuilderOptions<T, TElement> MustNotBeInProgress<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, IServiceProvider serviceProvider)
            where TElement : IRequest<BookingResponse>
        {
            return ruleBuilder.SetValidator(new MustNotBeInProgress<TElement>(serviceProvider));
        }
    }
}
