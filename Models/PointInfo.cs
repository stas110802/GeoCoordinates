﻿namespace GeoCoordinates.Models
{
    public class PointInfo
    {
        public string Address { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public override string ToString()
        {
            return $"Широта: {Latitude}\n" +
                   $"Долгота: {Longitude}\n" +
                   $"Найденный адрес: {Address}\n";
        }
    }
}
