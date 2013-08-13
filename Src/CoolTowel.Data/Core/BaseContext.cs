using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Core
{
    public class BaseContext : DbContext
    {
        public BaseContext(string connectionStringName) : base(connectionStringName) { }

        /// <summary>
        /// EF new versions hide operations such as 'Detach' from being exposed. If one need to change
        /// state of an object he may use this method to go so.
        /// </summary>
        /// <param name="entity">entity to change state</param>
        /// <param name="entityState">new state</param>
        public void ChangeObjectState(object entity, EntityState entityState)
        {
            ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.ChangeObjectState(entity, entityState);
        }

    }
}
