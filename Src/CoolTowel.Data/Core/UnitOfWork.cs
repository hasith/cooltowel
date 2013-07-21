using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTowel.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        internal DbContext Context { get; set; }
        internal Dictionary<Type, object> RepositoryCache { get; private set; }

        public UnitOfWork(DbContext dbContext)
        {
            RepositoryCache = new Dictionary<Type, object>();
            Context = dbContext;
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
            if (RepositoryCache.TryGetValue(typeof(IRepository<T>), out repoObj))
            {
                return (IRepository<T>)repoObj;
            }
            else
            {
                // Not found, add to cache, and return
                IRepository<T> repo = new Repository<T>(Context);
                RepositoryCache[typeof(IRepository<T>)] = repo;
                return repo;
            }
        }

        
    }
}
