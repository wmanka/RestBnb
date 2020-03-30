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
    public interface IJsonConverterHelper
    {
        Task GetAsListOfObjectsAndAddToDatabase();
    }

    public class JsonConverterHelper : IJsonConverterHelper
    {
        private readonly ICountriesService _countriesService;

        public JsonConverterHelper(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        public async Task GetAsListOfObjectsAndAddToDatabase()
        {
            using var r = new StreamReader(Directory.GetCurrentDirectory() + "\\Resources\\countries.json");
            var json = r.ReadToEnd();

            var countries = JsonConvert.DeserializeObject<IEnumerable<JsonCountryModel>>(json, Converter.Settings).ToList();

            await ParseCountriesToDatabase(countries);
        }

        private async Task<bool> ParseCountriesToDatabase(IEnumerable<JsonCountryModel> countries)
        {
            var countriesList = countries.Select(jsonCountryModel => new Country
            {
                Name = jsonCountryModel?.Name,
                Code = jsonCountryModel?.Iso3,
                States = jsonCountryModel?.States?.Select(s => new State
                {
                    Name = s.Key,
                    Cities = s.Value?.Select(cityName => new City
                    {
                        Name = cityName
                    }).ToList()
                }).ToList()
            })
                .ToList();

            return await _countriesService.CreateCountriesRangeAsync(countriesList);
        }
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


    public class JsonCountryModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iso3")]
        public string Iso3 { get; set; }

        [JsonProperty("iso2")]
        public string Iso2 { get; set; }

        [JsonProperty("phone_code")]
        public string PhoneCode { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("states")]
        public IDictionary<string, IEnumerable<string>> States { get; set; }
    }

}

