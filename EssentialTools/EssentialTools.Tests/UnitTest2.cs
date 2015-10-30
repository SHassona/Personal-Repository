using System;
using System.Linq;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void TestMethod1()
        {
            var discounter = new MinimumDiscountHelper();
            var target = new LinqValueCalculator(discounter);
            var goalToal = products.Sum(prod => prod.Price);

            var result = target.ValueProducts(products);

            Assert.AreEqual(goalToal, result);
        }
    }
}
