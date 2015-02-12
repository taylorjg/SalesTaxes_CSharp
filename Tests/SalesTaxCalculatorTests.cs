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
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Book", 10m));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(0m));
            Assert.That(receiptDetails.Total, Is.EqualTo(10m));
        }

        [Test]
        public void OneItemWithBasicTaxButNoImportDuty()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Perfume", 10m, SalesTaxTypes.BasicTax));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(1m));
            Assert.That(receiptDetails.Total, Is.EqualTo(11m));
        }

        [Test]
        public void OneItemWithNoBasicTaxButImportDuty()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Imported Perfume", 10m, SalesTaxTypes.ImportDuty));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(0.5m));
            Assert.That(receiptDetails.Total, Is.EqualTo(10.5m));
        }

        [Test]
        public void RoundingTest1()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Music CD", 14.99m, SalesTaxTypes.BasicTax));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(16.49m - 14.99m));
            Assert.That(receiptDetails.Total, Is.EqualTo(16.49m));
        }

        [Test]
        public void RoundingTest2()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(new BasketItem("Box of imported chocolates", 11.25m, SalesTaxTypes.ImportDuty));
            Assert.That(receiptDetails.SalesTax, Is.EqualTo(11.85m - 11.25m));
            Assert.That(receiptDetails.Total, Is.EqualTo(11.85m));
        }

        [Test]
        public void FirstExample()
        {
            var receiptDetails = SalesTaxCalculator.ProcessBasket(
                new BasketItem("Book", 12.49m),
                new BasketItem("Music CD", 14.99m, SalesTaxTypes.BasicTax),
                new BasketItem("Chocolate bar", 0.85m));
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
                new BasketItem("Imported box of chocolates", 10m, SalesTaxTypes.ImportDuty),
                new BasketItem("Imported bottle of perfume", 47.50m, SalesTaxTypes.BasicTax, SalesTaxTypes.ImportDuty));
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
                new BasketItem("Imported bottle of perfume", 27.99m, SalesTaxTypes.BasicTax, SalesTaxTypes.ImportDuty),
                new BasketItem("Bottle of perfume", 18.99m, SalesTaxTypes.BasicTax),
                new BasketItem("Packet of paracetamol", 9.75m),
                new BasketItem("Box of imported chocolates", 11.25m, SalesTaxTypes.ImportDuty));
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
