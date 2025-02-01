using GeoCoordinates.Models;
using GeoCoordinates.Options;
using GeoCoordinates.Types;
using GeoCoordinates.Utilities;
using Newtonsoft.Json;

namespace GeoCoordinates
{
    public sealed class ConfigManager
    {
        private Config? _config;

        public ApiOptions GetApiOptions(GeoType type)
        {
            var apiOptions = AttributeHelper.GetValueOf<ApiOptions, Config>(_config, type);
            if (apiOptions == null)
                throw new Exception($"Не удалось получить api настройки для {type}");

            return apiOptions;
        }

        public void SetApiOptions(GeoType type, string key)
        {
            var apiOptions = AttributeHelper.GetValueOf<ApiOptions, Config>(_config, type);
            if (apiOptions == null)
                throw new Exception($"Не удалось получить api настройки для {type}");

            apiOptions.PublicKey = key;
            UpdateConfig();
        }

        public void LoadConfig()
        {
            Config? cfg = null;

            if (FileManager.IsFileExists(Config.FilePath))
                cfg = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Config.FilePath));

            if (cfg == null)
            {
                _config = new Config();
                return;
            }

            _config = cfg;
            UpdateConfig();
        }

        private void UpdateConfig() 
        {
            if (_config == null)
                throw new Exception("Нельзя обновить конфиг пустыми данными!");
            FileManager.CreateReadyFile(Config.FilePath, _config);
        }
    }
}
