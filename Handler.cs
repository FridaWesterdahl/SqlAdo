using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlAdo
{
    class Handler
    {
        private SqlConnection dbcon;
        public Handler()
        {
            string connectionString =
            "Data Source=LAPTOP-MOP66LEC\\SQLEXPRESS;Initial Catalog=TelerikAcademy99;"
            + "Integrated Security=true";
            dbcon = new SqlConnection(connectionString);
        }
        public void AddCustomer()
        {
                dbcon.Open();
                Console.WriteLine("Begin with adding a unique CustomerID (5 characters):" +
                "\n-----------------------------");
            string customerID = Console.ReadLine().ToUpper();
            Console.WriteLine("Add a Contact name (first- and lastname):" +
                "\n-----------------------------");
            string contactName = Console.ReadLine();
            Console.WriteLine("Add an address:" +
                "\n-----------------------------");
            string address = Console.ReadLine();
            Console.WriteLine("Add a phonenumber:" +
                "\n-----------------------------");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Add a Company name:" +
                "\n-----------------------------");
            string companyName = Console.ReadLine();
            Console.WriteLine("Add a contact title (if any):" +
                "\n-----------------------------");
            string contactTitle = Console.ReadLine();
            Console.WriteLine("Add which city the customer is located in:" +
                "\n-----------------------------");
            string city = Console.ReadLine();
            Console.WriteLine("Add which region the customer is located in:" +
                "\n-----------------------------");
            string region = Console.ReadLine();
            Console.WriteLine("Add a postalcode:" +
                "\n-----------------------------");
            string postalCode = Console.ReadLine();
            Console.WriteLine("Add which country they are located in:" +
                "\n-----------------------------");
            string country = Console.ReadLine();
            Console.WriteLine("Enter a fax number if any:" +
                "\n-----------------------------");
            string fax = Console.ReadLine();

            string sqlAddCustomer =
                $"INSERT INTO Customers (CustomerID, ContactName, Address, Phone," +
                $"CompanyName, ContactTitle, City, Region, PostalCode," +
                $"Country, fax) " +
                $"VALUES('{customerID}','{contactName}','{address}','{phoneNumber}','{companyName}'," +
                $"'{contactTitle}','{city}','{region}','{postalCode}','{country}','{fax}');";
                SqlCommand command = new SqlCommand(sqlAddCustomer, dbcon);
                command.Connection = dbcon;
            int returnValue = command.ExecuteNonQuery();

            Console.WriteLine("Customer is added!");
            dbcon.Close();           

        }
        public void DeleteCustomer()
        {
                dbcon.Open();
                Console.WriteLine("Write the name or CustomerID you want to delete:");
                string userInput = Console.ReadLine();

                string sqlDeleteCustomer =
                    $"DELETE FROM Customers WHERE Customers.ContactName = " +
                    $"@userInput OR CustomerID = @userInput;";
                SqlCommand command = new SqlCommand(sqlDeleteCustomer, dbcon);
            command.Parameters.AddWithValue("@userInput",userInput);
                command.Connection = dbcon;
            int returnValue = command.ExecuteNonQuery();
            Console.WriteLine("Customer is deleted!");
            dbcon.Close();
        }
        public void UpdateEmployee()
        {
                dbcon.Open();
                Console.WriteLine("Write the EmployeeID of the employee you want to change:");
                int EmployeeID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the new address:");
                string userAddress = Console.ReadLine();

                string sqlUpdateEmployee =
                    $"UPDATE Addresses SET AddressText = '{userAddress}'" +
                    $"WHERE EmployeeID = '{EmployeeID}';";
                SqlCommand command = new SqlCommand(sqlUpdateEmployee, dbcon);
                command.Connection = dbcon;
            int returnValue = command.ExecuteNonQuery();
            Console.WriteLine("Address is updated!");
            dbcon.Close();
        }
        public void ShowCountrySales()
        {
                dbcon.Open();
                Console.WriteLine("Write which country you want to see the sales for:");
                string userCountry = Console.ReadLine();

                string sqlShowCountrySales =
                    $"SELECT e.FirstName + e.LastName AS Seller, SUM(od.UnitPrice) AS Sales," +
                    $"o.ShipCountry AS Country FROM Orders o INNER JOIN[Order Details] od " +
                    $"ON od.OrderID = o.OrderID " +
                    $"JOIN Employees e ON o.EmployeeID = e.EmployeeID " +
                    $"WHERE o.ShipCountry = '{userCountry}' " +
                    $"GROUP BY e.EmployeeID, e.FirstName + e.LastName, o.ShipCountry;";
                SqlCommand command = new SqlCommand(sqlShowCountrySales, dbcon);
                command.Connection = dbcon;
            
            SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        decimal Sales = (decimal)reader["Sales"];
                        string Country = (string)reader["Country"];
                        string Seller = (string)reader["Seller"];
                        Console.WriteLine("{0}, {1}, {2}", Sales, Country, Seller);
                    }
                }
            int returnValue = command.ExecuteNonQuery();
            dbcon.Close();

        }
        public void AddNewOrder()
        {
            AddCustomer();

            dbcon.Open();
             string sqlAddNewOrder =
                $"INSERT INTO Orders(CustomerID, EmployeeID, OrderDate, RequiredDate," +
                $"ShippedDate,ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion," +
                $"ShipPostalCode, ShipCountry)" +
                $"VALUES('FRIWE', 1, 2020 - 12 - 20, 2020 - 12 - 24, 2020 - 12 - 20," +
                $"2, 33, 'FAF', 'Kattgatan 20', 'Kattrineholm', 'KT', '666 99', 'Sweden');" +
                $"INSERT INTO[Order Details](OrderID, ProductID, UnitPrice, Quantity, Discount)" +
                $"VALUES(SCOPE_IDENTITY(), 10, 13, 7, 0);";
            SqlCommand command = new SqlCommand(sqlAddNewOrder, dbcon);
            command.Connection = dbcon;
            int returnValue = command.ExecuteNonQuery();
            Console.WriteLine("Order is confirmed!");
            dbcon.Close();
        }
        public void DeleteOrderAndCustomer()
        {
            Console.WriteLine("Write the Customers ID that you want to delete:");
            string delCusID = Console.ReadLine();
            dbcon.Open();
            string sqlDeleteOrderAndCustomer =
                $"DELETE FROM [Order Details] WHERE OrderID " +
                $"IN (SELECT o.OrderID FROM Orders o WHERE o.CustomerID = '{delCusID}' );" +
                $"DELETE FROM Orders WHERE CustomerID = '{delCusID}';" +
                $"DELETE FROM Customers WHERE CustomerID = '{delCusID}';";
            SqlCommand command = new SqlCommand(sqlDeleteOrderAndCustomer, dbcon);
            command.Connection = dbcon;
            int returnValue = command.ExecuteNonQuery();
            Console.WriteLine("The customer and its order is deleted!");
            dbcon.Close();
        }

    }
}
