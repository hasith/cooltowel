using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace CoolTowel.Data.Core
{
 

    public class Repository<T> : IRepository<T> where T : class, IIdentifier
    {

        protected DbContext DbContext { get; set; }
        internal DbSet<T> DbSet { get; set; }

        public Repository(DbContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }


        public virtual IQueryable<T> GetAll(string[] includes = null)
        {
            return DbSet.AsQueryable();
        }


        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void InsertOrUpdate(T entity)
        {
            DbSet.Add(entity);

            if (entity.Id == 0)
            {
                DbSet.Add(entity);
            }
            else
            {
                DbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Delete(T entityToDelete)
        {
            if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            DbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }

}
