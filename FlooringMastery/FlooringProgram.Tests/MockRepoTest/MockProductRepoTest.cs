using FlooringProgram.Data;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    public class MockProductRepoTest
    {
        [Test]
        public void GetAllProductTest()
        {
            var repo = new ProductMockModeRepo();
            var result = repo.GetAll();
            Assert.AreEqual(result.Count , 3);
            Assert.AreEqual(result[1].ProdType, "Laminate");
        }

        [Test]
        public void GetOneProductTest()
        {
            var repo = new ProductMockModeRepo();

            var result = repo.GetOne("Wood");
            

            Assert.AreEqual(result.CostPerSqFt, 5.50m);
        }
    }
}