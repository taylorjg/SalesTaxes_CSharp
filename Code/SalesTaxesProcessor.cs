using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class SalesTaxesProcessor
    {
        public ReceiptDetails Purchase(params BasketItem[] basketItems)
        {
            var receiptItems = new List<ReceiptItem>();

            foreach (var item in basketItems)
            {
                var itemSalesTax = 0m;

                if (item.SalesTaxes.Contains(SalesTaxes.BasicTax))
                {
                    itemSalesTax += CalculatePercentage(item.Price, 10);
                }

                if (item.SalesTaxes.Contains(SalesTaxes.ImportDuty))
                {
                    itemSalesTax += CalculatePercentage(item.Price, 5);
                }

                var receiptItem = new ReceiptItem(item, item.Price + itemSalesTax);
                receiptItems.Add(receiptItem);
            }

            return new ReceiptDetails(receiptItems);
        }

        private static decimal CalculatePercentage(decimal n, decimal p)
        {
            return RoundUp(n*p/100);
        }

        private static decimal RoundUp(decimal n)
        {
            return Math.Ceiling(n * 20m) / 20.0m;
        }
    }
}
