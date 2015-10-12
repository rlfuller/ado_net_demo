using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class ProductFileModeRepo : ILookUpDataRepo<Product>

    {
        private string[] _reader;
        private string _path;

        public ProductFileModeRepo(string path)
        {
            _path = path; 

        }

       public List<Product> GetAll()
        {


            List<Product> productsFound = new List<Product>();

            try
            {
                foreach (string file in Directory.EnumerateFiles(_path, "*.txt"))
                {
                    _reader = File.ReadAllLines(file);

                    for (int i = 1; i < _reader.Length; i++)
                    {
                        var columns = _reader[i].Split(',');
                        Product product = new Product();
                        product.ProdType = (columns[0]);
                        product.CostPerSqFt = decimal.Parse(columns[1]);
                        product.LaborCostPerSqFt = decimal.Parse(columns[2]);

                        productsFound.Add(product);
                    }
                }         
            }
            catch (Exception productNotFound)
            {
                Console.WriteLine($"ERROR!!!! {productNotFound.Message}");
            }
            return productsFound;
        }

        public Product GetOne(string input)
        {
            List<Product> products = GetAll();
            Product product = products.FirstOrDefault(p => p.ProdType == input);

            return product;
        }
    }
}