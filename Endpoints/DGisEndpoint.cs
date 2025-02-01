using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCoordinates.Endpoints
{
    public sealed class DGisEndpoint : BaseType
    {
        private DGisEndpoint(string value) : base(value) { }

        public static readonly DGisEndpoint GeoCode = new("/3.0/items/geocode");
    }
}
