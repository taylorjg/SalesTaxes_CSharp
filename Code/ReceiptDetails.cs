using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class ReceiptDetails
    {
        public ReceiptDetails(IEnumerable<ReceiptItem> items)
        {
            _items = items;
        }

        public decimal SalesTaxes
        {
            get { return Total - Items.Sum(x => x.BasketItem.Price); }
        }

        public decimal Total
        {
            get { return Items.Sum(x => x.PriceIncludingTaxes); }
        }

        public IEnumerable<ReceiptItem> Items
        {
            get { return _items; }
        }

        private readonly IEnumerable<ReceiptItem> _items;
    }
}
