using GeoCoordinates.Attributes;
using GeoCoordinates.Options;
using GeoCoordinates.Types;
using Newtonsoft.Json;

namespace GeoCoordinates.Models
{
    public sealed class Config
    {
        [JsonIgnore]
        public static string FilePath 
            => @$"{Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."))}\settings.json";

        [HasType(GeoType.DoubleGIS)]
        [JsonProperty("DGIS")]
        public ApiOptions DGISOptions { get; set; } = new()
        {
            BaseUri = "https://catalog.api.2gis.com"
        };

        [HasType(GeoType.Yandex)]
        [JsonProperty("Yandex")]
        public ApiOptions YandexOptions { get; set; } = new()
        {
            BaseUri = "https://geocode-maps.yandex.ru/1.x/"
        };
    }
}
