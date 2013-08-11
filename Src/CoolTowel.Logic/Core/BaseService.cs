using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Logic.Core
{
    public abstract class BaseService <Request, Response> 
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public BaseService(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        public abstract Response Execute(Request request);
    }
}
