using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Resources
{
    public class JsonConverterService : IJsonConverterService
    {
        private readonly ICountriesService _countriesService;

        public JsonConverterService(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        public async Task CreateCountriesWithStatesAndCitiesFromJsonAndAddThemToDatabase()
        {
            var countries = GetCountriesWithCorrespondingStatesAndCitiesFromJson();

            await CreateAndAddCountriesWithStatesAndCitiesToDatabase(countries);
        }

        private static IEnumerable<JsonCountryModel> GetCountriesWithCorrespondingStatesAndCitiesFromJson()
        {
            using var streamReader = new StreamReader(Directory.GetCurrentDirectory() + "\\Resources\\countries.json");
            var json = streamReader.ReadToEnd();

            return JsonConvert.DeserializeObject<IEnumerable<JsonCountryModel>>(json, Converter.Settings).ToList();
        }

        private async Task CreateAndAddCountriesWithStatesAndCitiesToDatabase(IEnumerable<JsonCountryModel> countriesFromJson)
        {
            var countries = ConvertJsonCountryModelsToListOfCountries(countriesFromJson);

            await _countriesService.CreateCountriesRangeAsync(countries);
        }

        private static IEnumerable<Country> ConvertJsonCountryModelsToListOfCountries(IEnumerable<JsonCountryModel> countriesFromJson)
        {
            return CreateListOfCountries(countriesFromJson);
        }

        private static IEnumerable<Country> CreateListOfCountries(IEnumerable<JsonCountryModel> countriesFromJson)
        {
            return countriesFromJson.Select(country => new Country
            {
                Name = country?.Name,
                Code = country?.Iso3,
                States = CreateListOfStatesPerCountry(country)
            }).ToList();
        }

        private static IEnumerable<State> CreateListOfStatesPerCountry(JsonCountryModel country)
        {
            return country?.States?.Select(citiesPerState => new State
            {
                Name = citiesPerState.Key,
                Cities = citiesPerState.Value?.Select(city => new City { Name = city }).ToList()
            }).ToList();
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                {
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                },
            };
        }
    }
}

