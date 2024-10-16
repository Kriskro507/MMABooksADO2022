using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;

using MySql.Data.MySqlClient;


namespace MMABooksTests
{
    public class ProductDBTests
    {
        [SetUp]

        public void SetUp()
        {

        }

        [Test]

        public void TestGetProducts()
        {
            List<Product> products = ProductDB.GetProducts();
            Assert.IsNotNull(products);
            Assert.AreEqual(16, products.Count);
        }


        [Test]
        public void TestGetProductByProductCode()
        {
            string productCode = "A4CS";

            Product product = ProductDB.GetProductByProductCode(productCode);

            Assert.IsNotNull(product, "Murach''s ASP.NET 4 Web Programming with C# 2010");
            Assert.AreEqual(productCode, product.ProductCode);
        }

    }
}
