using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        internal DatabaseContext Context { get; set; }
        internal Dictionary<Type, object> Repositories { get; private set; }

        public UnitOfWork(string connectionStringName, IList<Type> entityTypes)
        {
            Context = new DatabaseContext(connectionStringName, entityTypes);
            Repositories = new Dictionary<Type, object>();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }

        public IRepository<T> GetRepository<T>() where T : class, IIdentifier
        {
            // Look for repository in cache 
            object repoObj;
            if (Repositories.TryGetValue(typeof(IRepository<T>), out repoObj))
            {
                return (IRepository<T>)repoObj;
            }
            else
            {
                // Not found, add to cache, and return
                IRepository<T> repo = new Repository<T>(Context);
                Repositories[typeof(IRepository<T>)] = repo;
                return repo;
            }
        }

        
    }
}
