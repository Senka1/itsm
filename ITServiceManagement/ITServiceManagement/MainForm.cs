using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITServiceManagement
{
    public partial class MainForm : Form
    {
        private Button btnOpenRequestsForm;
        private Button btnOpenPersonnelForm;
        private Button btnOpenReportsForm;
        private Button btnLogout;
        private Button btnOpenFAQForm;

        public MainForm()
        {
            InitializeComponent();
            InitializeComponents();
        }
        private void InitializeComponents()
        {
            // Инициализация компонентов формы

            btnOpenRequestsForm = new Button();
            btnOpenRequestsForm.Text = "Управление заявками и задачами";
            btnOpenRequestsForm.Location = new System.Drawing.Point(30, 30);
            btnOpenRequestsForm.Size = new System.Drawing.Size(200, 23);
            btnOpenRequestsForm.Click += BtnOpenRequestsForm_Click;

            btnOpenPersonnelForm = new Button();
            btnOpenPersonnelForm.Text = "Управление персоналом";
            btnOpenPersonnelForm.Location = new System.Drawing.Point(30, 70);
            btnOpenPersonnelForm.Size = new System.Drawing.Size(200, 23);
            btnOpenPersonnelForm.Click += BtnOpenPersonnelForm_Click;

            btnOpenReportsForm = new Button();
            btnOpenReportsForm.Text = "Форма отчетности";
            btnOpenReportsForm.Location = new System.Drawing.Point(30, 110);
            btnOpenReportsForm.Size = new System.Drawing.Size(130, 23);
            btnOpenReportsForm.Click += BtnOpenReportsForm_Click;

            btnLogout = new Button();
            btnLogout.Text = "Выйти";
            btnLogout.Location = new System.Drawing.Point(30, 150);
            btnLogout.Click += BtnLogout_Click;

            btnOpenFAQForm = new Button();
            btnOpenFAQForm.Text = "Помощь";
            btnOpenFAQForm.Location = new System.Drawing.Point(30, 190);
            btnOpenFAQForm.Click += BtnOpenFAQForm_Click;

            Controls.Add(btnOpenFAQForm);
            Controls.Add(btnOpenRequestsForm);
            Controls.Add(btnOpenPersonnelForm);
            Controls.Add(btnOpenReportsForm);
            Controls.Add(btnLogout);

            // Другие настройки формы
            Text = "Главная форма";
            Size = new System.Drawing.Size(300, 250);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }
        private void BtnOpenFAQForm_Click(object sender, EventArgs e)
        {
            // Открытие формы FAQ
            FAQForm faqForm = new FAQForm();
            faqForm.Show();
        }
        private void BtnOpenRequestsForm_Click(object sender, EventArgs e)
        {
            // Открытие формы управления заявками и задачами
            RequestsForm requestsForm = new RequestsForm();
            requestsForm.Show();
        }

        private void BtnOpenPersonnelForm_Click(object sender, EventArgs e)
        {
            // Открытие формы управления персоналом
            PersonnelForm11 personnelForm = new PersonnelForm11();
            personnelForm.Show();
        }

        private void BtnOpenReportsForm_Click(object sender, EventArgs e)
        {
            // Открытие формы отчетности
            ReportsForm reportsForm = new ReportsForm();
            reportsForm.Show();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // Закрытие текущей формы (главной) и открытие формы авторизации
            Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
