using System;
using System.ComponentModel;
using FlooringMastery.WorkFlow;

namespace FlooringProgram.UI.WorkFlow
{
    public class MainMenu
    {
        public void Execute()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("****************************************************************");
                Console.WriteLine("*                Flooring Program                              *");
                Console.WriteLine("*                                                              *");
                Console.WriteLine("*                1. Display Orders                             *");
                Console.WriteLine("*                2. Add an Order                               *");
                Console.WriteLine("*                3. Edit an Order                              *");
                Console.WriteLine("*                4. Remove an Order                            *");
                Console.WriteLine("*                5. Quit                                       *");
                Console.WriteLine("*                                                              *");
                Console.WriteLine("****************************************************************");

                Console.WriteLine("Please enter your choice, 1-5.");

                string input = Console.ReadLine();

                if (input == "")
                {
                    continue;
                }

                if (input == "5")
                    break;

                ProcessChoice(input);

            } while (true);
        }

        private void ProcessChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    var displayWF = new DisplayOrdersWorkFlow();
                    displayWF.Execute();
                    break;
                case "2":
                    var addOrderWF = new AddOrderWorkFlow();
                    addOrderWF.Execute();
                    break;
                case "3":
                    var editOrderWorkFlow = new EditOrderWorkFlow();
                    editOrderWorkFlow.Execute();
                    break;
                case "4":
                    var deleteOrderWF = new DeleteWorkFlow();
                    deleteOrderWF.Execute();
                    break;
                default:
                    Console.WriteLine("This is not a valid choice. Please enter a number 1-5.");
                    break;
            }
        }
    }
}