namespace PillIdentifierForm.Forms
{
    partial class FormHinhAnhThuoc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvThuoc = new System.Windows.Forms.DataGridView();
            this.lblDrugTitle = new System.Windows.Forms.Label();
            this.pnlDropZone = new System.Windows.Forms.Panel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblDropHint = new System.Windows.Forms.Label();
            this.flpImages = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThuoc)).BeginInit();
            this.pnlDropZone.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.Panel1MinSize = 50;
            this.splitContainer1.Panel2MinSize = 50;
            this.splitContainer1.TabIndex = 0;
            //
            // splitContainer1.Panel1 — drug grid
            //
            this.splitContainer1.Panel1.Controls.Add(this.dgvThuoc);
            //
            // splitContainer1.Panel2 — image management area
            //
            this.splitContainer1.Panel2.Controls.Add(this.flpImages);
            this.splitContainer1.Panel2.Controls.Add(this.pnlDropZone);
            this.splitContainer1.Panel2.Controls.Add(this.lblDrugTitle);
            this.splitContainer1.Panel2.Controls.Add(this.pnlButtons);
            this.splitContainer1.Panel2.AllowDrop = true;
            //
            // dgvThuoc
            //
            this.dgvThuoc.AllowUserToAddRows = false;
            this.dgvThuoc.AllowUserToDeleteRows = false;
            this.dgvThuoc.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThuoc.Location = new System.Drawing.Point(0, 0);
            this.dgvThuoc.MultiSelect = false;
            this.dgvThuoc.Name = "dgvThuoc";
            this.dgvThuoc.ReadOnly = true;
            this.dgvThuoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThuoc.Size = new System.Drawing.Size(800, 250);
            this.dgvThuoc.TabIndex = 0;
            this.dgvThuoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThuoc_CellClick);
            //
            // lblDrugTitle
            //
            this.lblDrugTitle.BackColor = System.Drawing.Color.FromArgb(26, 95, 168);
            this.lblDrugTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDrugTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDrugTitle.ForeColor = System.Drawing.Color.White;
            this.lblDrugTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDrugTitle.Name = "lblDrugTitle";
            this.lblDrugTitle.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblDrugTitle.Size = new System.Drawing.Size(800, 30);
            this.lblDrugTitle.TabIndex = 3;
            this.lblDrugTitle.Text = "Chọn một thuốc từ danh sách để quản lý hình ảnh";
            this.lblDrugTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlDropZone
            //
            this.pnlDropZone.AllowDrop = true;
            this.pnlDropZone.BackColor = System.Drawing.Color.FromArgb(240, 245, 255);
            this.pnlDropZone.Controls.Add(this.lblDropHint);
            this.pnlDropZone.Controls.Add(this.btnBrowse);
            this.pnlDropZone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDropZone.Location = new System.Drawing.Point(0, 30);
            this.pnlDropZone.Name = "pnlDropZone";
            this.pnlDropZone.Padding = new System.Windows.Forms.Padding(8);
            this.pnlDropZone.Size = new System.Drawing.Size(800, 70);
            this.pnlDropZone.TabIndex = 2;
            this.pnlDropZone.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlDropZone_DragEnter);
            this.pnlDropZone.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlDropZone_DragDrop);
            //
            // lblDropHint
            //
            this.lblDropHint.AutoSize = false;
            this.lblDropHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDropHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDropHint.ForeColor = System.Drawing.Color.Gray;
            this.lblDropHint.Location = new System.Drawing.Point(8, 8);
            this.lblDropHint.Name = "lblDropHint";
            this.lblDropHint.Size = new System.Drawing.Size(650, 54);
            this.lblDropHint.TabIndex = 0;
            this.lblDropHint.Text = "Kéo thả file ảnh vào đây (hỗ trợ nhiều file), hoặc nhấn \"Chọn file...\" để duyệt";
            this.lblDropHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnBrowse
            //
            this.btnBrowse.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(110, 54);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Chọn file...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            // flpImages
            //
            this.flpImages.AllowDrop = true;
            this.flpImages.AutoScroll = true;
            this.flpImages.BackColor = System.Drawing.SystemColors.Window;
            this.flpImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpImages.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpImages.Location = new System.Drawing.Point(0, 100);
            this.flpImages.Name = "flpImages";
            this.flpImages.Padding = new System.Windows.Forms.Padding(4);
            this.flpImages.Size = new System.Drawing.Size(800, 200);
            this.flpImages.TabIndex = 1;
            this.flpImages.WrapContents = false;
            this.flpImages.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlDropZone_DragEnter);
            this.flpImages.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlDropZone_DragDrop);
            //
            // pnlButtons
            //
            this.pnlButtons.Controls.Add(this.btnClearAll);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 300);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(800, 44);
            this.pnlButtons.TabIndex = 0;
            //
            // btnSave
            //
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(26, 95, 168);
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 44);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnClearAll
            //
            this.btnClearAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearAll.Enabled = false;
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(110, 44);
            this.btnClearAll.TabIndex = 0;
            this.btnClearAll.Text = "Xóa tất cả";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            //
            // FormHinhAnhThuoc
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormHinhAnhThuoc";
            this.Text = "Thiết lập hình ảnh theo thuốc";
            this.Load += new System.EventHandler(this.FormHinhAnhThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThuoc)).EndInit();
            this.pnlDropZone.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvThuoc;
        private System.Windows.Forms.Label lblDrugTitle;
        private System.Windows.Forms.Panel pnlDropZone;
        private System.Windows.Forms.Label lblDropHint;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FlowLayoutPanel flpImages;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnSave;
    }
}
