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
    public partial class AddEditRequestForm : Form
    {
        private RequestsForm parentForm;
        private int? requestID;

        private TextBox txtID; // Новое поле для ввода ID
        private TextBox txtDescription;
        private ComboBox cmbStatus;
        private Button btnSave;
        private Button btnCancel;

        private string connectionString = "Data Source=WIN-8VNN0U4OJ82\\MSSQLSERVERE;Initial Catalog=ITServiceManagement;Integrated Security=True";

        public AddEditRequestForm(RequestsForm parentForm, int? requestID)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.requestID = requestID;

            InitializeComponents();

            if (requestID.HasValue)
            {
                LoadRequestDetails();
            }
        }
        private void InitializeComponents()
        {
            // Инициализация компонентов формы

            txtID = new TextBox(); // Новое текстовое поле для ввода ID
            txtID.Location = new System.Drawing.Point(30, 5);
            txtID.Size = new System.Drawing.Size(100, 20);
            txtID.Enabled = true; // Блокируем редактирование ID
            txtID.Visible = true; // Скрываем поле ID

            txtDescription = new TextBox();
            txtDescription.Location = new System.Drawing.Point(30, 30);
            txtDescription.Size = new System.Drawing.Size(300, 20);

            cmbStatus = new ComboBox();
            cmbStatus.Items.AddRange(new object[] { "Новая", "В процессе", "Завершена" });
            cmbStatus.Location = new System.Drawing.Point(30, 60);
            cmbStatus.Size = new System.Drawing.Size(150, 20);

            btnSave = new Button();
            btnSave.Text = "Сохранить";
            btnSave.Location = new System.Drawing.Point(30, 90);
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Location = new System.Drawing.Point(150, 90);
            btnCancel.Click += BtnCancel_Click;

            Controls.Add(txtID); // Добавляем новое поле для ввода ID
            Controls.Add(txtDescription);
            Controls.Add(cmbStatus);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            // Другие настройки формы
            Text = "Добавление/редактирование заявки";
            Size = new System.Drawing.Size(600, 300);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveRequest();
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadRequestDetails()
        {
            // Загрузка деталей заявки для редактирования
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID, Description, Status FROM Requests WHERE ID = @RequestID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestID.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtID.Text = reader["ID"].ToString(); // Заполняем поле ID
                            txtDescription.Text = reader["Description"].ToString();
                            cmbStatus.SelectedItem = reader["Status"].ToString();
                        }
                    }
                }
            }
        }

        private void SaveRequest()
        {
            // Сохранение или добавление заявки в базу данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (requestID.HasValue)
                {
                    // Редактирование существующей заявки
                    string query = "UPDATE Requests SET Description = @Description, Status = @Status WHERE ID = @RequestID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RequestID", txtID.Text);
                        command.Parameters.AddWithValue("@Description", txtDescription.Text);
                        command.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());

                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Добавление новой заявки
                    string query = "INSERT INTO Requests (ID, Description, Status) VALUES (@RequestID, @Description, @Status)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RequestID", txtID.Text);
                        command.Parameters.AddWithValue("@Description", txtDescription.Text);
                        command.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());

                        command.ExecuteNonQuery();
                    }
                }
            }

            parentForm.LoadRequests(); // Обновляем список заявок на родительской форме
            Close();
        }
        private void AddEditRequestForm_Load(object sender, EventArgs e)
        {

        }
    }
}
