using AutoMapper;
using RestBnb.Core.Contracts.V1.Requests.Bookings;
using RestBnb.Core.Contracts.V1.Requests.Users;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class DomainToRequestProfile : Profile
    {
        public DomainToRequestProfile()
        {


            CreateMap<User, UserUpdateRequest>();
        }
    }
}
