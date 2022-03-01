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
    public partial class FormPayment : Form
    {
        func func_ = new func();
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        
        public FormPayment()
        {
            InitializeComponent();
        }

        private void FormPayment_Load(object sender, EventArgs e)
        {
            func_.fun_connection(@const.url);
            loadComboBox();
            func_.fun_read("SELECT MsMenu.name NamaMenu, OrderDetail.qty Banyak, MsMenu.carbo Carbo, MsMenu.protein Protein, MsMenu.price Harga, OrderDetail.total Total FROM OrderDetail INNER JOIN MsMenu ON OrderDetail.menuId = MsMenu.id WHERE orderId='" + cmb_orderId.SelectedValue + "' ", dgv_payment);
            func_.fun_setText("SELECT SUM(total) hasil FROM OrderDetail WHERE orderId='" + cmb_orderId.SelectedValue + "';", "Total: ", label3, "hasil");
        }
        void loadComboBox()
        {
            func_.fun_setComboBox(@"SELECT DISTINCT TOP 100 orderId FROM OrderDetail WHERE status='unpaid' ", cmb_orderId, "orderId", "orderId");
        }

        void refresh()
        {
            txb_member.Clear();
            func_.fun_read("SELECT MsMenu.name NamaMenu, OrderDetail.qty Banyak, MsMenu.carbo Carbo, MsMenu.protein Protein, MsMenu.price Harga, OrderDetail.total Total FROM OrderDetail INNER JOIN MsMenu ON OrderDetail.menuId = MsMenu.id WHERE orderId='" + cmb_orderId.SelectedValue + "' AND status='unpaid' ", dgv_payment);
            loadComboBox();
            cmb_orderId.Text = "Pilih OrderId";
        }
        private void cmb_orderId_SelectedValueChanged(object sender, EventArgs e)
        {
            func_.fun_read("SELECT MsMenu.name NamaMenu, OrderDetail.qty Banyak, MsMenu.carbo Carbo, MsMenu.protein Protein, MsMenu.price Harga, OrderDetail.total Total FROM OrderDetail INNER JOIN MsMenu ON OrderDetail.menuId = MsMenu.id WHERE orderId='" + cmb_orderId.SelectedValue + "' ", dgv_payment);
            func_.fun_setText("SELECT SUM(total) hasil FROM OrderDetail WHERE orderId='" + cmb_orderId.SelectedValue + "';", "Total: ", label3, "hasil");
            func_.fun_setText("SELECT SUM(total) hasil FROM OrderDetail WHERE orderId='" + cmb_orderId.SelectedValue + "';", "", label8, "hasil");
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            if (cmb_typePay.SelectedIndex == 1)
            {
                if (txb_cardNumber.Text != "" && cmb_bankName.Text != "Pilih Nama Bank")
                {
                    func_.fun_update(@"UPDATE OrderHeader SET 
                paymentType='" + cmb_typePay.Text + "', bank='" + cmb_bankName.Text + "', cardNumber='" + txb_cardNumber.Text + "', total='" + label8.Text + "' WHERE id='" + cmb_orderId.Text + "' ");
                    func_.fun_query(@"UPDATE OrderDetail SET status='Paid' WHERE orderId='" + cmb_orderId.Text + "'");
                    refresh();
                }
                else
                {
                    MessageBox.Show("Harap Isi Semua Kolom!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (cmb_typePay.SelectedIndex == 0)
            {
                if(txb_cash.Text != "")
                {
                    func_.fun_update(@"UPDATE OrderHeader SET 
            paymentType='" + cmb_typePay.Text + "', bank='" + cmb_bankName.Text + "', cardNumber='" + txb_cardNumber.Text + "', total='" + label8.Text + "' WHERE id='" + cmb_orderId.Text + "' ");
                    func_.fun_query(@"UPDATE OrderDetail SET status='Paid' WHERE orderId='" + cmb_orderId.Text + "'");
                    refresh();
                }
                else
                {
                    MessageBox.Show("Harap Isi Semua Kolom!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Harap Pilih Metode Pembayaran!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        
    }
}
