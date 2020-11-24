using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Application.Cities.Queries;
using RestBnb.API.Application.Cities.Requests.QueryStrings;
using RestBnb.Core.Constants;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    public class CitiesController : BaseController
    {
        [HttpGet(ApiRoutes.Cities.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCitiesRequestQueryString requestQueryString)
        {
            var response = await Mediator.Send(new GetAllCitiesQuery(requestQueryString));
            
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Cities.Get)]
        public async Task<IActionResult> GetById(int cityId)
        {
            var response = await Mediator.Send(new GetCityByIdQuery(cityId));
            
            return Ok(response);
        }      
    }
}
