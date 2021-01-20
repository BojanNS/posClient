using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace POSCashSystem
{
    public class SqlHelper
    {
        MySqlConnection cn;
        public SqlHelper(string connectionString)
        {
            cn = new MySqlConnection(connectionString);
        }
        public bool IsConnection
        {
            get
            {
                if (cn.State == System.Data.ConnectionState.Closed)
                    cn.Open();
                return true;
                
            }
        }
        public void Close(MySqlConnection con)
        {
            con.Close();
        }
    }
}
