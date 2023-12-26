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
    public partial class PersonnelForm : Form
    {
        private DataGridView dgvPersonnel;
        private Button btnAddPerson;
        private Button btnEditPerson;
        private Button btnDeletePerson;

        private string connectionString = "Data Source=WIN-8VNN0U4OJ82\\MSSQLSERVERE;Initial Catalog=ITServiceManagement;Integrated Security=True";

        public PersonnelForm()
        {
            InitializeComponent();
            InitializeComponents();
            LoadPersonnel(); // Загружаем данные о персонале при открытии формы

        }
        private void InitializeComponents()
        {
            // Инициализация компонентов формы
            dgvPersonnel = new DataGridView();
            dgvPersonnel.Location = new System.Drawing.Point(30, 30);
            dgvPersonnel.Size = new System.Drawing.Size(600, 200);
            dgvPersonnel.ReadOnly = true;
            dgvPersonnel.AllowUserToAddRows = false;
            dgvPersonnel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonnel.MultiSelect = false;
            dgvPersonnel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPersonnel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            btnAddPerson = new Button();
            btnAddPerson.Text = "Добавить сотрудника";
            btnAddPerson.Location = new System.Drawing.Point(30, 250);
            btnAddPerson.Click += BtnAddPerson_Click;

            btnEditPerson = new Button();
            btnEditPerson.Text = "Редактировать сотрудника";
            btnEditPerson.Location = new System.Drawing.Point(160, 250);
            btnEditPerson.Click += BtnEditPerson_Click;

            btnDeletePerson = new Button();
            btnDeletePerson.Text = "Удалить сотрудника";
            btnDeletePerson.Location = new System.Drawing.Point(320, 250);
            btnDeletePerson.Click += BtnDeletePerson_Click;

            Controls.Add(dgvPersonnel);
            Controls.Add(btnAddPerson);
            Controls.Add(btnEditPerson);
            Controls.Add(btnDeletePerson);

            // Другие настройки формы
            Text = "Управление персоналом";
            Size = new System.Drawing.Size(700, 350);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }
        private void BtnAddPerson_Click(object sender, EventArgs e)
        {
            // Открываем форму для добавления нового сотрудника
            AddEditPersonForm addPersonForm = new AddEditPersonForm(this, null);
            addPersonForm.ShowDialog();
        }
        private void BtnEditPerson_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран сотрудник для редактирования
            if (dgvPersonnel.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного сотрудника
                int personID = Convert.ToInt32(dgvPersonnel.SelectedRows[0].Cells["ID"].Value);

                // Открываем форму для редактирования сотрудника
                AddEditPersonForm editPersonForm = new AddEditPersonForm(this, personID);
                editPersonForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для редактирования.");
            }
        }
        private void BtnDeletePerson_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран сотрудник для удаления
            if (dgvPersonnel.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного сотрудника
                int personID = Convert.ToInt32(dgvPersonnel.SelectedRows[0].Cells["ID"].Value);

                // Удаляем сотрудника
                if (DeletePerson(personID))
                {
                    MessageBox.Show("Сотрудник успешно удален.");
                    LoadPersonnel(); // Обновляем список сотрудников после удаления
                }
                else
                {
                    MessageBox.Show("Не удалось удалить сотрудника.");
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления.");
            }
        }
        public void LoadPersonnel()
        {
            // Загрузка списка сотрудников из базы данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID, Name, Position FROM Personnel";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvPersonnel.DataSource = dataTable;
                }
            }
        }
        private bool DeletePerson(int personID)
        {
            // Удаление сотрудника из базы данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Personnel WHERE ID = @PersonID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        private void PersonnelForm_Load(object sender, EventArgs e)
        {

        }
    }
}
