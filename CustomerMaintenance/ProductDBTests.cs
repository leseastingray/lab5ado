using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CustomerMaintenance
{
    [TestFixture]
    public class ProductDBTests
    {
        [Test]
        public void TestGetProduct()
        {
            // note: spaces needed due to ProductCode being char, not varchar
            Product p = ProductDB.GetProduct("2JST      ");

            Assert.AreEqual("2JST      ", p.ProductCode);
            Assert.AreEqual(6937, p.OnHandQuantity);
        }
    }
}
