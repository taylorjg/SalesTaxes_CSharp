using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class SalesTaxCalculator
    {
        public static ReceiptDetails ProcessBasket(params BasketItem[] basketItems)
        {
            var receiptItems = basketItems.Select(basketItem =>
            {
                var salesTaxTypes = GetApplicableSalesTaxTypes(basketItem);
                var salesTax = CalculateSalesTax(basketItem.Price, salesTaxTypes);
                return new ReceiptItem(basketItem, salesTax);
            });
            return new ReceiptDetails(receiptItems);
        }

        private static readonly Category[] CategoriesExemptFromBasicTax = {
            Category.Books,
            Category.Food,
            Category.Medicinal
        };

        private static IEnumerable<SalesTaxTypes> GetApplicableSalesTaxTypes(BasketItem basketItem)
        {
            var exemptFromBasicTax = CategoriesExemptFromBasicTax.Contains(basketItem.Category);
            if (!exemptFromBasicTax) yield return SalesTaxTypes.BasicTax;
            if (basketItem.IsImported) yield return SalesTaxTypes.ImportDuty;
        }

        private static readonly IDictionary<SalesTaxTypes, decimal> SalesTaxPercentages = new Dictionary<SalesTaxTypes, decimal>
        {
            {SalesTaxTypes.BasicTax, 10},
            {SalesTaxTypes.ImportDuty, 5}
        };

        private static decimal CalculateSalesTax(decimal price, IEnumerable<SalesTaxTypes> salesTaxTypes)
        {
            var percentage = salesTaxTypes.Select(stt => SalesTaxPercentages[stt]).Sum();
            return RoundUp(CalculatePercentage(price, percentage));
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
