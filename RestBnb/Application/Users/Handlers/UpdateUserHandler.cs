using AutoMapper;
using MediatR;
using RestBnb.API.Application.Users.Commands;
using RestBnb.API.Application.Users.Responses;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UpdateUserHandler(
            IUsersService usersService,
            IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersService.GetUserByIdAsync(request.Id);

            _mapper.Map(request, user);

            await _usersService.UpdateUserAsync(user);

            return _mapper.Map<UserResponse>(user);
        }
    }
}
