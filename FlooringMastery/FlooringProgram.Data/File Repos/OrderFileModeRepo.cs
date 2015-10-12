using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class OrderFileModeRepo : ITransactionDataRepo<Order>
    {
      
        private string _path;

        public OrderFileModeRepo(string path)
        {
            _path = path;
        }

        public Order Add(Order order, string _date)
        {
            string _fileName = $@"{_path}Orders_{_date}.txt";

            int iD = 1;
            bool exist = IsFileExist(_fileName); 

            if (exist)
            {
                var listofOrders = GetAll(_date);
                if (listofOrders.Count == 0)
                    iD = 1;
                else
                {
                    iD = listofOrders.Select(a => a.OrderNumber).Max() + 1;
                }
            }

            order.OrderNumber = iD;

            appendOrder(order,exist, _fileName);

            return order;
        }

        public void Edit(Order entry, string _date)
        {
            var listofOrders = GetAll(_date);
            string _fileName = $@"{_path}Orders_{_date}.txt";


            var result = listofOrders.IndexOf(listofOrders.FirstOrDefault(o => o.OrderNumber == entry.OrderNumber));

            listofOrders[result] = entry; 

            OverwriteFile(listofOrders, _fileName);
        }

        public List<Order> GetAll(string _date)
        {
            string _fileName = $@"{_path}Orders_{_date}.txt";
            List<Order> OrdersFound = null;

            try
            {
                var reader = File.ReadAllLines(_fileName);

                if (reader != null)
                {
                    OrdersFound = new List<Order>();

                    for (int i = 1; i < reader.Length; i++)
                    {
                        var columns = reader[i].Split(',');
                        Order order = new Order();
                        order.OrderNumber = int.Parse(columns[0]);
                        order.CustomerName = columns[1];
                        order.State = columns[2];
                        order.TaxRate = decimal.Parse(columns[3]);
                        order.ProductType = columns[4];
                        order.Area = decimal.Parse(columns[5]);
                        order.CostPerSqFt = decimal.Parse(columns[6]);
                        order.LaborCostPerSqFt = decimal.Parse(columns[7]);
                        order.TotalMaterialCost = decimal.Parse(columns[8]);
                        order.TotalLaborCost = decimal.Parse(columns[9]);
                        order.TotalTax = decimal.Parse(columns[10]);
                        order.Total = decimal.Parse(columns[11]);

                        OrdersFound.Add(order);
                    }
                    return OrdersFound;
                }
            }
            catch (Exception fileNotFound)
            {
                
                using (var writer = File.AppendText(_path + @"Order_Errors.txt"))
                {
                    if (File.Exists(_path + @"Order_Errors.txt"))
                    {
                        writer.WriteLine("InvalidOrderDate, CurrentDate, Error Message");
                    }
                    writer.WriteLine($"{_date},{DateTime.Now.ToString("MMddyyyy")},{fileNotFound.Message}");
                }
            }
            return OrdersFound;
        }

        public Order GetOne(int id, string _date)
        {
            List<Order> orders = GetAll(_date);
            Order order = orders.FirstOrDefault(o => o.OrderNumber == id);

            return order;
        }

        public void Remove(Order entry, string _date)
        {
            var listofOrders = GetAll(_date);
            string _fileName = $@"{_path}Orders_{_date}.txt";

            listofOrders.Remove(listofOrders.FirstOrDefault(o => o.OrderNumber == entry.OrderNumber)); 

            OverwriteFile(listofOrders, _fileName);

            using (var writer = File.AppendText(_path + @"Deleted_Orders.txt"))
            {
                if (File.Exists(_path + @"Deleted_Orders.txt"))
                {
                    writer.WriteLine("OrderDate, DeleteDate, OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                }
                writer.WriteLine($"{_date},{DateTime.Now.ToString("MMddyyyy")},{entry.OrderNumber},{entry.CustomerName},{entry.State},{entry.TaxRate},{entry.ProductType},{entry.Area},{entry.CostPerSqFt},{entry.LaborCostPerSqFt},{entry.TotalMaterialCost},{entry.TotalLaborCost},{entry.TotalTax},{entry.Total}");
            }
        }

        private void OverwriteFile(List<Order> Orders, string _fileName)
        {

            File.Delete(_fileName);

            using (var writer = File.CreateText(_fileName))
            {
                writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                foreach (var order in Orders)
                {
                    writer.WriteLine($"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSqFt},{order.LaborCostPerSqFt},{order.TotalMaterialCost},{order.TotalLaborCost},{order.TotalTax},{order.Total}");
                }
            }
        }

        private void appendOrder(Order order, bool y, string _fileName)
        {

            using (var writer = File.AppendText(_fileName))
            {
                if (!y)
                {
                    writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                }
                writer.WriteLine($"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSqFt},{order.LaborCostPerSqFt},{order.TotalMaterialCost},{order.TotalLaborCost},{order.TotalTax},{order.Total}");
            }
        }

        public bool IsFileExist(string _fileName)
        {
            
            return File.Exists(_fileName); 
        }
    }
}