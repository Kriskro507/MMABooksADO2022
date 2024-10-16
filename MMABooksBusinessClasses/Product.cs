using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MMABooksBusinessClasses
{
    public class Product
    {
        public Product() { }

        public Product(string productcode, string desc, decimal unitprice, int onhandquantity)
        {
            ProductCode = productcode;
            Description = desc;
            UnitPrice = unitprice;
            OnHandQuantity = onhandquantity;

        }

        private string productcode;
        private string desc;
        private decimal unitprice;
        private int onhandquantity;

        public string ProductCode
        {
            get
            {
                return productcode;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 10)
                    productcode = value;
                else
                    throw new ArgumentOutOfRangeException("Must be between 1-10");
            }
        }

        public string Description
        {
            get;
            set;
        }

        public decimal UnitPrice
        {
            get;
            set;
        }

        public int OnHandQuantity
        {
            get;
            set;
        }



    }

}
