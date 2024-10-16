using System;
using System.Collections.Generic;
using System.Text;


using MySql.Data.MySqlClient;
using MMABooksBusinessClasses;

namespace MMABooksDBClasses
{
    public static class ProductDB
    {

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement = "SELECT ProductCode, Description, UnitPrice, OnHandQuantity "
                                   + "FROM Products "
                                   + "ORDER BY ProductCode";
            MySqlCommand selectCommand =
                    new MySqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                MySqlDataReader preader = selectCommand.ExecuteReader();
                while (preader.Read())
                {
                    Product p = new Product();
                    p.ProductCode = preader["ProductCode"].ToString();
                    p.Description = preader["Description"].ToString();
                    p.UnitPrice = (decimal)preader["UnitPrice"];
                    p.OnHandQuantity = (int)preader["OnHandQuantity"];
                    products.Add(p);
                }
                preader.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return products;
        }
        public static Product GetProductByProductCode(string productCode)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement = "SELECT ProductCode, Description, UnitPrice, OnHandQuantity FROM Products WHERE ProductCode = @ProductCode";
            MySqlCommand selectCommand = new MySqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ProductCode", productCode);

            try
            {
                connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductCode = reader["ProductCode"].ToString(),
                        Description = reader["Description"].ToString(),
                        UnitPrice = (decimal)reader["UnitPrice"],
                        OnHandQuantity = (int)reader["OnHandQuantity"]
                    };
                    return product;
                }
                else
                {
                    return null; // No product found with the given ProductCode
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            finally
            {
                connection.Close();

            }


        }


        public static class Product
        {
            public static List<Product> GetProducts()
            {
                List<Product> products = new List<Product>();
                MySqlConnection connection = MMABooksDB.GetConnection();
                string selectStatement = "SELECT ProductCode, Description, UnitPrice, OnHandQuantity FROM Products ORDER BY ProductCode";
                MySqlCommand selectCommand = new MySqlCommand(selectStatement, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = selectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductCode = reader["ProductCode"].ToString(),
                            Description = reader["Description"].ToString(),
                            UnitPrice = (decimal)reader["UnitPrice"],
                            OnHandQuantity = (int)reader["OnHandQuantity"]
                        };
                        products.Add(product);
                    }
                    reader.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }


            }
        }
