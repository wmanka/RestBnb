using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface ICitiesService
    {
        Task<IEnumerable<City>> GetAllCitiesAsync(GetAllCitiesFilter filter = null);

        Task<City> GetCityByIdAsync(int cityId);
    }
}
