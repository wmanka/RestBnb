using FluentValidation;
using RestBnb.API.Application.Properties.Commands;
using RestBnb.API.Services;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Properties
{
    public class DeletePropertyCommandValidator : AbstractValidator<DeletePropertyCommand>
    {
        private readonly UserResolverService _userResolverService;
        private readonly IPropertiesService _propertiesService;
        public DeletePropertyCommandValidator(UserResolverService userResolverService, IPropertiesService propertiesService)
        {
            _userResolverService = userResolverService;
            _propertiesService = propertiesService;

            RuleFor(property => property.Id).NotNull();
            RuleFor(property => property)
                .MustAsync(Exist).WithMessage("The property does not exist.")
                .MustAsync(BeOwnedByCurrentUser).WithMessage("You are not the owner of this property.");
        }

        public async Task<bool> Exist(DeletePropertyCommand command, CancellationToken cancellationToken)
        {
            var property = await _propertiesService.GetPropertyByIdAsync(command.Id);

            return property != null;
        }

        public async Task<bool> BeOwnedByCurrentUser(DeletePropertyCommand command, CancellationToken cancellationToken)
        {
            return await _propertiesService.DoesUserOwnPropertyAsync(_userResolverService.GetUserId(), command.Id);
        }
    }
}
