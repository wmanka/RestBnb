using FluentValidation;
using RestBnb.API.Application.Properties.Commands;
using System;

namespace RestBnb.API.Validators.Properties.Commands
{
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        public UpdatePropertyCommandValidator(IServiceProvider serviceProvider)
        {
            RuleFor(booking => booking)
                .SetValidator(new DomainValidator())
                .DependentRules(() => RuleFor(booking => booking)
                    .SetValidator(new BusinessValidator(serviceProvider)));
        }

        private class DomainValidator : AbstractValidator<UpdatePropertyCommand>
        {
            internal DomainValidator()
            {
                RuleFor(property => property.Id).NotNull().GreaterThan(0);
                RuleFor(property => property.AccommodatesNumber).GreaterThan(0);
                RuleFor(property => property.BathroomNumber).GreaterThan(0);
                RuleFor(property => property.BedroomNumber).GreaterThan(0);
                RuleFor(property => property.Address).NotEmpty();
                RuleFor(property => property.PricePerNight).ScalePrecision(2, 6).GreaterThan(0);
                RuleFor(property => property.Name).NotEmpty();
                RuleFor(property => property.Description).NotEmpty();
                RuleFor(property => property.CityId).GreaterThan(0);
            }
        }

        private class BusinessValidator : AbstractValidator<UpdatePropertyCommand>
        {
            internal BusinessValidator(IServiceProvider serviceProvider)
            {
                CascadeMode = CascadeMode.Stop;

                RuleFor(property => property)
                    .MustExist(serviceProvider)
                    .MustBeOwnedByCurrentUser(serviceProvider);
            }
        }
    }
}
