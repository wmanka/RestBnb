using FluentValidation;
using RestBnb.API.Application.Properties.Requests;
using System;

namespace RestBnb.API.Validators.Properties
{
    public class UpdatePropertyRequestValidator : AbstractValidator<UpdatePropertyRequest>
    {
        public UpdatePropertyRequestValidator()
        {
            RuleFor(x => x.AccommodatesNumber).GreaterThan(0);
            RuleFor(x => x.BathroomNumber).GreaterThan(0);
            RuleFor(x => x.BedroomNumber).GreaterThan(0);
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.PricePerNight).ScalePrecision(2, 6).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.StartDate.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date);
            RuleFor(x => x.EndDate.Date).GreaterThanOrEqualTo(x => x.StartDate.Date);
        }
    }
}
