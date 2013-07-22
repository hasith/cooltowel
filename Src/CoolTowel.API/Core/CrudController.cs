using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;

namespace CoolTowel.API.Core
{
    
    public class CrudController<T> : UowController<T> where T:class, IIdentifier
    {
        protected IRepository<T> Repository { get; set; }

        public CrudController()
        {
            Repository = UnitOfWork.GetRepository<T>();
        }

        public override IQueryable<T> Get()
        {
            return Repository.GetAll();
        }

        [DebuggerHidden]
        protected override T GetEntityByKey(int key)
        {
           T entity = Repository.GetById(key);
           if (entity == null)
           {
               throwStatusException(HttpStatusCode.NotFound);
           }
           return entity;
        }

        [DebuggerHidden]
        protected override int GetKey(T entity)
        {
            if (entity == null)
            {
                throwStatusException(HttpStatusCode.BadRequest);
            }
            return entity.Id;
        }

        [DebuggerHidden]
        protected override T CreateEntity(T entity)
        {
            if (entity == null)
            {
                throwStatusException(HttpStatusCode.BadRequest);
            }
            entity = Repository.InsertOrUpdate(entity);
            UnitOfWork.Commit();
            return entity;
        }

        [DebuggerHidden]
        protected override T UpdateEntity(int key, T update)
        {
            if (key != update.Id)
            {
                throwStatusException(HttpStatusCode.BadRequest);
            } 

            update = Repository.Update(update);
            UnitOfWork.Commit();
            return update;
        }

        [DebuggerHidden]
        protected override T PatchEntity(int key, Delta<T> patchEntity)
        {
            T entity = Repository.GetById(key);
            if (entity == null)
            {
                throwStatusException(HttpStatusCode.NotFound);
            }
            patchEntity.Patch(entity);
            UnitOfWork.Commit();
            return entity;
        }

        [DebuggerHidden]
        public override void Delete([FromODataUri] int key)
        {
            T entity = Repository.GetById(key);
            if (entity == null)
            {
                throwStatusException(HttpStatusCode.NotFound);
            }

            Repository.Delete(entity);
            UnitOfWork.Commit();
        }

        [DebuggerHidden]
        private void throwStatusException(HttpStatusCode statusCode)
        {
            throw new HttpResponseException(statusCode);
        }
    }
}