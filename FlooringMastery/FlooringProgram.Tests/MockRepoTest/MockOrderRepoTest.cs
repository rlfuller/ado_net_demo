using FlooringProgram.Data;
using FlooringProgram.Models;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    public class MockOrderRepoTest
    {
        [Test]
        public void GetAllOrderTest()
        {
            string mode = "Mock";

            var repo = new OrderMockModeRepo();
            var result = repo.GetAll("06012013");
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[1].OrderNumber, 2);
        }

        [Test]
        public void GetOneOrderTest()
        {
            string mode = "Mock";

            var repo = new OrderMockModeRepo();

            var result = repo.GetOne(2, "06012013");

            Assert.AreEqual(result.CustomerName, "Eric");
        }

        [Test]
        public void AddOrderTest()
        {
            string mode = "Mock";

            var repo = new OrderMockModeRepo();

            var newOrder = repo.GetOne(2, "06012013");

            var result = repo.Add(newOrder, "06012013"); 

            Assert.AreEqual(result.OrderNumber, 3);
        }

        [Test]
        public void EditOrder()
        {
            string mode = "Mock";
            var repo = new OrderMockModeRepo();

            var newOrder = repo.GetOne(2, "06012013");
            
            newOrder.CustomerName = "Smith";
            repo.Edit(newOrder, "06012013");

            var results = repo.GetOne(2, "06012013");

            Assert.AreEqual(results.CustomerName, "Smith");
        }

        [Test]
        public void DeleteOrder()
        {
            var repo = new OrderMockModeRepo();
            string mode = "Mock";


            var order = repo.GetOne(1, "06012013");
            repo.Remove(order, "06012013");

            var results = repo.GetAll("06012013");

            Assert.AreEqual(results.Count, 1);
        }
    }
}