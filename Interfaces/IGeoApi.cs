namespace GeoCoordinates.Interfaces
{
    public interface IGeoApi
    {
        /// <summary>
        /// Возвращает географические координаты
        /// </summary>
        /// <param name="address">Адрес, координаты которого хотим получить</param>
        public string GetCoordByAddress(string address);
    }
}
