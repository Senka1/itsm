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
    public partial class AddEditPersonForm : Form
    {
        private PersonnelForm11 parentForm;
        private int? personID;
        private TextBox txtID;
        private TextBox txtName;
        private TextBox txtPosition;
        private TextBox txtLogin;
        private TextBox txtPassword; // Добавляем поле для пароля
        private ComboBox cmbRole;
        private Button btnSave;
        private Button btnCancel;

        private string connectionString = "Data Source=WIN-8VNN0U4OJ82\\MSSQLSERVERE;Initial Catalog=ITServiceManagement;Integrated Security=True";
        private PersonnelForm personnelForm;
        private object value;

        public AddEditPersonForm(PersonnelForm11 parentForm, int? personID)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.personID = personID;

            InitializeComponents();

            if (personID.HasValue)
            {
                LoadPersonDetails();
            }
        }

        public AddEditPersonForm(PersonnelForm personnelForm, object value)
        {
            this.personnelForm = personnelForm;
            this.value = value;
        }

        private void InitializeComponents()
        {
            // Инициализация компонентов формы
            txtID = new TextBox();
            txtID.Location = new System.Drawing.Point(30, 5);
            txtID.Size = new System.Drawing.Size(100, 20);
            txtID.Enabled = true; // Заблокируем редактирование ID
            Controls.Add(txtID); 
            txtName = new TextBox();
            txtName.Location = new System.Drawing.Point(30, 30);
            txtName.Size = new System.Drawing.Size(300, 20);

            txtPosition = new TextBox();
            txtPosition.Location = new System.Drawing.Point(30, 70);
            txtPosition.Size = new System.Drawing.Size(300, 20);

            txtLogin = new TextBox();
            txtLogin.Location = new System.Drawing.Point(30, 110);
            txtLogin.Size = new System.Drawing.Size(150, 20);

            txtPassword = new TextBox();
            txtPassword.Location = new System.Drawing.Point(30, 150);
            txtPassword.Size = new System.Drawing.Size(150, 20);
            txtPassword.PasswordChar = '*'; // Скрываем ввод пароля

            cmbRole = new ComboBox();
            cmbRole.Items.AddRange(new object[] { "Администратор", "Сотрудник" });
            cmbRole.Location = new System.Drawing.Point(30, 190);
            cmbRole.Size = new System.Drawing.Size(150, 20);

            btnSave = new Button();
            btnSave.Text = "Сохранить";
            btnSave.Location = new System.Drawing.Point(30, 230);
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Location = new System.Drawing.Point(150, 230);
            btnCancel.Click += BtnCancel_Click;

            Controls.Add(txtName);
            Controls.Add(txtPosition);
            Controls.Add(txtLogin);
            Controls.Add(txtPassword);
            Controls.Add(cmbRole);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            // Другие настройки формы
            Text = "Добавление/редактирование сотрудника";
            Size = new System.Drawing.Size(400, 300);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SavePerson();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadPersonDetails()
        {
            // Загрузка деталей сотрудника для редактирования
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID, Name, Position, Login, Role FROM Personnel WHERE ID = @PersonID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtID.Text = reader["ID"].ToString();
                            txtName.Text = reader["Name"].ToString();
                            txtPosition.Text = reader["Position"].ToString();
                            txtLogin.Text = reader["Login"].ToString();
                            cmbRole.SelectedItem = reader["Role"].ToString();
                        }
                    }
                }
            }
        }
        private void SavePerson()
        {
            // Сохранение или добавление сотрудника в базу данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (personID.HasValue)
                {
                    // Редактирование существующего сотрудника
                    string query = "UPDATE Personnel SET Name = @Name, Position = @Position, Login = @Login, Password = @Password, Role = @Role WHERE ID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", txtID.Text);
                        command.Parameters.AddWithValue("@Name", txtName.Text);
                        command.Parameters.AddWithValue("@Position", txtPosition.Text);
                        command.Parameters.AddWithValue("@Login", txtLogin.Text);
                        command.Parameters.AddWithValue("@Password", txtPassword.Text); // Помните о безопасности пароля
                        command.Parameters.AddWithValue("@Role", cmbRole.SelectedItem?.ToString() ?? DBNull.Value.ToString());

                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Добавление нового сотрудника
                    string query = "INSERT INTO Personnel (Name, Position, Login, Password, Role) VALUES (@Name, @Position, @Login, @Password, @Role)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@PersonID", txtID.Text);
                        command.Parameters.AddWithValue("@Name", txtName.Text);
                        command.Parameters.AddWithValue("@Position", txtPosition.Text);
                        command.Parameters.AddWithValue("@Login", txtLogin.Text);
                        command.Parameters.AddWithValue("@Password", txtPassword.Text); // Помните о безопасности пароля
                        command.Parameters.AddWithValue("@Role", cmbRole.SelectedItem?.ToString() ?? DBNull.Value.ToString());

                        command.ExecuteNonQuery();
                    }
                }
            }

            parentForm.LoadPersonnel(); // Обновляем список сотрудников на родительской форме
            Close();
        }

        private void AddEditPersonForm_Load(object sender, EventArgs e)
        {

        }
    }
}
