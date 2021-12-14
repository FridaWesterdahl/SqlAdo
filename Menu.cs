using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlAdo
{
    class Menu
    {
        public void Logo()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(@"
|''||''|        '||`                    '||      
   ||            ||                 ''   ||      
   ||    .|''|,  ||  .|''|, '||''|  ||   || //`  
   ||    ||..||  ||  ||..||  ||     ||   ||<<    
  .||.   `|...  .||. `|...  .||.   .||. .|| \\.  
                                                 
                                        ");

            Console.ResetColor();
        }
        private Handler _handler;
        public Menu()
        {
            _handler = new Handler();
        }
        public Menu ShowMenu()
        {
            Console.Clear();
            Logo();
            Console.WriteLine("What do you want to do? Enter the number below: \n" +
                "[1] Add a new customer\n" +
                "[2] Delete a customer\n" +
                "[3] Update the address for a employee\n" +
                "[4] Show sales for a country of your choosing\n" +
                "[5] Make a new order\n" +
                "[6] Delete a customer and its orders");
            int number = int.Parse(Console.ReadLine());
            switch (number)
            {
                case 1: _handler.AddCustomer();
                    Console.ReadLine();
                    break;
                case 2: _handler.DeleteCustomer();
                    Console.ReadLine();
                    break;
                case 3: _handler.UpdateEmployee();
                    Console.ReadLine();
                    break;
                case 4: _handler.ShowCountrySales();
                    Console.ReadLine();
                    break;
                case 5: _handler.AddNewOrder();
                    Console.ReadLine();
                    break;
                case 6: _handler.DeleteOrderAndCustomer();
                    Console.ReadLine();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have to choose a number between 1-6!");
                    Console.ResetColor();
                    Console.ReadLine();
                    break;
            }
            return ShowMenu();
        }
       
    }
}
