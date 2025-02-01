using GeoCoordinates.Endpoints;
using RestSharp;

namespace GeoCoordinates.Options
{
    public class RequestOptions
    {
        public required RestRequest Request { get; set; }
        public BaseType? Endpoint { get; set; }
        public string? FullPath { get; set; }
    }
}
