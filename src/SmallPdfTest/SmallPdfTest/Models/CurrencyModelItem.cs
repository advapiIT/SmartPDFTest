using Catel.Collections;
using Catel.Data;

namespace SmallPdfTest.Models
{
    public class CurrencyModelItem : ModelBase
    {
        public CurrencyModelItem()
        {
            Items = new FastBindingList<RowItem>();
        }

        public FastBindingList<RowItem> Items { get; set; }
    }
}