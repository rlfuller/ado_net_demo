using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.Config;
using FlooringProgram.Models;
using System.Data.Linq;

namespace FlooringProgram.Data.DataBase_Repos
{
    public class OrderDBRepo : ITransactionDataRepo<Order>
    {
        public Order Add(Order entry, string date)
        {
            int numRows;
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Insert into [Order]" +
                                  "(CustomerName, State, TaxRate,ProductType, Area,CostPerSqFt," +
                                  "LaborCostPerSqFt, TotalMaterialCost, TotalLaborCost,TotalTax,Total)" +
                                  "Values(@CustomerName, @State,@TaxRate,@ProductType,@Area,@CostPerSqFt," +
                                  "@LaborCostPerSqFt,@TotalMaterialCost,@TotalLaborCost,@TotalTax,@Total)";

                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CustomerName", entry.CustomerName);
                cmd.Parameters.AddWithValue("@State", entry.State);
                cmd.Parameters.AddWithValue("@TaxRate", entry.TaxRate);
                cmd.Parameters.AddWithValue("@ProductType", entry.ProductType);
                cmd.Parameters.AddWithValue("@Area", entry.Area);
                cmd.Parameters.AddWithValue("@CostPerSqFt", entry.CostPerSqFt);
                cmd.Parameters.AddWithValue("@LaborCostPerSqFt", entry.LaborCostPerSqFt);
                cmd.Parameters.AddWithValue("@TotalMaterialCost", entry.TotalMaterialCost);
                cmd.Parameters.AddWithValue("@TotalLaborCost", entry.TotalLaborCost);
                cmd.Parameters.AddWithValue("@TotalTax", entry.TotalTax);
                cmd.Parameters.AddWithValue("@Total", entry.Total);

                cn.Open();
                numRows = cmd.ExecuteNonQuery();

                int orderNum = GetMaxOrderNumber();

                entry.OrderNumber = orderNum;
            }

            return entry;

        }

        public void Edit(Order entry, string date)
        {
            int numRows;
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update [Order]" +
                                  "set CustomerName = @CustomerName, State = @State, TaxRate = @TaxRate," +
                                  "ProductType = @ProductType, Area = @Area, CostPerSqFt = @CostPerSqFt" +
                                  "LaborCostPerSqFt = @LaborCostPerSqFt, TotalMaterialCost = @TotalMaterialCost," +
                                  "TotalLaborCost = @TotalLaborCost, TotalTax = @TotalTax,Total = @Total" +
                                  "where CustomerNumber = @CustomerNumber and OrderDate = @OrderDate";

                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@OrderNumber", entry.OrderNumber);
                cmd.Parameters.AddWithValue("@CustomerName", entry.CustomerName);
                cmd.Parameters.AddWithValue("@State", entry.State);
                cmd.Parameters.AddWithValue("@TaxRate", entry.TaxRate);
                cmd.Parameters.AddWithValue("@ProductType", entry.ProductType);
                cmd.Parameters.AddWithValue("@Area", entry.Area);
                cmd.Parameters.AddWithValue("@CostPerSqFt", entry.CostPerSqFt);
                cmd.Parameters.AddWithValue("@LaborCostPerSqFt", entry.LaborCostPerSqFt);
                cmd.Parameters.AddWithValue("@TotalMaterialCost", entry.TotalMaterialCost);
                cmd.Parameters.AddWithValue("@TotalLaborCost", entry.TotalLaborCost);
                cmd.Parameters.AddWithValue("@TotalTax", entry.TotalTax);
                cmd.Parameters.AddWithValue("@Total", entry.Total);
                cmd.Parameters.AddWithValue("@OrderDate", entry.OrderDate);

                cn.Open();
                numRows = cmd.ExecuteNonQuery();
           }

        }

        public List<Order> GetAll(string date)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                DateTime myDate = new DateTime();
                myDate = Convert.ToDateTime(date);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from [Order] where OrderDate = @OrderDate";
                
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@OrderDate", myDate);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        orders.Add(PopulateFromDataReader(dr));
                    }
                }
            }

            return orders;
        }

        public Order GetOne(int id, string date)
        {
                Order order = new Order();

                using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "select * from [Order] where OrderDate = @OrderDate and OrderNumber = @OrderNumber";

                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@OrderDate", date);
                    cmd.Parameters.AddWithValue("@OrderNumber", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            order = PopulateFromDataReader(dr);
                        }
                    }
                }

                return order;
            }
        

        public void Remove(Order entry, string date)
        {
            int numRows;
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Delete from [Order]" +
                                  "where CustomerNumber = @CustomerNumber and OrderDate = @OrderDate";

                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@OrderNumber", entry.OrderNumber);
                cmd.Parameters.AddWithValue("@OrderDate", entry.OrderDate);

                cn.Open();
                numRows = cmd.ExecuteNonQuery();
            }
        }

        private Order PopulateFromDataReader(SqlDataReader dr)
        {
            
            Order order = new Order();
            DateTime orderdate = new DateTime();

            order.OrderNumber = (int)dr["OrderNumber"];
            order.State = dr["State"].ToString();
            order.ProductType = dr["ProductType"].ToString();
            order.Area = (decimal) dr["Area"];
            order.CostPerSqFt = (decimal) dr["CostPerSqFt"];
            order.LaborCostPerSqFt = (decimal) dr["LaborCostPerSqFt"];
            order.TaxRate = (decimal) dr["TaxRate"];
            order.OrderDate = (DateTime) dr["OrderDate"];
           
           //order.OrderDate = dr.GetDateTime(12);

            order.CustomerName = dr["CustomerName"].ToString();
            order.TotalLaborCost = (decimal) dr["TotalLaborCost"];
            order.TotalMaterialCost = (decimal) dr["TotalMaterialCost"];
            order.TotalTax = (decimal) dr["TotalTax"];
            order.Total = (decimal) dr["Total"];
           

            return order;
        }

        private int GetMaxOrderNumber()
        {
            int maxID = 0;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "GetMaxOrderNumber";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        maxID = (int) dr["MaxOrderNumber"];
                    }
                }
            }

            return maxID;
        }
        
    }
}
