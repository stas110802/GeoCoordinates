using GeoCoordinates.Options;
using RestSharp;

namespace GeoCoordinates
{
    public class BaseRequest
    {
        public RestClient? Client;
        public RequestOptions? RequestOptions { get; set; }
      
        public string Execute()
        {
            if (RequestOptions == null || Client == null)
                throw new NullReferenceException("[Request error] : First you need to execute 'Create' method.");

            var result = Client
                .Execute(RequestOptions.Request, RequestOptions.Request.Method)
                .Content;

            if (string.IsNullOrEmpty(result))
                throw new Exception("[Request error] Request fetch error.");

            return result;
        }

        public async Task<string> ExecuteAsync()
        {
            if (RequestOptions == null || Client == null)
                throw new NullReferenceException("[Request error] : First you need to execute 'Create' method.");

            var result = (await Client
                .ExecuteAsync(RequestOptions.Request, RequestOptions.Request.Method))             
                .Content;

            if (string.IsNullOrEmpty(result))
                throw new Exception("[Request error] Request fetch error.");

            return result;
        }
    }
}
