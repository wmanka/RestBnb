using AutoMapper;
using RestBnb.API.Contracts.V1.Requests;
using RestBnb.API.Contracts.V1.Requests.Queries;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<GetAllPropertiesQuery, GetAllPropertiesFilter>();

            CreateMap<CreatePropertyRequest, Property>();
        }
    }
}