using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface ICountriesConverterService
    {
        Task CreateCountriesWithStatesAndCitiesFromJsonAndAddThemToDatabase();
    }
}
