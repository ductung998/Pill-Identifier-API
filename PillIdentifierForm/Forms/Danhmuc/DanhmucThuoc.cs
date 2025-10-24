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
    public partial class DanhmucThuoc : Form
    {
        private List<HoatChat> _listHoatChat;

        BindingSource grid1 = new BindingSource();
        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.InsertData insertdata = new KetnoiDB.InsertData();
        KetnoiDB.UpdateData updatedata = new KetnoiDB.UpdateData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();

        public DanhmucThuoc()
        {
            InitializeComponent();
        }

        private void DanhmucThuoc_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            buttonThem.Enabled = false;
            buttonXoa.Enabled = false;
            buttonSua.Enabled = false;

            LoadHoatChat();

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
                textBoxIDThuoc.Text = row.Cells["IDThuoc"].Value != null ? row.Cells["IDThuoc"].Value.ToString() : "";
                textBoxThuoc.Text = row.Cells["TenThuoc"].Value != null ? row.Cells["TenThuoc"].Value.ToString() : "";
                textBoxDBC.Text = row.Cells["DangBaoChe"].Value != null ? row.Cells["DangBaoChe"].Value.ToString() : "";
                textBoxHamluong.Text = row.Cells["HamLuong"].Value != null ? row.Cells["HamLuong"].Value.ToString() : "";
                textBoxNSX.Text = row.Cells["NhaSX"].Value != null ? row.Cells["NhaSX"].Value.ToString() : "";
                textBoxSDK.Text = row.Cells["SDK"].Value != null ? row.Cells["SDK"].Value.ToString() : "";
                textBoxGhichu.Text = row.Cells["GhiChu"].Value != null ? row.Cells["GhiChu"].Value.ToString() : "";

                // Set combobox by IDHoatChat value
                if (row.Cells["IDHoatChat"].Value != null && row.Cells["IDHoatChat"].Value != DBNull.Value)
                {
                    int idHoatChat = Convert.ToInt32(row.Cells["IDHoatChat"].Value);
                    comboBoxHC.SelectedValue = idHoatChat;
                }
                else
                {
                    comboBoxHC.SelectedIndex = -1;
                }

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
                if (string.IsNullOrWhiteSpace(textBoxThuoc.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên thuốc!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxThuoc.Focus();
                    return;
                }

                // Get IDHoatChat from combobox
                int idHoatChat = comboBoxHC.SelectedValue != null ? Convert.ToInt32(comboBoxHC.SelectedValue) : 1;

                // Insert new record using KetnoiDB.InsertData
                if (insertdata.InsertThuoc(textBoxThuoc.Text.Trim(), textBoxSDK.Text.Trim(), idHoatChat,
                    textBoxHamluong.Text.Trim(), textBoxDBC.Text.Trim(), textBoxNSX.Text.Trim(), textBoxGhichu.Text.Trim()))
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
                if (string.IsNullOrWhiteSpace(textBoxIDThuoc.Text))
                {
                    MessageBox.Show("Vui lòng chọn thuốc cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa thuốc này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int idThuoc = int.Parse(textBoxIDThuoc.Text);

                    if (deletedata.DeleteThuoc(idThuoc))
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
                if (string.IsNullOrWhiteSpace(textBoxIDThuoc.Text))
                {
                    MessageBox.Show("Vui lòng chọn thuốc cần sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate input
                if (string.IsNullOrWhiteSpace(textBoxThuoc.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên thuốc!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxThuoc.Focus();
                    return;
                }

                // Get IDHoatChat from combobox
                int idHoatChat = comboBoxHC.SelectedValue != null ? Convert.ToInt32(comboBoxHC.SelectedValue) : 1;

                // Update record using KetnoiDB.UpdateData
                int idThuoc = int.Parse(textBoxIDThuoc.Text);

                if (updatedata.UpdateThuoc(idThuoc, textBoxThuoc.Text.Trim(), textBoxSDK.Text.Trim(), idHoatChat,
                    textBoxHamluong.Text.Trim(), textBoxDBC.Text.Trim(), textBoxNSX.Text.Trim(), textBoxGhichu.Text.Trim()))
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
                    Filter = "CSV Files (*.csv)|*.csv",
                    Title = "Chọn file để import"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    List<Thuoc> listThuoc = new List<Thuoc>();

                    ImportFromCSV(filePath, listThuoc);

                    if (listThuoc.Count > 0)
                    {
                        DialogResult result = MessageBox.Show(
                            "Tìm thấy " + listThuoc.Count.ToString() + " dòng dữ liệu. Bạn có muốn import?",
                            "Xác nhận import",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            if (bulkInsert.BulkInsertThuoc(listThuoc))
                            {
                                MessageBox.Show("Import thành công " + listThuoc.Count.ToString() + " bản ghi!", "Thông báo",
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
            buttonXoa.Enabled = false;
            buttonSua.Enabled = false;
            ClearTextBoxes();
        }

        private void refreshDatagrid()
        {
            try
            {
                // Get data from database
                List<Thuoc> dsThuoc = getdata.GetDSThuoc();

                // Create display list with HoatChat name
                var displayList = dsThuoc.Select(t => new
                {
                    IDThuoc = t.IDThuoc,
                    TenThuoc = t.TenThuoc,
                    SDK = t.SDK,
                    IDHoatChat = t.IDHoatChat,
                    TenHoatChat = GetTenHoatChat(t.IDHoatChat),
                    HamLuong = t.HamLuong,
                    DangBaoChe = t.DangBaoChe,
                    NhaSX = t.NhaSX,
                    GhiChu = t.GhiChu
                }).ToList();

                grid1.DataSource = null;
                grid1.DataSource = displayList;
                dataGridView1.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTenHoatChat(int idHoatChat)
        {
            if (_listHoatChat == null || _listHoatChat.Count == 0)
                return "";

            HoatChat hc = _listHoatChat.FirstOrDefault(h => h.IDHoatChat == idHoatChat);
            return hc != null ? hc.TenHoatChat : "";
        }

        private void ClearTextBoxes()
        {
            textBoxIDThuoc.Clear();
            textBoxThuoc.Clear();
            textBoxSDK.Clear();
            textBoxHamluong.Clear();
            textBoxDBC.Clear();
            textBoxNSX.Clear();
            textBoxGhichu.Clear();
            comboBoxHC.SelectedIndex = -1;
            textBoxThuoc.Focus();
        }

        private void ImportFromCSV(string filePath, List<Thuoc> listThuoc)
        {
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                bool isFirstRow = true;
                int lineNumber = 0;

                while (!sr.EndOfStream)
                {
                    lineNumber++;
                    string line = sr.ReadLine();

                    // Skip header row
                    if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue;
                    }

                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    try
                    {
                        string[] values = line.Split(',');

                        if (values.Length >= 1 && !string.IsNullOrWhiteSpace(values[0]))
                        {
                            Thuoc cd = new Thuoc();
                            cd.TenThuoc = values[0].Trim().Trim('"');
                            cd.SDK = values.Length > 1 ? values[1].Trim().Trim('"') : "";
                            cd.IDHoatChat = values.Length > 2 && !string.IsNullOrWhiteSpace(values[2]) ?
                                Convert.ToInt32(values[2].Trim().Trim('"')) : 1;
                            cd.HamLuong = values.Length > 3 ? values[3].Trim().Trim('"') : "";
                            cd.DangBaoChe = values.Length > 4 ? values[4].Trim().Trim('"') : "";
                            cd.NhaSX = values.Length > 5 ? values[5].Trim().Trim('"') : "";
                            cd.GhiChu = values.Length > 6 ? values[6].Trim().Trim('"') : "";
                            listThuoc.Add(cd);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tại dòng " + lineNumber.ToString() + ": " + ex.Message,
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void textBoxThuoc_TextChanged(object sender, EventArgs e)
        {
            buttonThem.Enabled = !string.IsNullOrWhiteSpace(textBoxThuoc.Text);
        }

        private void buttonGDHoatChat_Click(object sender, EventArgs e)
        {
            using (DanhmucHoatChat formcon = new DanhmucHoatChat())
            {
                formcon.ShowDialog();
                LoadHoatChat();
                refreshDatagrid(); // Refresh to show updated HoatChat names
            }
        }

        private void LoadHoatChat()
        {
            _listHoatChat = getdata.GetDSHoatChat().OrderBy(h => h.TenHoatChat).ToList();
            comboBoxHC.DataSource = _listHoatChat;
            comboBoxHC.DisplayMember = "TenHoatChat";
            comboBoxHC.ValueMember = "IDHoatChat";
            comboBoxHC.SelectedIndex = -1;
        }

        private void buttonFileMau_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Lưu Template CSV",
                FileName = "Thuoc_Template.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        sw.WriteLine("TenThuoc,SDK,IDHoatChat,HamLuong,DangBaoChe,NhaSX,GhiChu");
                        sw.WriteLine("\"Paracetamol 500mg\",\"VD-12345-18\",1,\"500mg\",\"Viên nén\",\"Hà Nội\",\"Ghi chú mẫu\"");
                    }

                    MessageBox.Show("Đã xuất template thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất template: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}