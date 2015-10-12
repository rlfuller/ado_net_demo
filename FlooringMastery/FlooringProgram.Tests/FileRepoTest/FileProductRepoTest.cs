using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data;
using NUnit.Framework;
using System.IO; 

namespace FlooringProgram.Tests.FileRepoTest
{
    [TestFixture]
    class FileProductRepoTest
    {
        [Test]
        public void GetAllProductTest()
        {
            var filePath = @"DataFiles\Products\";
            var repo = new ProductFileModeRepo(filePath);
            var result = repo.GetAll();
            Assert.AreEqual(result.Count, 4);
            Assert.AreEqual(result[1].ProdType, "Laminate");
        }

        [Test]
        public void GetOneProductTest()
        {
            var filePath = @"DataFiles\Products\";
            var repo = new ProductFileModeRepo(filePath);

            var result = repo.GetOne("Wood");

            Assert.AreEqual(result.CostPerSqFt, 5.15m);
        }
    }
}
