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
            // get Product w/ProductCode 2222 and store in variable p
            p = ProductDB.GetProduct("2222      ");
            // ProductCode of p should be equal to 2222
            Assert.AreEqual("2222      ", p.ProductCode);
            // OnHandQuantity of p should be equal to 12
            Assert.AreEqual(12, p.OnHandQuantity);
        }
        [Test]
        public void TestDeleteProduct()
        {
            // note: as written, this test only functions after TestAddProduct is run
            Product p = new Product();
            p.ProductCode = "2222";
            p.Description = "Test Product";
            p.UnitPrice = 100.00M;
            p.OnHandQuantity = 12;

            // delete Product p from ProductDB
            ProductDB.DeleteProduct(p);
            // GetProduct p in ProductDB should be equal to null (since it is deleted)
            Assert.AreEqual(null, ProductDB.GetProduct(p.ProductCode));
        }
        [Test]
        public void TestUpdateProduct()
        {
            Product p2 = new Product();
            p2.ProductCode = "zzzz";
            p2.Description = "Test Product 2";
            p2.UnitPrice = 99.99M;
            p2.OnHandQuantity = 2;

            Product p3 = new Product();
            p3.ProductCode = "zzzz2";
            p3.Description = "Updated Test Product 2";
            p3.UnitPrice = 99.99M;
            p3.OnHandQuantity = 3;

            // add Product p2 to ProductDB
            ProductDB.AddProduct(p2);
            // update pd with p3 properties
            ProductDB.UpdateProduct(p2, p3);
            // GetProduct p3 from ProductDB and store in variable q
            Product q = ProductDB.GetProduct(p3.ProductCode);

            // ProductCode of q should still be equal to zzzz2
            Assert.AreEqual("zzzz2     ", q.ProductCode);
            // OnHandQuantity should reflect update and be equal to 3
            Assert.AreEqual(3, q.OnHandQuantity);

            // Delete Product p3 from ProductDB (since no test set-up)
            ProductDB.DeleteProduct(p3);
        }
    }
}
