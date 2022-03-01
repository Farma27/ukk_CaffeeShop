using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace crud_restaurant
{
    public partial class Login : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=FARMA;Initial Catalog=UJIKOM;Integrated Security=True");
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        

        public Login()
        {
            InitializeComponent();
        }

        void login()
        {
            connection = new SqlConnection(@const.url);
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                string query = "SELECT Jabatan FROM pegawai WHERE Nama='" + txb_username.Text + "' AND Kata_Sandi='" + txb_password.Text + "' ";
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (Convert.ToString(reader["Jabatan"]) == "Admin")
                    {
                        this.Hide();
                        FormAdmin formAdmin = new FormAdmin();
                        formAdmin.FormClosed += (s, args) => this.Close();
                        formAdmin.Show();
                    }
                    if (Convert.ToString(reader["Jabatan"]) == "Kasir")
                    {
                        this.Hide();
                        FormCashier formCashier = new FormCashier();
                        formCashier.FormClosed += (s, args) => this.Close();
                        formCashier.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Username atau Password yang dimasukan tidak valid", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txb_username.Text != "" && txb_password.Text != "")
            {
                login();
            }
            else
            {
                MessageBox.Show("Harap isi Username dan Password","Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void txb_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            txb_password.PasswordChar = bunifuCheckBox1.Checked ? '\0' : '●';
        }
    }
}
