using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class Product
    {
        public string ProdType { get; set; }
        public decimal CostPerSqFt { get; set; }
        public decimal LaborCostPerSqFt { get; set; }
    }
}
