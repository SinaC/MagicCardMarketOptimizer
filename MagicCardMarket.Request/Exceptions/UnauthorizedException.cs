using System;

namespace MagicCardMarket.Request.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
