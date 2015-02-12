using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class SalesTaxCalculator
    {
        public static ReceiptDetails ProcessBasket(params BasketItem[] basketItems)
        {
            var seed = new List<ReceiptItem>();

            Func<List<ReceiptItem>, BasketItem, List<ReceiptItem>> func = (acc, item) =>
            {
                var salesTax = CalculateSalesTax(item);
                var receiptItem = new ReceiptItem(item, item.Price + salesTax);
                acc.Add(receiptItem);
                return acc;
            };

            return new ReceiptDetails(basketItems.Aggregate(seed, func));
        }

        private static readonly IDictionary<SalesTaxTypes, decimal> SalesTaxPercentages = new Dictionary<SalesTaxTypes, decimal>
        {
            {SalesTaxTypes.BasicTax, 10},
            {SalesTaxTypes.ImportDuty, 5}
        };

        private static decimal CalculateSalesTax(BasketItem item)
        {
            var percentage = item.SalesTaxTypes.Select(stt => SalesTaxPercentages[stt]).Sum();
            return RoundUp(CalculatePercentage(item.Price, percentage));
        }

        private static decimal CalculatePercentage(decimal n, decimal p)
        {
            return n*p/100;
        }

        public static decimal RoundUp(decimal n)
        {
            return Math.Ceiling(n*20m)/20.0m;
        }
    }
}
