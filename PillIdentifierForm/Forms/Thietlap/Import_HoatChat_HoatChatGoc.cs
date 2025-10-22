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

        BindingSource grid1 = new BindingSource();

        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();
        public ImportHoatChat_HoatChatGoc()
        {
            InitializeComponent();
        }

        private void ImportHoatChat_HoatChatGoc_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            LoadHoatChat();
            LoadHoatChatGoc();

            dataGridView1.DataSource = grid1;
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            if (_listLienKet.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Tìm thấy " + _listLienKet.Count.ToString() + " dòng dữ liệu. Bạn có muốn import?",
                    "Xác nhận import",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (bulkInsert.BulkInsertHoatChat_HoatChatGoc(_listLienKet))
                    {
                        MessageBox.Show("Import thành công " + _listLienKet.Count.ToString() + " bản ghi!", "Thông báo",
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

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "CSV Files|*.csv",
                    Title = "Chọn file để import (Cột 1: IDHoatChat, Cột 2: IDHoatChatGoc)"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(filePath).ToLower();

                    ImportFromCSV(filePath);
                }
                grid1.DataSource = _listLienKet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshDatagrid()
        {
            grid1.DataSource = getdata.GetDSHoatChat();
            dataGridView1.AutoResizeColumns();
        }

        private void ImportFromCSV(string filePath)
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
                        HoatChat_HoatChatGoc cd = new HoatChat_HoatChatGoc();
                        cd.IDHoatChat = Convert.ToInt32(values[0].Trim().Trim('"'));
                        cd.IDHoatChatGoc = Convert.ToInt32(values.Length > 1 ? values[1].Trim().Trim('"') : "");
                        _listLienKet.Add(cd);
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
            _listHoatChatGoc = getdata.GetDSHoatChatGoc().OrderBy(h => h.TenHoatChat).ToList();
        }
    }
}
