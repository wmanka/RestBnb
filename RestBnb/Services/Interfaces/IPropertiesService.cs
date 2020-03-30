using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface IPropertiesService
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync(GetAllPropertiesFilter filter = null);

        Task<bool> CreatePropertyAsync(Property property);

        Task<Property> GetPropertyByIdAsync(int propertyId);

        Task<bool> UpdatePropertyAsync(Property propertyToUpdate);

        Task<bool> DeletePropertyAsync(int propertyId);
        Task<bool> DoesUserOwnProperty(int userId, int propertyId);
    }
}