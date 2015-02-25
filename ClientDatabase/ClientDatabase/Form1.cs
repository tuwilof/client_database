
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
        string connectionString = "Server = 127.0.0.1; Port = 5432; User Id = postgres; Password = 123456; Database = postgres;";
        string type = "null";
        Model model;
        Button button;
        TextBox[] textBoxes;
        ComboBox[] comboBoxes;
        Table table;
        Form f;
        string nameIndex;
        string operation;
        string stdId;

        public Form1()
        {
            InitializeComponent();
            model = new Model();
            Filling filling = new Filling();
            filling.run(ref model);

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
                dataGridView1.AutoResizeColumns();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.abiturient");
            type = "abiturient";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.distsiplina");
            type = "distsiplina";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.spetsialnost");
            type = "spetsialnost";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.kafedra");
            type = "kafedra";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.prepodavatel");
            type = "prepodavatel";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.ekzamen");
            type = "ekzamen";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            viewTable("select * from public.vedomost");
            type = "vedomost";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            operation = "add";
            fork();
        }

        private void fork()
        {
            if (type == "null")
            {
                MessageBox.Show("Вы не выбрали таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (type == "abiturient")
            {
                table = model.table[0];
                nameIndex = "Id_attestata";
                formTheShape();
            }
            else if (type == "distsiplina")
            {
                table = model.table[1];
                nameIndex = "Id_distsipliny";
                formTheShape();
            }
            else if (type == "spetsialnost")
            {
                table = model.table[2];
                nameIndex = "Id_spetsialnosti";
                formTheShape();
            }
            else if (type == "kafedra")
            {
                table = model.table[3];
                nameIndex = "Id_kafedry";
                formTheShape();
            }
            else if (type == "prepodavatel")
            {
                table = model.table[4];
                nameIndex = "Id_tabelnogo_nomer";
                formTheShape();
            }
            else if (type == "ekzamen")
            {
                table = model.table[5];
                nameIndex = "Id_ekzamena";
                formTheShape();
            }
            else if (type == "vedomost")
            {
                table = model.table[6];
                nameIndex = "Id_vedomosti";
                formTheShape();
            }
        }

        private void formTheShape()
        {
            if (operation == "delete")
            {
                delete();
                return;
            }
            string[] strArrBaselineData = new string[0];
            getBaselineData(ref strArrBaselineData);
            f = new Form();
            f.Width = 320;
            f.Text = table.nameRu;

            int i = 0;
            textBoxes = new TextBox[table.column.Count];
            comboBoxes = new ComboBox[table.column.Count];
            foreach (var column in table.column)
            {
                if (column.type == "string" || column.type == "int" || column.type == "date")
                {
                    Label l = new Label();
                    l.Text = column.nameRu;
                    l.Location = new Point(25, 25 + i * 23);
                    l.Width = 150;
                    f.Controls.Add(l);

                    textBoxes[i] = new TextBox();
                    textBoxes[i].Location = new Point(180, 25 + i * 23);
                    if (operation == "edit")
                    {
                        textBoxes[i].Text = "" + strArrBaselineData[i];
                        if (i == 0)
                            textBoxes[i].ReadOnly = true;
                    }
                    f.Controls.Add(textBoxes[i]);
                }
                else if (column.type == "foreignKey")
                {
                    Label l = new Label();
                    l.Text = column.nameRu;
                    l.Location = new Point(25, 25 + i * 23);
                    l.Width = 150;
                    f.Controls.Add(l);

                    comboBoxes[i] = new ComboBox();
                    comboBoxes[i].Location = new Point(180, 25 + i * 23);
                    fillingComboBox(ref comboBoxes[i], column.parentTable);
                    if (operation == "edit")
                    {
                        comboBoxes[i].Text = "" + strArrBaselineData[i];
                    }
                    f.Controls.Add(comboBoxes[i]);
                }
                i++;
            }

            button = new Button();
            button.Text = "Добавить";
            button.Location = new Point(180, 25 + i * 23);
            f.Controls.Add(button);
            button.Click += new EventHandler(button_Click);

            f.Height = 100 + i * 23;

            f.ShowDialog();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (operation == "add")
            {
                string str = "INSERT INTO " + table.nameEn + " VALUES (";
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    if (table.column[i].type == "string")
                    {
                        str += "'" + textBoxes[i].Text + "'";
                    }
                    else if (table.column[i].type == "int")
                    {
                        str += textBoxes[i].Text;
                    }
                    else if (table.column[i].type == "date")
                    {
                        str += "'" + textBoxes[i].Text + "'";
                    }
                    else if (table.column[i].type == "foreignKey")
                    {
                        str += comboBoxes[i].Text;
                    }
                    if (i != textBoxes.Length - 1)
                    {
                        str += ", ";
                    }
                }
                str += ");";
                sqlQuery(str);
                viewTable("select * from public." + table.nameEn + "");
                f.Close();
            }
            else if (operation == "edit")
            {
                string str = "UPDATE " + table.nameEn + " SET ";
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    if (table.column[i].type == "string")
                    {
                        str += table.column[i].nameEn + " = '" + textBoxes[i].Text + "'";
                    }
                    else if (table.column[i].type == "int")
                    {
                        str += table.column[i].nameEn + " = " + textBoxes[i].Text;
                    }
                    else if (table.column[i].type == "date")
                    {
                        str += table.column[i].nameEn + " = '" + textBoxes[i].Text + "'";
                    }
                    else if (table.column[i].type == "foreignKey")
                    {
                        str += table.column[i].nameEn + " = " + comboBoxes[i].Text;
                    }
                    if (i != textBoxes.Length - 1)
                    {
                        str += ", ";
                    }
                }
                str += " WHERE " + nameIndex + " = " + stdId + ";";
                sqlQuery(str);
                viewTable("select * from public." + table.nameEn + "");
                f.Close();
            }
        }

        private void sqlQuery(String strSQLQuery)
        {
            try
            {
                NpgsqlConnection connDB = new NpgsqlConnection(connectionString);
                connDB.Open();
                NpgsqlCommand delCommand = new NpgsqlCommand(strSQLQuery, connDB);

                delCommand.ExecuteNonQuery();
                connDB.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fillingComboBox(ref ComboBox comboBox, string tableName)
        {
            try
            {
                NpgsqlConnection connDB = new NpgsqlConnection(connectionString);
                connDB.Open();
                NpgsqlCommand command = new NpgsqlCommand("select * from public." + tableName, connDB);

                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    int fieldcount = dr.FieldCount;
                    string[] strArr = new string[fieldcount];
                    for (int i = 0; i < fieldcount; i++)
                    {
                        strArr[i] = dr[i].ToString();
                    }
                    comboBox.Items.Add(strArr[0]);
                }
                connDB.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            operation = "edit";
            fork();
        }

        private void getBaselineData(ref string[] strArrBaselineData)
        {
            try
            {
                stdId = dataGridView1.CurrentCell.Value.ToString();
                int id = Int32.Parse(stdId);
                NpgsqlConnection connDB = new NpgsqlConnection(connectionString);
                connDB.Open();
                NpgsqlCommand command = new NpgsqlCommand("select * from public." + type + " where (" + nameIndex + "='" + stdId + "')", connDB);
                NpgsqlDataReader dr = command.ExecuteReader();
                dr.Read();
                int fieldcount = dr.FieldCount;
                strArrBaselineData = new string[fieldcount];
                for (int i = 0; i < fieldcount; i++)
                {
                    strArrBaselineData[i] = dr[i].ToString();
                }
                connDB.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("" + ex, "Ошибка выделения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            operation = "delete";
            fork();
        }

        private void delete()
        {
            try
            {
                stdId = dataGridView1.CurrentCell.Value.ToString();
                int id = Int32.Parse(stdId);
                NpgsqlConnection connDB = new NpgsqlConnection(connectionString);
                connDB.Open();
                string str = "DELETE FROM " + type + " WHERE " + nameIndex + " = " + stdId + ";";
                sqlQuery(str);
                connDB.Close();
                viewTable("select * from public." + type + "");
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("" + ex, "Ошибка выделения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
