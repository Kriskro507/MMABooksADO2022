using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data.MySqlClient;
using System.Data;
using MMABooksBusinessClasses;

namespace MMABooksDBClasses
{
    public static class CustomerDB
    {

        public static Customer GetCustomer(int customerID)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement
                = "SELECT CustomerID, Name, Address, City, State, ZipCode "
                + "FROM Customers "
                + "WHERE CustomerID = @CustomerID";
            MySqlCommand selectCommand =
                new MySqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@CustomerID", customerID);

            try
            {
                connection.Open();
                MySqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {
                    Customer customer = new Customer();
                    customer.CustomerID = (int)custReader["CustomerID"];
                    customer.Name = custReader["Name"].ToString();
                    customer.Address = custReader["Address"].ToString();
                    customer.City = custReader["City"].ToString();
                    customer.State = custReader["State"].ToString();
                    customer.ZipCode = custReader["ZipCode"].ToString();
                    return customer;
                }
                else
                {
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int AddCustomer(Customer customer)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string insertStatement =
                "INSERT Customers " +
                "(Name, Address, City, State, ZipCode) " +
                "VALUES (@Name, @Address, @City, @State, @ZipCode)";
            MySqlCommand insertCommand =
                new MySqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@Name", customer.Name);
            insertCommand.Parameters.AddWithValue(
                "@Address", customer.Address);
            insertCommand.Parameters.AddWithValue(
                "@City", customer.City);
            insertCommand.Parameters.AddWithValue(
                "@State", customer.State);
            insertCommand.Parameters.AddWithValue(
                "@ZipCode", customer.ZipCode);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                // MySQL specific code for getting last pk value
                string selectStatement =
                    "SELECT LAST_INSERT_ID()";
                MySqlCommand selectCommand =
                    new MySqlCommand(selectStatement, connection);
                int customerID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return customerID;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public static Customer GetCustomerByName(string customerName)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement = "SELECT CustomerID, Name, Address, City, State, ZipCode FROM Customers WHERE Name = @Name";
            MySqlCommand selectCommand = new MySqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@Name", customerName);

            try
            {
                connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        CustomerID = reader.GetInt32("CustomerID"),
                        Name = reader.GetString("Name"),
                        Address = reader.GetString("Address"),
                        City = reader.GetString("City"),
                        State = reader.GetString("State"),
                        ZipCode = reader.GetString("ZipCode")
                    };
                    return customer;
                }
                else
                {
                    return null; // Customer not found
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

        public static bool DeleteCustomerByName(Customer customer)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string deleteStatement = "DELETE FROM Customers WHERE Name LIKE @CustomerName";
            MySqlCommand deleteCommand = new MySqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@CustomerName", "%" + "%" + "%");

            try
            {
                connection.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Customer(s) deleted successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("No customer found with the given name.");
                    return false;
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

        public static bool UpdateCustomerName(string oldName, string newName)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string updateStatement = "UPDATE Customers SET Name = @NewName WHERE Name = @OldName";
            MySqlCommand updateCommand = new MySqlCommand(updateStatement, connection);
            updateCommand.Parameters.AddWithValue("@OldName", oldName);
            updateCommand.Parameters.AddWithValue("@NewName", newName);

            try
            {
                connection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Customer name updated successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("No customer found with the given name.");
                    return false;
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


        /*      public static bool DeleteCustomer(Customer customer)
              {
                  // get a connection to the database
                  string
                      MySqlConnection connection = MMABooksDB.GetConnection();
                  string deleteStatement =
                      "DELETE FROM Customers " +
                      "WHERE CustomerID = @CustomerID " +
                      "AND Name = @Name " +
                      "AND Address = @Address " +
                      "AND City = @City " +
                      "AND State = @State " +
                      "AND ZipCode = @ZipCode";
                  // set up the command object

                  try
                  {
                      connection.Open();

                      string query = "DELETE FROM Customers WHERE Name = @Name";
                      using (var command = new MySqlCommand(query, connection))
                      {
                          command.Parameters.AddWithValue("@Name", customerName);
                          int rowsAffected = command.ExecuteNonQuery();

                          if (rowsAffected > 0)
                          {
                              Console.WriteLine("Customer deleted successfully.");
                          }
                          else
                          {
                              Console.WriteLine("No customer found with the given name.");
                          }

                          // open the connection
                           // execute the command
                           // if the number of records returned = 1, return true otherwise return false
                      }
                  catch (MySqlException ex)
                  {
                      // throw the exception
                  }
                  finally
                  {
                      // close the connection
                  }

                  return false;
              }
        */
        public static bool UpdateCustomer(Customer oldCustomer,
            Customer newCustomer)
        {
            // create a connection
            string updateStatement =
                "UPDATE Customers SET " +
                "Name = @NewName, " +
                "Address = @NewAddress, " +
                "City = @NewCity, " +
                "State = @NewState, " +
                "ZipCode = @NewZipCode " +
                "WHERE CustomerID = @OldCustomerID " +
                "AND Name = @OldName " +
                "AND Address = @OldAddress " +
                "AND City = @OldCity " +
                "AND State = @OldState " +
                "AND ZipCode = @OldZipCode";
            // setup the command object
            try
            {
                // open the connection
                // execute the command
                // if the number of records returned = 1, return true otherwise return false
            }
            catch (MySqlException ex)
            {
                // throw the exception
            }
            finally
            {
                // close the connection
            }

            return false;
        }
    }
}
