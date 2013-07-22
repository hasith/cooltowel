using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoolTowel.API
{
    public class EndpointConfigurator
    {
        public HttpConfiguration Configure(HttpConfiguration config) 
        {
            config.MapHttpAttributeRoutes();

            //let keep only the JSON formatter
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=301869.
            config.EnableQuerySupport(new QueryableAttribute() { MaxExpansionDepth = 5 });

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            //enable cross domain requests
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //OData based rest endpoint
            config.Routes.MapODataRoute(
                "ODataRoute",
                "rest",
                new RestApiModelBuilder().GetEdmModel());

            //RPC based rpc endpoint
            config.Routes.MapHttpRoute(
                name: "RPCRoute",
                routeTemplate: "rpc/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            return config;
        }
    }
}