using GeoCoordinates.Endpoints;
using GeoCoordinates.Interfaces;
using GeoCoordinates.Models;
using GeoCoordinates.Options;
using GeoCoordinates.Types;
using GeoCoordinates.Utilities;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace GeoCoordinates.API
{
    public sealed class DGisClient : IGeoApiAsync
    {
        private readonly BaseRestApi<BaseRequest> _api;

        public DGisClient(ApiOptions options) 
        {
            _api = new BaseRestApi<BaseRequest>(options);
        }

        public async Task<Result<List<PointInfo>>> GetCoordByAddressAsync(string address)
        {
            Result<List<PointInfo>> result;

            var query = @$"?q={address}&fields=items.point&key={_api.ApiOptions.PublicKey}";
            var response = (await _api
                .CreateRequest(Method.Get, DGisEndpoint.GeoCode, query)
                .ExecuteAsync())
                .FromJson<JToken>();

            
            var items = response["result"]?["items"];

            if(items == null)
            {
                var errorResponse = response["meta"]?["error"];
                var msg = $"{errorResponse?["type"]}, {errorResponse?["message"]}";
                var error = new Error(msg, ErrorType.ExecuteRequest);
                result = new(error);

                return result;
            }
            
            var list = new List<PointInfo>();
            foreach (var item in items)
            {
                var point = item["point"];
                var isValidLat = decimal.TryParse(point?["lat"]?.ToString(), out var lat);
                var isValidLon = decimal.TryParse(point?["lon"]?.ToString(), out var lon);
                if (!isValidLon || !isValidLat)
                {
                    result = new(
                        new Error("Ошибка при попытке получить координаты", ErrorType.ValidationError));
                    break;
                }

                var fullAddress = item["full_name"]?.ToString();
                if(string.IsNullOrWhiteSpace(fullAddress))
                {
                    result = new(
                        new Error("Не удалось получить предпологаемый адрес", ErrorType.ValidationError));
                    break;
                }

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
            return "2ГИС";
        }
    }
}
