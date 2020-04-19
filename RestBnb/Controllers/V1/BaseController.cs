using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestBnb.API.Controllers.V1
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected readonly IMediator Mediator;

        public BaseController(IMapper mapper, IMediator mediator)
        {
            Mapper = mapper;
            Mediator = mediator;
        }
    }
}