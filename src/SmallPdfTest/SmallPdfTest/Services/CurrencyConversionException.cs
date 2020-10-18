using System;
using System.Runtime.Serialization;
using SmallPdfTest.Models;

namespace SmallPdfTest.Services
{
    [Serializable]
    internal class CurrencyConversionException : Exception
    {
        private CurrencyQuery query;

        public CurrencyConversionException()
        {
        }

        public CurrencyConversionException(CurrencyQuery query)
        {
            this.query = query;
        }

        public CurrencyConversionException(string message) : base(message)
        {
        }

        public CurrencyConversionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CurrencyConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}