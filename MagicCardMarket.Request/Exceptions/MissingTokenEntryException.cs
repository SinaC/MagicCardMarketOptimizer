using System;
namespace MagicCardMarket.Request.Exceptions
{
    public class TokenException : Exception
    {
        public TokenException(string message) : base(message)
        {
        }
    }
}
