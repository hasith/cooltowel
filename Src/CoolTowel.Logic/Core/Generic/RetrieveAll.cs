using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Logic.Core.Generic
{
    public class RetrieveAll<Entity> : BaseService<object, IQueryable<Entity>> where Entity : class, IIdentifier 
    {
        public RetrieveAll(IUnitOfWork uow) : base(uow) { }

        public override IQueryable<Entity> Execute(object request = null)
        {
            return UnitOfWork.GetRepository<Entity>().GetAll();
        }
    }
}
