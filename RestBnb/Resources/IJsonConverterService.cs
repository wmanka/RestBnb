using System.Threading.Tasks;

namespace RestBnb.API.Resources
{
    public interface IJsonConverterService
    {
        Task CreateCountriesWithStatesAndCitiesFromJsonAndAddThemToDatabase();
    }
}
