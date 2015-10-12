using System;
using System.Collections.Generic;
using System.Linq;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class OrderMockModeRepo : ITransactionDataRepo<Order>
    {
        private static List<Order> _orders = new List<Order>
        {
            {new Order() {OrderNumber = 1, CustomerName= "Wise", State= "OH", TaxRate=0.0625m ,ProductType ="Wood", Area = 100m,CostPerSqFt=5.15m,LaborCostPerSqFt=4.75m,TotalMaterialCost = 515m,TotalLaborCost=475m,TotalTax=61.88m,Total=1051.88m}},
            {new Order() {OrderNumber = 2, CustomerName= "Eric", State= "OH", TaxRate=0.0625m ,ProductType ="Wood", Area = 100m,CostPerSqFt=5.15m,LaborCostPerSqFt=4.75m,TotalMaterialCost = 515m,TotalLaborCost=475m,TotalTax=61.88m,Total=1051.88m}}
        };

        public Order Add(Order entry, string date)
        {
            int id = _orders.Select(o => o.OrderNumber).Max()+1;
            entry.OrderNumber = id;
            _orders.Add(entry); 
            return entry;
        }

        public void Edit(Order entry, string date)
        {
            var result = _orders.IndexOf(_orders.FirstOrDefault(o => o.OrderNumber == entry.OrderNumber));

            _orders[result] = entry; 
        }

        public List<Order> GetAll( string date)
        {
            return _orders;
        }

        public Order GetOne(int id, string date)
        {
            return _orders.FirstOrDefault(o => o.OrderNumber == id); 
        }

        public void Remove(Order entry, string date)
        {
            _orders.Remove(_orders.FirstOrDefault(o => o.OrderNumber == entry.OrderNumber));
        }
    }
}