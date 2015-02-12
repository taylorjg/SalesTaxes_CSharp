namespace Code
{
    public class ReceiptItem
    {
        public ReceiptItem(BasketItem basketItem, decimal salesTax)
        {
            _basketItem = basketItem;
            _salesTax = salesTax;
        }

        public BasketItem BasketItem
        {
            get { return _basketItem; }
        }

        public decimal SalesTax
        {
            get { return _salesTax; }
        }

        public decimal PriceIncludingSalesTax
        {
            get { return _basketItem.Price +  _salesTax; }
        }

        private readonly BasketItem _basketItem;
        private readonly decimal _salesTax;
    }
}
