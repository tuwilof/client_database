
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
        string connectionString = "Server = 127.0.0.1; Port = 5432; User Id=postgres; Password = 123456; Database = postgres;";

        public Form1()
        {
            InitializeComponent();
        }

        private void viewTable(string sqlQuery)
        {
            dataGridView1.Columns.Clear();
            try
            {
                NpgsqlConnection connDB = new NpgsqlConnection(connectionString);
                connDB.Open();
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connDB);

                NpgsqlDataReader dr = command.ExecuteReader();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    dataGridView1.Columns.Add(col);
                    col.Name = dr.GetName(i);
                }

                while (dr.Read())
                {
                    int fieldcount = dr.FieldCount;
                    string[] strArr = new string[fieldcount];
                    for (int i = 0; i < fieldcount; i++)
                    {
                        strArr[i] = dr[i].ToString();
                    }
                    dataGridView1.Rows.Add(strArr);
                }
                connDB.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(""+ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.abiturient");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.distsiplina");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.spetsialnost");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.kafedra");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.prepodavatel");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.ekzamen");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.vedomost");
        }
    }
}
