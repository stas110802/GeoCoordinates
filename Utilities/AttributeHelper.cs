using GeoCoordinates.Attributes;

namespace GeoCoordinates.Utilities
{
    public static class AttributeHelper
    {
        public static TKey? GetValueOf<TKey, TValue>(TValue searchObj, object description)
            where TValue : class
        {

            var props = typeof(TValue).GetProperties().ToArray();
            foreach (var prop in props)
            {
                var attributes = prop.GetCustomAttributes(false);
                foreach (var att in attributes)
                {
                    if (att is HasTypeAttribute attribute)
                    {
                        var value = attribute.Value;
                        if (description.Equals(value))
                        {
                            return (TKey)prop.GetValue(searchObj, null);
                        }
                    }
                }
            }

            return default;
        }
    }
}
