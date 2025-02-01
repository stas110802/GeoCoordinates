using GeoCoordinates.Interfaces;
using GeoCoordinates.Models;
using GeoCoordinates.Options;
using GeoCoordinates.Types;
using GeoCoordinates.Utilities;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace GeoCoordinates.API
{
    public sealed class YandexClient : IGeoApiAsync
    {
        private readonly BaseRestApi<BaseRequest> _api;

        public YandexClient(ApiOptions options)
        {
            _api = new BaseRestApi<BaseRequest>(options);
        }

        public async Task<Result<List<PointInfo>>> GetCoordByAddressAsync(string address)
        {
            Result<List<PointInfo>> result;

            var query = @$"?apikey={_api.ApiOptions.PublicKey}&geocode={address}&lang=ru_RU&format=json";
            var response = (await _api
                .CreateRequest(Method.Get, null, query)
                .ExecuteAsync())
                .FromJson<JToken>();

            var items = response["response"]?["GeoObjectCollection"]?["featureMember"];
         
            if(items == null)
            {
                var msg = $"{response["error"]}, {response["message"]}";
                result = new(new Error(msg, ErrorType.ExecuteRequest));

                return result;
            }

            var list = new List<PointInfo>();
            foreach (var item in items)
            {
                var point = item["GeoObject"]?["Point"]?["pos"].ToString().Split(' ');
                
                var isValidLon = decimal.TryParse(point?[0], CultureInfo.InvariantCulture, out var lon);
                var isValidLat = decimal.TryParse(point?[1], CultureInfo.InvariantCulture, out var lat);                  
                if (!isValidLon || !isValidLat)
                    throw new ValidationException("Не удалось получить координаты");

                var fullAddress = item["GeoObject"]?["metaDataProperty"]
                    ?["GeocoderMetaData"]?["Address"]?["formatted"]?.ToString();
                if(string.IsNullOrEmpty(fullAddress))
                    throw new ValidationException("Не удалось получить предпологаемый адрес");

                list.Add(new PointInfo
                {
                    Latitude = lat,
                    Longitude = lon,
                    Address = fullAddress
                });
                
            }
            result = new(list);

            return result;
        }

        public override string ToString()
        {
            return "Яндекс карты";
        }
    }
}
