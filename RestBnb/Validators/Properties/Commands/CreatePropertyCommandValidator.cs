using FluentValidation;
using RestBnb.API.Application.Properties.Commands;
using System;

namespace RestBnb.API.Validators.Properties.Commands
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(property => property.AccommodatesNumber).GreaterThan(0);
            RuleFor(property => property.BathroomNumber).GreaterThan(0);
            RuleFor(property => property.BedroomNumber).GreaterThan(0);
            RuleFor(property => property.Address).NotEmpty();
            RuleFor(property => property.PricePerNight).ScalePrecision(2, 6).GreaterThan(0);
            RuleFor(property => property.Name).NotEmpty();
            RuleFor(property => property.Description).NotEmpty();
            RuleFor(property => property.CityId).GreaterThan(0);
            RuleFor(property => property.StartDate.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date);
            RuleFor(property => property.EndDate.Date).GreaterThanOrEqualTo(x => x.StartDate.Date);
        }
    }
}
