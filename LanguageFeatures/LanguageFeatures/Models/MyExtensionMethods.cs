using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> cartParam)
        {
            return cartParam.Sum(prod => prod.Price);
        }

        public static IEnumerable<Product> FilterByCategory(
            this IEnumerable<Product> productEnum, string categoryParam)
        {
            return productEnum.Where(prod => prod.Category == categoryParam);
        }

        public static IEnumerable<Product> Filter(
            this IEnumerable<Product> productEnum, Func<Product, bool> selectorParam )
        {
            return productEnum.Where(selectorParam);
        }
    }
}