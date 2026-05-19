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
        private List<MauSac> _listMauSac;

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
            panel1_SizeChanged(this, EventArgs.Empty);
            dataGridViewThuoc.Focus();
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            const int margin = 12;
            const int gap = 5;
            int center = panel1.Width / 2;

            // Block 1 row 1: two equal-width ID textboxes (label | box | label | box)
            int idLeft = textBoxIDNhandang.Left;
            int label2W = label2.PreferredWidth > 0 ? label2.PreferredWidth : 66;
            int idW = Math.Max(30, (center - gap - idLeft - label2W - 2 * gap) / 2);
            textBoxIDNhandang.Width = idW;
            label2.Left = idLeft + idW + gap;
            textBoxIDThuoc.Left = label2.Left + label2W + gap;
            textBoxIDThuoc.Width = idW;

            // Block 1 row 2: two equal-width imprint textboxes (label above each)
            int imprintLeft = txtKhacDauMatTruoc.Left;
            int imprintW = Math.Max(30, (center - gap - imprintLeft - gap) / 2);
            txtKhacDauMatTruoc.Width = imprintW;
            label10.Left = imprintLeft + imprintW + gap;
            txtKhacDauMatSau.Left = imprintLeft + imprintW + gap;
            txtKhacDauMatSau.Width = imprintW;

            // Block 2 row 1: Hình dạng label + combo fill the right half
            label5.Left = center;
            cboHinhDang.Left = center + label5.Width + gap;
            cboHinhDang.Width = Math.Max(50, panel1.Width - cboHinhDang.Left - margin);

            // Block 2 row 2: two equal-width Màu sắc combos fill the right half
            int mauSacTotal = panel1.Width - center - margin;
            int comboW = Math.Max(50, (mauSacTotal - gap) / 2);
            labelMauSac1.Left = center;
            cboMauSac1.Left = center;
            cboMauSac1.Width = comboW;
            labelMauSac2.Left = center + comboW + gap;
            cboMauSac2.Left = center + comboW + gap;
            cboMauSac2.Width = Math.Max(50, panel1.Width - cboMauSac2.Left - margin);
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
            cboHinhDang.SelectedIndex = -1;
            cboDangThuoc.SelectedIndex = -1;
            cboLoaiViThuoc.SelectedIndex = -1;
            cboLoaiRanh.SelectedIndex = -1;
            cboMauSac1.SelectedIndex = -1;
            cboMauSac2.SelectedIndex = -1;
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

        private void LoadMauSacForThuoc(int idThuoc)
        {
            List<MauSac> colors = getdata.GetMauSacbyIDThuoc(idThuoc);
            cboMauSac1.SelectedIndex = -1;
            cboMauSac2.SelectedIndex = -1;
            if (colors.Count >= 1)
                SetComboBoxValue(cboMauSac1, colors[0].IDMauSac);
            if (colors.Count >= 2)
                SetComboBoxValue(cboMauSac2, colors[1].IDMauSac);
        }

        private void SaveMauSacForThuoc(int idThuoc)
        {
            deletedata.DeleteAllThuoc_MauSacByIDThuoc(idThuoc);

            int id1 = cboMauSac1.SelectedValue != null ? Convert.ToInt32(cboMauSac1.SelectedValue) : -1;
            int id2 = cboMauSac2.SelectedValue != null ? Convert.ToInt32(cboMauSac2.SelectedValue) : -1;

            if (id1 > 0)
                insertdata.InsertThuoc_MauSac(new Thuoc_MauSac { IDThuoc = idThuoc, IDMauSac = id1 });
            if (id2 > 0 && id2 != id1)
                insertdata.InsertThuoc_MauSac(new Thuoc_MauSac { IDThuoc = idThuoc, IDMauSac = id2 });
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

                // Load MauSac — add a blank "Không" option at top
                _listMauSac = getdata.GetDSMauSac().OrderBy(m => m.TenMauSac).ToList();
                List<MauSac> mauSacList = new List<MauSac>();
                mauSacList.Add(new MauSac { IDMauSac = -1, TenMauSac = "(Không)" });
                mauSacList.AddRange(_listMauSac);

                cboMauSac1.DataSource = new List<MauSac>(mauSacList);
                cboMauSac1.DisplayMember = "TenMauSac";
                cboMauSac1.ValueMember = "IDMauSac";
                cboMauSac1.SelectedIndex = -1;

                cboMauSac2.DataSource = new List<MauSac>(mauSacList);
                cboMauSac2.DisplayMember = "TenMauSac";
                cboMauSac2.ValueMember = "IDMauSac";
                cboMauSac2.SelectedIndex = -1;

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
                dataGridViewThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridViewThuoc.DataSource = gridThuoc;
                dataGridViewThuoc.Columns["IDThuoc"].HeaderText = "Mã thuốc";
                dataGridViewThuoc.Columns["IDThuoc"].Width = 60;
                dataGridViewThuoc.Columns["TenThuoc"].HeaderText = "Tên thương mại";
                dataGridViewThuoc.Columns["TenThuoc"].Width = 220;
                dataGridViewThuoc.Columns["SDK"].Width = 130;
                dataGridViewThuoc.Columns["IDHoatChat"].HeaderText = "Mã hoạt chất";
                dataGridViewThuoc.Columns["IDHoatChat"].Width = 85;
                dataGridViewThuoc.Columns["TenHoatChat"].HeaderText = "Tên hoạt chất";
                dataGridViewThuoc.Columns["TenHoatChat"].Width = 180;
                dataGridViewThuoc.Columns["HamLuong"].HeaderText = "Hàm lượng";
                dataGridViewThuoc.Columns["HamLuong"].Width = 100;
                dataGridViewThuoc.Columns["DangBaoChe"].HeaderText = "Dạng bào chế";
                dataGridViewThuoc.Columns["DangBaoChe"].Width = 120;
                dataGridViewThuoc.Columns["NhaSX"].HeaderText = "Nhà sản xuất";
                dataGridViewThuoc.Columns["NhaSX"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

                // Bulk-load all drug-color links, resolve names in-memory (no N+1)
                List<Thuoc_MauSac> allLinks = getdata.GetDSThuoc_MauSac();
                var colorByThuoc = allLinks
                    .GroupBy(l => l.IDThuoc)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(l =>
                        {
                            MauSac ms = _listMauSac != null ? _listMauSac.FirstOrDefault(m => m.IDMauSac == l.IDMauSac) : null;
                            return ms != null ? ms.TenMauSac : string.Empty;
                        }).Where(n => !string.IsNullOrEmpty(n)).ToList()
                    );

                var displayData = _listNhanDang.Select(n =>
                {
                    List<string> colors;
                    if (!colorByThuoc.TryGetValue(n.IDThuoc, out colors))
                        colors = new List<string>();
                    return new
                    {
                        n.IDNhanDang,
                        n.IDThuoc,
                        n.CoKhacDau,
                        n.KhacDauMatTruoc,
                        n.KhacDauMatSau,
                        n.IDHinhDang,
                        n.TenHinhDang,
                        n.IDDangThuoc,
                        n.TenDangThuoc,
                        n.IDLoaiViThuoc,
                        n.TenLoaiVi,
                        n.IDLoaiRanh,
                        n.TenLoaiRanh,
                        n.KichThuoc,
                        n.MaHinh,
                        TenMauSac1 = colors.Count >= 1 ? colors[0] : string.Empty,
                        TenMauSac2 = colors.Count >= 2 ? colors[1] : string.Empty
                    };
                }).ToList();

                grid1.DataSource = null;
                grid1.DataSource = displayData;
                dgvData.DataSource = grid1;
                dgvData.AutoResizeColumns();

                dgvData.Columns["IDNhanDang"].HeaderText = "Mã nhận dạng";
                dgvData.Columns["IDThuoc"].HeaderText = "Mã thuốc";
                dgvData.Columns["CoKhacDau"].HeaderText = "Có khắc dấu";
                dgvData.Columns["KhacDauMatTruoc"].HeaderText = "Khắc dấu mặt 1";
                dgvData.Columns["KhacDauMatSau"].HeaderText = "Khắc dấu mặt 2";
                dgvData.Columns["TenHinhDang"].HeaderText = "Tên hình dạng";
                dgvData.Columns["TenDangThuoc"].HeaderText = "Tên dạng thuốc";
                dgvData.Columns["TenLoaiVi"].HeaderText = "Tên loại vỉ";
                dgvData.Columns["TenLoaiRanh"].HeaderText = "Tên loại rãnh";
                dgvData.Columns["KichThuoc"].HeaderText = "Kích thước";
                dgvData.Columns["TenMauSac1"].HeaderText = "Màu sắc 1";
                dgvData.Columns["TenMauSac2"].HeaderText = "Màu sắc 2";

                // Hide ID columns or configure them
                dgvData.Columns["IDHinhDang"].Visible = false;
                dgvData.Columns["IDDangThuoc"].Visible = false;
                dgvData.Columns["IDLoaiViThuoc"].Visible = false;
                dgvData.Columns["IDLoaiRanh"].Visible = false;
                dgvData.Columns["MaHinh"].Visible = false;

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
                    SaveMauSacForThuoc(entity.IDThuoc);
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
                    SaveMauSacForThuoc(entity.IDThuoc);
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

                        if (values.Length < 8)
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

                textBoxKichThuoc.Text = row.Cells["KichThuoc"].Value != null ? row.Cells["KichThuoc"].Value.ToString() : "";

                // Set combobox values
                SetComboBoxValue(cboHinhDang, row.Cells["IDHinhDang"].Value);
                SetComboBoxValue(cboDangThuoc, row.Cells["IDDangThuoc"].Value);
                SetComboBoxValue(cboLoaiViThuoc, row.Cells["IDLoaiViThuoc"].Value);
                SetComboBoxValue(cboLoaiRanh, row.Cells["IDLoaiRanh"].Value);

                if (row.Cells["IDThuoc"].Value != null)
                    LoadMauSacForThuoc(Convert.ToInt32(row.Cells["IDThuoc"].Value));

                buttonXoa.Enabled = true;
                buttonSua.Enabled = true;
                dgvData.Focus();
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
                    string header = "IDThuoc,CoKhacDau,KhacDauMatTruoc,KhacDauMatSau,IDHinhDang,IDDangThuoc,IDLoaiViThuoc,IDLoaiRanh";
                    string example = "1,true,ABC123,XYZ456,1,1,1,1";

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
                        textBoxKichThuoc.Text = existing.KichThuoc.ToString();

                        SetComboBoxValue(cboHinhDang, existing.IDHinhDang);
                        SetComboBoxValue(cboDangThuoc, existing.IDDangThuoc);
                        SetComboBoxValue(cboLoaiViThuoc, existing.IDLoaiViThuoc);
                        SetComboBoxValue(cboLoaiRanh, existing.IDLoaiRanh);
                        LoadMauSacForThuoc(idThuoc);

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
                        cboHinhDang.SelectedIndex = -1;
                        cboDangThuoc.SelectedIndex = -1;
                        cboLoaiViThuoc.SelectedIndex = -1;
                        cboLoaiRanh.SelectedIndex = -1;
                        cboMauSac1.SelectedIndex = -1;
                        cboMauSac2.SelectedIndex = -1;

                        buttonXoa.Enabled = false;
                        buttonSua.Enabled = false;
                    }
                    dataGridViewThuoc.Focus();
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

    }
}