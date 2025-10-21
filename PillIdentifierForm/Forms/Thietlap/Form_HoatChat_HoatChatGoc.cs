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
    public partial class FormHoatChat_HoatChatGoc : Form
    {
        private List<HoatChat> _listHoatChat;
        private List<HoatChatGoc> _listHoatChatGoc;
        private List<HoatChat_HoatChatGoc> _listLienKet;
        HoatChat workingHC;

        BindingSource grid1 = new BindingSource();

        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.InsertData insertdata = new KetnoiDB.InsertData();
        KetnoiDB.UpdateData updatedata = new KetnoiDB.UpdateData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();
        public FormHoatChat_HoatChatGoc()
        {
            InitializeComponent();
        }

        private void FormHoatChat_HoatChatGoc_Load(object sender, EventArgs e)
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
            try
            {

                deletedata.DeleteHoatChat_HoatChatGoc(workingHC.IDHoatChat);

                foreach (HoatChat_HoatChatGoc i in _listLienKet)
                {
                    insertdata.InsertHoatChat_HoatChatGoc(i.IDHoatChat, i.IDHoatChatGoc);
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
                    Title = "Chọn file để import (Cột 1: TenHoatChat, Cột 2: MoTa)"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(filePath).ToLower();

                    List<HoatChat> listHoatChat = new List<HoatChat>();
                    
                    ImportFromCSV(filePath, listHoatChat);

                    if (listHoatChat.Count > 0)
                    {
                        DialogResult result = MessageBox.Show(
                            "Tìm thấy " + listHoatChat.Count.ToString() + " dòng dữ liệu. Bạn có muốn import?",
                            "Xác nhận import",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            if (bulkInsert.BulkInsertHoatChat(listHoatChat))
                            {
                                MessageBox.Show("Import thành công " + listHoatChat.Count.ToString() + " bản ghi!", "Thông báo",
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

        private void refreshDatagrid()
        {
            grid1.DataSource = getdata.GetDSHoatChat();
            dataGridView1.AutoResizeColumns();
        }

        private void ImportFromCSV(string filePath, List<HoatChat> listHoatChat)
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
                        HoatChat cd = new HoatChat();
                        cd.TenHoatChat = values[0].Trim().Trim('"');
                        cd.LoaiHoatChat = values.Length > 1 ? values[1].Trim().Trim('"') : "";
                        listHoatChat.Add(cd);
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
            comboBoxHC.DataSource = _listHoatChat;
            comboBoxHC.DisplayMember = "TenHoatChat";
            comboBoxHC.ValueMember = "IDHoatChat";
        }
        private void LoadHoatChatGoc()
        {
            _listHoatChatGoc = getdata.GetDSHoatChatGoc().OrderBy(h => h.TenHoatChat).ToList();
            comboBoxHCG.DataSource = _listHoatChatGoc;
            comboBoxHCG.DisplayMember = "TenHoatChatGoc";
            comboBoxHCG.ValueMember = "IDHoatChatGoc";
        }

        private void comboBoxHC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idHoatChat = (int)comboBoxHC.SelectedValue;

            workingHC = getdata.GetHC(idHoatChat);

            foreach (HoatChatGoc goc in workingHC.dsHCG)
                _listLienKet.Add(new HoatChat_HoatChatGoc(idHoatChat, goc.IDHoatChatGoc));

            LoadLinkedList();
        }
        private void LoadLinkedList()
        {
            if (comboBoxHC.SelectedValue == null) return;
            grid1.DataSource = _listLienKet;
        }

        private void buttonThemHCG_Click(object sender, EventArgs e)
        {
            if (comboBoxHC.SelectedValue == null || comboBoxHCG.SelectedValue == null) return;

            int idHoatChat = (int)comboBoxHC.SelectedValue;
            int idHoatChatGoc = (int)comboBoxHCG.SelectedValue;

            if (_listLienKet.Any(l => l.IDHoatChat == idHoatChat && l.IDHoatChatGoc == idHoatChatGoc))
            {
                MessageBox.Show("Liên kết này đã tồn tại.");
                return;
            }
            else
            {
                _listLienKet.Add(new HoatChat_HoatChatGoc { IDHoatChat = idHoatChat, IDHoatChatGoc = idHoatChatGoc });
            }
            
            LoadLinkedList();
        }

        private void buttonXoaHCG_Click(object sender, EventArgs e)
        {

        }
    }
}
