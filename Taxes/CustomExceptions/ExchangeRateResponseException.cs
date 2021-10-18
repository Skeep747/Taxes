using System;

namespace Taxes.CustomExceptions
{
    public sealed class ExchangeRateResponseException : Exception
    {
        public ExchangeRateResponseException()
        {

        }

        public ExchangeRateResponseException(string message): base(message)
        {

        }

        public ExchangeRateResponseException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
