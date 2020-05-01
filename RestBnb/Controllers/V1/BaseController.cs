using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RestBnb.API.Controllers.V1
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}