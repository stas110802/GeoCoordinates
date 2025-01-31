using GeoCoordinates.Endpoints;
using GeoCoordinates.Interfaces;
using GeoCoordinates.Models;
using GeoCoordinates.Options;
using GeoCoordinates.Utilities;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GeoCoordinates.API
{
    public sealed class DGisClient //: IGeoApi
    {
        private readonly BaseRestApi<BaseRequest> _api;

        public DGisClient(ApiOptions options) 
        {
            _api = new BaseRestApi<BaseRequest>(options);
        }

        public IEnumerable<PointInfo> GetCoordByAddress(string address)
        {
            var result = new List<PointInfo>();

            var query = @$"?q={address}&fields=items.point&key={_api.ApiOptions.PublicKey}";
            var response = _api
                .CreateRequest(Method.Get, DGisEndpoint.GeoCode, query)
                .Execute()
                .FromJson<JToken>()?["result"]?["items"];

            if (response != null)
            {
                foreach (var item in response)
                {
                    var point = item["point"];
                    var isValidLat = decimal.TryParse(point?["lat"]?.ToString(), out var lat);
                    var isValidLon = decimal.TryParse(point?["lon"]?.ToString(), out var lon);
                    if (!isValidLon || !isValidLat)
                        throw new ValidationException("Не удалось получить координаты");

                    var fullAddress = item["full_name"]?.ToString();
                    if(string.IsNullOrWhiteSpace(fullAddress))
                        throw new ValidationException("Не удалось получить предпологаемый адрес");

                    result.Add(new PointInfo
                    {
                        Latitude = lat,
                        Longitude = lon,
                        Address = fullAddress
                    });
                }
            }

            return result;
        }
    }
}
