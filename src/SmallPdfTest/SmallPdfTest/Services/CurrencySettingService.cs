using System.Configuration;
using Catel.Configuration;

namespace SmallPdfTest.Services
{
    public class CurrencySettingService : ICurrencySettingService
    {
        public CurrencySettingService(IConfigurationService configurationService)
        {
            Endpoint = ConfigurationManager.AppSettings.Get("currencyLayer:Endpoint");
            APIKey = ConfigurationManager.AppSettings.Get("currencyLayer:APIKey");
        }


        public string Endpoint { get; }
        public string APIKey { get; }
    }

    public interface ICurrencySettingService
    {
        string Endpoint { get; }
        string APIKey { get; }
    }
}