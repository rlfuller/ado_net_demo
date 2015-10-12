using System;
using FlooringProgram.Data;
using NUnit.Framework;
using System.Configuration;
using FlooringProgram.Models;

namespace FlooringProgram.Tests
{
    [TestFixture]
    public class FileOrderRepoTest
    {
        [Test]
        public static void getAllOrdersTest()
        {
            string path = @"DataFiles\Orders\";

            var repo = new OrderFileModeRepo(path);

            var result = repo.GetAll("06012013"); 

            Assert.AreEqual(result.Count,2);
            Assert.AreEqual(result[0].OrderNumber, 1);
        }

        [Test]
        public static void isFileExistTest()
        {
            string path = @"DataFiles\Orders\";
            var repo = new OrderFileModeRepo(path);

            var result = repo.IsFileExist(@"DataFiles\Orders\Orders_06012013.txt");

            Assert.IsTrue(result);
        }

        [Test]
        public static void getOneTest()
        {
            string path = @"DataFiles\Orders\";
            var repo = new OrderFileModeRepo(path);

            var result = repo.GetOne(1, "06012013");

            Assert.AreEqual(result.OrderNumber, 1);
            Assert.AreEqual(result.CustomerName, "Wise");
        }

        [Test]
        public static void DeleteTest()
        {
            string path = @"DataFiles\Orders\";
            var repo = new OrderFileModeRepo(path);
            Order result1 = repo.GetOne(2, "06012013");

            repo.Remove(result1, "06012013");
            var result = repo.GetAll("06012013");

            Assert.AreNotEqual(result.Count, 2);
            //Assert.AreEqual( result[1].CustomerName, "Eise");
            Assert.AreEqual( result[0].OrderNumber , 1);
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public static void EditTest()
        {
            string path = @"DataFiles\Orders\";
            var repo = new OrderFileModeRepo(path);
            Order result1 = repo.GetOne(1, "06012013");

            result1.CustomerName = "Ice"; 

            repo.Edit(result1, "06012013");
            var result = repo.GetAll("06012013");

            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0].CustomerName, "Ice");
        }

        [Test]
        public static void AddTest()
        {

            string path = @"DataFiles\Orders\";
            var repo = new OrderFileModeRepo(path);
            var repo2 = new OrderFileModeRepo(path);
            
         

            Order result = repo2.GetOne(2, "06012013");

            result.CustomerName = "ICE";

            repo.Add(result, "06012013");

            var result2 = repo.GetAll("06012013");

            Assert.AreEqual(result2[0].CustomerName, "Wise");
        }

    }
}