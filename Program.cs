using GeoCoordinates.API;
using GeoCoordinates.Models;
using GeoCoordinates.Options;

var dgClient = new DGisClient(new ApiOptions()
{
    BaseUri = "https://catalog.api.2gis.com",
    PublicKey = "no no mr fish)"
});

var yandexClient = new YandexClient(new ApiOptions()
{
    BaseUri = "https://geocode-maps.yandex.ru/1.x/",
    PublicKey = "where the key:) )"
});
// https://api-maps.yandex.ru/2.1?apikey=ваш API-ключ&lang=ru_RU

//var result1 = dgClient.GetCoordByAddress("Москва, Садовническая, 25");
var result2 = yandexClient.GetCoordByAddress("Москва, Садовническая, 25");
Console.WriteLine("2 GIS: ");
//PrintRes(result1);

Console.WriteLine("Yandex: ");
PrintRes(result2);


static void PrintRes(IEnumerable<PointInfo> result)
{
    foreach (var item in result)
    {
        Console.WriteLine(item.Address);
        Console.WriteLine(item.Latitude);
        Console.WriteLine(item.Longitude);
        Console.WriteLine();
    }
}
