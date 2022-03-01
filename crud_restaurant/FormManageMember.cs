using crud_restaurant.crud_restaurant;
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
    public partial class FormManageMember : Form
    {
        func func_ = new func();
        utils utils = new utils(@const.url);

        public FormManageMember()
        {
            InitializeComponent();
        }

        void refresh()
        {
            txb_cari.Clear();
            txb_memberID.Clear();
            txb_username.Clear();
            txb_password.Clear();
            txb_address.Clear();
            cb_role.Text = "Pilih Jabatan...";
            cb_gender.Text = "Pilih Jenis Kelamin...";
            utils.readToDataGrid("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai", dgv_manageMember);
            //func_.fun_read("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai", dgv_manageMember);
        }

        private void FormManageMember_Load(object sender, EventArgs e)
        {
            txb_memberID.Visible = false;
            bunifuLabel3.Visible = false;
            utils.readToDataGrid("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai", dgv_manageMember);
            //func_.fun_read("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai", dgv_manageMember);
        }

        private void btn_cari_Click(object sender, EventArgs e)
        {
                if (txb_cari.Text != "") {
                utils.readToDataGrid("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai WHERE Nama='" + txb_cari.Text + "' OR Jabatan='" + txb_cari.Text + "' OR Alamat='" + txb_cari.Text + "' OR Jenis_Kelamin='" + txb_cari.Text + "' ", dgv_manageMember);
                    //func_.fun_read("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai WHERE Nama='" + txb_cari.Text + "' OR Jabatan='" + txb_cari.Text + "' OR Alamat='" + txb_cari.Text + "' OR Jenis_Kelamin='" + txb_cari.Text + "' ", dgv_manageMember); 
                }
                else
                {
                utils.readToDataGrid("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai", dgv_manageMember);

                    //func_.fun_read("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai", dgv_manageMember);
                }
            
        }
        
        private void btn_insert_Click(object sender, EventArgs e)
        {
            if(txb_username.Text != "" && txb_password.Text != "" && txb_address.Text != "" && cb_role.Text != "Pilih Jabatan..." && cb_gender.Text != "Pilih Jenis Kelamin...") 
            {
                string query = @"INSERT INTO pegawai([Nama],[Kata_Sandi],[Jabatan],[Alamat],[Jenis_Kelamin]) VALUES('"+txb_username.Text+"','"+txb_password.Text+"','"+cb_role.SelectedItem+"','"+txb_address.Text+"','"+cb_gender.SelectedItem+"');";
                if (MessageBox.Show("Tambahkan Pegawai Baru??", "Informasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    utils.create(query);
                    //func_.fun_query(query);
                refresh();
            }
            else
            {
                MessageBox.Show("Harap Isi Semua Kolom!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txb_memberID.Text != "" && txb_username.Text != "" && txb_password.Text != "" && txb_address.Text != "" && cb_role.Text != "Pilih Jabatan..." && cb_gender.Text != "Pilih Jenis Kelamin...")
            {
                string query = @"UPDATE pegawai SET Nama='" + txb_username.Text + "', Kata_Sandi='" + txb_password.Text + "', Alamat='" + txb_address.Text + "', Jabatan='" + cb_role.Text + "', Jenis_Kelamin='" + cb_gender.Text + "' WHERE id='" + int.Parse(txb_memberID.Text) + "' ";
                if (MessageBox.Show("Perbarui Pegawai Ini??", "Informasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    utils.update(query);
                    //func_.fun_update(query);
                refresh();
            }
            else
            {
                MessageBox.Show("Harap Isi Semua Kolom!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txb_memberID.Text != "")
            {
                string query = "DELETE pegawai WHERE id='" + int.Parse(txb_memberID.Text) + "'";
                if (MessageBox.Show("Hapus Pegawai Ini??", "Informasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    utils.delete(query);
                refresh();
            }
            else
            {
                MessageBox.Show("Harap Isi Semua Kolom!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_manageMember_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //it checks if the row index of the cell is greater than or equal to zero
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dgv_manageMember.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                txb_memberID.Text = row.Cells[0].Value.ToString();
                txb_username.Text = row.Cells[1].Value.ToString();
                txb_address.Text = row.Cells[2].Value.ToString();
                cb_role.SelectedItem = row.Cells[3].Value.ToString();
                cb_gender.SelectedItem = row.Cells[4].Value.ToString();
            }
        }

        private void txb_cari_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txb_cari.Text != "")
                {
                    utils.readToDataGrid("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai WHERE Nama='" + txb_cari.Text + "' OR Jabatan='" + txb_cari.Text + "' OR Alamat='" + txb_cari.Text + "' OR Jenis_Kelamin='" + txb_cari.Text + "' ", dgv_manageMember);
                }
                else
                {
                    utils.readToDataGrid("SELECT id MemberID, Nama Nama, Kata_Sandi KataSandi, Jabatan Jabatan, Alamat Alamat, Jenis_Kelamin JenisKelamin FROM pegawai", dgv_manageMember);
                }
            }
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            txb_password.PasswordChar = bunifuCheckBox1.Checked ? '\0' : '●';
        }
    }
}
