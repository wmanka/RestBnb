using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly DataContext _dataContext;

        public CountriesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateCountriesRangeAsync(IEnumerable<Country> countries)
        {
            await _dataContext.Countries.AddRangeAsync(countries);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }
    }
}
