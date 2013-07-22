using CoolTowel.Data.Core;
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
            Database.SetInitializer(new DropAlways(new List<ISeed> { 
                new SeedBaseData(), 
                new SeedSampleData()
            }));
        }

        public static IUnitOfWork Create(string connectionStringName)
        {
            IUnitOfWork uow = new UnitOfWork(new DatabaseContext(connectionStringName));
            return uow;
        }
    }
}
