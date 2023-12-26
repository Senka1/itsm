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

namespace ITServiceManagement
{
    public partial class RegistrationForm : Form
    {
        private Label lblLogin;
        private Label lblPassword;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnRegister;
        private Button btnBackToLoginForm;
        private string connectionString = "Data Source=WIN-8VNN0U4OJ82\\MSSQLSERVERE;Initial Catalog=ITServiceManagement;Integrated Security=True";

        public RegistrationForm()
        {
            InitializeComponent(); InitializeComponents();
        }
        private void InitializeComponents()
        {
            // Инициализация компонентов формы

            lblLogin = new Label();
            lblLogin.Text = "Логин:";
            lblLogin.Location = new System.Drawing.Point(30, 30);

            lblPassword = new Label();
            lblPassword.Text = "Пароль:";
            lblPassword.Location = new System.Drawing.Point(30, 60);

            txtLogin = new TextBox();
            txtLogin.Location = new System.Drawing.Point(120, 30);
            txtLogin.Size = new System.Drawing.Size(150, 20);

            txtPassword = new TextBox();
            txtPassword.Location = new System.Drawing.Point(120, 60);
            txtPassword.Size = new System.Drawing.Size(150, 20);
            txtPassword.PasswordChar = '*';

            btnRegister = new Button();
            btnRegister.Text = "Зарегистрироваться";
            btnRegister.Location = new System.Drawing.Point(120, 90);
            btnRegister.Size = new System.Drawing.Size(130, 23);
            btnRegister.Click += BtnRegister_Click;
            btnBackToLoginForm = new Button(); // Инициализация новой кнопки
            btnBackToLoginForm.Text = "Войти";
            btnBackToLoginForm.Location = new System.Drawing.Point(50, 120);
            btnBackToLoginForm.Click += BtnBackToLoginForm_Click;

            Controls.Add(lblLogin);
            Controls.Add(lblPassword);
            Controls.Add(txtLogin);
            Controls.Add(txtPassword);
            Controls.Add(btnRegister);
            Controls.Add(btnBackToLoginForm);

            // Другие настройки формы
            Text = "Форма регистрации";
            Size = new System.Drawing.Size(300, 180);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Обработка нажатия кнопки регистрации
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            if (IsLoginAvailable(login))
            {
                // Регистрация нового пользователя
                RegisterUser(login, password);
                MessageBox.Show("Регистрация успешна! Теперь вы можете войти в систему.");
                Close();
            }
            else
            {
                MessageBox.Show("Указанный логин уже занят. Пожалуйста, выберите другой логин.");
            }
        }
        private bool IsLoginAvailable(string login)
        {
            // Проверка доступности логина
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Reg WHERE login = @Login";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);

                    int count = (int)command.ExecuteScalar();
                    return count == 0;
                }
            }
        }
        private void RegisterUser(string login, string password)
        {
            // Регистрация нового пользователя в базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Reg (login, password) VALUES (@Login, @Password)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    command.ExecuteNonQuery();
                }
            }
        }
        private void BtnBackToLoginForm_Click(object sender, EventArgs e)
        {
            // Возврат на форму авторизации
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            Close();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
