using System;
using System.Linq;
using FlooringProgram.BLL;
using FlooringProgram.UI;

namespace FlooringMastery.WorkFlow
{
    public class DeleteWorkFlow
    {
        public void Execute()
        {
            Console.Clear();
            string date = HelperMethod.GetDate();
            Console.Clear();
            var orderManager = new OrderManager(Program.Mode, Program.OrderPath);

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
                if (orderFound.Success)
                {
                    Console.Clear();
                    HelperMethod.displayOrders(orderFound.Data);
                    do
                    {
                        Console.WriteLine("Delete order? Y/N");
                        var commit = Console.ReadLine().ToUpper();
                        if (commit == "Y" || commit == "YES")
                        {
                            var delete_Result = orderManager.DeleteOrder(orderFound.Data[0], date);

                            if (delete_Result.Success)
                            {
                                Console.Clear();
                                Console.WriteLine("Your order is now deleted!");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine(delete_Result.Message);
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else if (commit == "N" || commit == "NO")
                            break;
                        Console.Clear();
                        Console.WriteLine("Your input is invalid. Press any key to continue...");
                        Console.ReadKey();
                    } while (true);
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("That is not a valid Order ID. Press any key to continue ...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Order Date is not found! Press any key to continue ..."); 
                Console.ReadKey();
            }
        }
    }
}