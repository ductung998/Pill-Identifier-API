using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PillIdentifierForm
{
    public partial class Giaodienchinh : Form
    {
        private Form activeForm = null;
        public Giaodienchinh()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void danhMụcChỉĐịnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.DanhmucChiDinh DMChidinh = new Forms.DanhmucChiDinh();
            openChildForm(DMChidinh);
        }

        private void Giaodienchinh_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void openChildForm(Form childForm) //Mở các giao diện con
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(childForm);
            panelContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void danhMụcDạngThuốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.DanhmucDangThuoc DMDangThuoc = new Forms.DanhmucDangThuoc();
            openChildForm(DMDangThuoc);
        }

        private void danhMụcHoạtChấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.DanhmucHoatChat DMHoatChat = new Forms.DanhmucHoatChat();
            openChildForm(DMHoatChat);
        }

        private void danhMụcThuốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.DanhmucThuoc DMThuoc = new Forms.DanhmucThuoc();
            openChildForm(DMThuoc);
        }

        private void danhMụcMàuSắcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.DanhmucMauSac DMMauSac = new Forms.DanhmucMauSac();
            openChildForm(DMMauSac);
        }

        private void danhMụcHìnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.DanhmucHinhDang DMHinhDang = new Forms.DanhmucHinhDang();
            openChildForm(DMHinhDang);
        }

        private void danhMụcLoạiRãnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.DanhmucLoaiRanh DMLoaiRanh = new Forms.DanhmucLoaiRanh();
            openChildForm(DMLoaiRanh);
        }

        private void danhMụcLoạiVỉToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.DanhmucLoaiViThuoc DMLoaiViThuoc = new Forms.DanhmucLoaiViThuoc();
            openChildForm(DMLoaiViThuoc);
        }

        private void thiếtLậpHoạtChấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FormHoatChat_HoatChatGoc FormHoatChat_HoatChatGoc = new Forms.FormHoatChat_HoatChatGoc();
            openChildForm(FormHoatChat_HoatChatGoc);
        }

        private void thiếtLậpNhậnDạngThuốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FormNhanDangThuoc FormNhanDangThuoc = new Forms.FormNhanDangThuoc();
            openChildForm(FormNhanDangThuoc);
        }

        private void danhMụcHoạtChấtGốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.DanhmucHoatChatGoc DanhmucHoatChatGoc = new Forms.DanhmucHoatChatGoc();
            openChildForm(DanhmucHoatChatGoc);
        }

        private void thiếtLậpHìnhẢnhThuốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Form_MauSacThuoc Form_MauSacThuoc = new Forms.Form_MauSacThuoc();
            openChildForm(Form_MauSacThuoc);
        }

        private void traCứuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Tracuu Tracuu = new Forms.Tracuu();
            openChildForm(Tracuu);
        }
    }
}
