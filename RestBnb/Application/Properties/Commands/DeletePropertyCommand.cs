using MediatR;
using RestBnb.API.Application.Properties.Responses;

namespace RestBnb.API.Application.Properties.Commands
{
    public class DeletePropertyCommand : IRequest<PropertyResponse>
    {
        public int Id { get; set; }

        public DeletePropertyCommand(int id)
        {
            Id = id;
        }
    }
}
