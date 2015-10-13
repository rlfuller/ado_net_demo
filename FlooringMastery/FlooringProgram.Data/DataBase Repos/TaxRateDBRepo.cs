using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.Config;
using FlooringProgram.Models;

namespace FlooringProgram.Data.DataBase_Repos
{
    public class TaxRateDBRepo : ILookUpDataRepo<State>
    {
        public List<State> GetAll()
        {
            
            List<State> states = new List<State>();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from State";

                cmd.Connection = cn;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        states.Add(PopulateFromDataReader(dr));
                    }
                }
            }

            return states;
        }

        public State GetOne(string input)
        {
            
            State state = new State();

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "select * from State WHERE StateAbbrev = @StateAbbrev";

                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@StateAbbrev", input);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        state = PopulateFromDataReader(dr);
                    }
                }

            }

            return state;
        }

        private State PopulateFromDataReader(SqlDataReader dr)
        {

            
            State state = new State();

            state.StateAbbrev = dr["StateAbbrev"].ToString();
            state.StateName = dr["StateName"].ToString();
            state.TaxRate = (decimal)dr["TaxRate"];
            
            return state;
        }
    }
}
