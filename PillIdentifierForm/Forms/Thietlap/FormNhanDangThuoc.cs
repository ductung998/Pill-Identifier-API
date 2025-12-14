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
    public partial class FormNhanDangThuoc : Form
    {
        private List<Thuoc> _listThuoc;
        private List<HoatChat> _listHoatChat;
        private List<NhanDangThuoc> _listNhanDang;

        BindingSource grid1 = new BindingSource();
        BindingSource gridThuoc = new BindingSource();

        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.InsertData insertdata = new KetnoiDB.InsertData();
        KetnoiDB.UpdateData updatedata = new KetnoiDB.UpdateData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();

        public FormNhanDangThuoc()
        {
            InitializeComponent();
            LoadDropdowns();
            LoadData();
            txtKhacDauMatTruoc.Enabled = false;
            txtKhacDauMatSau.Enabled = false;
        }

        private void FormNhanDangThuoc_Load(object sender, EventArgs e)
        {
            ClearForm();
        }

        #region helper

        public class ComboBoxItem
        {
            public object Value { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void SetComboBoxValue(ComboBox cbo, object value)
        {
            if (value == null || value == DBNull.Value)
            {
                cbo.SelectedIndex = -1;
                return;
            }

            // For comboboxes with class objects as datasource
            if (cbo.DataSource != null)
            {
                for (int i = 0; i < cbo.Items.Count; i++)
                {
                    object item = cbo.Items[i];

                    // Get the ValueMember property value
                    var valueProperty = item.GetType().GetProperty(cbo.ValueMember);
                    if (valueProperty != null)
                    {
                        object itemValue = valueProperty.GetValue(item, null);
                        if (itemValue != null && itemValue.ToString() == value.ToString())
                        {
                            cbo.SelectedIndex = i;
                            return;
                        }
                    }
                }
            }

            cbo.SelectedIndex = -1;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxIDThuoc.Text))
            {
                MessageBox.Show("Vui lòng chọn Thuốc từ danh sách.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboHinhDang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Hình Dạng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHinhDang.Focus();
                return false;
            }

            if (cboDangThuoc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Dạng Thuốc.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDangThuoc.Focus();
                return false;
            }

            return true;
        }

        private NhanDangThuoc GetEntityFromForm()
        {
            NhanDangThuoc entity = new NhanDangThuoc();
            entity.IDThuoc = int.Parse(textBoxIDThuoc.Text);
            entity.CoKhacDau = chkCoKhacDau.Checked;
            entity.KhacDauMatTruoc = txtKhacDauMatTruoc.Text;
            entity.KhacDauMatSau = txtKhacDauMatSau.Text;

            // Get values from comboboxes with class object datasource
            entity.IDHinhDang = Convert.ToInt32(cboHinhDang.SelectedValue);
            entity.IDDangThuoc = Convert.ToInt32(cboDangThuoc.SelectedValue);

            entity.IDLoaiViThuoc = cboLoaiViThuoc.SelectedValue != null && cboLoaiViThuoc.SelectedIndex >= 0
                ? Convert.ToInt32(cboLoaiViThuoc.SelectedValue)
                : 0;

            entity.IDLoaiRanh = cboLoaiRanh.SelectedValue != null && cboLoaiRanh.SelectedIndex >= 0
                ? Convert.ToInt32(cboLoaiRanh.SelectedValue)
                : 0;

            entity.MaHinh = txtMaHinh.Text;
            entity.KichThuoc = textBoxKichThuoc.Text != "" ? Convert.ToDouble(textBoxKichThuoc.Text) : 0;

            return entity;
        }

        private void ClearForm()
        {
            textBoxIDNhandang.Clear();
            textBoxIDThuoc.Clear();
            chkCoKhacDau.Checked = false;
            txtKhacDauMatTruoc.Clear();
            txtKhacDauMatSau.Clear();
            txtMaHinh.Clear();
            cboHinhDang.SelectedIndex = -1;
            cboDangThuoc.SelectedIndex = -1;
            cboLoaiViThuoc.SelectedIndex = -1;
            cboLoaiRanh.SelectedIndex = -1;
            comboBoxFilterHoatChat.SelectedIndex = -1;

            buttonXoa.Enabled = false;
            buttonSua.Enabled = false;
        }

        private string GetTenHoatChat(int idHoatChat)
        {
            if (_listHoatChat == null || _listHoatChat.Count == 0)
                return "";

            HoatChat hc = _listHoatChat.FirstOrDefault(h => h.IDHoatChat == idHoatChat);
            return hc != null ? hc.TenHoatChat : "";
        }
        #endregion

        private void LoadDropdowns()
        {
            try
            {
                // Load HinhDang
                List<HinhDang> dsHinhDang = getdata.GetDSHinhDang().OrderBy(h => h.TenHinhDang).ToList();
                cboHinhDang.DataSource = dsHinhDang;
                cboHinhDang.DisplayMember = "TenHinhDang";
                cboHinhDang.ValueMember = "IDHinhDang";
                cboHinhDang.SelectedIndex = -1;

                // Load DangThuoc
                List<DangThuoc> dsDangThuoc = getdata.GetDSDangThuoc().OrderBy(d => d.TenDangThuoc).ToList();
                cboDangThuoc.DataSource = dsDangThuoc;
                cboDangThuoc.DisplayMember = "TenDangThuoc";
                cboDangThuoc.ValueMember = "IDDangThuoc";
                cboDangThuoc.SelectedIndex = -1;

                // Load LoaiViThuoc with empty option
                List<LoaiViThuoc> dsLoaiVi = getdata.GetDSLoaiViThuoc().OrderBy(v => v.TenLoaiVi).ToList();
                cboLoaiViThuoc.DataSource = dsLoaiVi;
                cboLoaiViThuoc.DisplayMember = "TenLoaiVi";
                cboLoaiViThuoc.ValueMember = "IDLoaiViThuoc";
                cboLoaiViThuoc.SelectedIndex = -1;

                // Load LoaiRanh with empty option
                List<LoaiRanh> dsLoaiRanh = getdata.GetDSLoaiRanh().OrderBy(r => r.TenLoaiRanh).ToList();
                cboLoaiRanh.DataSource = dsLoaiRanh;
                cboLoaiRanh.DisplayMember = "TenLoaiRanh";
                cboLoaiRanh.ValueMember = "IDLoaiRanh";
                cboLoaiRanh.SelectedIndex = -1;

                // Load HoatChat for filter - add "All" option at the beginning
                _listHoatChat = getdata.GetDSHoatChat().OrderBy(h => h.TenHoatChat).ToList();

                // Create a list with "All" option
                List<HoatChat> filterList = new List<HoatChat>();
                filterList.Add(new HoatChat { IDHoatChat = -1, TenHoatChat = "(Tất cả)" });
                filterList.AddRange(_listHoatChat);

                comboBoxFilterHoatChat.DataSource = filterList;
                comboBoxFilterHoatChat.DisplayMember = "TenHoatChat";
                comboBoxFilterHoatChat.ValueMember = "IDHoatChat";
                comboBoxFilterHoatChat.SelectedIndex = 0; // Select "All" by default
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThuoc()
        {
            try
            {
                // Get all Thuoc
                _listThuoc = getdata.GetDSThuoc().OrderBy(h => h.TenThuoc).ToList();

                // Filter if checkbox is checked
                List<Thuoc> displayList = _listThuoc;

                if (checkBoxFilterNotAssigned.Checked)
                {
                    // Get list of IDThuoc that have NhanDang
                    List<int> assignedIDs = _listNhanDang.Select(n => n.IDThuoc).Distinct().ToList();
                    // Filter to show only Thuoc without NhanDang
                    displayList = displayList.Where(t => !assignedIDs.Contains(t.IDThuoc)).ToList();
                }

                // Filter by HoatChat if selected
                if (comboBoxFilterHoatChat.SelectedValue != null &&
                    comboBoxFilterHoatChat.SelectedValue is int)
                {
                    int selectedHoatChatID = (int)comboBoxFilterHoatChat.SelectedValue;

                    // Only filter if not "All" (-1)
                    if (selectedHoatChatID != -1)
                    {
                        displayList = displayList.Where(t => t.IDHoatChat == selectedHoatChatID).ToList();
                    }
                }

                // Create display list with HoatChat name
                var displayData = displayList.Select(t => new
                {
                    IDThuoc = t.IDThuoc,
                    TenThuoc = t.TenThuoc,
                    SDK = t.SDK,
                    IDHoatChat = t.IDHoatChat,
                    TenHoatChat = GetTenHoatChat(t.IDHoatChat),
                    HamLuong = t.HamLuong,
                    DangBaoChe = t.DangBaoChe,
                    NhaSX = t.NhaSX
                }).ToList();

                gridThuoc.DataSource = null;
                gridThuoc.DataSource = displayData;
                dataGridViewThuoc.DataSource = gridThuoc;
                dataGridViewThuoc.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Thuốc: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // Load NhanDang data
                _listNhanDang = getdata.GetDSNhanDangThuoc();
                grid1.DataSource = _listNhanDang;
                dgvData.DataSource = grid1;
                dgvData.AutoResizeColumns();

                // Hide ID columns or configure them
                dgvData.Columns["IDHinhDang"].Visible = false;
                dgvData.Columns["IDDangThuoc"].Visible = false;
                dgvData.Columns["IDLoaiViThuoc"].Visible = false;
                dgvData.Columns["IDLoaiRanh"].Visible = false;

                // Load Thuoc list
                LoadThuoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handled by SelectionChanged event
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                NhanDangThuoc entity = GetEntityFromForm();

                bool result = insertdata.InsertNhanDangThuoc(entity);

                if (result)
                {
                    LoadData();
                    ClearForm();
                    MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm mới thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxIDNhandang.Text))
                {
                    MessageBox.Show("Vui lòng chọn bản ghi cần xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int id = int.Parse(textBoxIDNhandang.Text);
                    bool kq = deletedata.DeleteNhanDangThuoc(id);

                    if (kq)
                    {
                        LoadData();
                        ClearForm();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỏi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxIDNhandang.Text))
                {
                    MessageBox.Show("Vui lòng chọn bản ghi cần cập nhật.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput()) return;

                NhanDangThuoc entity = GetEntityFromForm();
                entity.IDNhanDang = int.Parse(textBoxIDNhandang.Text);

                bool result = updatedata.UpdateNhanDangThuoc(entity);

                if (result)
                {
                    LoadData();
                    ClearForm();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            ofd.Title = "Chọn File CSV";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImportCSV(ofd.FileName);
            }
        }

        private void buttonXoatrang_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ImportCSV(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                if (lines.Length < 2)
                {
                    MessageBox.Show("File CSV không có dữ liệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<NhanDangThuoc> entities = new List<NhanDangThuoc>();
                int successCount = 0;
                int errorCount = 0;
                StringBuilder errors = new StringBuilder();

                // Skip header row (line 0)
                for (int i = 1; i < lines.Length; i++)
                {
                    try
                    {
                        string[] values = lines[i].Split(',');

                        if (values.Length < 9)
                        {
                            errors.AppendLine("Dòng " + (i + 1).ToString() + ": Không đủ cột dữ liệu");
                            errorCount++;
                            continue;
                        }

                        NhanDangThuoc entity = new NhanDangThuoc();
                        entity.IDThuoc = int.Parse(values[0].Trim());
                        entity.CoKhacDau = bool.Parse(values[1].Trim());
                        entity.KhacDauMatTruoc = values[2].Trim();
                        entity.KhacDauMatSau = values[3].Trim();
                        entity.IDHinhDang = int.Parse(values[4].Trim());
                        entity.IDDangThuoc = int.Parse(values[5].Trim());
                        entity.IDLoaiViThuoc = string.IsNullOrEmpty(values[6].Trim()) ? 0 : int.Parse(values[6].Trim());
                        entity.IDLoaiRanh = string.IsNullOrEmpty(values[7].Trim()) ? 0 : int.Parse(values[7].Trim());
                        entity.MaHinh = values[8].Trim();

                        entities.Add(entity);
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine("Dòng " + (i + 1).ToString() + ": " + ex.Message);
                        errorCount++;
                    }
                }

                if (entities.Count > 0)
                {
                    bulkInsert.BulkInsertNhanDangThuoc(entities);
                    LoadData();
                }

                string message = "Kết quả import:\n- Thành công: " + successCount.ToString() + "\n- Lỗi: " + errorCount.ToString();
                if (errors.Length > 0)
                {
                    message += "\n\nChi tiết lỗi:\n" + errors.ToString();
                }

                MessageBox.Show(message, "Kết quả Import", MessageBoxButtons.OK,
                    errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi import CSV: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxThuoc_TextChanged(object sender, EventArgs e)
        {
            buttonThem.Enabled = !string.IsNullOrWhiteSpace(textBoxIDThuoc.Text);
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvData.SelectedRows[0];

                textBoxIDNhandang.Text = row.Cells["IDNhanDang"].Value != null ? row.Cells["IDNhanDang"].Value.ToString() : "";
                textBoxIDThuoc.Text = row.Cells["IDThuoc"].Value != null ? row.Cells["IDThuoc"].Value.ToString() : "";
                chkCoKhacDau.Checked = row.Cells["CoKhacDau"].Value != null ? Convert.ToBoolean(row.Cells["CoKhacDau"].Value) : false;
                txtKhacDauMatTruoc.Text = row.Cells["KhacDauMatTruoc"].Value != null ? row.Cells["KhacDauMatTruoc"].Value.ToString() : "";
                txtKhacDauMatSau.Text = row.Cells["KhacDauMatSau"].Value != null ? row.Cells["KhacDauMatSau"].Value.ToString() : "";
                txtMaHinh.Text = row.Cells["MaHinh"].Value != null ? row.Cells["MaHinh"].Value.ToString() : "";

                // Set combobox values
                SetComboBoxValue(cboHinhDang, row.Cells["IDHinhDang"].Value);
                SetComboBoxValue(cboDangThuoc, row.Cells["IDDangThuoc"].Value);
                SetComboBoxValue(cboLoaiViThuoc, row.Cells["IDLoaiViThuoc"].Value);
                SetComboBoxValue(cboLoaiRanh, row.Cells["IDLoaiRanh"].Value);

                buttonXoa.Enabled = true;
                buttonSua.Enabled = true;
            }
        }

        private void chkCoKhacDau_CheckedChanged(object sender, EventArgs e)
        {
            txtKhacDauMatTruoc.Enabled = chkCoKhacDau.Checked;
            txtKhacDauMatSau.Enabled = chkCoKhacDau.Checked;

            if (!chkCoKhacDau.Checked)
            {
                txtKhacDauMatTruoc.Clear();
                txtKhacDauMatSau.Clear();
            }
        }

        private void buttonFileMau_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Files (*.csv)|*.csv";
            sfd.Title = "Lưu Template CSV";
            sfd.FileName = "NhanDangThuoc_Template.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string header = "IDThuoc,CoKhacDau,KhacDauMatTruoc,KhacDauMatSau,IDHinhDang,IDDangThuoc,IDLoaiViThuoc,IDLoaiRanh,MaHinh";
                    string example = "1,true,ABC123,XYZ456,1,1,1,1,MH001";

                    File.WriteAllText(sfd.FileName, header + "\n" + example, Encoding.UTF8);

                    MessageBox.Show("Đã xuất template thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất template: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if clicked row is valid (not header row)
                if (e.RowIndex < 0) return;

                DataGridViewRow row = dataGridViewThuoc.Rows[e.RowIndex];

                // Get IDThuoc from selected row
                if (row.Cells["IDThuoc"].Value != null)
                {
                    int idThuoc = Convert.ToInt32(row.Cells["IDThuoc"].Value);
                    textBoxIDThuoc.Text = idThuoc.ToString();

                    // Check if this Thuoc already has NhanDang
                    NhanDangThuoc existing = _listNhanDang.FirstOrDefault(n => n.IDThuoc == idThuoc);

                    if (existing != null)
                    {
                        // Load existing NhanDang data
                        textBoxIDNhandang.Text = getdata.GetNhanDangByThuoc(idThuoc).IDNhanDang.ToString();
                        chkCoKhacDau.Checked = existing.CoKhacDau;
                        txtKhacDauMatTruoc.Text = existing.KhacDauMatTruoc;
                        txtKhacDauMatSau.Text = existing.KhacDauMatSau;
                        txtMaHinh.Text = existing.MaHinh;
                        textBoxKichThuoc.Text = existing.KichThuoc.ToString();

                        SetComboBoxValue(cboHinhDang, existing.IDHinhDang);
                        SetComboBoxValue(cboDangThuoc, existing.IDDangThuoc);
                        SetComboBoxValue(cboLoaiViThuoc, existing.IDLoaiViThuoc);
                        SetComboBoxValue(cboLoaiRanh, existing.IDLoaiRanh);

                        buttonXoa.Enabled = true;
                        buttonSua.Enabled = true;
                    }
                    else
                    {
                        // Clear form for new entry
                        textBoxIDNhandang.Clear();
                        chkCoKhacDau.Checked = false;
                        txtKhacDauMatTruoc.Clear();
                        txtKhacDauMatSau.Clear();
                        txtMaHinh.Clear();
                        cboHinhDang.SelectedIndex = -1;
                        cboDangThuoc.SelectedIndex = -1;
                        cboLoaiViThuoc.SelectedIndex = -1;
                        cboLoaiRanh.SelectedIndex = -1;

                        buttonXoa.Enabled = false;
                        buttonSua.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn thuốc: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxFilterNotAssigned_CheckedChanged(object sender, EventArgs e)
        {
            LoadThuoc();
        }

        private void comboBoxFilterHoatChat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThuoc();
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a record is selected
                if (dgvData.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một dòng trước khi thêm hình ảnh.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the IDNhanDang from the selected row
                int idNhanDang = Convert.ToInt32(dgvData.CurrentRow.Cells["IDNhanDang"].Value);

                // Open file dialog to select image
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "Chọn hình ảnh thuốc";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the file extension
                    string fileExtension = Path.GetExtension(openFileDialog.FileName);

                    // Get the drugs folder using the new method
                    string drugsFolder = getdata.GetDrugImagesFolder();

                    // Create the new filename using IDNhanDang
                    string newFileName = idNhanDang.ToString() + fileExtension;
                    string destinationPath = Path.Combine(drugsFolder, newFileName);

                    // Check if file already exists
                    if (File.Exists(destinationPath))
                    {
                        DialogResult result = MessageBox.Show(
                            "Hình ảnh cho ID này đã tồn tại. Bạn có muốn ghi đè?",
                            "Xác nhận",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }

                    // Copy the file to the destination
                    File.Copy(openFileDialog.FileName, destinationPath, true);

                    // Update the textbox with the new filename
                    txtMaHinh.Text = newFileName;

                    MessageBox.Show("Đã lưu hình ảnh thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hình ảnh: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}