using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Data;
using FlooringProgram.Models;
using NUnit.Framework;

namespace FlooringProgram.Tests.BLLTest
{
    [TestFixture]
    class OrderManagerTest
    {
        [Test]
        public void GetAllOrderTest()
        {
            string mode = "File";
            string productPath = @"DataFiles\Orders\";
            var orderManager = new OrderManager(mode, productPath);
            var result = orderManager.GetAllOrder("06012013"); 

            Assert.AreEqual(result.Data.Count, 2);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data[1].OrderNumber,2);
        }

        [Test]
        public void GetAllOrderTestFailswithIncorrectDate()
        {
            string mode = "File";
            string productPath = @"DataFiles\Orders\";
            var orderManager = new OrderManager(mode, productPath);
            var result = orderManager.GetAllOrder("09012013");

            //Assert.AreEqual(result.Data, null);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void GetAllOrderMockTest()
        {
            string mode = "Mock";
            string productPath = @"DataFiles\Orders\";
            var orderManager = new OrderManager(mode, productPath);
            var result = orderManager.GetAllOrder("06012013");

            Assert.AreEqual(result.Data.Count, 2);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data[1].OrderNumber, 2);
        }

        [Test]
        public void AddOrderTestExistingFile()
        {
            string mode = "File";
            string productPath = @"DataFiles\Orders\";
            var orderManager = new OrderManager(mode, productPath);

            Order order = new Order()
            {
                OrderNumber = 1,
                CustomerName = "Ringo",
                State = "OH",
                TaxRate = 0.0625m,
                ProductType = "Wood",
                Area = 100m,
                CostPerSqFt = 5.15m,
                LaborCostPerSqFt = 4.75m,
                TotalMaterialCost = 515m,
                TotalLaborCost = 475m,
                TotalTax = 61.88m,
                Total = 1051.88m
            };

            var result = orderManager.AddOrder(order,"06012013");

            Assert.AreEqual(result.Data.Count, 1);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data[0].CustomerName, "Ringo");
        }

        [Test]
        public void AddOrderTestNewFile()
        {
            string mode = "File";
            string productPath = @"DataFiles\Orders\";
            var orderManager = new OrderManager(mode, productPath);

            Order order = new Order()
            {
                OrderNumber = 1,
                CustomerName = "Ringo",
                State = "OH",
                TaxRate = 0.0625m,
                ProductType = "Wood",
                Area = 100m,
                CostPerSqFt = 5.15m,
                LaborCostPerSqFt = 4.75m,
                TotalMaterialCost = 515m,
                TotalLaborCost = 475m,
                TotalTax = 61.88m,
                Total = 1051.88m
            };

            var result = orderManager.AddOrder(order,"06012013");

            Assert.AreEqual(result.Data.Count, 1);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data[0].CustomerName, "Ringo");
        }

        [Test]
        public void EditOrder()
        {
            string mode = "File";
            string productPath = @"DataFiles\Orders\";
            var orderManager = new OrderManager(mode, productPath);

            var repo = new OrderFileModeRepo(productPath);
            var order = repo.GetOne(1, "06012013");
            order.State = "IN";
            order.TaxRate = 6.00m;

            var result = orderManager.EditOrder(order, "06012013");
           
            Assert.AreEqual(result.Data[0].State, "IN");
            Assert.AreEqual(result.Data[0].TaxRate, 6.00m);
            Assert.AreEqual(result.Data[0].TotalTax, 59.4m);
            Assert.IsTrue(result.Success);
            
        }

        [Test]
        public void DeleteOrder()
        {
            string mode = "File";
            string productPath = @"DataFiles\Orders\";
            var orderManager = new OrderManager(mode, productPath);

            var repo = new OrderFileModeRepo(productPath);
            var order = repo.GetOne(2, "06012013");
            
            var result = orderManager.DeleteOrder(order, "06012013");

            Assert.AreEqual(result.Data.Count, 1);
            Assert.IsTrue(result.Success);

        }
    }
}
