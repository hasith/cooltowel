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

            context.Suppliers.Add(new Supplier { GUID = Guid.NewGuid(), Name = "Super Man", Description = "can do anything" });
            context.Suppliers.Add(new Supplier { GUID = Guid.NewGuid(), Name = "Spider Man", Description = "eats only spiders" });
            context.Suppliers.Add(new Supplier { GUID = Guid.NewGuid(), Name = "Iron Man", Description = "supply iron" });
            context.Suppliers.Add(new Supplier { GUID = Guid.NewGuid(), Name = "Ben 10", Description = "alian troubles" });
            context.Suppliers.Add(new Supplier { GUID = Guid.NewGuid(), Name = "Mr Been", Description = "Very reliable" });
            context.SaveChanges();
        }
    }
}
