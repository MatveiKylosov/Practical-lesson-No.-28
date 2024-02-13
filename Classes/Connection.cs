using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Practical_lesson_No._28.Classes
{
    public class Connection
    {
        static readonly string connectionString = "Server=127.0.0.1;Database=СomputerСlub;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
