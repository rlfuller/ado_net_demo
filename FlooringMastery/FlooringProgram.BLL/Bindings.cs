using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data;
using FlooringProgram.Models;
using Ninject.Modules;
using System.Configuration; 


namespace FlooringProgram.BLL
{
    class Bindings : NinjectModule
    {

        public override void Load()
        {
            if (ConfigurationManager.AppSettings["Mode"] == "File")
            {
                Bind<ILookUpDataRepo<Product>>().To<ProductFileModeRepo>().WithConstructorArgument("path", ConfigurationManager.AppSettings["Products"]);
                Bind<ITransactionDataRepo<Order>>().To<OrderFileModeRepo>().WithConstructorArgument("path", ConfigurationManager.AppSettings["Orders"]);
                Bind<ILookUpDataRepo<State>>().To<TaxRateFileModeRepo>().WithConstructorArgument("path", ConfigurationManager.AppSettings["States"]);
            }
            else
            {
                Bind<ITransactionDataRepo<Order>>().To<OrderMockModeRepo>();
                Bind<ILookUpDataRepo<Product>>().To<ProductMockModeRepo>();
                Bind<ILookUpDataRepo<State>>().To<TaxRateMockModeRepo>();
            }
        }
    }
}
