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

namespace crud_restaurant
{
    public partial class FormAdmin : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        public FormAdmin()
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btn_order_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormOrder formOrder = new FormOrder();
            formOrder.FormClosed += (s,args) => this.Close();
            formOrder.Show();
        }

        private void btn_manageMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormManageMenu formMenu = new FormManageMenu();
            formMenu.FormClosed += (s, args) => this.Close();
            formMenu.Show();
        }

        private void btn_manageMember_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormManageMember formMember = new FormManageMember();
            formMember.FormClosed += (s, args) => this.Close();
            formMember.Show();
        }

        private void btn_laporan_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormReport formReport = new FormReport();
            formReport.FormClosed += (s, args) => this.Close();
            formReport.Show();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login formLogin = new Login();
            formLogin.FormClosed += (s, args) => this.Close();
            formLogin.Show();
        }

        private void btn_manageTable_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormManageMeja formMeja = new FormManageMeja();
            formMeja.FormClosed += (s, args) => this.Close();
            formMeja.Show();
        }
    }
}
