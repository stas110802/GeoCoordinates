namespace GeoCoordinates.Endpoints
{
    public abstract class BaseEndpoint (string value)
    {
        public string Value { get; init; } = value;
    }
}
