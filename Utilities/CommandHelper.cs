using GeoCoordinates.Attributes;

namespace GeoCoordinates.Utilities
{
    public static class CommandHelper
    {
        /// <summary>
        /// Возвращает все методы с атрибутом "ConsoleCommand" 
        /// </summary>
        /// <param name="target">class with commands</param>
        /// <param name="type">type of commands class</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<ConsoleKey, T> GetConsoleCommands<T>(object target, Type type)
            where T : class, MulticastDelegate
        {
            var result = new Dictionary<ConsoleKey, T>();
            var methods = type.GetMethods();

            foreach (var method in methods)
            {
                if (Attribute.GetCustomAttributes(method, typeof(ConsoleCommandAttribute)).FirstOrDefault() is not ConsoleCommandAttribute attr)
                    continue;
                try
                {
                    var action = (T)Delegate.CreateDelegate(typeof(T), target, method);
                    result.Add(attr.Key, action);
                }
                catch (Exception) { }
            }

            return result;
        }
    }
}
