using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestBnb.Core.Contracts.V1.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(a => a.ErrorMessage))
                    .ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var (fieldName, error) in errorsInModelState)
                {
                    foreach (var subError in error)
                    {
                        errorResponse.Errors.Add(new ErrorModel
                        {
                            FieldName = fieldName,
                            Message = subError
                        });
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}