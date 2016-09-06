using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi2Book.DatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var contex = new WebApi2BookDbEntities();
            var query = from e in contex.TaskUser.Include("Task")
                       select e.Task.Subject;
            var model = query.ToList();
//            var singleOrDefault = model.SingleOrDefault();
//            if (singleOrDefault != null) Console.WriteLine(singleOrDefault.Task.Subject);
            Console.ReadKey();
            

        }
    }
}
