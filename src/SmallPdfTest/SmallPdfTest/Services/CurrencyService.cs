using System;
using System.Threading.Tasks;
using Catel.Logging;
using RestSharp;
using SmallPdfTest.Models;

namespace SmallPdfTest.Services
{
    public class CurrencyService : ICurrencyService
    {
        public const string AccessKey = "access_key";
        public const string From = "from";
        public const string To = "to";
        public const string Amount = "amount";
        public const string Format = "format";
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly ICurrencySettingService currencySettingService;

        public CurrencyService(ICurrencySettingService currencySettingService)
        {
            this.currencySettingService = currencySettingService;
        }

        public async Task<decimal?> GetConvertedValueAsync(string currencyFrom, string currencyTo, decimal value)
        {
            try
            {
                var client = new RestClient(currencySettingService.Endpoint);
                var request = new RestRequest("convert", Method.GET);

                request.AddQueryParameter(AccessKey, currencySettingService.APIKey);
                request.AddQueryParameter(From, currencyFrom);
                request.AddQueryParameter(To, currencyTo);
                request.AddQueryParameter(Amount, value.ToString());
                request.AddQueryParameter(Format, "1");

                request.Timeout = 5000;
                var res = await client.GetAsync<CurrencyLayerConversionModel>(request).ConfigureAwait(false);

                if (res.Success) return res.Result;

                throw new CurrencyConversionException(res.Query);
            }
            catch (Exception ex)
            {
                Log.Error(ex);


                return null;
            }
        }
    }

    public interface ICurrencyService
    {
        Task<decimal?> GetConvertedValueAsync(string currencyFrom, string currencyTo, decimal value);
    }
}