using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoolTowel.Model;
using CoolTowel.Data.Core;
using CoolTowel.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace CoolTowel.Test.Data
{
    [TestClass]
    public class ConcurrencyExceptionTest : BaseDbIntegrationTest
    {
        private static int EntityId { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            DeleteDbFile();

            //create an instance to be used in test
            IUnitOfWork Uow = UowFactory.Create("DefaultConnection");
            var repo = Uow.GetEntityRepository<Product>();
            var item = new Product() { Name = "Pro one", Category = "Best Seller" };
            var inserted = repo.InsertOrUpdate(item);
            Uow.Commit();
            EntityId = inserted.Id;
        }


        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void TestConcurrency()
        {
            //client 1 tacking a instance and updating
            UnitOfWork client1Uow = (UnitOfWork)UowFactory.Create("DefaultConnection");
            var client1Repo = client1Uow.GetEntityRepository<Product>();
            var client1Product = client1Repo.GetById(EntityId);
            //lets detach before saving to simulate detached client behaviour
            client1Uow.Context.ChangeObjectState(client1Product, EntityState.Detached);
            client1Product.Name = "Client 1 new name";

            //client 2 tacking the same instance and updating
            UnitOfWork client2Uow = (UnitOfWork)UowFactory.Create("DefaultConnection");
            var client2Repo = client2Uow.GetEntityRepository<Product>();
            var client2Product = client2Repo.GetById(EntityId);
            client2Uow.Context.ChangeObjectState(client2Product, EntityState.Detached);
            client2Product.Name = "Client 2 new name";

            //now save the client 1
            client1Repo.InsertOrUpdate(client1Product);
            client1Uow.Commit();

            //now try to save the client 2, this should result in an concurrency exception
            client2Repo.InsertOrUpdate(client2Product);
            client2Uow.Commit();

        }
    }
}
