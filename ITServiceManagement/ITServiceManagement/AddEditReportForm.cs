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
    public partial class AddEditReportForm : Form
    {
        private ReportsForm parentForm;
        private int? reportID; // Используем Nullable<int>, чтобы различать добавление и редактирование
        private TextBox txtDate;
        private TextBox txtReportType;
        private TextBox txtDescription;
        private Button btnSave;

        private string connectionString = "Data Source=WIN-8VNN0U4OJ82\\MSSQLSERVERE;Initial Catalog=ITServiceManagement;Integrated Security=True";

        public AddEditReportForm(ReportsForm parentForm, int? reportID)
        {
            InitializeComponent();

            this.parentForm = parentForm;
            this.reportID = reportID;

            if (reportID.HasValue)
            {
                Text = "Редактирование отчета";
                LoadReportData();
            }
            else
            {
                Text = "Добавление отчета";
            }
        }
        private void LoadReportData()
        {
            // Загружаем данные отчета для редактирования
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Date, ReportType, Description FROM Reports WHERE ID = @ReportID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReportID", reportID);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtDate.Text = reader["Date"].ToString();
                        txtReportType.Text = reader["ReportType"].ToString();
                        txtDescription.Text = reader["Description"].ToString();
                    }
                }
            }
        }
        private void InitializeComponent()
        {
            // Инициализация компонентов формы
            txtDate = new TextBox();
            txtDate.Location = new System.Drawing.Point(20, 20);
            txtDate.Size = new System.Drawing.Size(200, 20);
            txtDate.MaxLength = 50;

            txtReportType = new TextBox();
            txtReportType.Location = new System.Drawing.Point(20, 50);
            txtReportType.Size = new System.Drawing.Size(200, 20);
            txtReportType.MaxLength = 100;

            txtDescription = new TextBox();
            txtDescription.Location = new System.Drawing.Point(20, 80);
            txtDescription.Size = new System.Drawing.Size(400, 150);
            txtDescription.Multiline = true;

            btnSave = new Button();
            btnSave.Text = "Сохранить";
            btnSave.Location = new System.Drawing.Point(20, 250);
            btnSave.Click += BtnSave_Click;

            Controls.Add(txtDate);
            Controls.Add(txtReportType);
            Controls.Add(txtDescription);
            Controls.Add(btnSave);

            // Другие настройки формы
            Size = new System.Drawing.Size(450, 350);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            FormClosing += AddEditReportForm_FormClosing;
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Сохраняем отчет
            if (SaveReport())
            {
                MessageBox.Show("Отчет успешно сохранен.");
                parentForm.LoadReports(); // Обновляем список отчетов на родительской форме
                Close();
            }
            else
            {
                MessageBox.Show("Не удалось сохранить отчет.");
            }
        }
        private bool SaveReport()
        {
            // Сохранение отчета в базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query;
                if (reportID.HasValue)
                {
                    // Редактирование отчета
                    query = "UPDATE Reports SET Date = @Date, ReportType = @ReportType, Description = @Description WHERE ID = @ReportID";
                }
                else
                {
                    // Добавление нового отчета
                    query = "INSERT INTO Reports (Date, ReportType, Description) VALUES (@Date, @ReportType, @Description)";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", txtDate.Text);
                    command.Parameters.AddWithValue("@ReportType", txtReportType.Text);
                    command.Parameters.AddWithValue("@Description", txtDescription.Text);

                    if (reportID.HasValue)
                    {
                        command.Parameters.AddWithValue("@ReportID", reportID);
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        private void AddEditReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Закрываем форму и возвращаемся к родительской форме
            parentForm.Enabled = true;
        }
        private void AddEditReportForm_Load(object sender, EventArgs e)
        {

        }
    }
}
