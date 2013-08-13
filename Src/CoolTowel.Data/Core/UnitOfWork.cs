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
        public BaseContext Context { get; set; }
        internal Dictionary<Type, object> RepositoryCache { get; private set; }

        public UnitOfWork(BaseContext dbContext)
        {
            RepositoryCache = new Dictionary<Type, object>();
            Context = dbContext;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T> GetEntityRepository<T>() where T : class, IIdentifier
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

        public R GetCustomRepository<R>() where R : class
        {
            object repoObj;
            if (RepositoryCache.TryGetValue(typeof(R), out repoObj))
            {
                return (R)repoObj;
            }
            else
            {
                throw new NotImplementedException("There is no repository registered for the type " + typeof(R) + " in the UnitOfWork");
            }
            
        }

        public void RegisterEntityRepository<E>(IRepository<E> repo) where E : class, IIdentifier
        {
            RepositoryCache[typeof(IRepository<E>)] = repo;
        }

        public void RegisterCustomRepository<R>(R repo) where R : class
        {
            RepositoryCache.Add(typeof(R), repo);
        }
    }
}
