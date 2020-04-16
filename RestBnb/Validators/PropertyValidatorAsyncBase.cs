using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators
{
    public abstract class PropertyValidatorAsyncBase : PropertyValidator
    {
        public override bool ShouldValidateAsync(ValidationContext context)
        {
            return context.IsAsync() || Options.AsyncCondition != null;
        }

        protected PropertyValidatorAsyncBase(string errorMessage) : base(errorMessage) { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return Task.Run(() => IsValidAsync(context, new CancellationToken())).GetAwaiter().GetResult();
        }

        protected abstract override Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation);
    }
}
