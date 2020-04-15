using MediatR;
using RestBnb.API.Application.Properties.Commands;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Properties.Handlers
{
    public class DeletePropertyHandler : IRequestHandler<DeletePropertyCommand>
    {
        private readonly IPropertiesService _propertiesService;

        public DeletePropertyHandler(
            IPropertiesService propertiesService)
        {
            _propertiesService = propertiesService;
        }

        public async Task<Unit> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            await _propertiesService.DeletePropertyAsync(request.Id);

            return Unit.Value;
        }
    }
}
