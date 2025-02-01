using GeoCoordinates.API;
using GeoCoordinates.Attributes;
using GeoCoordinates.Interfaces;
using GeoCoordinates.Types;
using GeoCoordinates.Utilities;

namespace GeoCoordinates.Commands
{
    internal sealed class MainCommands : BaseCommandsObject
    {
        private DGisClient? _dGisClient;
        private YandexClient? _yandexClient;
        private readonly ConfigManager _config;

        public MainCommands()
        {
            _config = new ConfigManager();
            _config.LoadConfig();
            InitGeoClientsFromCfg();
        }

        public override void PrintCommands()
        {
            Console.Clear();
            PrintInfo("1", "Получить 2ГИС географические координаты");
            PrintInfo("2", "Получить Яндекс географические координаты");
            PrintInfo("3", "Установить api-ключ для 2ГИС");
            PrintInfo("4", "Установить api-ключ для Яндекс");
            PrintInfo("Q", "выход");
        }

        [ConsoleCommand(ConsoleKey.D1)]
        public async Task GetDGisCoord()
        {
            await GetCoord(_dGisClient);
        }

        [ConsoleCommand(ConsoleKey.D2)]
        public async Task GetYandexCoord()
        {
            await GetCoord(_yandexClient);
        }

        [ConsoleCommand(ConsoleKey.D3)]
        public void SetDGisApiKey()
        {
            SetKey(GeoType.DoubleGIS);
        }

        [ConsoleCommand(ConsoleKey.D4)]
        public void SetYandexApiKey()
        {
            SetKey(GeoType.Yandex);
        }

        private void PrintInfo(string key, string text)
        {
            ConsoleHelper.Write($"[{key}]", ConsoleColor.Blue);
            ConsoleHelper.WriteLine($" - {text}", ConsoleColor.Gray);
        }

        private void InitGeoClientsFromCfg()
        {
            _dGisClient = new (_config.GetApiOptions(GeoType.DoubleGIS));
            _yandexClient = new(_config.GetApiOptions(GeoType.Yandex));
        }

        private async Task GetCoord(IGeoApiAsync? api)
        {
            Console.Clear();
            if (api == null)
            {
                ConsoleHelper.WriteLine($"Сначала укажите api-ключ для {api}", ConsoleColor.Gray);
                Thread.Sleep(1500);

                return;
            }

            ConsoleHelper.Write("Введите адрес: ", ConsoleColor.Gray);
            var address = Console.ReadLine();

            var result = await api.GetCoordByAddressAsync(address);
            if (result.IsFailure)
            {
                Console.Clear();
                ConsoleHelper.WriteLine($"[{result.Error.Type}] : {result.Error.Message}", ConsoleColor.Red);
                Console.ReadKey();

                return;
            }

            var count = result.Value.Count();

            if (count == 0)
            {
                ConsoleHelper.WriteLine("Ничего не найдено по указанному адресу.", ConsoleColor.Gray);
                Thread.Sleep(1500);

                return;
            }
            Console.WriteLine($"Результатов по запросу найдено {count}: ");
            result.Value.ForEach(
                x => Console.Write($"{x}\n"));

            Console.ReadKey();
        }

        private void SetKey(GeoType type)
        {
            Console.Clear();
            Console.Write("Укажите api-ключ: ");
            var key = ConsoleHelper.ReadSecretString();
            _config.SetApiOptions(type, key);
        }
    }
}
