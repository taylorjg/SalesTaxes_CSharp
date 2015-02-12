namespace Code
{
    public class BasketItem
    {
        public BasketItem(string description, decimal price, params SalesTaxTypes[] salesTaxTypes)
        {
            _description = description;
            _price = price;
            _salesTaxTypes = salesTaxTypes;
        }

        public string Description
        {
            get { return _description; }
        }

        public decimal Price
        {
            get { return _price; }
        }

        public SalesTaxTypes[] SalesTaxTypes
        {
            get { return _salesTaxTypes; }
        }

        private readonly string _description;
        private readonly decimal _price;
        private readonly SalesTaxTypes[] _salesTaxTypes;
    }
}
