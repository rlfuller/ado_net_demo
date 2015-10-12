using System;
using System.Collections.Generic;
using System.Net.Cache;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.UI;

namespace FlooringMastery.WorkFlow
{
    public class AddOrderWorkFlow
    {
        private string _currentDate = DateTime.Now.ToString("MMddyyyy");


        private ProductManager _productManager = new ProductManager(Program.Mode, Program.ProductPath);
        private StateManager _stateManager = new StateManager(Program.Mode, Program.StatePath);
        private HelperMethod _helper = new HelperMethod();

        public void Execute()
        {
            var orderManager = new OrderManager(Program.Mode, Program.OrderPath);
            Console.Clear();
            //var name = getCustomerName(); 
            var _name = _helper.ValidCustomerName(getCustomerName());
            var _product = _helper.validProductRepo(getProductName());
            var _state = _helper.validStateRepo(getStateAbbrev()); 
            var _area = _helper.validArea(getArea());
            decimal _taxRate = _stateManager.getTaxRate(_state);
            decimal _laborCostPerSqFt = _productManager.GetLaborCostPerSqFt(_product);
            decimal _costPerSqFt = _productManager.GetCostPerSqFt(_product);
            decimal _totalLaborCost = orderManager.TotalLaborCostCal(_area, _laborCostPerSqFt);
            decimal _totalMaterialCost = orderManager.TotalMaterialCostCal(_area, _costPerSqFt);
            decimal _totalTax = orderManager.TotalTaxCal(_taxRate, _totalMaterialCost, _totalLaborCost);
            decimal _total = orderManager.TotalCal(_taxRate, _totalMaterialCost, _totalLaborCost);

            var newOrder = new List<Order>()
            { new Order()
                {
                    CustomerName = _name,
                    ProductType = _product,
                    State = _state,
                    TaxRate = _taxRate,
                    Area = _area,
                    LaborCostPerSqFt = _laborCostPerSqFt,
                    CostPerSqFt = _costPerSqFt,
                    TotalMaterialCost = _totalMaterialCost,
                    TotalLaborCost = _totalLaborCost,
                    TotalTax = _totalTax,
                    Total = _total
                 }
            };
            Console.Clear();
            Console.WriteLine("Here is your new order...");

            HelperMethod.displayOrders(newOrder);

            do
            {
                Console.WriteLine("Commit new order? Y/N");
                var commit = Console.ReadLine().ToUpper();
                if (commit == "Y" || commit == "YES")
                {
                    var result = orderManager.AddOrder(newOrder[0], _currentDate);

                    if (result.Success)
                    {
                        Console.WriteLine("You order is now added!");
                        HelperMethod.displayOrders(result.Data);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(); 
                        break;
                    }
                    else
                    {
                        Console.WriteLine(result.Message);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                }
                else if (commit == "N" || commit == "NO")
                    break;
                Console.WriteLine("Your input is invalid. Press any key to continue...");
                Console.ReadKey();
            } while (true);
        }

        //string name, product type, area, state 
        private string getCustomerName()
        {
            Console.Clear();
            Console.Write("Please enter customer name : ");
            return Console.ReadLine();
        }


        private string getProductName()
        {
            Console.Clear();
            Console.Write("Please enter product name : ");
            return Console.ReadLine();
        }

        private string getStateAbbrev()
        {
            Console.Clear();
            Console.Write("Please enter state abbreviation : ");
            return Console.ReadLine();
        }

        private string getArea()
        {
            Console.Clear();
            Console.Write("Please enter area : ");
            return Console.ReadLine();
        }
    }
}