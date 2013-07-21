using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Seed
{
    class DropIfChanged : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        internal DropIfChanged(IList<ISeed> seeds)
        {
            Seeds = seeds;
        }

        protected override void Seed(DatabaseContext context)
        {
            foreach (var seed in Seeds)
            {
                seed.Seed(context);
            }
        }

        public IList<ISeed> Seeds { get; set; }
    }
}
