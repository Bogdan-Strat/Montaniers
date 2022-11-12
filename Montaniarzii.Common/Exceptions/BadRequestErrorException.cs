using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.Common.Exceptions
{
    public class BadRequestErrorException : Exception
    {
        public BadRequestErrorException(string message) : base(message)
        {
        }

        public BadRequestErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadRequestErrorException()
        {
        }
    }
}
