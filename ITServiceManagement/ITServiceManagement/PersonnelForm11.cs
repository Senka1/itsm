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
    public partial class PersonnelForm11 : Form
    {
        private string connectionString = "Data Source=WIN-8VNN0U4OJ82\\MSSQLSERVERE;Initial Catalog=ITServiceManagement;Integrated Security=True"; 

        public PersonnelForm11()
        {
            InitializeComponent();
            LoadPersonnel();
        }
        public void LoadPersonnel()
        {
            try
            {
                // Загрузка списка персонала из базы данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT ID, Name, Position, Login, Role FROM Personnel";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Очищаем столбцы перед добавлением новых

                        // Привязываем DataGridView к источнику данных
                        dataGridView1.DataSource = dataTable;

                        // Устанавливаем режим автоматического размера столбцов
                        dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                        // Устанавливаем видимость заголовков столбцов
                        dataGridView1.ColumnHeadersVisible = true;

                        // Устанавливаем режим выделения целых строк
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PersonnelForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'iTServiceManagementDataSet.Personnel' table. You can move, or remove it, as needed.
            this.personnelTableAdapter.Fill(this.iTServiceManagementDataSet.Personnel);

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddEditPersonForm addEditForm = new AddEditPersonForm(this, null);
            addEditForm.ShowDialog();
        }

        private void btnEditPerson_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного сотрудника
                int personID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                // Открываем форму для редактирования сотрудника
                AddEditPersonForm editPersonForm = new AddEditPersonForm(this, personID);
                editPersonForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для редактирования.");
            }
        }

        private void btnDeletePerson_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем ID выбранной строки
                int selectedPersonID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                // Запрашиваем подтверждение перед удалением
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Выполняем удаление сотрудника
                    DeletePerson(selectedPersonID);
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            void DeletePerson(int personID)
            {
                try
                {
                    // Удаление сотрудника из базы данных
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM Personnel WHERE ID = @PersonID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PersonID", personID);
                            command.ExecuteNonQuery();
                        }
                    }

                    // Обновляем отображение данных после удаления
                    LoadPersonnel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении сотрудника: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
