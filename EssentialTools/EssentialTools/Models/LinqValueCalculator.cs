using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;

namespace EssentialTools.Models
{
    public class LinqValueCalculator: IValueCalculator
    {
        private IDiscountHelper discounter;
        private static int counter;
        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;
            System.Diagnostics.Debug.WriteLine($"Utworzono egzemplarz {++counter}");
        }
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return discounter.ApplyDiscount(products.Sum(prod => prod.Price));
        }
    }

}