using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCoordinates.Utilities
{
    public static class FileManager
    {

        /// <summary>
        /// Создает пустой файл
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public static void CreateEmptyFile(string filePath)
        {
            using var file = File.CreateText(filePath);
            file.Close();
        }

        /// <summary>
        /// Записывает данные в файл
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="data">Данные для записи в файл</param>
        public static void CreateReadyFile<T>(string filePath, T data)
        {
            var isFileExists = IsFileExists(filePath);
            if (isFileExists == false)
                CreateEmptyFile(filePath);

            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }


        /// <summary>
        /// Проверяет создан ли указанный файл
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsFileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
