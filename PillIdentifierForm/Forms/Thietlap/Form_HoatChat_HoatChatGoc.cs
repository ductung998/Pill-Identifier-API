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
        private BindingList<HoatChat_HoatChatGoc> _listLienKet;
        HoatChat workingHC;

        BindingSource grid1 = new BindingSource();

        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.InsertData insertdata = new KetnoiDB.InsertData();
        KetnoiDB.UpdateData updatedata = new KetnoiDB.UpdateData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();

        private bool capnhat = false; // Flag to track unsaved changes

        public FormHoatChat_HoatChatGoc()
        {
            InitializeComponent();
        }

        private void FormHoatChat_HoatChatGoc_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            LoadHoatChat();
            LoadHoatChatGoc();
            LoadListLienKet();

            comboBoxHC.SelectedIndex = -1;
            comboBoxHCG.SelectedIndex = -1;

            LoadLinkedList();
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            if (capnhat)
            {
                DialogResult result = MessageBox.Show(
                    "Có thay đổi chưa được lưu. Bạn có muốn lưu trước khi thoát?",
                    "Xác nhận",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    buttonSua_Click(sender, e);
                    if (capnhat) return; // If save failed, don't close
                }
                else if (result == DialogResult.Cancel)
                {
                    return; // Don't close
                }
            }

            this.Close();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (workingHC == null)
                {
                    MessageBox.Show("Vui lòng chọn Hoạt Chất trước.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                deletedata.DeleteHoatChat_HoatChatGoc(workingHC.IDHoatChat);

                foreach (HoatChat_HoatChatGoc i in _listLienKet)
                {
                    insertdata.InsertHoatChat_HoatChatGoc(i.IDHoatChat, i.IDHoatChatGoc);
                }

                capnhat = false; // Reset flag after successful save
                MessageBox.Show("Cập nhật thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            using (ImportHoatChat_HoatChatGoc formcon = new ImportHoatChat_HoatChatGoc())
            {
                formcon.ShowDialog();
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
            comboBoxHCG.DisplayMember = "TenHoatChat";
            comboBoxHCG.ValueMember = "IDHoatChatGoc";
        }

        private void LoadListLienKet()
        {
            _listLienKet = new BindingList<HoatChat_HoatChatGoc>();
        }

        private void comboBoxHC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHC.SelectedValue == null || !(comboBoxHC.SelectedValue is int))
                    return;

                // Check for unsaved changes before switching
                if (capnhat)
                {
                    DialogResult result = MessageBox.Show(
                        "Có thay đổi chưa được lưu. Bạn có muốn lưu trước khi chuyển sang Hoạt Chất khác?",
                        "Xác nhận",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        buttonSua_Click(sender, e);
                        if (capnhat) return; // If save failed, don't switch
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        // Revert to previous selection
                        if (workingHC != null)
                        {
                            comboBoxHC.SelectedValue = workingHC.IDHoatChat;
                        }
                        return;
                    }
                    else // DialogResult.No
                    {
                        capnhat = false; // Discard changes
                    }
                }

                int idHoatChat = (int)comboBoxHC.SelectedValue;
                workingHC = getdata.GetHC(idHoatChat);

                _listLienKet.Clear();

                if (workingHC != null && workingHC.dsHCG != null)
                {
                    foreach (HoatChatGoc goc in workingHC.dsHCG)
                    {
                        _listLienKet.Add(new HoatChat_HoatChatGoc
                        {
                            IDHoatChat = idHoatChat,
                            IDHoatChatGoc = goc.IDHoatChatGoc
                        });
                    }
                }

                LoadLinkedList();
                capnhat = false; // Reset flag after loading new data
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLinkedList()
        {
            if (comboBoxHC.SelectedValue == null) return;

            var displayList = _listLienKet.Select(l => new
            {
                IDHoatChat = l.IDHoatChat,
                TenHoatChat = GetTenHoatChat(l.IDHoatChat),
                IDHoatChatGoc = l.IDHoatChatGoc,
                TenHoatChatGoc = GetTenHoatChatGoc(l.IDHoatChatGoc)
            }).ToList();

            grid1.DataSource = null;
            grid1.DataSource = displayList;
            dataGridView1.DataSource = grid1;
            dataGridView1.Refresh();
        }

        private string GetTenHoatChat(int id)
        {
            HoatChat hc = _listHoatChat.FirstOrDefault(h => h.IDHoatChat == id);
            return hc != null ? hc.TenHoatChat : "";
        }

        private string GetTenHoatChatGoc(int id)
        {
            HoatChatGoc hcg = _listHoatChatGoc.FirstOrDefault(g => g.IDHoatChatGoc == id);
            return hcg != null ? hcg.TenHoatChat : "";
        }

        private void buttonThemHCG_Click(object sender, EventArgs e)
        {
            if (comboBoxHC.SelectedValue == null || comboBoxHCG.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn cả Hoạt Chất và Hoạt Chất Gốc.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idHoatChat = (int)comboBoxHC.SelectedValue;
            int idHoatChatGoc = (int)comboBoxHCG.SelectedValue;

            if (_listLienKet.Any(l => l.IDHoatChat == idHoatChat && l.IDHoatChatGoc == idHoatChatGoc))
            {
                MessageBox.Show("Liên kết này đã tồn tại.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _listLienKet.Add(new HoatChat_HoatChatGoc
            {
                IDHoatChat = idHoatChat,
                IDHoatChatGoc = idHoatChatGoc
            });

            capnhat = true; // Mark as having unsaved changes
        }

        private void buttonXoaHCG_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa liên kết này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    HoatChat_HoatChatGoc item = row.DataBoundItem as HoatChat_HoatChatGoc;
                    if (item != null)
                    {
                        _listLienKet.Remove(item);
                    }
                }

                capnhat = true; // Mark as having unsaved changes
            }
        }
    }
}