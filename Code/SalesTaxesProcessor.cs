using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class SalesTaxesProcessor
    {
        public static ReceiptDetails Purchase(params BasketItem[] basketItems)
        {
            var seed = new List<ReceiptItem>();

            Func<List<ReceiptItem>, BasketItem, List<ReceiptItem>> func = (acc, item) =>
            {
                var salesTaxes = CalculateSalesTaxes(item);
                var receiptItem = new ReceiptItem(item, item.Price + salesTaxes);
                acc.Add(receiptItem);
                return acc;
            };

            return new ReceiptDetails(basketItems.Aggregate(seed, func));
        }

        private static readonly IDictionary<SalesTaxes, decimal> SalesTaxPercentages = new Dictionary<SalesTaxes, decimal>
        {
            {SalesTaxes.BasicTax, 10},
            {SalesTaxes.ImportDuty, 5}
        };

        private static decimal CalculateSalesTaxes(BasketItem item)
        {
            var percentage = item.SalesTaxes.Select(st => SalesTaxPercentages[st]).Sum();
            return CalculatePercentage(item.Price, percentage).RoundUp();
        }

        private static decimal CalculatePercentage(decimal n, decimal p)
        {
            return (n*p/100);
        }
    }
}
