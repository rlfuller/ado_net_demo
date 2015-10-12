using System;
using System.Collections.Generic;

namespace FlooringProgram.Models
{
    public interface ITransactionDataRepo<T>
    {
        List<T> GetAll(string date);
        T GetOne(int id, string date);
        T Add(T entry, string date);
        void Edit(T entry, string date);
        void Remove(T entry, string date);
    }
}