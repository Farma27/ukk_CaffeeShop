using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crud_restaurant
{
    public partial class FormCashier : Form
    {
        public FormCashier()
        {
            InitializeComponent();
        }

        private void btn_payment_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormPayment formPay = new FormPayment();
            formPay.FormClosed += (s, args) => this.Close();
            formPay.Show();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login formLogin = new Login();
            formLogin.FormClosed += (s, args) => this.Close();
            formLogin.Show();
        }
    }
}
