using Microsoft.Data.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoolTowel.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var configurator = new EndpointConfigurator();
            configurator.Configure(config);
        }
    }
}
