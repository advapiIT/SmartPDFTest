namespace SmallPdfTest.Models
{
    /* {
     "success": true,
     "terms": "https://currencylayer.com/terms",
     "privacy": "https://currencylayer.com/privacy",
     "query": {
         "from": "USD",
         "to": "EUR",
         "amount": 10
     },
     "info": {
         "timestamp": 1602702245,
         "quote": 0.850804
     },
     "result": 8.50804
     }*/

    public class CurrencyLayerConversionModel
    {
        public bool Success { get; set; }
        public decimal Result { get; set; }

        public CurrencyQuery Query { get; set; }
    }

    public class CurrencyQuery
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Amount { get; set; }
    }
}