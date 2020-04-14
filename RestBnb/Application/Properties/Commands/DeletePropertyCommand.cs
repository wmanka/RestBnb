using MediatR;

namespace RestBnb.API.Application.Properties.Commands
{
    public class DeletePropertyCommand : IRequest
    {
        public int Id { get; set; }

        public DeletePropertyCommand(int id)
        {
            Id = id;
        }
    }
}
