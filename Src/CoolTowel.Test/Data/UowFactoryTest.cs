using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoolTowel.Data;
using CoolTowel.Model;
using System.IO;
using System.Linq;
using System.Configuration;
using CoolTowel.Data.Core;
using System.Diagnostics;


namespace CoolTowel.Test.Data
{
    [TestClass]
    public class UowFactoryTest
    {
        
        private static CoolTowel.Data.Core.IUnitOfWork Uow { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            //set Data Directory for the connection string to use
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Empty));

            //lets delete any existing database files
            var databaseFile = Path.Combine((string)AppDomain.CurrentDomain.GetData("DataDirectory"), "CoolTowelDatabase.mdf");
            var databaseLogFile = Path.Combine((string)AppDomain.CurrentDomain.GetData("DataDirectory"), "CoolTowelDatabase_log.ldf");
            File.Delete(databaseFile);
            File.Delete(databaseLogFile);


            //create the unit of work to be tested
            Uow = UowFactory.Create("DefaultConnection");

            Trace.TraceInformation("CRUD tests initializing");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Uow.Dispose();
        }

        [TestMethod]
        public void TestCrudProduct()
        {
            Product tester = new Product() { GUID = Guid.NewGuid(), Name = "Test Product", Price = 289, Category = "Hot" };
            crudEntity(tester);
        }

        [TestMethod]
        public void TestCrudSupplier()
        {
            Supplier tester = new Supplier() { GUID = Guid.NewGuid(), Name = "Test Supplier", Description = "Great for testing"};
            crudEntity(tester);
        }

        private void crudEntity<T>(T tester) where T:class, IIdentifier
        {
            var repository = Uow.GetRepository<T>();
            //Insert
            var inserted = repository.InsertOrUpdate(tester);
            Uow.Commit();
            Assert.IsTrue(inserted.Id > 0);
            Assert.AreEqual(tester.GUID, inserted.GUID);

            //query GetAll
            var products = repository.GetAll();
            var retrieved = products.Where(p => p.GUID == tester.GUID).FirstOrDefault();
            Assert.AreEqual(tester.GUID, retrieved.GUID);

            //GetById
            var retrievedById = repository.GetById(inserted.Id);
            Assert.AreEqual(tester.GUID, retrievedById.GUID);

            //Update
            Guid newGuid = Guid.NewGuid();
            retrievedById.GUID = newGuid;
            repository.InsertOrUpdate(retrievedById);
            Uow.Commit();
            Assert.AreEqual(newGuid, repository.GetById(inserted.Id).GUID);

            //Update
            newGuid = Guid.NewGuid();
            retrievedById.GUID = newGuid;
            repository.Update(retrievedById);
            Uow.Commit();
            Assert.AreEqual(newGuid, repository.GetById(inserted.Id).GUID);

            //Delete
            repository.Delete(retrievedById);
            Uow.Commit();
            retrievedById = repository.GetById(inserted.Id);
            Assert.IsNull(retrievedById);

        }

        
    }
}
