using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FlooringMastery.WorkFlow;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.UI
{
    public class DisplayOrdersWorkFlow
    {
        public void Execute()
        {
            string date = HelperMethod.GetDate();
            var orderManager = new OrderManager(Program.Mode, Program.OrderPath);
            var result = orderManager.GetAllOrder(date);

            if (result.Success)
            {
                HelperMethod.displayOrders(result.Data);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}