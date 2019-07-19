using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_dot_core.Classes
{
    public class Configurations
    {
        public DataBaseConfiguration DataBase { get; set; }
    }

    public class DataBaseConfiguration
    {
        public string DatabaseName { get; set; }
        public string ServerHost { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string ConnectionString => $"Host={ServerHost};Database={DatabaseName};Username={Username};Password={Password}";
    }
}
