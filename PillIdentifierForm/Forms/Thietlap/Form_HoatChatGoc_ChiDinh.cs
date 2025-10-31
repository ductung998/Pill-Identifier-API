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
    public partial class Form_HoatChatGoc_ChiDinh : Form
    {
        private List<HoatChatGoc> _listHoatChatGoc;
        private List<ChiDinh> _listChiDinh;
        private BindingList<HoatChatGoc_ChiDinh> _listLienKet;
        HoatChatGoc workingHCG;

        BindingSource grid1 = new BindingSource();

        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.InsertData insertdata = new KetnoiDB.InsertData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();

        private bool capnhat = false; // Flag to track unsaved changes

        public Form_HoatChatGoc_ChiDinh()
        {
            InitializeComponent();
        }

        private void Form_HoatChatGoc_ChiDinh_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            LoadHoatChatGoc();
            LoadChiDinh();
            LoadListLienKet();

            comboBoxHCG.SelectedIndex = -1;
            comboBoxCD.SelectedIndex = -1;

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
                if (workingHCG == null)
                {
                    MessageBox.Show("Vui lòng chọn Hoạt Chất trước.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                deletedata.DeleteHoatChatGoc_ChiDinh(workingHCG.IDHoatChatGoc);

                foreach (HoatChatGoc_ChiDinh i in _listLienKet)
                {
                    insertdata.InsertHoatChatGoc_ChiDinh(i);
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
            using (ImportHoatChatGoc_ChiDinh formcon = new ImportHoatChatGoc_ChiDinh())
            {
                formcon.ShowDialog();
            }
        }

        private void buttonGDHoatChatGoc_Click(object sender, EventArgs e)
        {
            using (DanhmucHoatChatGoc formcon = new DanhmucHoatChatGoc())
            {
                formcon.ShowDialog();
                LoadHoatChatGoc();
            }
        }

        private void LoadHoatChatGoc()
        {
            _listHoatChatGoc = getdata.GetDSHoatChatGoc().OrderBy(h => h.TenHoatChat).ToList();
            comboBoxHCG.DataSource = _listHoatChatGoc;
            comboBoxHCG.DisplayMember = "TenHoatChatGoc";
            comboBoxHCG.ValueMember = "IDHoatChatGoc";
        }

        private void LoadChiDinh()
        {
            _listChiDinh = getdata.GetDSChiDinh().OrderBy(h => h.TenChiDinh).ToList();
            comboBoxCD.DataSource = _listChiDinh;
            comboBoxCD.DisplayMember = "TenHoatChatGoc";
            comboBoxCD.ValueMember = "IDChiDinh";
        }

        private void LoadListLienKet()
        {
            _listLienKet = new BindingList<HoatChatGoc_ChiDinh>();
        }

        private void comboBoxHC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHCG.SelectedValue == null || !(comboBoxHCG.SelectedValue is int))
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
                        if (workingHCG != null)
                        {
                            comboBoxHCG.SelectedValue = workingHCG.IDHoatChatGoc;
                        }
                        return;
                    }
                    else // DialogResult.No
                    {
                        capnhat = false; // Discard changes
                    }
                }

                int idHoatChatGoc = (int)comboBoxHCG.SelectedValue;
                workingHCG = getdata.GetHCG(idHoatChatGoc);

                _listLienKet.Clear();

                if (workingHCG != null && workingHCG.dsCD != null)
                {
                    foreach (ChiDinh cd in workingHCG.dsCD)
                    {
                        _listLienKet.Add(new HoatChatGoc_ChiDinh
                        {
                            IDHoatChatGoc = idHoatChatGoc,
                            IDChiDinh = cd.IDChiDinh
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
            if (comboBoxHCG.SelectedValue == null) return;

            var displayList = _listLienKet.Select(l => new
            {
                IDHoatChatGoc = l.IDHoatChatGoc,
                TenHoatChatGoc = GetTenHoatChatGoc(l.IDHoatChatGoc),
                IDChiDinh = l.IDChiDinh,
                TenChiDinh = GetTenChiDinh(l.IDChiDinh)
            }).ToList();

            grid1.DataSource = null;
            grid1.DataSource = displayList;
            dataGridView1.DataSource = grid1;
            dataGridView1.Refresh();
        }

        private string GetTenHoatChatGoc(int id)
        {
            HoatChatGoc hc = _listHoatChatGoc.FirstOrDefault(h => h.IDHoatChatGoc == id);
            return hc != null ? hc.TenHoatChat : "";
        }

        private string GetTenChiDinh(int id)
        {
            ChiDinh hcg = _listChiDinh.FirstOrDefault(g => g.IDChiDinh == id);
            return hcg != null ? hcg.TenChiDinh : "";
        }

        private void buttonThemHCG_Click(object sender, EventArgs e)
        {
            if (comboBoxHCG.SelectedValue == null || comboBoxCD.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn cả Hoạt Chất Gốc và Chỉ định.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idHoatChatGoc = (int)comboBoxHCG.SelectedValue;
            int idChiDinh = (int)comboBoxCD.SelectedValue;

            if (_listLienKet.Any(l => l.IDHoatChatGoc == idHoatChatGoc && l.IDChiDinh == idChiDinh))
            {
                MessageBox.Show("Liên kết này đã tồn tại.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _listLienKet.Add(new HoatChatGoc_ChiDinh
            {
                IDHoatChatGoc = idHoatChatGoc,
                IDChiDinh = idChiDinh
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
                    HoatChatGoc_ChiDinh item = row.DataBoundItem as HoatChatGoc_ChiDinh;
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