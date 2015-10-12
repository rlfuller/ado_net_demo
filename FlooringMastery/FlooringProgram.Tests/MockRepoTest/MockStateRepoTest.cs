using FlooringProgram.Data;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    public class MockStateRepoTest
    {
        [Test]
        public void GetAllTaxTest()
        {
            var repo = new TaxRateMockModeRepo();
            var result = repo.GetAll();
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0].StateAbbrev, "NY");
        }

        [Test]
        public void GetOneTaxTest()
        {
            var repo = new TaxRateMockModeRepo();

            var result = repo.GetOne("NY");

            Assert.AreEqual(result.TaxRate, 0.075m);
        }
    }
}