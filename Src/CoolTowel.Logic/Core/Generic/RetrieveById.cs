using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Logic.Core.Generic
{
    public class RetrieveById<Entity> : BaseService<int, Entity> where Entity : class, IIdentifier 
    {
        public RetrieveById(IUnitOfWork uow) : base(uow) { }

        public override Entity Execute(int id)
        {
            return UnitOfWork.GetEntityRepository<Entity>().GetById(id);
        }
    }
}
