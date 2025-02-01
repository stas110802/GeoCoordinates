using Newtonsoft.Json;

namespace GeoCoordinates.Options
{
    public class ApiOptions
    {
        [JsonIgnore]
        public required string BaseUri { get; set; }

        [JsonProperty("key")]
        public string? PublicKey { get; set; }
    }
}
