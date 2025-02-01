using GeoCoordinates.Types;

namespace GeoCoordinates.Models
{
    public class Error
    {
        public Error()
        {
            Message = string.Empty;
            Type = ErrorType.None;
        }
        public Error(string msg, ErrorType type)
        {
            Message = msg;
            Type = type;
        }

        public string Message { get; set; }
        public ErrorType Type { get; set; }
    }
}
