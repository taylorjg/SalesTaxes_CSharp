namespace Code
{
    public class ReceiptItem
    {
        public ReceiptItem(BasketItem basketItem, decimal priceIncludingSalesTax)
        {
            _basketItem = basketItem;
            _priceIncludingSalesTax = priceIncludingSalesTax;
        }

        public BasketItem BasketItem
        {
            get { return _basketItem; }
        }

        public decimal PriceIncludingSalesTax
        {
            get { return _priceIncludingSalesTax; }
        }

        private readonly BasketItem _basketItem;
        private readonly decimal _priceIncludingSalesTax;
    }
}
