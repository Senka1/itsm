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
    public partial class FAQForm : Form
    {
        private Label lblTitle;
        private RichTextBox rtbFAQ;
        private PictureBox pictureBox;
        public FAQForm()
        {
            InitializeComponent();
            InitializeComponents();
        }
        private void InitializeComponents()
        {
            // Заголовок формы
            lblTitle = new Label();
            lblTitle.Text = "Часто задаваемые вопросы (FAQ)";
            lblTitle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(10, 10);
            lblTitle.Size = new System.Drawing.Size(380, 30);

            // Текстовое поле для FAQ
            rtbFAQ = new RichTextBox();
            rtbFAQ.Multiline = true;
            rtbFAQ.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbFAQ.Text = "Q: Как работать с программой?\nA: Нажимая на кнопки навигации, вы сможете открыть окна просмотра всех нужных данных.\n\nQ: Как взаимодействовать с данными\nA: При нажатии на кнопки добавления/редактирования будет открыто окно, где вы и сможете взаимодействовать с данными, для удаления необходимо нажать кнопку 'удалить'.";
            rtbFAQ.Font = new System.Drawing.Font("Arial", 12);
            rtbFAQ.Location = new System.Drawing.Point(10, 50);
            rtbFAQ.Size = new System.Drawing.Size(380, 200);

           
            pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Location = new System.Drawing.Point(10, 260);
            pictureBox.Size = new System.Drawing.Size(380, 100);

            Controls.Add(lblTitle);
            Controls.Add(rtbFAQ);
            Controls.Add(pictureBox);

            // Другие настройки формы
            Text = "FAQ";
            Size = new System.Drawing.Size(400, 400);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }
        private void FAQForm_Load(object sender, EventArgs e)
        {

        }
    }
}
