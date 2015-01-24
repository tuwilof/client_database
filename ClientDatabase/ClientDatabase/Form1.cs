using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            connect();
        }

        private void connect()
        {
            string connectionString = "Server = 127.0.0.1; Port = 5433; User Id=postgres; Password = 123456; Database = postgres;";
            NpgsqlConnection db = new NpgsqlConnection(connectionString);
            db.Open();
        }
    }
}
