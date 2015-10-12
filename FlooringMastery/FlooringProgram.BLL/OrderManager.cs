using System;
using System.Collections.Generic;
using System.Reflection;
using FlooringProgram.Data;
using FlooringProgram.Models;
using Ninject;

namespace FlooringProgram.BLL
{
    public class OrderManager
    {
        
        private string _mode;
        private string _prod; 
        private ITransactionDataRepo<Order> _orderRepo;

        public OrderManager(string mode, string orderPath)
        {
            _mode = mode;
            _prod = orderPath;

            IKernel kernel = new StandardKernel(new Bindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _orderRepo = kernel.Get<ITransactionDataRepo<Order>>();

            //Regular injection 
            //if (mode == "File")
            //{
            //    _orderRepo = new OrderFileModeRepo(_prod);
            //}
            //else
            //{
            //    _orderRepo = new OrderMockModeRepo();
            //}
        }

        public Response<Order> GetAllOrder(string _date)
        {
            Response<Order> response = new Response<Order>();
            try
            {
                var listOrders = _orderRepo.GetAll(_date);

                if (listOrders != null)
                {
                    response.Data = listOrders;
                    response.Success = true; 
                }
                else
                {
                    response.Success = false;
                    response.Message = "ORDER NOT FOUND";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "ERROR: Please try again later. " + ex;
            }

            return response; 
        }

        public Response<Order> AddOrder(Order order, string _date)
        {
            Response<Order> response = new Response<Order>();
            Order newOrder = new Order();

            newOrder.CustomerName = order.CustomerName;
            newOrder.Area = order.Area;
            newOrder.ProductType = order.ProductType;
            newOrder.State = order.State;
            newOrder.CostPerSqFt = order.CostPerSqFt; 
            newOrder.LaborCostPerSqFt = order.LaborCostPerSqFt;
            newOrder.TaxRate = order.TaxRate;
            newOrder.TotalMaterialCost = order.TotalMaterialCost; 
            newOrder.TotalLaborCost = order.TotalLaborCost;
            newOrder.TotalTax = order.TotalTax;
            newOrder.Total = order.Total; 

            try
            {
                var responseOrder = _orderRepo.Add(newOrder,_date);
                response.Success = true;
                response.Message = "Account Added!";
                response.Data = new List<Order> { responseOrder }; 
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "ERROR, Please try again later"; 
            }
                                       
            return response;
        }

        public decimal TotalMaterialCostCal(decimal area, decimal MaterialCostPerSqFt)
        {
            return (decimal.Round(area * MaterialCostPerSqFt, 2));
        }

        public decimal TotalLaborCostCal(decimal area, decimal laborCostPerSqFt)
        {
            return (decimal.Round(area * laborCostPerSqFt, 2));
        }

        public decimal TotalTaxCal(decimal taxRate, decimal totalMaterialCost, decimal totalLaborCost)
        {
            return decimal.Round(taxRate / 100m * (totalMaterialCost + totalLaborCost), 2);
        }

        public decimal TotalCal(decimal tax, decimal totalMaterialCost, decimal totalLaborCost)
        {
            return decimal.Round(tax + totalMaterialCost + totalLaborCost,2); 
        }

        public Response<Order> EditOrder(Order order, string _date)
        {
            Response<Order> response = new Response<Order>();

            order.TotalMaterialCost = order.Area * order.CostPerSqFt;
            order.TotalLaborCost = order.Area * order.LaborCostPerSqFt;
            order.TotalTax = order.TaxRate / 100m * (order.TotalMaterialCost + order.TotalLaborCost);
            order.Total = order.TotalMaterialCost + order.TotalLaborCost + order.TotalTax;

            try
            {
                _orderRepo.Edit(order,_date);
                response.Success = true;
                response.Message = "Account Edited!";
                response.Data = new List<Order> {order};
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an unexpected error. Please try again later. " + ex;
            }

            return response;
        }

        public Response<Order> DeleteOrder(Order order, string _date)
        {
            Response<Order> response = new Response<Order>();

            try
            {
                _orderRepo.Remove(order, _date);
                response.Success = true;
                response.Message = "Account Edited!";
                response.Data = new List<Order> { order };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an unexpected error. Please try again later. " + ex;
            }

            return response;
         }

        public Response<Order> GetOneOrder(int id, string _date)
        {
            Response<Order> response = new Response<Order>();
            var orderFound = _orderRepo.GetOne(id, _date);

            try
            {
                if (orderFound != null)
                {
                    response.Success = true;
                    response.Data = new List<Order>() {orderFound};
                }
                else
                {
                    response.Success = false;
                    response.Message = "Order is not found.";
                }
            }
            catch (Exception ex)
            {
                response.Message = "There was an unexpected error. Please try again later. " + ex;
            }
            return response;
        }

    }
}