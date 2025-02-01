namespace GeoCoordinates.Attributes
{
    public class HasTypeAttribute() : Attribute
    {

        public HasTypeAttribute(object value) : this()
        {
            Value = value;
        }

        public object Value { get; set; }
    }
}
