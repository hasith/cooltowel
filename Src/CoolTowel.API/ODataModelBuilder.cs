using CoolTowel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.OData.Builder;

namespace CoolTowel.API
{
    public class ODataModelBuilder : ODataConventionModelBuilder
    {

        public ODataModelBuilder() 
        {
            EntitySet<Product>("Products");
            EntitySet<Supplier>("Suppliers");
        }
        
    }
}