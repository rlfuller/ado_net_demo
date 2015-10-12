using System;
using System.Collections.Generic;

namespace FlooringProgram.Models
{
    public interface ILookUpDataRepo<T>
    {
        List<T> GetAll();
        T GetOne(string input);
    }
}