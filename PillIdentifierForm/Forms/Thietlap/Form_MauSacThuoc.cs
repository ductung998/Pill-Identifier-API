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
    public partial class FormMauSacThuoc : Form
    {
        private List<Thuoc> _listThuoc;
        private List<MauSac> _listMauSac;
        private List<Thuoc_MauSac> _listLienKet;
        Thuoc workingThuoc;

        BindingSource grid1 = new BindingSource();

        KetnoiDB.GetData getdata = new KetnoiDB.GetData();
        KetnoiDB.InsertData insertdata = new KetnoiDB.InsertData();
        KetnoiDB.UpdateData updatedata = new KetnoiDB.UpdateData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();
        KetnoiDB.BulkInsertData bulkInsert = new KetnoiDB.BulkInsertData();
        public FormMauSacThuoc()
        {
            InitializeComponent();
        }

        private void FormMauSacThuoc_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            LoadThuoc();
            LoadMauSac();

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
                deletedata.DeleteThuoc_MauSac(workingThuoc.IDThuoc);

                foreach (Thuoc_MauSac i in _listLienKet)
                {
                    insertdata.InsertThuoc_MauSac(i.IDThuoc, i.IDMauSac);
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
            comboBoxHC.DataSource = _listThuoc;
            comboBoxHC.DisplayMember = "TenThuoc";
            comboBoxHC.ValueMember = "IDThuoc";
        }
        private void LoadMauSac()
        {
            _listMauSac = getdata.GetDSMauSac().OrderBy(h => h.TenMauSac).ToList();
            comboBoxHCG.DataSource = _listMauSac;
            comboBoxHCG.DisplayMember = "TenMauSac";
            comboBoxHCG.ValueMember = "IDMauSac";
        }

        private void comboBoxHC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idThuoc = (int)comboBoxHC.SelectedValue;

            workingThuoc = getdata.GetThuoc(idThuoc);

            foreach (MauSac mausac in workingThuoc.Mausac)
                _listLienKet.Add(new Thuoc_MauSac(idThuoc, mausac.IDMauSac));

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

            int idThuoc = (int)comboBoxHC.SelectedValue;
            int idMauSac = (int)comboBoxHCG.SelectedValue;

            if (_listLienKet.Any(l => l.IDThuoc == idThuoc && l.IDMauSac == idMauSac))
            {
                MessageBox.Show("Liên kết này đã tồn tại.");
                return;
            }
            else
            {
                _listLienKet.Add(new Thuoc_MauSac { IDThuoc = idThuoc, IDMauSac = idMauSac });
            }
            
            LoadLinkedList();
        }

        private void buttonXoaHCG_Click(object sender, EventArgs e)
        {

        }
    }
}
