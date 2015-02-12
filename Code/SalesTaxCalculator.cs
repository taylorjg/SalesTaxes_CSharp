using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class SalesTaxCalculator
    {
        public static ReceiptDetails ProcessBasket(params BasketItem[] basketItems)
        {
            var receiptItems = basketItems.Select(basketItem => new ReceiptItem(basketItem, CalculateSalesTax(basketItem)));
            return new ReceiptDetails(receiptItems);
        }

        private static readonly IDictionary<SalesTaxTypes, decimal> SalesTaxPercentages = new Dictionary<SalesTaxTypes, decimal>
        {
            {SalesTaxTypes.BasicTax, 10},
            {SalesTaxTypes.ImportDuty, 5}
        };

        private static decimal CalculateSalesTax(BasketItem basketItem)
        {
            var percentage = basketItem.SalesTaxTypes.Select(stt => SalesTaxPercentages[stt]).Sum();
            return RoundUp(CalculatePercentage(basketItem.Price, percentage));
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
