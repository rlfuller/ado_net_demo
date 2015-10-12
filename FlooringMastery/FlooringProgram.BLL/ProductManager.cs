using System.Linq;
using System.Reflection;
using FlooringProgram.Data;
using FlooringProgram.Models;
using Ninject;


namespace FlooringProgram.BLL
{
    public class ProductManager
    {
        private ILookUpDataRepo<Product> _prodRepo;

        public ProductManager(string mode, string productPath)
        {
            IKernel kernel = new StandardKernel(new Bindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _prodRepo = kernel.Get<ILookUpDataRepo<Product>>();

            //if (mode == "File")
            //{
            //    _prodRepo = new ProductFileModeRepo(productPath);
            //}
            //else
            //{
            //    _prodRepo = new ProductMockModeRepo();
            //}
        }

        public decimal GetCostPerSqFt(string productName)
        {
            var costPerSqft =  _prodRepo.GetOne(productName).CostPerSqFt;
            return costPerSqft; 
        }

        public decimal GetLaborCostPerSqFt(string productName)
        {
            return _prodRepo.GetOne(productName).LaborCostPerSqFt; 
        }

        public bool IsProductName(string productName)
        {
            return _prodRepo.GetAll().Any(p => p.ProdType == productName);
        }
    }
}