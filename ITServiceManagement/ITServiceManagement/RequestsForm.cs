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
    public partial class RequestsForm : Form
    {
        private DataGridView dgvRequests;
        private Button btnAddRequest;
        private Button btnEditRequest;
        private Button btnDeleteRequest;

        private string connectionString = "Data Source=WIN-8VNN0U4OJ82\\MSSQLSERVERE;Initial Catalog=ITServiceManagement;Integrated Security=True";
        public RequestsForm()
        {
            InitializeComponent();
            InitializeComponents();
            LoadRequests(); // Загружаем заявки при открытии формы
        }
        private void InitializeComponents()
        {
            // Инициализация компонентов формы

            dgvRequests = new DataGridView();
            dgvRequests.Location = new System.Drawing.Point(30, 30);
            dgvRequests.Size = new System.Drawing.Size(600, 200);
            dgvRequests.ReadOnly = true;
            dgvRequests.AllowUserToAddRows = false;
            dgvRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRequests.MultiSelect = false;
            dgvRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRequests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            btnAddRequest = new Button();
            btnAddRequest.Text = "Добавить заявку";
            btnAddRequest.Location = new System.Drawing.Point(30, 250);
            btnAddRequest.Click += BtnAddRequest_Click;

            btnEditRequest = new Button();
            btnEditRequest.Text = "Редактировать заявку";
            btnEditRequest.Location = new System.Drawing.Point(160, 250);
            btnEditRequest.Click += BtnEditRequest_Click;

            btnDeleteRequest = new Button();
            btnDeleteRequest.Text = "Удалить заявку";
            btnDeleteRequest.Location = new System.Drawing.Point(320, 250);
            btnDeleteRequest.Click += BtnDeleteRequest_Click;

            Controls.Add(dgvRequests);
            Controls.Add(btnAddRequest);
            Controls.Add(btnEditRequest);
            Controls.Add(btnDeleteRequest);

            // Другие настройки формы
            Text = "Управление заявками и задачами";
            Size = new System.Drawing.Size(700, 350);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }
        private void BtnAddRequest_Click(object sender, EventArgs e)
        {
            // Открываем форму для добавления новой заявки
            AddEditRequestForm addRequestForm = new AddEditRequestForm(this, null);
            addRequestForm.ShowDialog();
        }
        private void BtnEditRequest_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли заявка для редактирования
            if (dgvRequests.SelectedRows.Count > 0)
            {
                // Получаем ID выбранной заявки
                int requestID = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells["ID"].Value);

                // Открываем форму для редактирования заявки
                AddEditRequestForm editRequestForm = new AddEditRequestForm(this, requestID);
                editRequestForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования.");
            }
        }
        private void BtnDeleteRequest_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли заявка для удаления
            if (dgvRequests.SelectedRows.Count > 0)
            {
                // Получаем ID выбранной заявки
                int requestID = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells["ID"].Value);

                // Удаляем заявку
                if (DeleteRequest(requestID))
                {
                    MessageBox.Show("Заявка успешно удалена.");
                    LoadRequests(); // Обновляем список заявок после удаления
                }
                else
                {
                    MessageBox.Show("Не удалось удалить заявку.");
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления.");
            }
        }
        public void LoadRequests()
        {
            // Загрузка списка заявок из базы данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID, Description, Status FROM Requests";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvRequests.DataSource = dataTable;
                }
            }
        }
        private bool DeleteRequest(int requestID)
        {
            // Удаление заявки из базы данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Requests WHERE ID = @RequestID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestID);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        private void RequestsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
