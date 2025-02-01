using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCoordinates.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConsoleCommandAttribute : Attribute
    {
        public ConsoleCommandAttribute(ConsoleKey key)
        {
            Key = key;
        }

        public ConsoleKey Key { get; }
    }
}
