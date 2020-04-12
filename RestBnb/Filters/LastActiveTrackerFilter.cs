using Microsoft.AspNetCore.Mvc.Filters;
using RestBnb.API.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Filters
{
    public class LastActiveTrackerFilter : IAsyncActionFilter
    {
        private readonly IUsersService _usersService;

        public LastActiveTrackerFilter(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            var id = context.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "id")?.Value;

            if (id != null)
            {
                var user = await _usersService.GetUserByIdAsync(int.Parse(id));
                user.LastActive = DateTime.UtcNow;
                await _usersService.UpdateUserAsync(user);
            }
        }
    }
}
