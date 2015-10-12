using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class TaxRateFileModeRepo : ILookUpDataRepo<State>
    {

        private string[] _contents;
        private string _path;

        public TaxRateFileModeRepo(string path)
        {
            _path = path;

        }
        public List<State> GetAll()
        {

            List<State> statesFound = new List<State>();

            try
            {
                foreach (string file in Directory.EnumerateFiles(_path, "*.txt"))
                {
                    _contents = File.ReadAllLines(file);
                }

                for (int i = 1; i < _contents.Length; i++)
                {
                    var columns = _contents[i].Split(',');
                    State state = new State();
                    state.StateAbbrev = columns[0];
                    state.StateName = columns[1];
                    state.TaxRate = decimal.Parse(columns[2]);

                    statesFound.Add(state);
                }
            }
            catch (Exception productNotFound)
            {
                Console.WriteLine($"ERROR!!!! {productNotFound.Message}");
            }
            return statesFound;
        }

        public State GetOne(string input)
        {
            List<State> states = GetAll();
            State state = states.FirstOrDefault(s => s.StateAbbrev == input);

            return state;
        }
    }
}