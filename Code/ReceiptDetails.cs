using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class ReceiptDetails
    {
        public ReceiptDetails(IEnumerable<ReceiptItem> receiptItems)
        {
            _receiptItems = receiptItems;
        }

        public decimal SalesTax
        {
            get { return ReceiptItems.Sum(x => x.SalesTax); }
        }

        public decimal Total
        {
            get { return ReceiptItems.Sum(x => x.PriceIncludingSalesTax); }
        }

        public IEnumerable<ReceiptItem> ReceiptItems
        {
            get { return _receiptItems; }
        }

        private readonly IEnumerable<ReceiptItem> _receiptItems;
    }
}
