namespace PillIdentifierForm.Forms
{
    partial class FormNhanDangThuoc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIDThuoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonThoat = new System.Windows.Forms.Button();
            this.buttonSua = new System.Windows.Forms.Button();
            this.buttonXoa = new System.Windows.Forms.Button();
            this.buttonThem = new System.Windows.Forms.Button();
            this.buttonXoatrang = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboLoaiRanh = new System.Windows.Forms.ComboBox();
            this.cboLoaiViThuoc = new System.Windows.Forms.ComboBox();
            this.cboDangThuoc = new System.Windows.Forms.ComboBox();
            this.cboHinhDang = new System.Windows.Forms.ComboBox();
            this.txtMaHinh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkCoKhacDau = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtKhacDauMatSau = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKhacDauMatTruoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIDNhandang = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonFileMau = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBoxFilterNotAssigned = new System.Windows.Forms.CheckBox();
            this.dataGridViewThuoc = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThuoc)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(470, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã thuốc";
            // 
            // textBoxIDThuoc
            // 
            this.textBoxIDThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIDThuoc.Location = new System.Drawing.Point(542, 128);
            this.textBoxIDThuoc.Name = "textBoxIDThuoc";
            this.textBoxIDThuoc.Size = new System.Drawing.Size(480, 22);
            this.textBoxIDThuoc.TabIndex = 3;
            this.textBoxIDThuoc.TextChanged += new System.EventHandler(this.textBoxThuoc_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Danh sách chỉ định";
            // 
            // buttonThoat
            // 
            this.buttonThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonThoat.Location = new System.Drawing.Point(879, 9);
            this.buttonThoat.Name = "buttonThoat";
            this.buttonThoat.Size = new System.Drawing.Size(143, 43);
            this.buttonThoat.TabIndex = 15;
            this.buttonThoat.Text = "Thoát";
            this.buttonThoat.UseVisualStyleBackColor = true;
            this.buttonThoat.Click += new System.EventHandler(this.buttonThoat_Click);
            // 
            // buttonSua
            // 
            this.buttonSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSua.Location = new System.Drawing.Point(730, 9);
            this.buttonSua.Name = "buttonSua";
            this.buttonSua.Size = new System.Drawing.Size(143, 43);
            this.buttonSua.TabIndex = 14;
            this.buttonSua.Text = "Sửa";
            this.buttonSua.UseVisualStyleBackColor = true;
            this.buttonSua.Click += new System.EventHandler(this.buttonSua_Click);
            // 
            // buttonXoa
            // 
            this.buttonXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXoa.Location = new System.Drawing.Point(581, 9);
            this.buttonXoa.Name = "buttonXoa";
            this.buttonXoa.Size = new System.Drawing.Size(143, 43);
            this.buttonXoa.TabIndex = 13;
            this.buttonXoa.Text = "Xóa";
            this.buttonXoa.UseVisualStyleBackColor = true;
            this.buttonXoa.Click += new System.EventHandler(this.buttonXoa_Click);
            // 
            // buttonThem
            // 
            this.buttonThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonThem.Location = new System.Drawing.Point(432, 9);
            this.buttonThem.Name = "buttonThem";
            this.buttonThem.Size = new System.Drawing.Size(143, 43);
            this.buttonThem.TabIndex = 12;
            this.buttonThem.Text = "Thêm";
            this.buttonThem.UseVisualStyleBackColor = true;
            this.buttonThem.Click += new System.EventHandler(this.buttonThem_Click);
            // 
            // buttonXoatrang
            // 
            this.buttonXoatrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXoatrang.Location = new System.Drawing.Point(283, 9);
            this.buttonXoatrang.Name = "buttonXoatrang";
            this.buttonXoatrang.Size = new System.Drawing.Size(143, 43);
            this.buttonXoatrang.TabIndex = 11;
            this.buttonXoatrang.Text = "Xóa trắng";
            this.buttonXoatrang.UseVisualStyleBackColor = true;
            this.buttonXoatrang.Click += new System.EventHandler(this.buttonXoatrang_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridViewThuoc);
            this.panel1.Controls.Add(this.checkBoxFilterNotAssigned);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.cboLoaiRanh);
            this.panel1.Controls.Add(this.cboLoaiViThuoc);
            this.panel1.Controls.Add(this.cboDangThuoc);
            this.panel1.Controls.Add(this.cboHinhDang);
            this.panel1.Controls.Add(this.txtMaHinh);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chkCoKhacDau);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtKhacDauMatSau);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtKhacDauMatTruoc);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxIDNhandang);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxIDThuoc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel1.Size = new System.Drawing.Size(1034, 248);
            this.panel1.TabIndex = 13;
            // 
            // cboLoaiRanh
            // 
            this.cboLoaiRanh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiRanh.FormattingEnabled = true;
            this.cboLoaiRanh.Location = new System.Drawing.Point(764, 218);
            this.cboLoaiRanh.Name = "cboLoaiRanh";
            this.cboLoaiRanh.Size = new System.Drawing.Size(258, 24);
            this.cboLoaiRanh.TabIndex = 27;
            // 
            // cboLoaiViThuoc
            // 
            this.cboLoaiViThuoc.FormattingEnabled = true;
            this.cboLoaiViThuoc.Location = new System.Drawing.Point(473, 218);
            this.cboLoaiViThuoc.Name = "cboLoaiViThuoc";
            this.cboLoaiViThuoc.Size = new System.Drawing.Size(285, 24);
            this.cboLoaiViThuoc.TabIndex = 26;
            // 
            // cboDangThuoc
            // 
            this.cboDangThuoc.FormattingEnabled = true;
            this.cboDangThuoc.Location = new System.Drawing.Point(232, 218);
            this.cboDangThuoc.Name = "cboDangThuoc";
            this.cboDangThuoc.Size = new System.Drawing.Size(235, 24);
            this.cboDangThuoc.TabIndex = 25;
            // 
            // cboHinhDang
            // 
            this.cboHinhDang.FormattingEnabled = true;
            this.cboHinhDang.Location = new System.Drawing.Point(12, 218);
            this.cboHinhDang.Name = "cboHinhDang";
            this.cboHinhDang.Size = new System.Drawing.Size(214, 24);
            this.cboHinhDang.TabIndex = 24;
            // 
            // txtMaHinh
            // 
            this.txtMaHinh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaHinh.Location = new System.Drawing.Point(473, 171);
            this.txtMaHinh.Name = "txtMaHinh";
            this.txtMaHinh.Size = new System.Drawing.Size(549, 22);
            this.txtMaHinh.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(470, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Mã hình";
            // 
            // chkCoKhacDau
            // 
            this.chkCoKhacDau.AutoSize = true;
            this.chkCoKhacDau.Location = new System.Drawing.Point(12, 173);
            this.chkCoKhacDau.Name = "chkCoKhacDau";
            this.chkCoKhacDau.Size = new System.Drawing.Size(109, 21);
            this.chkCoKhacDau.TabIndex = 21;
            this.chkCoKhacDau.Text = "Có khắc dấu";
            this.chkCoKhacDau.UseVisualStyleBackColor = true;
            this.chkCoKhacDau.CheckedChanged += new System.EventHandler(this.chkCoKhacDau_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(270, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 17);
            this.label10.TabIndex = 20;
            this.label10.Text = "Khắc dấu mặt sau";
            // 
            // txtKhacDauMatSau
            // 
            this.txtKhacDauMatSau.Location = new System.Drawing.Point(273, 171);
            this.txtKhacDauMatSau.Name = "txtKhacDauMatSau";
            this.txtKhacDauMatSau.Size = new System.Drawing.Size(194, 22);
            this.txtKhacDauMatSau.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(124, 151);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 17);
            this.label9.TabIndex = 18;
            this.label9.Text = "Khắc dấu mặt trước";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(761, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Loại rãnh";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(470, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Loại vỉ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(232, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Dạng thuốc";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Hình dạng";
            // 
            // txtKhacDauMatTruoc
            // 
            this.txtKhacDauMatTruoc.Location = new System.Drawing.Point(127, 171);
            this.txtKhacDauMatTruoc.Name = "txtKhacDauMatTruoc";
            this.txtKhacDauMatTruoc.Size = new System.Drawing.Size(140, 22);
            this.txtKhacDauMatTruoc.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mã nhận dạng";
            // 
            // textBoxIDNhandang
            // 
            this.textBoxIDNhandang.Enabled = false;
            this.textBoxIDNhandang.Location = new System.Drawing.Point(117, 128);
            this.textBoxIDNhandang.Name = "textBoxIDNhandang";
            this.textBoxIDNhandang.Size = new System.Drawing.Size(347, 22);
            this.textBoxIDNhandang.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonFileMau);
            this.panel2.Controls.Add(this.buttonImport);
            this.panel2.Controls.Add(this.buttonXoatrang);
            this.panel2.Controls.Add(this.buttonThoat);
            this.panel2.Controls.Add(this.buttonSua);
            this.panel2.Controls.Add(this.buttonThem);
            this.panel2.Controls.Add(this.buttonXoa);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 471);
            this.panel2.Name = "panel2";
            this.panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel2.Size = new System.Drawing.Size(1034, 55);
            this.panel2.TabIndex = 14;
            // 
            // buttonFileMau
            // 
            this.buttonFileMau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFileMau.Location = new System.Drawing.Point(12, 9);
            this.buttonFileMau.Name = "buttonFileMau";
            this.buttonFileMau.Size = new System.Drawing.Size(116, 43);
            this.buttonFileMau.TabIndex = 16;
            this.buttonFileMau.Text = "File mẫu";
            this.buttonFileMau.UseVisualStyleBackColor = true;
            this.buttonFileMau.Click += new System.EventHandler(this.buttonFileMau_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.Location = new System.Drawing.Point(134, 9);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(143, 43);
            this.buttonImport.TabIndex = 10;
            this.buttonImport.Text = "Import...";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvData);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 248);
            this.panel3.Name = "panel3";
            this.panel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel3.Size = new System.Drawing.Size(1034, 223);
            this.panel3.TabIndex = 15;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(12, 6);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1010, 211);
            this.dgvData.TabIndex = 16;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellClick);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 17);
            this.label11.TabIndex = 28;
            this.label11.Text = "Danh sách thuốc";
            // 
            // checkBoxFilterNotAssigned
            // 
            this.checkBoxFilterNotAssigned.AutoSize = true;
            this.checkBoxFilterNotAssigned.Location = new System.Drawing.Point(221, 8);
            this.checkBoxFilterNotAssigned.Name = "checkBoxFilterNotAssigned";
            this.checkBoxFilterNotAssigned.Size = new System.Drawing.Size(218, 21);
            this.checkBoxFilterNotAssigned.TabIndex = 29;
            this.checkBoxFilterNotAssigned.Text = "Lọc thuốc chưa có nhận dạng";
            this.checkBoxFilterNotAssigned.UseVisualStyleBackColor = true;
            this.checkBoxFilterNotAssigned.CheckedChanged += new System.EventHandler(this.checkBoxFilterNotAssigned_CheckedChanged);
            // 
            // dataGridViewThuoc
            // 
            this.dataGridViewThuoc.AllowUserToAddRows = false;
            this.dataGridViewThuoc.AllowUserToDeleteRows = false;
            this.dataGridViewThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewThuoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewThuoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewThuoc.Location = new System.Drawing.Point(12, 29);
            this.dataGridViewThuoc.MultiSelect = false;
            this.dataGridViewThuoc.Name = "dataGridViewThuoc";
            this.dataGridViewThuoc.ReadOnly = true;
            this.dataGridViewThuoc.RowTemplate.Height = 24;
            this.dataGridViewThuoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewThuoc.Size = new System.Drawing.Size(1010, 93);
            this.dataGridViewThuoc.TabIndex = 17;
            this.dataGridViewThuoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewThuoc_CellClick);
            // 
            // FormNhanDangThuoc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1034, 526);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormNhanDangThuoc";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormNhanDangThuoc";
            this.Load += new System.EventHandler(this.FormNhanDangThuoc_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThuoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIDThuoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonThoat;
        private System.Windows.Forms.Button buttonSua;
        private System.Windows.Forms.Button buttonXoa;
        private System.Windows.Forms.Button buttonThem;
        private System.Windows.Forms.Button buttonXoatrang;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIDNhandang;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.TextBox txtKhacDauMatTruoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtKhacDauMatSau;
        private System.Windows.Forms.TextBox txtMaHinh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkCoKhacDau;
        private System.Windows.Forms.ComboBox cboLoaiRanh;
        private System.Windows.Forms.ComboBox cboLoaiViThuoc;
        private System.Windows.Forms.ComboBox cboDangThuoc;
        private System.Windows.Forms.ComboBox cboHinhDang;
        private System.Windows.Forms.Button buttonFileMau;
        private System.Windows.Forms.CheckBox checkBoxFilterNotAssigned;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridViewThuoc;
    }
}