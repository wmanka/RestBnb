using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface IPropertyImagesService
    {
        Task<IEnumerable<PropertyImage>> GetAllAsync(int propertyId);

        Task<bool> CreateAsync(PropertyImage property);

        Task<bool> DeleteAsync(int imageId);
    }
}
