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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataBase database = new DataBase();
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;
            string querystring = $"insert into login(login, password) values('{loginUser}', '{passUser}')";
                SqlCommand command = new SqlCommand(querystring, database.getConnection());
            database.openConnection();
            if(command.ExecuteNonQuery()==1)
            {
                MessageBox.Show("Аккаунт создан", "Успех");
                Login login = new Login();
                this.Hide();
                login.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан");
            }
            database.closeConnection();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
