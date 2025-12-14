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
    public partial class ImportHoatChat_HoatChatGoc : Form
    {
        private List<HoatChat> _listHoatChat;
        private List<HoatChatGoc> _listHoatChatGoc;
        private List<HoatChat_HoatChatGoc> _listLienKet;
        private List<HoatChat_HoatChatGoc> _listTong;

        BindingSource grid1 = new BindingSource();
        BindingSource gridTong = new BindingSource();

        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();

        public ImportHoatChat_HoatChatGoc()
        {
            InitializeComponent();
        }

        private void ImportHoatChat_HoatChatGoc_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            LoadHoatChat();
            LoadHoatChatGoc();
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
                    HoatChat_HoatChatGoc item = _listLienKet[i];

                    // Check if HoatChat exists
                    if (!_listHoatChat.Any(h => h.IDHoatChat == item.IDHoatChat))
                    {
                        errors.AppendLine("Dòng " + (i + 1).ToString() + ": IDHoatChat " + item.IDHoatChat.ToString() + " không tồn tại");
                        errorCount++;
                    }

                    // Check if HoatChatGoc exists
                    if (!_listHoatChatGoc.Any(h => h.IDHoatChatGoc == item.IDHoatChatGoc))
                    {
                        errors.AppendLine("Dòng " + (i + 1).ToString() + ": IDHoatChatGoc " + item.IDHoatChatGoc.ToString() + " không tồn tại");
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
                    if (bulkInsert.BulkInsertHoatChat_HoatChatGoc(_listLienKet))
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
            _listLienKet = new List<HoatChat_HoatChatGoc>();
        }

        private void DisplayImportedData()
        {
            var displayList = _listLienKet.Select(l => new
            {
                IDHoatChat = l.IDHoatChat,
                TenHoatChat = GetTenHoatChat(l.IDHoatChat),
                IDHoatChatGoc = l.IDHoatChatGoc,
                TenHoatChatGoc = GetTenHoatChatGoc(l.IDHoatChatGoc)
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
                            HoatChat_HoatChatGoc cd = new HoatChat_HoatChatGoc();
                            cd.IDHoatChat = Convert.ToInt32(values[0].Trim().Trim('"'));
                            cd.IDHoatChatGoc = Convert.ToInt32(values[1].Trim().Trim('"'));
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

        private void buttonGDHoatChat_Click(object sender, EventArgs e)
        {
            using (DanhmucHoatChat formcon = new DanhmucHoatChat())
            {
                formcon.ShowDialog();
                LoadHoatChat();
            }
        }

        private void LoadHoatChat()
        {
            _listHoatChat = getdata.GetDSHoatChat().OrderBy(h => h.TenHoatChat).ToList();
        }

        private void LoadHoatChatGoc()
        {
            _listHoatChatGoc = getdata.GetDSHoatChatGoc().OrderBy(h => h.TenHoatChatGoc).ToList();
        }

        private string GetTenHoatChat(int id)
        {
            HoatChat hc = _listHoatChat.FirstOrDefault(h => h.IDHoatChat == id);
            return hc != null ? hc.TenHoatChat : "(Không tìm thấy ID: " + id.ToString() + ")";
        }

        private string GetTenHoatChatGoc(int id)
        {
            HoatChatGoc hcg = _listHoatChatGoc.FirstOrDefault(g => g.IDHoatChatGoc == id);
            return hcg != null ? hcg.TenHoatChatGoc : "(Không tìm thấy ID: " + id.ToString() + ")";
        }

        private void buttonGetTong_Click(object sender, EventArgs e)
        {
            try
            {
                // Get all existing relationships from database
                _listTong = getdata.GetDSHoatChat_HoatChatGoc();

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
                    IDHoatChat = l.IDHoatChat,
                    TenHoatChat = GetTenHoatChat(l.IDHoatChat),
                    IDHoatChatGoc = l.IDHoatChatGoc,
                    TenHoatChatGoc = GetTenHoatChatGoc(l.IDHoatChatGoc)
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
                    FileName = "HoatChat_HoatChatGoc_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // Write header
                        sw.WriteLine("IDHoatChat,TenHoatChat,IDHoatChatGoc,TenHoatChatGoc");

                        // Write data
                        foreach (HoatChat_HoatChatGoc item in _listTong)
                        {
                            string tenHC = GetTenHoatChat(item.IDHoatChat);
                            string tenHCG = GetTenHoatChatGoc(item.IDHoatChatGoc);

                            sw.WriteLine(string.Format("{0},\"{1}\",{2},\"{3}\"",
                                item.IDHoatChat,
                                tenHC.Replace("\"", "\"\""), // Escape quotes
                                item.IDHoatChatGoc,
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
                FileName = "HoatChat_HoatChatGoc_Template.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        sw.WriteLine("IDHoatChat,IDHoatChatGoc");
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