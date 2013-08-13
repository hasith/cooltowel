using CoolTowel.Data.Core;
using CoolTowel.Logic.Core.Generic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            Repository = UnitOfWork.GetEntityRepository<T>();
        }

        public override IQueryable<T> Get()
        {
            return new RetrieveAll<T>(UnitOfWork).Execute();
        }

        protected override T GetEntityByKey([FromODataUri] int key)
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
                var message = "Entity data is not recieved in the request";

                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(message),
                };
                throw new HttpResponseException(resp);
            }

            try
            {
                entity = new CreateOrUpdate<T>(UnitOfWork).Execute(entity);
                UnitOfWork.Commit();
                return entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = "Database concurrency error detected. Entities may have been modified or deleted since entities were loaded. Please reload the entity to obtain the new values.";

                var resp = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new StringContent(message),
                    ReasonPhrase = ex.Message,
                };
                throw new HttpResponseException(resp);
            }
        }

        protected override T UpdateEntity(int key, T entity)
        {
            if (key != entity.Id)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return CreateEntity(entity);

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
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Entity with ID = {0} not found", key))
                };
                throw new HttpResponseException(resp);
            }

            try
            {
                new Delete<T>(UnitOfWork).Execute(entity);
                UnitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = "Database concurrency error detected. Entities may have been modified or deleted since entities were loaded. Please reload the entity to obtain the new values.";
   
                var resp = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new StringContent(message),
                    ReasonPhrase = ex.Message,
                };
                throw new HttpResponseException(resp);
            }
        }

    }
}