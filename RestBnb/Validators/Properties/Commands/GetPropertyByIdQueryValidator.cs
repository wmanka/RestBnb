using FluentValidation;
using RestBnb.API.Application.Properties.Queries;

namespace RestBnb.API.Validators.Properties.Commands
{
    public class GetPropertyByIdQueryValidator : AbstractValidator<GetPropertyByIdQuery>
    {
        public GetPropertyByIdQueryValidator()
        {
            RuleFor(property => property.Id).NotNull();
        }
    }
}
