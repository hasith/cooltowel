using CoolTowel.Data.Core;
using CoolTowel.Data.Repos;
using CoolTowel.Data.Seed;
using CoolTowel.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data
{
    public static class UowFactory
    {

        static UowFactory()
        {
            Database.SetInitializer(new DropIfChanged(new List<ISeed> { 
                new SeedBaseData(), 
                new SeedSampleData()
            }));
        }

        public static IUnitOfWork Create(string connectionStringName)
        {
            var context = new DatabaseContext(connectionStringName);
            var uow = new UnitOfWork(context);
            //register any extended entity repository or custom repositories
            uow.RegisterEntityRepository<Product>(new ProductRepository(context));

            return uow;
        }
    }
}
