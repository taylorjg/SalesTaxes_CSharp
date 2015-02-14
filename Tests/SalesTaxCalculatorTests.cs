using System.Linq;
using Code;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SalesTaxCalculatorTests
    {
        [Test]
        public void OneItemWithNoBasicTaxAndNoImportDuty()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Book", 10m, Category.Books));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(0m));
            Assert.That(receiptDetails.Total, Is.EqualTo(10m));
        }

        [Test]
        public void OneItemWithBasicTaxButNoImportDuty()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Perfume", 10m));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(1m));
            Assert.That(receiptDetails.Total, Is.EqualTo(11m));
        }

        [Test]
        public void OneItemWithNoBasicTaxButImportDuty()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Imported chocolates", 10m, Category.Food, true));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(0.5m));
            Assert.That(receiptDetails.Total, Is.EqualTo(10.5m));
        }

        [Test]
        public void RoundingTest1()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Music CD", 14.99m));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(16.49m - 14.99m));
            Assert.That(receiptDetails.Total, Is.EqualTo(16.49m));
        }

        [Test]
        public void RoundingTest2()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Box of imported chocolates", 11.25m, Category.Food, true));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(11.85m - 11.25m));
            Assert.That(receiptDetails.Total, Is.EqualTo(11.85m));
        }

        [Test]
        public void FirstExample()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(
                new BasketItem("Book", 12.49m, Category.Books),
                new BasketItem("Music CD", 14.99m),
                new BasketItem("Chocolate bar", 0.85m, Category.Food));
            var receiptItems = receiptDetails.ReceiptItems.ToList();
            Assert.That(receiptItems[0].PriceIncludingSalesTax, Is.EqualTo(12.49m));
            Assert.That(receiptItems[1].PriceIncludingSalesTax, Is.EqualTo(16.49m));
            Assert.That(receiptItems[2].PriceIncludingSalesTax, Is.EqualTo(0.85m));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(1.5m));
            Assert.That(receiptDetails.Total, Is.EqualTo(29.83m));
        }

        [Test]
        public void SecondExample()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(
                new BasketItem("Imported box of chocolates", 10m, Category.Food, true),
                new BasketItem("Imported bottle of perfume", 47.50m, true));
            var receiptItems = receiptDetails.ReceiptItems.ToList();
            Assert.That(receiptItems[0].PriceIncludingSalesTax, Is.EqualTo(10.50m));
            Assert.That(receiptItems[1].PriceIncludingSalesTax, Is.EqualTo(54.65m));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(7.65m));
            Assert.That(receiptDetails.Total, Is.EqualTo(65.15m));
        }

        [Test]
        public void ThirdExample()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(
                new BasketItem("Imported bottle of perfume", 27.99m, true),
                new BasketItem("Bottle of perfume", 18.99m),
                new BasketItem("Packet of paracetamol", 9.75m, Category.Medicinal),
                new BasketItem("Box of imported chocolates", 11.25m, Category.Food, true));
            var receiptItems = receiptDetails.ReceiptItems.ToList();
            Assert.That(receiptItems[0].PriceIncludingSalesTax, Is.EqualTo(32.19m));
            Assert.That(receiptItems[1].PriceIncludingSalesTax, Is.EqualTo(20.89m));
            Assert.That(receiptItems[2].PriceIncludingSalesTax, Is.EqualTo(9.75m));
            Assert.That(receiptItems[3].PriceIncludingSalesTax, Is.EqualTo(11.85m));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(6.70m));
            Assert.That(receiptDetails.Total, Is.EqualTo(74.68m));
        }
    }
}
