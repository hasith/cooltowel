using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http.OData.Builder;
using Microsoft.Data.Edm;
using CoolTowel.API;
using CoolTowel.Model;
using CoolTowel.Data.Core;
using CoolTowel.Test;
using System.Linq;

namespace CoolTowel.Test.Api
{
    [TestClass]
    public class CrudControllerTest : BaseDbIntegrationTest
    {
        
        private static string UrlBase = "http://some.server/";
        private static HttpClient Client;
        private static HttpServer Server;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            DeleteDbFile();

            var configurator = new EndpointConfigurator();
            var config = configurator.Configure(new HttpConfiguration());
            
            Server = new HttpServer(config);
            Client = new HttpClient(Server);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            if (Server != null)
            {
                Server.Dispose();
            }
        }


        [TestMethod]
        public void TestRestProduct()
        {
            var obj = new Product() { Name = "test rest product", GUID = Guid.NewGuid(), Category="Hot", Price=234};
            crudEntity<Product>("Products", obj);
        }

        [TestMethod]
        public void TestRestSupplier()
        {
            var obj = new Supplier() { Name = "test rest supplier", GUID = Guid.NewGuid(), Description = "good supplier" };
            crudEntity<Supplier>("Suppliers", obj);
        }

        private void crudEntity<T>(string resourceName, T entity) where T:class, IIdentifier
        {

            //insert POST
            T inserted = insert(resourceName, entity);
            Assert.IsTrue(inserted.Id > 0);

            //retreve GET (id)
            var response = retrieve<T>(resourceName, inserted.Id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            T retrieved = response.Content.ReadAsAsync<T>().Result;
            Assert.AreEqual(retrieved.GUID, inserted.GUID);

            //retreve GET all
            IQueryable<T> all = retrieveAll<T>(resourceName);
            Assert.AreEqual(1, all.Count());

            //update POST
            Guid newGuid = Guid.NewGuid();
            entity.GUID = newGuid;
            entity.Id = inserted.Id;
            T updated = update(resourceName, entity);
            Assert.AreEqual(newGuid, updated.GUID);

            //delete DELETE
            delete<T>(resourceName, inserted.Id);
            response = retrieve<T>(resourceName, inserted.Id);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        private T update<T>(string resourceName, T entity) where T : class, IIdentifier
        {
            var endPoint = "rest/" + resourceName;
            var request = createRequest(endPoint, HttpMethod.Post, entity);
            using (HttpResponseMessage response = Client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                return response.Content.ReadAsAsync<T>().Result;
                
            }
        }

        private void delete<T>(string resourceName, int id) where T : class, IIdentifier
        {
            var endPoint = "rest/" + resourceName + "(" + id + ")";
            var request = createRequest(endPoint, HttpMethod.Delete);
            using (HttpResponseMessage response = Client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        private HttpResponseMessage retrieve<T>(string resourceName, int id) where T : class, IIdentifier
        {
            var endPoint = "rest/" + resourceName + "(" + id + ")";
            var request = createRequest(endPoint, HttpMethod.Get);
            using (HttpResponseMessage response = Client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response);
                return response;
            }
        }

        private IQueryable<T> retrieveAll<T>(string resourceName) where T : class, IIdentifier
        {
            var endPoint = "rest/" + resourceName;
            var request = createRequest(endPoint, HttpMethod.Get);
            using (HttpResponseMessage response = Client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                return response.Content.ReadAsAsync<IQueryable<T>>().Result;
                
            }
        }

        private T insert<T>(string resourceName, T entity) where T : class, IIdentifier
        {
            var endPoint = "rest/" + resourceName;
            var request = createRequest(endPoint, HttpMethod.Post, entity);

            using (HttpResponseMessage response = Client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                return response.Content.ReadAsAsync<T>().Result;
            }
        }

        private HttpRequestMessage createRequest(string url, HttpMethod method)
        {
            var request = new HttpRequestMessage();

            request.RequestUri = new Uri(UrlBase + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = method;

            return request;
        }

        private HttpRequestMessage createRequest<T>(string url, HttpMethod method, T content) where T : class
        {
            HttpRequestMessage request = createRequest(url, method);
            request.Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter());

            return request;
        }

 
    }
}
