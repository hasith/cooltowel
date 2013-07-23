using CoolTowel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Seed
{
    class SeedSampleData : ISeed
    {
        public void Seed(DatabaseContext context)
        {
            
            context.Database.CreateIfNotExists();

            context.Products.Add(new Product { GUID = Guid.NewGuid(), Name = "Rubber Duck", Price = 345 });
            context.Products.Add(new Product { GUID = Guid.NewGuid(), Name = "Metalic Pen", Price = 356 }); 
            context.Products.Add(new Product { GUID = Guid.NewGuid(), Name = "White Cap", Price = 443 });
            context.Products.Add(new Product { GUID = Guid.NewGuid(), Name = "Bottle Wine", Price = 424 });
            context.Products.Add(new Product { GUID = Guid.NewGuid(), Name = "Paper Bag", Price = 765 });
            context.Products.Add(new Product { GUID = Guid.NewGuid(), Name = "Sheet Tan", Price = 325 });
            context.SaveChanges();


        }
    }
}
