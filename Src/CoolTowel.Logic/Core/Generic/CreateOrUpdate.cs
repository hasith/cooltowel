using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Logic.Core.Generic
{
    public class CreateOrUpdate<Entity> : BaseService<Entity, Entity> where Entity : class, IIdentifier 
    {
        public CreateOrUpdate(IUnitOfWork uow) : base(uow) { }

        public override Entity Execute(Entity entity)
        {
            return UnitOfWork.GetEntityRepository<Entity>().InsertOrUpdate(entity);
        }
    }
}
