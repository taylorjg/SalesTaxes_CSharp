namespace Code
{
    public class BasketItem
    {
        public BasketItem(string description, decimal price, Category category, bool isImported)
        {
            _description = description;
            _price = price;
            _category = category;
            _isImported = isImported;
        }

        public BasketItem(string description, decimal price, Category category)
            : this(description, price, category, false)
        {
        }

        public BasketItem(string description, decimal price, bool isImported)
            : this(description, price, Category.Other, isImported)
        {
        }

        public BasketItem(string description, decimal price)
            : this(description, price, Category.Other, false)
        {
        }

        public string Description
        {
            get { return _description; }
        }

        public decimal Price
        {
            get { return _price; }
        }

        public Category Category
        {
            get { return _category; }
        }

        public bool IsImported
        {
            get { return _isImported; }
        }

        private readonly string _description;
        private readonly decimal _price;
        private readonly Category _category;
        private readonly bool _isImported;
    }
}
