using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data;
using NUnit.Framework;

namespace FlooringProgram.Tests.FileRepoTest
{
    [TestFixture]
    class FileStateRepoTest
    {
        [Test]
        public void GetAllStateTest()
        {
            var filePath = @"DataFiles\States\";
            var repo = new TaxRateFileModeRepo(filePath);
            var result = repo.GetAll();

            Assert.AreEqual(result.Count, 4);
            Assert.AreEqual(result[1].StateAbbrev, "PA");
        }

        [Test]
        public void GetOneStateTest()
        {
            var filePath = @"DataFiles\States\";
            var repo = new TaxRateFileModeRepo(filePath);

            var result = repo.GetOne("IN");

            Assert.AreEqual(result.TaxRate, 6.00m);
        }
    }
}
