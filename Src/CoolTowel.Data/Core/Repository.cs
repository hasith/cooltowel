﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;


namespace CoolTowel.Data.Core
{
 

    public class Repository<T> : IRepository<T> where T : class, IIdentifier
    {

        protected DbContext Context { get; set; }
        internal DbSet<T> EntitySet { get; set; }

        public Repository(DbContext context)
        {
            Context = context;
            EntitySet = context.Set<T>();
        }


        public virtual IQueryable<T> GetAll()
        {
            return EntitySet.AsQueryable();
        }


        public T GetById(int id)
        {
            return EntitySet.Find(id);
        }

        public virtual T InsertOrUpdate(T entity)
        {
            T ret = null;
            if (entity.Id == 0)
            {
                ret = EntitySet.Add(entity);
            }
            else
            {
                return Update(entity);
            }
            return ret;
        }

        public virtual T Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                EntitySet.Attach(entityToDelete);
            }
            return EntitySet.Remove(entityToDelete);
        }

        public virtual T Update(T entityToUpdate)
        {
            try
            {
                T ret = EntitySet.Attach(entityToUpdate);
                Context.Entry(entityToUpdate).State = EntityState.Modified;
                return ret;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (T)entry.Entity;
                var databaseValues = (T)entry.GetDatabaseValues().ToObject();

                throw ex;
            }
        }
    }

}
