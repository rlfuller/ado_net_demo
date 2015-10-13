using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.Config;
using FlooringProgram.Models;

namespace FlooringProgram.Data.DataBase_Repos
{
    public class ProductDBRepo : ILookUpDataRepo<Product>
    {
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from Product";

                cmd.Connection = cn;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        products.Add(PopulateFromDataReader(dr));
                    }
                }
            }

            return products;
        }

        public Product GetOne(string input)
        {
            
            Product product = new Product();

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "select * from Product WHERE ProdType = @ProdType";

                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProdType", input);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        product = PopulateFromDataReader(dr);
                    }
                }

            }

            return product;
            }

        private Product PopulateFromDataReader(SqlDataReader dr)
        {
            
            Product product = new Product();

            product.ProdType = dr["ProdType"].ToString();
            product.CostPerSqFt = (decimal)dr["CostPerSqFt"];
            product.LaborCostPerSqFt = (decimal)dr["LaborCostPerSqFt"];
            
            return product;
        }


    }
}
