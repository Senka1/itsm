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
    public partial class ReportsForm : Form
    {
        private DataGridView dgvReports;
        private Button btnAddReport;
        private Button btnDeleteReport;

        private string connectionString = "Data Source=WIN-8VNN0U4OJ82\\MSSQLSERVERE;Initial Catalog=ITServiceManagement;Integrated Security=True";

        public ReportsForm()
        {
            InitializeComponent();
            InitializeComponents();
            LoadReports(); // Загружаем отчеты при открытии формы
        }
        private void InitializeComponents()
        {
            // Инициализация компонентов формы
            dgvReports = new DataGridView();
            dgvReports.Location = new System.Drawing.Point(30, 30);
            dgvReports.Size = new System.Drawing.Size(600, 200);
            dgvReports.ReadOnly = true;
            dgvReports.AllowUserToAddRows = false;
            dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReports.MultiSelect = false;
            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            btnAddReport = new Button();
            btnAddReport.Text = "Добавить отчет";
            btnAddReport.Location = new System.Drawing.Point(30, 250);
            btnAddReport.Click += BtnAddReport_Click;

            btnDeleteReport = new Button();
            btnDeleteReport.Text = "Удалить отчет";
            btnDeleteReport.Location = new System.Drawing.Point(160, 250);
            btnDeleteReport.Click += BtnDeleteReport_Click;

            Controls.Add(dgvReports);
            Controls.Add(btnAddReport);
            Controls.Add(btnDeleteReport);

            // Другие настройки формы
            Text = "Управление отчетностью";
            Size = new System.Drawing.Size(700, 350);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }
        private void BtnAddReport_Click(object sender, EventArgs e)
        {
            // Открываем форму для добавления нового отчета
            AddEditReportForm addReportForm = new AddEditReportForm(this, null);
            addReportForm.ShowDialog();
        }
        private void BtnDeleteReport_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран отчет для удаления
            if (dgvReports.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного отчета
                int reportID = Convert.ToInt32(dgvReports.SelectedRows[0].Cells[0].Value);

                // Удаляем отчет
                if (DeleteReport(reportID))
                {
                    MessageBox.Show("Отчет успешно удален.");
                    LoadReports(); // Обновляем список отчетов после удаления
                }
                else
                {
                    MessageBox.Show("Не удалось удалить отчет.");
                }
            }
            else
            {
                MessageBox.Show("Выберите отчет для удаления.");
            }
        }
        public void LoadReports()
        {
            // Загрузка списка отчетов из базы данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID, Date, ReportType, Description FROM Reports";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvReports.DataSource = dataTable;
                }
            }
        }
        private bool DeleteReport(int reportID)
        {
            // Удаление отчета из базы данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Reports WHERE ID = @ReportID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReportID", reportID);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        private void ReportsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
