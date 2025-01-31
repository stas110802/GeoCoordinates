using GeoCoordinates.Endpoints;
using GeoCoordinates.Options;
using RestSharp;

namespace GeoCoordinates.API
{
    public sealed class BaseRestApi<T>
        where T : BaseRequest, new()
    {
        public BaseRestApi(ApiOptions apiOptions)
        {
            ApiOptions = apiOptions;
        } 

        public ApiOptions ApiOptions { get; set; }

        public T CreateRequest(Method method, BaseEndpoint? endpoint = null,
            string? query = null)
        {
            var full = endpoint?.Value + query;
            var result = new T
            {
                Client = new RestClient(ApiOptions.BaseUri),
                RequestOptions = new RequestOptions
                {
                    FullPath = full,
                    Endpoint = endpoint,
                    Request = new RestRequest(full)
                    {
                        Method = method
                    }
                }
            };

            return result;
        }
    }
}
