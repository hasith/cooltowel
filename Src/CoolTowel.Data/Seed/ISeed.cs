using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Seed
{
    interface ISeed
    {
        void Seed(DatabaseContext context);
    }
}
