using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace itsm1
{
    public partial class Login : Form
    {
        DataBase database = new DataBase();
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser= textBox1.Text;
            var passUser= textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id, login, password from login where login= '{loginUser}' and password='{passUser}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if(table.Rows.Count==1)
            {
                MessageBox.Show("Вы вошли!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information );
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
            }
            else
                MessageBox.Show("Аккаунта не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

    }
}
