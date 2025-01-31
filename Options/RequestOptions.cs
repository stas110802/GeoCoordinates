using GeoCoordinates.Endpoints;
using RestSharp;

namespace GeoCoordinates.Options
{
    public class RequestOptions
    {
        public required RestRequest Request { get; set; }
        public BaseEndpoint? Endpoint { get; set; }
        public string? FullPath { get; set; }
    }
}
