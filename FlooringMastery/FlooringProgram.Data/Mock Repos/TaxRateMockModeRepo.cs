using System;
using System.Collections.Generic;
using System.Linq;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class TaxRateMockModeRepo : ILookUpDataRepo<State>
    {
        public List<State> GetAll()
        {
            List<State> TaxRate = new List<State>
            {
                {new State() {StateAbbrev = "NY", StateName = "NEW YORK", TaxRate = 0.075m}},
                {new State() {StateAbbrev = "OH", StateName = "OHIO", TaxRate = 0.078m}},
                {new State() {StateAbbrev = "NC", StateName = "NORTH CAROLINA", TaxRate = 0.065m}}
            };
            return TaxRate;
        }

        public State GetOne(string input)
        {
            List <State> states = GetAll();
            var result = states.FirstOrDefault(s => s.StateAbbrev == input);
            return result;
        }
    }
}