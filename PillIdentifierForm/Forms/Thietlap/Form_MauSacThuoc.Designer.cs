namespace PillIdentifierForm.Forms
{
    partial class Form_MauSacThuoc
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
            this.label4 = new System.Windows.Forms.Label();
            this.buttonThoat = new System.Windows.Forms.Button();
            this.buttonSua = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonXoaHCG = new System.Windows.Forms.Button();
            this.buttonThemHCG = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxHCG = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonImport = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewThuoc = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIDThuoc = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThuoc)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Chọn thuốc";
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
            this.buttonThoat.TabIndex = 8;
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
            this.buttonSua.TabIndex = 9;
            this.buttonSua.Text = "Cập nhật";
            this.buttonSua.UseVisualStyleBackColor = true;
            this.buttonSua.Click += new System.EventHandler(this.buttonSua_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxIDThuoc);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dataGridViewThuoc);
            this.panel1.Controls.Add(this.buttonXoaHCG);
            this.panel1.Controls.Add(this.buttonThemHCG);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.comboBoxHCG);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel1.Size = new System.Drawing.Size(1034, 187);
            this.panel1.TabIndex = 13;
            // 
            // buttonXoaHCG
            // 
            this.buttonXoaHCG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXoaHCG.Location = new System.Drawing.Point(981, 158);
            this.buttonXoaHCG.Name = "buttonXoaHCG";
            this.buttonXoaHCG.Size = new System.Drawing.Size(41, 23);
            this.buttonXoaHCG.TabIndex = 13;
            this.buttonXoaHCG.Text = "-";
            this.buttonXoaHCG.UseVisualStyleBackColor = true;
            this.buttonXoaHCG.Click += new System.EventHandler(this.buttonXoaHCG_Click);
            // 
            // buttonThemHCG
            // 
            this.buttonThemHCG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonThemHCG.Location = new System.Drawing.Point(934, 158);
            this.buttonThemHCG.Name = "buttonThemHCG";
            this.buttonThemHCG.Size = new System.Drawing.Size(41, 23);
            this.buttonThemHCG.TabIndex = 12;
            this.buttonThemHCG.Text = "+";
            this.buttonThemHCG.UseVisualStyleBackColor = true;
            this.buttonThemHCG.Click += new System.EventHandler(this.buttonThemHCG_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonGDThuoc_Click);
            // 
            // comboBoxHCG
            // 
            this.comboBoxHCG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxHCG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHCG.FormattingEnabled = true;
            this.comboBoxHCG.Location = new System.Drawing.Point(404, 158);
            this.comboBoxHCG.Name = "comboBoxHCG";
            this.comboBoxHCG.Size = new System.Drawing.Size(524, 24);
            this.comboBoxHCG.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Màu sắc";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonImport);
            this.panel2.Controls.Add(this.buttonThoat);
            this.panel2.Controls.Add(this.buttonSua);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 471);
            this.panel2.Name = "panel2";
            this.panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel2.Size = new System.Drawing.Size(1034, 55);
            this.panel2.TabIndex = 14;
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.Location = new System.Drawing.Point(581, 9);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(143, 43);
            this.buttonImport.TabIndex = 13;
            this.buttonImport.Text = "Import...";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 187);
            this.panel3.Name = "panel3";
            this.panel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel3.Size = new System.Drawing.Size(1034, 284);
            this.panel3.TabIndex = 15;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 6);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1010, 272);
            this.dataGridView1.TabIndex = 8;
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
            this.dataGridViewThuoc.Location = new System.Drawing.Point(12, 35);
            this.dataGridViewThuoc.MultiSelect = false;
            this.dataGridViewThuoc.Name = "dataGridViewThuoc";
            this.dataGridViewThuoc.ReadOnly = true;
            this.dataGridViewThuoc.RowTemplate.Height = 24;
            this.dataGridViewThuoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewThuoc.Size = new System.Drawing.Size(1010, 117);
            this.dataGridViewThuoc.TabIndex = 9;
            this.dataGridViewThuoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewThuoc_CellClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "ID thuốc";
            // 
            // textBoxIDThuoc
            // 
            this.textBoxIDThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxIDThuoc.Enabled = false;
            this.textBoxIDThuoc.Location = new System.Drawing.Point(78, 158);
            this.textBoxIDThuoc.Name = "textBoxIDThuoc";
            this.textBoxIDThuoc.Size = new System.Drawing.Size(253, 22);
            this.textBoxIDThuoc.TabIndex = 15;
            // 
            // Form_MauSacThuoc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1034, 526);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_MauSacThuoc";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form_MauSacThuoc";
            this.Load += new System.EventHandler(this.Form_MauSacThuoc_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThuoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonThoat;
        private System.Windows.Forms.Button buttonSua;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBoxHCG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonXoaHCG;
        private System.Windows.Forms.Button buttonThemHCG;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.DataGridView dataGridViewThuoc;
        private System.Windows.Forms.TextBox textBoxIDThuoc;
        private System.Windows.Forms.Label label1;
    }
}