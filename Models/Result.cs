using GeoCoordinates.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCoordinates.Models
{
    public class Result<T> 
        where T : class, new()
    {
        public Result(T val)
        {
            Value = val;
            Error = new Error();
        }

        public Result(Error err)
        {
            Error = err;          
        }

        public T? Value { get; set; }
        public Error Error { get; set; }
        public bool IsSuccess => Error.Type == ErrorType.None;
        public bool IsFailure => Error.Type != ErrorType.None;
    }
}
