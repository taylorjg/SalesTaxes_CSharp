namespace Code
{
    public class BasketItem
    {
        public BasketItem(string description, decimal price, params SalesTaxes[] salesTaxes)
        {
            _description = description;
            _price = price;
            _salesTaxes = salesTaxes;
        }

        public string Description
        {
            get { return _description; }
        }

        public decimal Price
        {
            get { return _price; }
        }

        public SalesTaxes[] SalesTaxes
        {
            get { return _salesTaxes; }
        }

        private readonly string _description;
        private readonly decimal _price;
        private readonly SalesTaxes[] _salesTaxes;
    }
}
