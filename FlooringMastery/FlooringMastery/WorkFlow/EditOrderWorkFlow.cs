using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.UI;

namespace FlooringMastery.WorkFlow
{
    internal class EditOrderWorkFlow
    {
        public void Execute()
        {
            Console.Clear();
            string date = HelperMethod.GetDate();
            Console.Clear();
            var orderManager = new OrderManager(Program.Mode, Program.OrderPath);
            var helper = new HelperMethod();

            var retrieve_result = orderManager.GetAllOrder(date);

            if (retrieve_result.Success)
            {
                HelperMethod.displayOrders(retrieve_result.Data);
                int id; 

                while (true)
                {
                    id = HelperMethod.GetID();
                    if (id == 0)
                        break;
                    var success = retrieve_result.Data.Any(o => o.OrderNumber == id);

                    if (success)
                        break; 

                    Console.WriteLine("Your input ID is not valid, press enter to continue...");
                    Console.ReadKey();
                }

                if (id == 0)
                    return;

                var orderFound = orderManager.GetOneOrder(id, date);
                
                Order newOrder = new Order();

                if (orderFound.Success)
                {
                    Console.Clear();
                    newOrder = orderFound.Data[0];
                    HelperMethod.displayOrders(orderFound.Data);
                    string name = getCustomerName(orderFound.Data[0].CustomerName);
                    if (name != "")
                    {
                        newOrder.CustomerName = helper.ValidCustomerName(name);
                    }
                    string stateAbbrev = getState(orderFound.Data[0].State);
                    if (stateAbbrev != "")
                    {
                        newOrder.State = helper.validStateRepo(stateAbbrev);
                    }
                    string product = getProduct(orderFound.Data[0].ProductType);
                    if (product != "")
                    {
                        newOrder.ProductType = helper.validProductRepo(product);
                    }
                    string area = getArea(orderFound.Data[0].Area);
                    if (area != "")
                    {
                        newOrder.Area = helper.validArea(area);
                    }
                    var response = orderManager.EditOrder(newOrder, date);

                    if (response.Success)
                    {
                        Console.WriteLine("\n\n\n");
                        Console.WriteLine("Your order has been edited, here is your edited order details. ");
                        HelperMethod.displayOrders(response.Data);
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("That is not a valid Order ID. Press any key to continue ... ");
                    Console.ReadKey();
                }

            }
            else
            {
                Console.WriteLine("There are no orders for that date. Press any key to continue ...");
                Console.ReadKey();
            }
        }

        private string getCustomerName(string name)
        {
            Console.Write($"Enter Customer name ({name}):");
            return Console.ReadLine(); 
        }

        private string getState(string state)
        {
            Console.Write($"Enter state abbreviation ({state}):");
            return Console.ReadLine(); 
        }

        private string getProduct(string product)
        {
            Console.Write($"Enter product type ({product}):");
            return Console.ReadLine();
        }

        private string getArea(decimal area)
        {
            Console.Write($"Enter area ({area}):");
            return Console.ReadLine();
        }
    }
}
