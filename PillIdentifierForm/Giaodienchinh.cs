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
            Forms.DanhmucChidinh DMChidinh = new Forms.DanhmucChidinh();
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
    }
}
