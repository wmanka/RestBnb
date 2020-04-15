using FluentValidation;
using RestBnb.API.Application.Properties.Commands;
using RestBnb.API.Services;
using RestBnb.API.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Properties
{
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        private readonly UserResolverService _userResolverService;
        private readonly IPropertiesService _propertiesService;

        public UpdatePropertyCommandValidator(
            UserResolverService userResolverService,
            IPropertiesService propertiesService)
        {
            _userResolverService = userResolverService;
            _propertiesService = propertiesService;

            RuleFor(property => property.Id).NotNull().GreaterThan(0);
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

            RuleFor(property => property)
                .MustAsync(Exist).WithMessage("The property does not exist.")
                .MustAsync(BeOwnedByCurrentUser).WithMessage("You are not the owner of this property.");
        }

        public async Task<bool> Exist(UpdatePropertyCommand command, CancellationToken cancellationToken)
        {
            var property = await _propertiesService.GetPropertyByIdAsync(command.Id);

            return property != null;
        }

        public async Task<bool> BeOwnedByCurrentUser(UpdatePropertyCommand command, CancellationToken cancellationToken)
        {
            return await _propertiesService.DoesUserOwnPropertyAsync(_userResolverService.GetUserId(), command.Id);
        }
    }
}
