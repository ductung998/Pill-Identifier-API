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
    public partial class Form_MauSacThuoc : Form
    {
        private List<Thuoc> _listThuoc;
        private List<MauSac> _listMauSac;
        private BindingList<Thuoc_MauSac> _listLienKet;
        Thuoc workingThuoc;

        BindingSource grid1 = new BindingSource();
        BindingSource gridThuoc = new BindingSource();

        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.InsertData insertdata = new KetnoiDB.InsertData();
        KetnoiDB.UpdateData updatedata = new KetnoiDB.UpdateData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();

        private bool capnhat = false; // Flag to track unsaved changes

        public Form_MauSacThuoc()
        {
            InitializeComponent();
        }

        private void Form_MauSacThuoc_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            LoadThuoc();
            LoadMauSac();
            LoadListLienKet();

            comboBoxHCG.SelectedIndex = -1;

            // Clear the linked list initially
            grid1.DataSource = null;
            dataGridView1.DataSource = grid1;
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
                if (workingThuoc == null)
                {
                    MessageBox.Show("Vui lòng chọn Thuốc trước.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                deletedata.DeleteThuoc_MauSac(workingThuoc.IDThuoc);

                foreach (Thuoc_MauSac i in _listLienKet)
                {
                    insertdata.InsertThuoc_MauSac(i);
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
            using (ImportThuoc_MauSac formcon = new ImportThuoc_MauSac())
            {
                formcon.ShowDialog();
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
            gridThuoc.DataSource = _listThuoc;
            dataGridViewThuoc.DataSource = gridThuoc;
            dataGridViewThuoc.AutoResizeColumns();
        }

        private void LoadMauSac()
        {
            _listMauSac = getdata.GetDSMauSac().OrderBy(h => h.TenMauSac).ToList();
            comboBoxHCG.DataSource = _listMauSac;
            comboBoxHCG.DisplayMember = "TenMauSac";
            comboBoxHCG.ValueMember = "IDMauSac";
        }

        private void LoadListLienKet()
        {
            _listLienKet = new BindingList<Thuoc_MauSac>();
        }

        private void LoadLinkedList()
        {
            if (workingThuoc == null) return;

            var displayList = _listLienKet.Select(l => new
            {
                IDThuoc = l.IDThuoc,
                TenThuoc = GetTenThuoc(l.IDThuoc),
                IDMauSac = l.IDMauSac,
                TenMauSac = GetTenMauSac(l.IDMauSac)
            }).ToList();

            grid1.DataSource = null;
            grid1.DataSource = displayList;
            dataGridView1.DataSource = grid1;
            dataGridView1.Refresh();
        }

        private string GetTenThuoc(int id)
        {
            Thuoc hc = _listThuoc.FirstOrDefault(h => h.IDThuoc == id);
            return hc != null ? hc.TenThuoc : "";
        }

        private string GetTenMauSac(int id)
        {
            MauSac hcg = _listMauSac.FirstOrDefault(g => g.IDMauSac == id);
            return hcg != null ? hcg.TenMauSac : "";
        }

        private void buttonThemHCG_Click(object sender, EventArgs e)
        {
            if (workingThuoc == null || comboBoxHCG.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn cả Thuốc và Màu Sắc.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idThuoc = workingThuoc.IDThuoc;
            int idMauSac = (int)comboBoxHCG.SelectedValue;

            if (_listLienKet.Any(l => l.IDThuoc == idThuoc && l.IDMauSac == idMauSac))
            {
                MessageBox.Show("Liên kết này đã tồn tại.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _listLienKet.Add(new Thuoc_MauSac
            {
                IDThuoc = idThuoc,
                IDMauSac = idMauSac
            });

            LoadLinkedList(); // Refresh display
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
                // Get the selected row's data
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Get IDs from the anonymous type display object
                    int idThuoc = Convert.ToInt32(selectedRow.Cells["IDThuoc"].Value);
                    int idMauSac = Convert.ToInt32(selectedRow.Cells["IDMauSac"].Value);

                    // Find and remove from the actual binding list
                    Thuoc_MauSac itemToRemove = _listLienKet.FirstOrDefault(l =>
                        l.IDThuoc == idThuoc && l.IDMauSac == idMauSac);

                    if (itemToRemove != null)
                    {
                        _listLienKet.Remove(itemToRemove);
                        LoadLinkedList(); // Refresh display
                        capnhat = true; // Mark as having unsaved changes
                    }
                }
            }
        }

        private void dataGridViewThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if clicked row is valid (not header row)
                if (e.RowIndex < 0) return;

                // Check for unsaved changes before switching
                if (capnhat)
                {
                    DialogResult result = MessageBox.Show(
                        "Có thay đổi chưa được lưu. Bạn có muốn lưu trước khi chuyển sang Thuốc khác?",
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
                        if (workingThuoc != null)
                        {
                            // Find and select the previous row
                            foreach (DataGridViewRow row in dataGridViewThuoc.Rows)
                            {
                                if (row.Cells["IDThuoc"].Value != null &&
                                    Convert.ToInt32(row.Cells["IDThuoc"].Value) == workingThuoc.IDThuoc)
                                {
                                    dataGridViewThuoc.ClearSelection();
                                    row.Selected = true;
                                    break;
                                }
                            }
                        }
                        return;
                    }
                    else // DialogResult.No
                    {
                        capnhat = false; // Discard changes
                    }
                }

                DataGridViewRow selectedRow = dataGridViewThuoc.Rows[e.RowIndex];

                // Get IDThuoc from the selected row
                if (selectedRow.Cells["IDThuoc"].Value == null) return;

                int idThuoc = Convert.ToInt32(selectedRow.Cells["IDThuoc"].Value);

                // Update textbox
                textBoxIDThuoc.Text = idThuoc.ToString();

                // Load the Thuoc details with its color list
                workingThuoc = getdata.GetThuoc(idThuoc);

                _listLienKet.Clear();

                if (workingThuoc != null && workingThuoc.Mausac != null)
                {
                    foreach (MauSac goc in workingThuoc.Mausac)
                    {
                        _listLienKet.Add(new Thuoc_MauSac
                        {
                            IDThuoc = idThuoc,
                            IDMauSac = goc.IDMauSac
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
    }
}