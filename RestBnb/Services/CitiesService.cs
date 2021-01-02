using Microsoft.EntityFrameworkCore;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly DataContext _dataContext;

        public CitiesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync(GetAllCitiesFilter filter = null)
        {
            var cities = _dataContext.Cities.AsQueryable();

            cities = AddFiltersOnQuery(filter, cities);

            return await cities.ToListAsync();
        }

        public async Task<City> GetCityByIdAsync(int cityId)
        {
            return await _dataContext.Cities.SingleOrDefaultAsync(x => x.Id == cityId);
        }

        private static IQueryable<City> AddFiltersOnQuery(GetAllCitiesFilter filter, IQueryable<City> cities)
        {
            if (!string.IsNullOrWhiteSpace(filter?.Name))
            {
                cities = cities.Where(x => x.Name.Contains(filter.Name));
            }

            return cities;
        }
    }
}
