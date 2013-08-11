using CoolTowel.Data.Core;
using CoolTowel.Logic.Core.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;

namespace CoolTowel.API.Core
{
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CrudController<T> : UowController<T> where T:class, IIdentifier
    {
        protected IRepository<T> Repository { get; set; }

        public CrudController()
        {
            Repository = UnitOfWork.GetRepository<T>();
        }

        public override IQueryable<T> Get()
        {
            return new RetrieveAll<T>(UnitOfWork).Execute();
        }

        protected override T GetEntityByKey(int key)
        {
            T entity = new RetrieveById<T>(UnitOfWork).Execute(key);
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
            return entity.Id;
        }

        protected override T CreateEntity(T entity)
        {
            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            entity = new CreateOrUpdate<T>(UnitOfWork).Execute(entity);
            UnitOfWork.Commit();
            return entity;
        }

        protected override T UpdateEntity(int key, T update)
        {
            if (key != update.Id)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            update = new CreateOrUpdate<T>(UnitOfWork).Execute(update);
            UnitOfWork.Commit();
            return update;
        }

        protected override T PatchEntity(int key, Delta<T> patchEntity)
        {
            T entity = GetEntityByKey(key);
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
            T entity = GetEntityByKey(key);
            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            new Delete<T>(UnitOfWork).Execute(entity);
            UnitOfWork.Commit();
        }

    }
}