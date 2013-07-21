using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
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

        protected override T GetEntityByKey(int key)
        {
           T entity = Repository.GetById(key);
           if (entity == null)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
           return entity;
        }

        protected override int GetKey(T entity)
        {
            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                return entity.Id;
            }    
        }

        protected override T CreateEntity(T entity)
        {
            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                entity = Repository.InsertOrUpdate(entity);
                UnitOfWork.Commit();
                return entity;
            }
        }

        protected override T UpdateEntity(int key, T update)
        {
            if (key != update.Id)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            } 

            update = Repository.Update(update);
            UnitOfWork.Commit();
            return update;
        }

        protected override T PatchEntity(int key, Delta<T> patchEntity)
        {
            T entity = Repository.GetById(key);
            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            patchEntity.Patch(entity);
            UnitOfWork.Commit();
            return entity;
        }

        public override void Delete([FromODataUri] int key)
        {
            T entity = Repository.GetById(key);
            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Repository.Delete(entity);
            UnitOfWork.Commit();
        }

    }
}