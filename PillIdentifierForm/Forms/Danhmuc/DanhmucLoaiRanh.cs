using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ClassChung;

namespace PillIdentifierForm.Forms
{
    public partial class DanhmucLoaiRanh : Form
    {
        BindingSource grid1 = new BindingSource();
        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.InsertData insertdata = new KetnoiDB.InsertData();
        KetnoiDB.UpdateData updatedata = new KetnoiDB.UpdateData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();
        public DanhmucLoaiRanh()
        {
            InitializeComponent();
        }

        private void DanhmucLoaiRanh_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            buttonThem.Enabled = false;
            buttonXoa.Enabled = false;
            buttonSua.Enabled = false;
            dataGridView1.DataSource = grid1;
            refreshDatagrid();
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if clicked row is valid (not header row)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populate textboxes with selected row data
                textBoxIDLoaiRanh.Text = row.Cells["IDLoaiRanh"].Value.ToString();
                textBoxLoaiRanh.Text = row.Cells["TenLoaiRanh"].Value.ToString();

                // Enable buttons after selection
                buttonXoa.Enabled = true;
                buttonSua.Enabled = true;
            }
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(textBoxLoaiRanh.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên loại rãnh!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxLoaiRanh.Focus();
                    return;
                }

                LoaiRanh them = new LoaiRanh
                {
                    TenLoaiRanh = textBoxLoaiRanh.Text.Trim()
                };

                // Insert new record using KetnoiDB.InsertData
                if (insertdata.InsertLoaiRanh(them))
                {
                    MessageBox.Show("Thêm mới thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTextBoxes();
                    refreshDatagrid();
                }
                else
                {
                    MessageBox.Show("Thêm mới thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate ID
                if (string.IsNullOrWhiteSpace(textBoxIDLoaiRanh.Text))
                {
                    MessageBox.Show("Vui lòng chọn loại rãnh cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa loại rãnh này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int idLoaiRanh = int.Parse(textBoxIDLoaiRanh.Text);

                    if (deletedata.DeleteLoaiRanh(idLoaiRanh))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearTextBoxes();
                        refreshDatagrid();
                        buttonXoa.Enabled = false;
                        buttonSua.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate ID
                if (string.IsNullOrWhiteSpace(textBoxIDLoaiRanh.Text))
                {
                    MessageBox.Show("Vui lòng chọn loại rãnh cần sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate input
                if (string.IsNullOrWhiteSpace(textBoxLoaiRanh.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên loại rãnh!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxLoaiRanh.Focus();
                    return;
                }

                // Update record using KetnoiDB.UpdateData
                int idLoaiRanh = int.Parse(textBoxIDLoaiRanh.Text);

                if (updatedata.UpdateLoaiRanh(idLoaiRanh, textBoxLoaiRanh.Text.Trim()))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTextBoxes();
                    refreshDatagrid();
                    buttonXoa.Enabled = false;
                    buttonSua.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "CSV Files|*.csv",
                    Title = "Chọn file để import (Cột 1: TenLoaiRanh)"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(filePath).ToLower();

                    List<LoaiRanh> listLoaiRanh = new List<LoaiRanh>();
                    
                    ImportFromCSV(filePath, listLoaiRanh);

                    if (listLoaiRanh.Count > 0)
                    {
                        DialogResult result = MessageBox.Show(
                            "Tìm thấy " + listLoaiRanh.Count.ToString() + " dòng dữ liệu. Bạn có muốn import?",
                            "Xác nhận import",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            if (bulkInsert.BulkInsertLoaiRanh(listLoaiRanh))
                            {
                                MessageBox.Show("Import thành công " + listLoaiRanh.Count.ToString() + " bản ghi!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                refreshDatagrid();
                            }
                            else
                            {
                                MessageBox.Show("Import thất bại!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu trong file!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonXoatrang_Click(object sender, EventArgs e)
        {
            buttonImport.Enabled = false;
            buttonXoa.Enabled = false;
            buttonSua.Enabled = false;
            ClearTextBoxes();
        }

        private void refreshDatagrid()
        {
            grid1.DataSource = getdata.GetDSLoaiRanh();
            dataGridView1.AutoResizeColumns();
        }

        private void ClearTextBoxes()
        {
            textBoxIDLoaiRanh.Clear();
            textBoxLoaiRanh.Clear();
            textBoxLoaiRanh.Focus();
        }
        private void ImportFromCSV(string filePath, List<LoaiRanh> listLoaiRanh)
        {
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                bool isFirstRow = true;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    // Skip header row
                    if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue;
                    }

                    string[] values = line.Split(',');

                    if (values.Length >= 1 && !string.IsNullOrWhiteSpace(values[0]))
                    {
                        LoaiRanh cd = new LoaiRanh();
                        cd.TenLoaiRanh = values[0].Trim().Trim('"');
                        listLoaiRanh.Add(cd);
                    }
                }
            }
        }
        private void textBoxLoaiRanh_TextChanged(object sender, EventArgs e)
        {
            buttonThem.Enabled = !string.IsNullOrWhiteSpace(textBoxLoaiRanh.Text);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
