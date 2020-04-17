using AutoMapper;
using MediatR;
using RestBnb.API.Application.Users.Queries;
using RestBnb.API.Application.Users.Responses;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Users.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(
            IUsersService usersService,
            IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersService.GetUserByIdAsync(request.Id);

            return _mapper.Map<User, UserResponse>(user);
        }
    }
}
