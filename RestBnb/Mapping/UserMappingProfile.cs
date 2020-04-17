using AutoMapper;
using RestBnb.API.Application.Users.Commands;
using RestBnb.API.Application.Users.Requests;
using RestBnb.API.Application.Users.Responses;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UpdateUserRequest, UpdateUserCommand>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
