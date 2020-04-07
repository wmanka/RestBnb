using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestBnb.Core.Entities
{
    public class CountryFromJson
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
