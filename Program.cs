using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlAdo
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu _menu = new Menu();
            _menu.ShowMenu();
        }
    }
}

