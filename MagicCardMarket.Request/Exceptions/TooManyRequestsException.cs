using System;

namespace MagicCardMarket.Request.Exceptions
{
    public class TooManyRequestsException : Exception
    {
        public TooManyRequestsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
