using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.UI;

namespace FlooringMastery.WorkFlow
{
    internal class HelperMethod
    {
        private ProductManager _productManager = new ProductManager(Program.Mode, Program.ProductPath);
        private StateManager _stateManager = new StateManager(Program.Mode, Program.StatePath);

        internal static void displayOrders(List<Order> orders)
        {
            Console.WriteLine("OrderNum | CustomerName | State | TaxRate | ProductType | Area | CostPerSqFt | LaborCostPerSqFt | MaterialCost | LaborCost |    Tax    | Total");
            foreach (var order in orders)
            {
                Console.WriteLine($"{order.OrderNumber,-9}| {order.CustomerName,-13}| {order.State,-6}| {order.TaxRate/100,-8:P}| {order.ProductType,-12}| {order.Area,-5}| {order.CostPerSqFt,-12:c}| {order.LaborCostPerSqFt,-17:c}| {order.TotalMaterialCost,-13:c}| {order.TotalLaborCost,-10:c}| {order.TotalTax,-10:c}| {order.Total,-8:c}");
            }
        }

        internal static string GetDate()
        {
            string input_date;
            DateTime date;
            string output_date; 

            do
            {
                Console.WriteLine("Enter the Order Date.");
                input_date = Console.ReadLine();

                bool success = DateTime.TryParse(input_date, out date);

                if (success)
                {
                    output_date = date.ToString("MMddyyyy"); 
                    return output_date;
                }

                Console.WriteLine("This is not valid input. Date must be in valid format. Press any key to continue.");
                Console.ReadKey();
            } while (true);
        }

        internal static int GetID()
        {
            int ID;
            string userInput;
            do
            {
                Console.WriteLine("Please enter OrderID or 'R' to return to the main menu.");
                userInput = Console.ReadLine();
                if (userInput.ToUpper() == "R")
                    return 0;
                bool success = int.TryParse(userInput, out ID);
                if (success)
                {
                    return ID; 
                }
                Console.WriteLine("Your input is invalid, press any key to coninue...");
                Console.ReadKey(); 
            } while (true);
        }

        internal static string validProductName(string productName)
        {
            while (productName == "")
            {
                Console.WriteLine("Product cannot be null. Please enter valid Product. Press any Key to continue. ");
                Console.ReadKey();
                Console.Write("Please enter product name : ");
                productName = Console.ReadLine();
            }

            return (productName.Substring(0, 1).ToUpper() + productName.Substring(1).ToLower());
        }


        internal string validProductRepo(string productName)
        {
            do
            {
                productName = validProductName(productName);

                if (_productManager.IsProductName(productName))
                {
                    return productName;
                }
                Console.WriteLine("Product is not valid, please enter a valid product. Press any key to continue...");
                Console.ReadKey();
                Console.Write("Please enter product name : ");
                productName = Console.ReadLine();
            } while (true);
        }

        internal string validStateAbbrev(string stateabbrev)
        {
            while (stateabbrev == "")
            {
                Console.WriteLine("State cannot be null. Please enter valid state. Press any Key to continue. ");
                Console.ReadKey();
                Console.Write("Please enter State Abbreviation : ");
                stateabbrev = Console.ReadLine();
            }

            return stateabbrev.ToUpper();
        }


        internal string validStateRepo(string stateabbrev)
        {
            do
            {
                stateabbrev = validStateAbbrev(stateabbrev);

                if (_stateManager.isState(stateabbrev))
                {
                    return stateabbrev;
                }
                Console.WriteLine("State is not valid, please enter a valid State abbreviation. Press any key to continue...");
                Console.ReadKey();
                Console.Write("Please enter state abbrevation : ");
                stateabbrev = Console.ReadLine();
            } while (true);
        }

        internal decimal validArea(string area_str)
        {
            bool parse_success;
            decimal area;
            do
            {
                parse_success = decimal.TryParse(area_str, out area);
                if (parse_success && area > 0)
                {
                    return area;
                }
                Console.WriteLine("Area is not valid format, press any key to continue.");
                Console.ReadKey();
                Console.Write("Please enter area : ");
                area_str = Console.ReadLine();
            } while (true);
        }

        internal string ValidCustomerName(string _name)
        {
            while (_name == "" || _name.Contains(","))
            {
                Console.WriteLine("Customer name cannot be blank, customer name cannot contain comma. Press any key to continue...");
                Console.ReadKey();
                Console.Write("Please enter customer name : ");
                _name = Console.ReadLine();
            }
            return _name;
        }

    }
}
