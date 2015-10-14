using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Przejście do adresu URL pokazującego przykład";
        }

        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();
            myProduct.Name = "Kajak";
            string productName = myProduct.Name;
            return View("Result", (object)String.Format("Nazwa produktu: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            Product myProduct = new Product
            {
                ProductID = 100,
                Name = "Kajak",
                Description = "Lódka jednosobowa",
                Price = 275M,
                Category = "Sporty wodne"
            };
            
            return View("Result", (object)String.Format("Nazwa produktu: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kajak", Price=275M },
                    new Product {Name="Kamizelka ratunkowa", Price=48.95M },
                    new Product {Name="Piłka nożna", Price=19.50M },
                    new Product {Name="Flaga narożna", Price=34.95M },
                }
            };

            var cartTotal = cart.TotalPrices();
            return View("Result", (object)String.Format("Razem: {0:c}", cartTotal));
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kajak", Category = "Sporty wodne", Price=275M },
                    new Product {Name="Kamizelka ratunkowa",Category = "Sporty wodne",  Price=48.95M },
                    new Product {Name="Piłka nożna", Category = "Piłka nożna", Price=19.50M },
                    new Product {Name="Flaga narożna",Category = "Piłka nożna",  Price=34.95M },
                }
            };

            var total = products.FilterByCategory("Piłka nożna").Sum(prod => prod.Price);
            return View("Result", (object)String.Format("Razem: {0:c}", total));
        }

        public ViewResult UseExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kajak", Category = "Sporty wodne", Price=275M },
                    new Product {Name="Kamizelka ratunkowa",Category = "Sporty wodne",  Price=48.95M },
                    new Product {Name="Piłka nożna", Category = "Piłka nożna", Price=19.50M },
                    new Product {Name="Flaga narożna",Category = "Piłka nożna",  Price=34.95M },
                }
            };
            

            //            Func<Product, bool> categoryFilter = delegate(Product prod)
            //            {
            //                return prod.Category == "Piłka nożna";
            //            };
            // Func<Product, bool> categoryFilter = prod => prod.Category == "Piłka nożna";
            var total = products.Filter(prod => prod.Category == "Piłka nożna"||prod.Price>20).Sum(prod => prod.Price);
            return View("Result", (object)String.Format("Razem: {0:c}", total));
        }

        public ViewResult CreateAnonArray()
        {

            var oddsAndEnds = new[]
            {
                new Product {Name = "MVC", Category = "Sporty wodne"},
                new Product {Name = "Kapelusz", Category = "Sporty wodne"},
                new Product {Name = "Jabłko", Category = "Piłka nożna"},
            };

            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append("");
            }
            return View("Result", (object)result.ToString());
        }
        public ViewResult FindProducts()
        {
            Product[] products =
            {
                new Product {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                new Product {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M},
            };

            var foundProducts = products.OrderByDescending(prod => prod.Price).
                Take(3).Select(prod => new { prod.Name, prod.Price});

            products[2] = new Product { Name="Stadion", Price = 7960M };
            
            var result = new StringBuilder();

            foreach (var prod in foundProducts)
            {
                result.AppendFormat("Cena: {0} ", prod.Price);
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult SumProducts()
        {
            Product[] products =
            {
                new Product {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                new Product {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M},
            };

            var results = products.Sum(prod => prod.Price);

            products[2] = new Product { Name = "Stadion", Price = 7960M };

            return View("Result", (object)String.Format("Razem: {0:c}", results));
        }
    }
}