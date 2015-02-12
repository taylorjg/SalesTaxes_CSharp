using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class SalesTaxesProcessor
    {
        public ReceiptDetails Purchase(params BasketItem[] basketItems)
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

        private static decimal CalculateSalesTaxes(BasketItem item)
        {
            var salesTaxes = 0m;

            if (item.SalesTaxes.Contains(SalesTaxes.BasicTax))
            {
                salesTaxes += CalculatePercentage(item.Price, 10).RoundUp();
            }

            if (item.SalesTaxes.Contains(SalesTaxes.ImportDuty))
            {
                salesTaxes += CalculatePercentage(item.Price, 5).RoundUp();
            }

            return salesTaxes;
        }

        private static decimal CalculatePercentage(decimal n, decimal p)
        {
            return (n*p/100);
        }
    }
}
