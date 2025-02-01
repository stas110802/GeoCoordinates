namespace GeoCoordinates.Endpoints
{
    public sealed class DGisEndpoint : BaseEndpoint
    {
        private DGisEndpoint(string value) : base(value) { }

        public static readonly DGisEndpoint GeoCode = new("/3.0/items/geocode");
    }
}
