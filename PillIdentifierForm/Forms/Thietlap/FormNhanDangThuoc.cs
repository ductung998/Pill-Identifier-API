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
        BindingSource grid1 = new BindingSource();
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
            cbo.SelectedIndex = -1;

            if (value == null || value == DBNull.Value)
            {
                // For nullable comboboxes, select the first item (empty option)
                if (cbo == cboLoaiViThuoc || cbo == cboLoaiRanh)
                {
                    cbo.SelectedIndex = 0;
                }
                return;
            }

            for (int i = 0; i < cbo.Items.Count; i++)
            {
                ComboBoxItem item = cbo.Items[i] as ComboBoxItem;
                if (item != null && item.Value != null)
                {
                    if (item.Value.ToString() == value.ToString())
                    {
                        cbo.SelectedIndex = i;
                        return;
                    }
                }
            }
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxIDThuoc.Text))
            {
                MessageBox.Show("Vui lòng nhập ID Thuốc.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxIDThuoc.Focus();
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

            ComboBoxItem itemHinhDang = (ComboBoxItem)cboHinhDang.SelectedItem;
            entity.IDHinhDang = Convert.ToInt32(itemHinhDang.Value);

            ComboBoxItem itemDangThuoc = (ComboBoxItem)cboDangThuoc.SelectedItem;
            entity.IDDangThuoc = Convert.ToInt32(itemDangThuoc.Value);

            if (cboLoaiViThuoc.SelectedIndex > 0)
            {
                ComboBoxItem itemViThuoc = (ComboBoxItem)cboLoaiViThuoc.SelectedItem;
                entity.IDLoaiViThuoc = itemViThuoc.Value != null ? (int?)Convert.ToInt32(itemViThuoc.Value) : null;
            }
            else
            {
                entity.IDLoaiViThuoc = null;
            }

            if (cboLoaiRanh.SelectedIndex > 0)
            {
                ComboBoxItem itemRanh = (ComboBoxItem)cboLoaiRanh.SelectedItem;
                entity.IDLoaiRanh = itemRanh.Value != null ? (int?)Convert.ToInt32(itemRanh.Value) : null;
            }
            else
            {
                entity.IDLoaiRanh = null;
            }

            entity.MaHinh = txtMaHinh.Text;

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
        }
        #endregion
        private void FormNhanDangThuoc_Load(object sender, EventArgs e)
        {

        }

        private void LoadDropdowns()
        {
            // Add empty item for nullable fields
            ComboBoxItem emptyItemVi = new ComboBoxItem();
            emptyItemVi.Value = null;
            emptyItemVi.Text = "(Không chọn)";
            cboLoaiViThuoc.Items.Add(emptyItemVi);

            ComboBoxItem emptyItemRanh = new ComboBoxItem();
            emptyItemRanh.Value = null;
            emptyItemRanh.Text = "(Không chọn)";
            cboLoaiRanh.Items.Add(emptyItemRanh);
            // TODO: Load actual data from database
            // Example:
            // var hinhDangs = dal.GetHinhDangs();
            // foreach(var hd in hinhDangs)
            //     cboHinhDang.Items.Add(new ComboBoxItem { Value = hd.ID, Text = hd.Ten });
            cboHinhDang.DataSource = getdata.GetDSHinhDang();
            cboDangThuoc.DataSource = getdata.GetDSDangThuoc();
            cboLoaiViThuoc.DataSource = getdata.GetDSLoaiViThuoc();
            cboLoaiRanh.DataSource = getdata.GetDSLoaiRanh();

            cboHinhDang.DisplayMember = "Text";
            cboHinhDang.ValueMember = "Value";
            cboDangThuoc.DisplayMember = "Text";
            cboDangThuoc.ValueMember = "Value";
            cboLoaiViThuoc.DisplayMember = "Text";
            cboLoaiViThuoc.ValueMember = "Value";
            cboLoaiRanh.DisplayMember = "Text";
            cboLoaiRanh.ValueMember = "Value";
        }

        private void LoadData()
        {
            try
            {
                grid1.DataSource = getdata.GetDSNhanDangThuoc();
                dgvData.DataSource = grid1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if clicked row is valid (not header row)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];

                // Populate textboxes with selected row data
                textBoxIDNhandang.Text = row.Cells["IDThuoc"].Value.ToString();
                textBoxIDThuoc.Text = row.Cells["TenThuoc"].Value.ToString();

                // Enable buttons after selection
                buttonXoa.Enabled = true;
                buttonSua.Enabled = true;
            }
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                NhanDangThuoc entity = GetEntityFromForm();

                bool result = insertdata.InsertNhanDangThuoc(
                    entity.IDThuoc,
                    entity.CoKhacDau,
                    entity.KhacDauMatTruoc,
                    entity.KhacDauMatSau,
                    entity.IDHinhDang,
                    entity.IDDangThuoc,
                    entity.IDLoaiViThuoc,
                    entity.IDLoaiRanh,
                    entity.MaHinh
                );
                

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
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                bool result = updatedata.UpdateNhanDangThuoc(entity.IDNhanDang,
                    entity.IDThuoc,
                    entity.CoKhacDau,
                    entity.KhacDauMatTruoc,
                    entity.KhacDauMatSau,
                    entity.IDHinhDang,
                    entity.IDDangThuoc,
                    entity.IDLoaiViThuoc,
                    entity.IDLoaiRanh,
                    entity.MaHinh);

                if (result)
                {
                    LoadData();
                    ClearForm();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            LoadData();
            ClearForm();
        }

        private void refreshDatagrid()
        {
            grid1.DataSource = getdata.GetDSThuoc();
            dgvData.AutoResizeColumns();
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
                        entity.IDLoaiViThuoc = string.IsNullOrEmpty(values[6].Trim()) ? (int?)null : int.Parse(values[6].Trim());
                        entity.IDLoaiRanh = string.IsNullOrEmpty(values[7].Trim()) ? (int?)null : int.Parse(values[7].Trim());
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

        private void LoadHoatChat()
        {
            List<HoatChat> ds = getdata.GetDSHoatChat().OrderBy(h => h.TenHoatChat).ToList();
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
    }
}
