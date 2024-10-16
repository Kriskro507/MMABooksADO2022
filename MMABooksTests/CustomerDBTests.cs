using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerDBTests
    {

        [Test]
        public void TestGetCustomer()
        {
            Customer c = CustomerDB.GetCustomer(4);
            Assert.AreEqual(4, c.CustomerID);
        }

        [Test]
        public void TestCreateCustomer()
        {
            Customer c = new Customer();
            c.Name = "Mickey Mouse";
            c.Address = "101 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "10101";

            int customerID = CustomerDB.AddCustomer(c);
            c = CustomerDB.GetCustomer(customerID);
            Assert.AreEqual("Mickey Mouse", c.Name);
        }


        [Test]
        public void TestDeleteCustomer()
        {

            string customerName = "Mickey Mouse";


            Customer c = CustomerDB.GetCustomerByName(customerName);
            Assert.IsNotNull(c, "Customer should exist before deletion.");
            Assert.AreEqual("Mickey Mouse", c.Name);
            Assert.AreEqual("101 Main Street", c.Address);
            Assert.AreEqual("Orlando", c.City);
            Assert.AreEqual("FL", c.State);
            Assert.AreEqual("10101", c.ZipCode);


            bool isDeleted = CustomerDB.DeleteCustomerByName(c);
            Assert.IsTrue(isDeleted, "Customer should be deleted.");


            c = CustomerDB.GetCustomerByName(customerName);
            Assert.IsNull(c, "Customer should no longer exist in the database.");
        }

        [Test]
        public void TestUpdateCustomerName()
        {

            string oldName = "Mickey Mouse";
            string newName = "Mickey House";


            bool isUpdated = CustomerDB.UpdateCustomerName(oldName, newName);
            Assert.IsTrue(isUpdated, "Customer name should be updated.");


            Customer c = CustomerDB.GetCustomerByName(newName);
            Assert.IsNotNull(c, "Customer should exist with the new name.");
            Assert.AreEqual(newName, c.Name);
        }

    }
}
