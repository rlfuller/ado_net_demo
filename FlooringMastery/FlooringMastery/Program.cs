using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.UI.WorkFlow;
using System.Configuration; 

namespace FlooringProgram.UI
{
    public class Program
    {
        public static readonly string Mode = ConfigurationManager.AppSettings["Mode"];
        public static readonly string ProductPath = ConfigurationManager.AppSettings["Products"];
        public static readonly string OrderPath = ConfigurationManager.AppSettings["Orders"];
        public static readonly string StatePath = ConfigurationManager.AppSettings["States"];

        static void Main(string[] args)
        {
            var menu = new MainMenu();
            menu.Execute();
            
        }
    }
}
