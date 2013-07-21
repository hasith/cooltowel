using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoolTowel.Data;
using CoolTowel.Model;
using System.IO;
using System.Linq;
using System.Configuration;


namespace CoolTowel.Test.Data
{
    [TestClass]
    public class UowFactoryTest
    {
        
        private CoolTowel.Data.Core.IUnitOfWork Uow { get; set; }

        [TestInitialize]
        public void Initialize()
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
        }

        [TestCleanup]
        public void Cleanup()
        {
            Uow.Dispose();
        }

        [TestMethod]
        public void TestCreate()
        {
            Product tester = new Product() { Name = "Test Product", Price = 289, Category = "Hot" };
            var productRepo = Uow.GetRepository<Product>();

            //Insert
            Product inserted = productRepo.InsertOrUpdate(tester);
            Uow.Commit();
            Assert.IsTrue(inserted.Id > 0);
            Assert.AreEqual(tester.Category, inserted.Category);

            //query GetAll
            IQueryable<Product> products = productRepo.GetAll();
            Product retrieved = products.Where(p => p.Name == tester.Name).FirstOrDefault();
            Assert.AreEqual(tester.Category, retrieved.Category);

            //GetById
            Product retrievedById = productRepo.GetById(inserted.Id);
            Assert.AreEqual(tester.Category, retrievedById.Category);

            //Update
            retrievedById.Category = "Cool";
            productRepo.InsertOrUpdate(retrievedById);
            Uow.Commit();
            Assert.AreEqual("Cool", productRepo.GetById(inserted.Id).Category);

            //Delete
            productRepo.Delete(retrievedById);
            Uow.Commit();
            retrievedById = productRepo.GetById(inserted.Id);
            Assert.IsNull(retrievedById);

        }

        
    }
}
