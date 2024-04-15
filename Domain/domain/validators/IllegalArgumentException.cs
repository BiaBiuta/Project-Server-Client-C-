

using System;

namespace Domain.domain.validators
{
    public class IllegalArgumentException:ArgumentException
    {
        public IllegalArgumentException():base()
        {
       
        }
        public IllegalArgumentException(string message) : base(message)
        {
        }

        public IllegalArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public IllegalArgumentException(string message, string paramName) : base(message, paramName)
        {
        }

        public IllegalArgumentException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
        {
        }
    }
}