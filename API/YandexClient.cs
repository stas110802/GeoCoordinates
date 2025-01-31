using GeoCoordinates.Endpoints;
using GeoCoordinates.Models;
using GeoCoordinates.Options;
using GeoCoordinates.Utilities;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCoordinates.API
{
    public sealed class YandexClient
    {
        private readonly BaseRestApi<BaseRequest> _api;

        public YandexClient(ApiOptions options)
        {
            _api = new BaseRestApi<BaseRequest>(options);
        }

        public IEnumerable<PointInfo> GetCoordByAddress(string address)
        {
            var result = new List<PointInfo>();

            var query = @$"?apikey={_api.ApiOptions.PublicKey}&geocode={address}&lang=ru_RU&format=json";
            var response = _api
                .CreateRequest(Method.Get, null, query)
                .Execute()
                .FromJson<JToken>()
                ["response"]?["GeoObjectCollection"]?["featureMember"];

       
            if (response != null)
            {
                foreach (var item in response)
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
