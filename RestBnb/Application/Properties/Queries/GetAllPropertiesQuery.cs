using MediatR;
using RestBnb.API.Application.Properties.Requests.QueryStrings;
using RestBnb.API.Application.Properties.Responses;
using System.Collections.Generic;

namespace RestBnb.API.Application.Properties.Queries
{
    public class GetAllPropertiesQuery : IRequest<IEnumerable<PropertyResponse>>
    {
        public GetAllPropertiesRequestQueryString PropertiesRequestQueryString { get; set; }

        public GetAllPropertiesQuery(GetAllPropertiesRequestQueryString propertiesRequestQueryString)
        {
            PropertiesRequestQueryString = propertiesRequestQueryString;
        }
    }
}
