using System;
using System.Collections.Generic;
using System.Linq;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] products =
        {
            new Product { Name = "Kajak", Category = "Sporty wodne", Price = 275M},
            new Product { Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
            new Product { Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
            new Product { Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M},
        };

        [TestMethod]
        public void Sum_Products_Correctly()
        {
//            var discounter = new MinimumDiscountHelper();
//            var target = new LinqValueCalculator(discounter);
//            var goalToal = products.Sum(prod => prod.Price);

            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(moq => moq.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);

            var result = target.ValueProducts(products);

            Assert.AreEqual(products.Sum(prod=>prod.Price), result);
        }

        private IEnumerable<Product> createProduct(decimal value)
        {
            return new[] {new Product {Price=value} };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PassThroughVariableDiscounts()
        {
            //            var discounter = new MinimumDiscountHelper();
            //            var target = new LinqValueCalculator(discounter);
            //            var goalToal = products.Sum(prod => prod.Price);

            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(moq => moq.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            mock.Setup(moq => moq.ApplyDiscount(It.Is<decimal>(v=>v==0))).Throws<ArgumentOutOfRangeException>();
            mock.Setup(moq => moq.ApplyDiscount(It.Is<decimal>(v=>v>100))).Returns<decimal>(total => total*0.9M);
            mock.Setup(moq => moq.ApplyDiscount(It.IsInRange<decimal>(10,100,Range.Inclusive))).Returns<decimal>(total => total-5);
            var target = new LinqValueCalculator(mock.Object);

            var FiveDollarDiscount = target.ValueProducts(createProduct(5));
            var TenDollarDiscount = target.ValueProducts(createProduct(10));
            var FiftyDollarDiscount = target.ValueProducts(createProduct(50));
            var HundredDollarDiscount = target.ValueProducts(createProduct(100));
            var FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));

            Assert.AreEqual(5, FiveDollarDiscount, "Niepowodzenie 5 zł ");
            Assert.AreEqual(5, TenDollarDiscount, "Niepowodzenie 10 zł ");
            Assert.AreEqual(45, FiftyDollarDiscount, "Niepowodzenie 50 zł ");
            Assert.AreEqual(95, HundredDollarDiscount, "Niepowodzenie 100 zł ");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "Niepowodzenie 500 zł ");
            target.ValueProducts(createProduct(0));
        }
    }
}
