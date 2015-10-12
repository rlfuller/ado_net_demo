using System.Collections.Generic;
using System.Linq;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class ProductMockModeRepo: ILookUpDataRepo<Product>
    {
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>
            {
                {new Product() {ProdType = "Wood", CostPerSqFt = 5.50m, LaborCostPerSqFt = 8.75m}},
                {new Product() {ProdType = "Laminate", CostPerSqFt = 3.50m, LaborCostPerSqFt = 7.75m}},
                {new Product() {ProdType = "Slate", CostPerSqFt = 8.50m, LaborCostPerSqFt = 20.75m}}
            };
            return products;
        }

        public Product GetOne(string input)
        {
            List<Product> products = GetAll();

            var result = products.FirstOrDefault(p => p.ProdType == input);

            return result;

        }
    }
}