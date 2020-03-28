using System.Collections.Generic;
using System.Threading.Tasks;
using RestBnb.Core.Entities;

namespace RestBnb.API.Services.Interfaces
{
    public interface IPropertiesService
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync(GetAllPropertiesFilter filter = null);

        Task<bool> CreatePropertyAsync(Property property);

        Task<Property> GetPropertyByIdAsync(int propertyId);

        Task<bool> UpdatePropertyAsync(Property propertyToUpdate);

        Task<bool> DeletePropertyAsync(int propertyId);
    }
}