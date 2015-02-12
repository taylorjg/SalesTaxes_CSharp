using Code;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SalesTaxesProcessorTests
    {
        [Test]
        public void OneItemWithNoBasicTaxAndNoImportDuty()
        {
            var processor = new SalesTaxesProcessor();
            var receiptDetails = processor.Purchase(new BasketItem("Book", 10m));
            Assert.That(receiptDetails.SalesTaxes, Is.EqualTo(0m));
            Assert.That(receiptDetails.Total, Is.EqualTo(10m));
        }

        [Test]
        public void OneItemWithBasicTaxButNoImportDuty()
        {
            var processor = new SalesTaxesProcessor();
            var receiptDetails = processor.Purchase(new BasketItem("Perfume", 10m, SalesTaxes.BasicTax));
            Assert.That(receiptDetails.SalesTaxes, Is.EqualTo(1m));
            Assert.That(receiptDetails.Total, Is.EqualTo(11m));
        }

        [Test]
        public void OneItemWithNoBasicTaxButImportDuty()
        {
            var processor = new SalesTaxesProcessor();
            var receiptDetails = processor.Purchase(new BasketItem("Imported Perfume", 10m, SalesTaxes.ImportDuty));
            Assert.That(receiptDetails.SalesTaxes, Is.EqualTo(0.5m));
            Assert.That(receiptDetails.Total, Is.EqualTo(10.5m));
        }

        [Test]
        public void RoundingShizzle()
        {
            var processor = new SalesTaxesProcessor();
            var receiptDetails = processor.Purchase(new BasketItem("Music CD", 14.99m, SalesTaxes.BasicTax));
            Assert.That(receiptDetails.SalesTaxes, Is.EqualTo(16.49m - 14.99m));
            Assert.That(receiptDetails.Total, Is.EqualTo(16.49m));
        }

        [Test]
        public void RoundingShizzle2()
        {
            var processor = new SalesTaxesProcessor();
            var receiptDetails = processor.Purchase(new BasketItem("Box of imported chocolates", 11.25m, SalesTaxes.ImportDuty));
            Assert.That(receiptDetails.SalesTaxes, Is.EqualTo(11.85m - 11.25m));
            Assert.That(receiptDetails.Total, Is.EqualTo(11.85m));
        }

        [Test]
        public void FirstExample()
        {
            var processor = new SalesTaxesProcessor();
            var receiptDetails = processor.Purchase(
                new BasketItem("Book", 12.49m),
                new BasketItem("Music CD", 14.99m, SalesTaxes.BasicTax),
                new BasketItem("Chocolate bar", 0.85m));
            Assert.That(receiptDetails.SalesTaxes, Is.EqualTo(1.5m));
            Assert.That(receiptDetails.Total, Is.EqualTo(29.83m));
        }

        [Test]
        public void SecondExample()
        {
            var processor = new SalesTaxesProcessor();
            var receiptDetails = processor.Purchase(
                new BasketItem("Imported box of chocolates", 10m, SalesTaxes.ImportDuty),
                new BasketItem("Imported bottle of perfume", 47.50m, SalesTaxes.BasicTax, SalesTaxes.ImportDuty));
            Assert.That(receiptDetails.SalesTaxes, Is.EqualTo(7.65m));
            Assert.That(receiptDetails.Total, Is.EqualTo(65.15m));
        }

        [Test]
        public void ThirdExample()
        {
            var processor = new SalesTaxesProcessor();
            var receiptDetails = processor.Purchase(
                new BasketItem("Imported bottle of perfume", 27.99m, SalesTaxes.BasicTax, SalesTaxes.ImportDuty),
                new BasketItem("Bottle of perfume", 18.99m, SalesTaxes.BasicTax),
                new BasketItem("Packet of paracetamol", 9.75m),
                new BasketItem("Box of imported chocolates", 11.25m, SalesTaxes.ImportDuty));
            Assert.That(receiptDetails.SalesTaxes, Is.EqualTo(6.70m));
            Assert.That(receiptDetails.Total, Is.EqualTo(74.68m));
        }
    }
}
