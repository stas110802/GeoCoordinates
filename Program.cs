using GeoCoordinates.Commands;
using GeoCoordinates.Models;
using GeoCoordinates.Utilities;

// Проверка создан ли конфиг, если нет, то создаст пустой
if (FileManager.IsFileExists(Config.FilePath) == false)
    FileManager.CreateEmptyFile(Config.FilePath);

var commands = new MainCommands();
// Вывод доступных команд
commands.PrintCommands();
// Считываем нажатую клавишу для вызова команд
await commands.ReadActionCommandKey();
