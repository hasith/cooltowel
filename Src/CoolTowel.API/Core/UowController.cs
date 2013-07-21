using CoolTowel.Data;
using CoolTowel.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.OData;

namespace CoolTowel.API.Core
{
    public class UowController<T> : EntitySetController<T, int> where T : class, IIdentifier
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        public UowController()
        {
            UnitOfWork = UowFactory.Create("DefaultConnection");
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}