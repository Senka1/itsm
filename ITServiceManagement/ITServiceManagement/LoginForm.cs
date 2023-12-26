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

namespace ITServiceManagement
{
    public partial class LoginForm : Form
    {
        private string connectionString = "Data Source=WIN-8VNN0U4OJ82\\MSSQLSERVERE;Initial Catalog=ITServiceManagement;Integrated Security=True";

        public LoginForm()
        {
            InitializeComponent();
            InitializeComponents();
        }
        private void InitializeComponents()
        {
            
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            if (IsValidUser(login, password))
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль. Пожалуйста, повторите попытку.");
            }
        }
        private bool IsValidUser(string login, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Reg WHERE login = @Login AND password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void BtnOpenRegistrationForm_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Show();
            this.Hide();
        }
    }
}
