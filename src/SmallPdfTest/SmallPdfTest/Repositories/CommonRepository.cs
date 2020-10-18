using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SmallPdfTest.Models;

namespace SmallPdfTest.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        public IList<ComboItem> GetCurrencies()
        {
            var content = File.ReadAllText("AppData/Currencies.json");

            var result = JsonConvert.DeserializeObject<IList<ComboItem>>(content);
            return result;
        }
    }

    public interface ICommonRepository
    {
        IList<ComboItem> GetCurrencies();
    }
}