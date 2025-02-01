using GeoCoordinates.Models;

namespace GeoCoordinates.Interfaces
{
    public interface IGeoApiAsync
    {
        /// <summary>
        /// Возвращает географические координаты
        /// </summary>
        /// <param name="address">Адрес, координаты которого хотим получить</param>
        public Task<Result<List<PointInfo>>> GetCoordByAddressAsync(string address);
    }
}
