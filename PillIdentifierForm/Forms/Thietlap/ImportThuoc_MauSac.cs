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
    public partial class ImportThuoc_MauSac : Form
    {
        private List<Thuoc> _listThuoc;
        private List<MauSac> _listMauSac;
        private List<Thuoc_MauSac> _listLienKet;
        private List<Thuoc_MauSac> _listTong;

        BindingSource grid1 = new BindingSource();
        BindingSource gridTong = new BindingSource();

        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();

        public ImportThuoc_MauSac()
        {
            InitializeComponent();
        }

        private void ImportThuoc_MauSac_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            LoadThuoc();
            LoadMauSac();
            LoadListLienKet();

            dataGridView1.DataSource = grid1;
            dataGridViewTong.DataSource = gridTong;
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (_listLienKet == null || _listLienKet.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để import!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate data before import
                StringBuilder errors = new StringBuilder();
                int errorCount = 0;

                for (int i = 0; i < _listLienKet.Count; i++)
                {
                    Thuoc_MauSac item = _listLienKet[i];

                    // Check if Thuoc exists
                    if (!_listThuoc.Any(h => h.IDThuoc == item.IDThuoc))
                    {
                        errors.AppendLine("Dòng " + (i + 1).ToString() + ": IDThuoc " + item.IDThuoc.ToString() + " không tồn tại");
                        errorCount++;
                    }

                    // Check if MauSac exists
                    if (!_listMauSac.Any(h => h.IDMauSac == item.IDMauSac))
                    {
                        errors.AppendLine("Dòng " + (i + 1).ToString() + ": IDMauSac " + item.IDMauSac.ToString() + " không tồn tại");
                        errorCount++;
                    }
                }

                if (errorCount > 0)
                {
                    MessageBox.Show("Tìm thấy " + errorCount.ToString() + " lỗi:\n\n" + errors.ToString(),
                        "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Tìm thấy " + _listLienKet.Count.ToString() + " dòng dữ liệu hợp lệ.\n\n" +
                    "Lưu ý: Các liên kết trùng lặp sẽ bị bỏ qua.\n\n" +
                    "Bạn có muốn import?",
                    "Xác nhận import",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (bulkInsert.BulkInsertThuoc_MauSac(_listLienKet))
                    {
                        MessageBox.Show("Import thành công " + _listLienKet.Count.ToString() + " bản ghi!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the import grid
                        _listLienKet.Clear();
                        grid1.DataSource = null;
                        grid1.DataSource = _listLienKet;

                        // Refresh the total grid if it's loaded
                        if (_listTong != null && _listTong.Count > 0)
                        {
                            buttonGetTong_Click(null, null);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Import thất bại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi import: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                    Title = "Chọn file để import"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Clear previous data
                    LoadListLienKet();

                    ImportFromCSV(filePath);

                    // Display imported data with names
                    DisplayImportedData();

                    MessageBox.Show("Đã đọc " + _listLienKet.Count.ToString() + " dòng dữ liệu từ file.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadListLienKet()
        {
            _listLienKet = new List<Thuoc_MauSac>();
        }

        private void DisplayImportedData()
        {
            var displayList = _listLienKet.Select(l => new
            {
                IDThuoc = l.IDThuoc,
                TenThuoc = GetTenThuoc(l.IDThuoc),
                IDMauSac = l.IDMauSac,
                TenMauSac = GetTenMauSac(l.IDMauSac)
            }).ToList();

            grid1.DataSource = null;
            grid1.DataSource = displayList;
            dataGridView1.AutoResizeColumns();
        }

        private void ImportFromCSV(string filePath)
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

                        if (values.Length >= 2 &&
                            !string.IsNullOrWhiteSpace(values[0]) &&
                            !string.IsNullOrWhiteSpace(values[1]))
                        {
                            Thuoc_MauSac cd = new Thuoc_MauSac();
                            cd.IDThuoc = Convert.ToInt32(values[0].Trim().Trim('"'));
                            cd.IDMauSac = Convert.ToInt32(values[1].Trim().Trim('"'));
                            _listLienKet.Add(cd);
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

        private void buttonGDThuoc_Click(object sender, EventArgs e)
        {
            using (DanhmucThuoc formcon = new DanhmucThuoc())
            {
                formcon.ShowDialog();
                LoadThuoc();
            }
        }

        private void LoadThuoc()
        {
            _listThuoc = getdata.GetDSThuoc().OrderBy(h => h.TenThuoc).ToList();
        }

        private void LoadMauSac()
        {
            _listMauSac = getdata.GetDSMauSac().OrderBy(h => h.TenMauSac).ToList();
        }

        private string GetTenThuoc(int id)
        {
            Thuoc hc = _listThuoc.FirstOrDefault(h => h.IDThuoc == id);
            return hc != null ? hc.TenThuoc : "(Không tìm thấy ID: " + id.ToString() + ")";
        }

        private string GetTenMauSac(int id)
        {
            MauSac hcg = _listMauSac.FirstOrDefault(g => g.IDMauSac == id);
            return hcg != null ? hcg.TenMauSac : "(Không tìm thấy ID: " + id.ToString() + ")";
        }

        private void buttonGetTong_Click(object sender, EventArgs e)
        {
            try
            {
                // Get all existing relationships from database
                _listTong = getdata.GetDSThuoc_MauSac();

                if (_listTong == null || _listTong.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong cơ sở dữ liệu.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gridTong.DataSource = null;
                    return;
                }

                // Display with names
                var displayList = _listTong.Select(l => new
                {
                    IDThuoc = l.IDThuoc,
                    TenThuoc = GetTenThuoc(l.IDThuoc),
                    IDMauSac = l.IDMauSac,
                    TenMauSac = GetTenMauSac(l.IDMauSac)
                }).ToList();

                gridTong.DataSource = null;
                gridTong.DataSource = displayList;
                dataGridViewTong.AutoResizeColumns();

                MessageBox.Show("Đã tải " + _listTong.Count.ToString() + " bản ghi từ cơ sở dữ liệu.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExportCSVTong_Click(object sender, EventArgs e)
        {
            try
            {
                if (_listTong == null || _listTong.Count == 0)
                {
                    MessageBox.Show("Vui lòng nhấn 'Lấy tổng' trước khi xuất file.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv",
                    Title = "Lưu file CSV",
                    FileName = "Thuoc_MauSac_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // Write header
                        sw.WriteLine("IDThuoc,TenThuoc,IDMauSac,TenMauSac");

                        // Write data
                        foreach (Thuoc_MauSac item in _listTong)
                        {
                            string tenHC = GetTenThuoc(item.IDThuoc);
                            string tenHCG = GetTenMauSac(item.IDMauSac);

                            sw.WriteLine(string.Format("{0},\"{1}\",{2},\"{3}\"",
                                item.IDThuoc,
                                tenHC.Replace("\"", "\"\""), // Escape quotes
                                item.IDMauSac,
                                tenHCG.Replace("\"", "\"\"")));
                        }
                    }

                    MessageBox.Show("Đã xuất " + _listTong.Count.ToString() + " bản ghi ra file CSV!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonFileMau_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Lưu Template CSV",
                FileName = "Thuoc_MauSac_Template.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        sw.WriteLine("IDThuoc,IDMauSac");
                        sw.WriteLine("1,1");
                        sw.WriteLine("1,2");
                        sw.WriteLine("2,3");
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