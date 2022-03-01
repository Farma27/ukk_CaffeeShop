using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crud_restaurant
{
    public partial class FormManageMenu : Form
    {
        func func_ = new func();
        SqlConnection connection;
        SqlCommand command;
        public FormManageMenu()
        {
            InitializeComponent();
        }

        private void FormManageMenu_Load(object sender, EventArgs e)
        {
            bunifuLabel3.Visible = false;
            txb_menuID.Visible = false;
            func_.fun_connection(@const.url);
            func_.fun_read("SELECT id MenuID, Nama_Menu NamaMenu, Harga Harga, Kategori Kategori, Foto Foto FROM menu", dgv_manageMenu);
            dgv_manageMenu.Columns[0].Visible = false;
        }

        void refresh()
        {
            txb_cari.Clear();
            cb_kategori.SelectedItem = null;
            txb_filePath.Clear();
            txb_hargaMenu.Clear();
            txb_menuID.Clear();
            txb_namaMenu.Clear();
            pb_fotoFile.Image = null;
            func_.fun_read("SELECT id MenuID, Nama_Menu NamaMenu, Harga Harga, Kategori Kategori, Foto Foto FROM menu", dgv_manageMenu);
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            if(txb_namaMenu.Text != "" && txb_hargaMenu.Text != "" && txb_filePath.Text != "" && cb_kategori.Text != "")
            {
                if (MessageBox.Show("Tambahkan Menu Baru??", "Informasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "INSERT INTO menu([Nama_Menu],[Harga],[Kategori],[Foto]) VALUES('" + txb_namaMenu.Text + "', '" + txb_hargaMenu.Text + "', '" + cb_kategori.Text + "', @pic)";
                    func_.fun_insert_image(query,pb_fotoFile);
                    refresh();
                }
            }
            else
            {
                MessageBox.Show("Harap Isi Semua Kolom!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txb_menuID.Text != "" && txb_namaMenu.Text != "" && txb_hargaMenu.Text != "" && txb_filePath.Text != "" && cb_kategori.Text != "")
            {
                if (MessageBox.Show("Perbarui Menu Ini??", "Informasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "UPDATE menu SET Nama_Menu='" + txb_namaMenu.Text + "', Harga='" + txb_hargaMenu.Text + "', Kategori='" + cb_kategori.Text + "', photo=@Pic WHERE id='" + int.Parse(txb_menuID.Text) + "'";
                    func_.fun_insert_image(query, pb_fotoFile);
                }
            }
            else
            {
                MessageBox.Show("Harap Isi Semua Kolom!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txb_menuID.Text != "")
            {
                if (MessageBox.Show("Hapus Menu Ini??", "Informasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    func_.fun_update("DELETE menu WHERE id='" + int.Parse(txb_menuID.Text) + "'");
                    refresh();
                }
            }
            else
            {
                MessageBox.Show("Harap Isi Semua Kolom!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_pickFile_Click(object sender, EventArgs e)
        {
            func_.fun_pickFile(txb_filePath,pb_fotoFile);
        }

        private void btn_cari_Click(object sender, EventArgs e)
        {
            if (txb_cari.Text != "")
            {
                func_.fun_read("SELECT id MenuID, Nama_Menu NamaMenu, Harga Harga, Kategori Kategori FROM menu WHERE Nama_Menu='"+txb_cari.Text+ "' OR Harga='" + txb_cari.Text + "' OR Kategori='" + txb_cari.Text + "' ", dgv_manageMenu);
            }
            else
            {
                func_.fun_read("SELECT id MenuID, Nama_Menu NamaMenu, Harga Harga, Kategori Kategori FROM menu", dgv_manageMenu);
            }
        }

        private void dgv_mangeMenu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //it checks if the row index of the cell is greater than or equal to zero
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dgv_manageMenu.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                txb_menuID.Text = row.Cells[0].Value.ToString();
                txb_namaMenu.Text = row.Cells[1].Value.ToString();
                txb_hargaMenu.Text = row.Cells[2].Value.ToString();
                cb_kategori.SelectedItem = row.Cells[3].Value.ToString();
                if (DBNull.Value.Equals(row.Cells[4].Value))
                {
                    pb_fotoFile.Image = null;
                }
                else
                {
                    pb_fotoFile.Image = func_.ConvertByteToArray((byte[])row.Cells[4].Value);
                }
            }
        }
        private void txb_cari_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txb_cari.Text != "")
                {
                    func_.fun_read("SELECT id MenuID, Nama_Menu NamaMenu, Harga Harga, Kategori Kategori FROM menu WHERE Harga='" + Convert.ToInt32(txb_cari.Text) + "' OR Nama_Menu='"+txb_cari.Text+ "' OR Kategori='" + txb_cari.Text + "' ", dgv_manageMenu);
                }
                else
                {
                    func_.fun_read("SELECT id MenuID, Nama_Menu NamaMenu, Harga Harga, Kategori Kategori FROM menu", dgv_manageMenu);
                }
            }
        }
    }
}
