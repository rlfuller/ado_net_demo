using System.Linq;
using System.Reflection;
using FlooringProgram.Data;
using FlooringProgram.Models;
using Ninject;


namespace FlooringProgram.BLL
{
    public class StateManager
    {
        private ILookUpDataRepo<State> _stateRepo;

        public StateManager(string mode, string statePath)
        {
            IKernel kernel = new StandardKernel(new Bindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _stateRepo = kernel.Get<ILookUpDataRepo<State>>();

            //if (mode == "File")
            //{
            //    _stateRepo = new TaxRateFileModeRepo(statePath);
            //}
            //else
            //{
            //    _stateRepo = new TaxRateMockModeRepo();
            //}
        }

        public decimal getTaxRate(string stateAbbrev)
        {
            return _stateRepo.GetOne(stateAbbrev).TaxRate;
        }

        public bool isState(string stateAbbrev)
        {
            return _stateRepo.GetAll().Any(p => p.StateAbbrev == stateAbbrev);
        }
    }
}