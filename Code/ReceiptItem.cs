namespace Code
{
    public class ReceiptItem
    {
        public ReceiptItem(BasketItem basketItem, decimal priceIncludingTaxes)
        {
            _basketItem = basketItem;
            _priceIncludingTaxes = priceIncludingTaxes;
        }

        public BasketItem BasketItem
        {
            get { return _basketItem; }
        }

        public decimal PriceIncludingTaxes
        {
            get { return _priceIncludingTaxes; }
        }

        private readonly BasketItem _basketItem;
        private readonly decimal _priceIncludingTaxes;
    }
}
