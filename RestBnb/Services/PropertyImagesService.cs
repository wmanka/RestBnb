using Microsoft.EntityFrameworkCore;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class PropertyImagesService : IPropertyImagesService
    {
        private readonly DataContext _dataContext;

        public PropertyImagesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateAsync(PropertyImage propertyImage)
        {
            await _dataContext.PropertyImages.AddAsync(propertyImage);

            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> DeleteAsync(int imageId)
        {
            var image = await _dataContext.PropertyImages
                .SingleOrDefaultAsync(x => x.Id == imageId);

            _dataContext.PropertyImages.Remove(image);

            var removed = await _dataContext.SaveChangesAsync();

            return removed > 0;
        }

        public async Task<IEnumerable<PropertyImage>> GetAllAsync(int propertyImageId)
        {
            return await _dataContext.PropertyImages
                .Where(x => x.PropertyId == propertyImageId).ToListAsync();
        }
    }
}
