namespace FlooringProgram.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string State { get; set; }
        public decimal TaxRate { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSqFt { get; set; }
        public decimal LaborCostPerSqFt { get; set; }
        public decimal TotalMaterialCost { get; set; }
        public decimal TotalLaborCost { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }
       
    }
}