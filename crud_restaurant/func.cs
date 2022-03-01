using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Data.OleDb;
using Bunifu.UI.WinForms;
using Guna.UI2.WinForms;

namespace crud_restaurant
{
    class func
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        
        public void fun_connection(string url)
        {
            connection = new SqlConnection(url);
        }

        public void fun_insert(string query)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Ditambahkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void fun_update(string query)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Diperbarui", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void fun_query(string query)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
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

        public void fun_delete(string query)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Dihapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void fun_read(string query, DataGridView dgv)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                command = new SqlCommand(query, connection);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgv.DataSource = dt;
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

        public void fun_insert_image(string query, PictureBox pb)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                command = new SqlCommand(query, connection);
                MemoryStream stream = new MemoryStream();
                pb.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] pic = stream.ToArray();
                command.Parameters.AddWithValue("@Pic", pic);
                command.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Ditambahkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //internal void fun_pickFile(BunifuTextBox txb_filePath, Guna2PictureBox pb_fotoFile)
        //{
        //    throw new NotImplementedException();
        //}

        public Image ConvertByteToArray(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        public void fun_pickFile(BunifuTextBox txb, Guna2PictureBox pb)
        {
            Stream myStream = null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image File(*.jpg; *.jpe; *.jpeg; *.bmp; *.png) | *.jpg; *.jpe; *.jpeg; *.bmp; *.png";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = fileDialog.OpenFile()) != null)
                    {
                        string fileName = fileDialog.FileName;
                        txb.Text = fileName;
                        if (myStream.Length > 512000)
                        {
                            MessageBox.Show("Ukuran File Terlalu Besar!");
                        }
                        else
                        {
                            pb.Load(fileName);
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void fun_setComboBox(string sql, ComboBox cb, string valueString, string displayString)
        {
            DataTable dtz = new DataTable();
            dtz = ResultDataTable(sql);
            cb.DataSource = dtz;
            cb.DisplayMember = displayString;
            cb.ValueMember = valueString;
        }

        public void fun_setText(string query, string label, Label lb, string field)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lb.Text = $"{label}{reader[field].ToString()}";
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

        public DataTable ResultDataTable(string command)
        {
               try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    SqlCommand cmd = new SqlCommand(command, connection);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show(ex.Message + " : ' " + command + " ' ");
                    return null;
                }
            finally
            {
                connection.Close();
            }
        }
    }
}
