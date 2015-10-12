using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Data;
using NUnit.Framework;
using FlooringProgram.UI; 

namespace FlooringProgram.Tests.BLLTest
{
    [TestFixture]
    class ProductManagerTest
    {
        [Test]
        public void getCostPerSqFtTest()
        {

            string mode = "File";
            string productPath = @"DataFiles\Products\"; 

            var manager = new ProductManager(mode, productPath);

            var result = manager.GetCostPerSqFt("Tile");

            Assert.AreEqual(result, 3.50m);
        }

        [Test]
        public void GetLaborCostPerSqFtTestSucceeds()
        {

            string mode = "File";
            string productPath = @"DataFiles\Products\";

            var manager = new ProductManager(mode, productPath);

            var result = manager.GetLaborCostPerSqFt("Tile");

            Assert.AreEqual(result, 4.15m);
        }

       
    }
}
