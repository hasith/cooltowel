using CoolTowel.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Core
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext(string connectionStringName, IList<Type> entitityTypes) :base (connectionStringName)
        {
            foreach (var entityType in entitityTypes)
            {
                Set(entityType);
            }
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
    }
}
