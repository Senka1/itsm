using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace itsm1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'itsmDataSet4.RequestDescription' table. You can move, or remove it, as needed.
            this.requestDescriptionTableAdapter.Fill(this.itsmDataSet4.RequestDescription);
            // TODO: This line of code loads data into the 'itsmDataSet4.Reports' table. You can move, or remove it, as needed.
            this.reportsTableAdapter1.Fill(this.itsmDataSet4.Reports);
            this.requestTableAdapter1.Fill(this.itsmDataSet4.Request);
            this.reportsTableAdapter.Fill(this.itsmDataSet3.Reports);
            this.personnelTableAdapter.Fill(this.itsmDataSet2.Personnel);
            this.requestTableAdapter.Fill(this.itsmDataSet1.Request);

        }
                                     //кнопка добавить заявка
        public void button1_Click(object sender, EventArgs e)
        {
            string тип = comboBox1.SelectedItem.ToString();
            string статус = comboBox2.SelectedItem.ToString();
            string описание = textBox1.Text;
            int персонал = Convert.ToInt32(textBox2.Text);

            string query = "INSERT INTO Request (Тип, Статус, Описание, ПерсоналID) VALUES (@Тип, @Статус, @Описание, @Персонал)";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Тип", тип);
                    command.Parameters.AddWithValue("@Статус", статус);
                    command.Parameters.AddWithValue("@Описание", описание);
                    command.Parameters.AddWithValue("@Персонал", персонал);

                    command.ExecuteNonQuery();
                }
            }

            textBox1.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            string query = "DELETE FROM Request WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }

            textBox1.Clear() ;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string тип = comboBox1.SelectedItem.ToString();
            string статус = comboBox2.SelectedItem.ToString();
            string описание = textBox1.Text;

            string query = "UPDATE Request SET Тип = @Тип, Статус = @Статус, Описание = @Описание WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Тип", тип);
                    command.Parameters.AddWithValue("@Статус", статус);
                    command.Parameters.AddWithValue("@Описание", описание);

                    command.ExecuteNonQuery();
                }
            }
            textBox1 .Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            updateDGV();
        }
        public void updateDGV()
        {
            string query = "SELECT * FROM Request";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }
        
            
                                         //кнопка добавить персонал
        private void button8_Click(object sender, EventArgs e)
        {
            string имя = textBox3.Text;
            string должность = textBox4.Text;
            string логин = textBox5.Text;
            string роль = comboBox3.SelectedItem.ToString();

            string query = "INSERT INTO Personnel (Имя, Должность, Логин, Роль) VALUES (@Имя, @Должность, @Логин, @Роль)";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Имя", имя);
                    command.Parameters.AddWithValue("@Должность", должность);
                    command.Parameters.AddWithValue("@Логин", логин);
                    command.Parameters.AddWithValue("@Роль", роль);

                    command.ExecuteNonQuery();
                }
            }

        }
                        //редактирование персонала
        private void button7_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string имя = textBox3.Text;
            string должность = textBox4.Text;
            string логин = textBox5.Text;
            string роль = comboBox3.SelectedItem.ToString();

            string query = "UPDATE Personnel SET Имя = @Имя, Должность = @Должность, Логин = @Логин, Роль = @Роль WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Имя", имя);
                    command.Parameters.AddWithValue("@Должность", должность);
                    command.Parameters.AddWithValue("@Логин", логин);
                    command.Parameters.AddWithValue("@Роль", роль);

                    command.ExecuteNonQuery();
                }
            }
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
        public void updateDGV2()
        {
            string query = "SELECT * FROM Personnel";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView2.DataSource = dataTable;
                }
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            updateDGV2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);

            string query = "DELETE FROM Personnel WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }

            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
        public void updateDGV3()
        {
            string query = "SELECT * FROM Reports";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView3.DataSource = dataTable;
                }
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            updateDGV3();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();
                string query = "INSERT INTO Reports (Дата, Тип, Описание, ЗаявкаID) VALUES (@Дата, @Тип, @Описание, @ЗаявкаID)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Дата", textBox6.Text);
                command.Parameters.AddWithValue("@Тип", textBox7.Text);
                command.Parameters.AddWithValue("@Описание", textBox8.Text);
                command.Parameters.AddWithValue("@ЗаявкаID", textBox9.Text);

                command.ExecuteNonQuery();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();
                string query = "UPDATE Reports SET Дата = @Дата, Тип = @Тип, Описание = @Описание, ЗаявкаID = @ЗаявкаID WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Дата", textBox6.Text);
                command.Parameters.AddWithValue("@Тип", textBox7.Text);
                command.Parameters.AddWithValue("@Описание", textBox8.Text);
                command.Parameters.AddWithValue("@ЗаявкаID", textBox9.Text);
                command.Parameters.AddWithValue("@ID", id);

                command.ExecuteNonQuery();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();
                string query = "DELETE FROM Reports WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                command.ExecuteNonQuery();
            }
        }
        public void updateDGV4()
        {
            string query = "SELECT * FROM RequestDescription";

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView4.DataSource = dataTable;
                }
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            updateDGV4();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO RequestDescription (ЗаявкаID, Приоритет, Детальное_описание) VALUES (@ЗаявкаID, @Приоритет, @Детальное_описание)", connection);

                cmd.Parameters.AddWithValue("@ЗаявкаID", textBox13.Text);
                cmd.Parameters.AddWithValue("@Приоритет", comboBox4.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Детальное_описание", textBox11.Text);

                cmd.ExecuteNonQuery();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE RequestDescription SET ЗаявкаID = @ЗаявкаID, Приоритет = @Приоритет, Детальное_описание = @Детальное_описание WHERE ID = @ID", connection);

                cmd.Parameters.AddWithValue("@ЗаявкаID", textBox13.Text);
                cmd.Parameters.AddWithValue("@Приоритет", comboBox4.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Детальное_описание", textBox11.Text);
                cmd.Parameters.AddWithValue("@ID", selectedRow.Cells["ID"].Value);

                cmd.ExecuteNonQuery();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-8VNN0U4OJ82\MSSQLSERVERE;Initial Catalog=itsm;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM RequestDescription WHERE ID = @ID", connection);
                cmd.Parameters.AddWithValue("@ID", selectedRow.Cells["ID"].Value);
                cmd.ExecuteNonQuery();
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
