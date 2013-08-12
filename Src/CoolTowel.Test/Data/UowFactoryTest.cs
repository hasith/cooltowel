﻿using System;
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
    public class UowFactoryTest : BaseDbIntegrationTest
    {
        
        private static CoolTowel.Data.Core.IUnitOfWork Uow { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            DeleteDbFile();
            //create the unit of work to be tested
            Uow = UowFactory.Create("DefaultConnection");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Uow.Dispose();
        }

        [TestMethod]
        public void TestCrudProduct()
        {
            Product tester = new Product() { GUID = Guid.NewGuid(), Name = "Test DB Product", Price = 289, Category = "Hot" };
            crudEntity(tester);
        }

        [TestMethod]
        public void TestCrudSupplier()
        {
            Supplier tester = new Supplier() { GUID = Guid.NewGuid(), Name = "Test DB Supplier", Description = "Great for testing"};
            crudEntity(tester);
        }

        private void crudEntity<T>(T tester) where T:class, IIdentifier
        {
            var repository = Uow.GetEntityRepository<T>();
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
