using System.Collections.Generic;
using RestBnb.Core.Entities;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface ICountriesService
    {
        Task<bool> CreateCountriesRangeAsync(IEnumerable<Country> countries);
    }
}
