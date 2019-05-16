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
        [Test]
        public void TestAddProduct()
        {
            // note: this test can only be run once until you have written a set-up
            //  the set-up will reset the database, so that the test can be run

            // create a new Product and set properties
            Product p = new Product();
            p.ProductCode = "2222";
            p.Description = "Test Product";
            p.UnitPrice = 100.00M;
            p.OnHandQuantity = 12;

            // add the new Product p to the ProductDB
            ProductDB.AddProduct(p);

            p = ProductDB.GetProduct("2222      ");
            Assert.AreEqual("2222      ", p.ProductCode);
            Assert.AreEqual(12, p.OnHandQuantity);
        }
        [Test]
        public void TestDeleteProduct()
        {
            Product p = new Product();
            p.ProductCode = "2222";
            p.Description = "Test Product";
            p.UnitPrice = 100.00M;
            p.OnHandQuantity = 12;

            ProductDB.DeleteProduct(p);
            Assert.AreEqual(null, ProductDB.GetProduct(p.ProductCode));
        }
    }
}
