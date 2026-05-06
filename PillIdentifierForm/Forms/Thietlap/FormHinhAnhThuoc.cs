using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassChung;
using PillIdentifierForm.Helpers;

namespace PillIdentifierForm.Forms
{
    public partial class FormHinhAnhThuoc : Form
    {
        // ── Status ────────────────────────────────────────────────────────────
        private enum ImageStatus { Saved, New, Modified, Deleted }

        // ── One image item in the list ────────────────────────────────────────
        private class ImageItem
        {
            // All DB rows that share this DuongDanHinh for the selected drug.
            // Needed so we can update/delete every fan-out row in one pass.
            public List<HinhAnhThuocChiTiet> DbRows = new List<HinhAnhThuocChiTiet>();

            public string DuongDanHinh;    // current public URL (empty string if New)
            public string LocalFilePath;   // local path to upload (set for New / Modified)
            public string OldDuongDanHinh; // Drive URL to delete after upload (Modified only)

            public int Order;          // display order stored in MoTa
            public int OriginalOrder;  // used to detect changes on Saved items

            public ImageStatus Status;
            public string DisplayName; // filename shown in the row label
        }

        // ── DB helpers ────────────────────────────────────────────────────────
        KetnoiDB.GetData getdata       = new KetnoiDB.GetData();
        KetnoiDB.InsertData insertdata = new KetnoiDB.InsertData();
        KetnoiDB.UpdateData updatedata = new KetnoiDB.UpdateData();
        KetnoiDB.DeleteData deletedata = new KetnoiDB.DeleteData();

        // ── State ─────────────────────────────────────────────────────────────
        private List<Thuoc>     _listThuoc       = new List<Thuoc>();
        private List<ImageItem> _items           = new List<ImageItem>();
        private int             _selectedIDThuoc = -1;
        private bool            _hasUnsaved      = false;

        private static readonly string[] ImageExtensions =
            { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

        // ── Constructor ───────────────────────────────────────────────────────
        public FormHinhAnhThuoc()
        {
            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        // LOAD
        // ─────────────────────────────────────────────────────────────────────
        private void FormHinhAnhThuoc_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // Set splitter after the form is fully sized to avoid the
            // "SplitterDistance must be between Panel1MinSize and Width - Panel2MinSize" error
            splitContainer1.SplitterDistance = (int)(splitContainer1.Height * 0.40);
            LoadDrugGrid();
        }

        private void LoadDrugGrid()
        {
            try
            {
                _listThuoc = getdata.GetDSThuoc();
                dgvThuoc.DataSource = _listThuoc;
                dgvThuoc.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách thuốc: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // DRUG GRID SELECTION
        // ─────────────────────────────────────────────────────────────────────
        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _listThuoc.Count) return;

            if (_hasUnsaved)
            {
                var result = MessageBox.Show(
                    "Có thay đổi chưa được lưu. Bạn có muốn tiếp tục không?",
                    "Chưa lưu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.No) return;
            }

            var thuoc = _listThuoc[e.RowIndex];
            _selectedIDThuoc = thuoc.IDThuoc;
            lblDrugTitle.Text = "Hình ảnh cho: " + thuoc.TenThuoc;

            btnSave.Enabled    = true;
            btnClearAll.Enabled = true;

            LoadImagesForDrug(_selectedIDThuoc);
        }

        // ─────────────────────────────────────────────────────────────────────
        // LOAD IMAGES FOR SELECTED DRUG
        // ─────────────────────────────────────────────────────────────────────
        private void LoadImagesForDrug(int idThuoc)
        {
            _items.Clear();
            _hasUnsaved = false;

            try
            {
                var allRows = getdata.GetDSHinhAnhbyThuoc(idThuoc);

                // Group by DuongDanHinh — each unique URL = one UI row
                var groups = allRows
                    .GroupBy(r => r.DuongDanHinh)
                    .ToList();

                foreach (var group in groups)
                {
                    int order = 0;
                    int.TryParse(group.First().MoTa, out order);

                    string url  = group.Key ?? "";
                    // string name = string.IsNullOrEmpty(url)
                    //     ? "(không có URL)"
                    //     : Path.GetFileName(url.Split('?')[0]);
                    string name = "(không có URL)";
                    if (!string.IsNullOrEmpty(url))
                    {
                        // Dùng Regex để tìm phần text nằm sau chữ ?name= hoặc &name=
                        var match = System.Text.RegularExpressions.Regex.Match(url, @"[&?]name=([^&]+)");
            
                        if (match.Success)
                        {
                            // Giải mã url (ví dụ: biến %20 thành dấu cách, giải mã tiếng Việt có dấu)
                            name = Uri.UnescapeDataString(match.Groups[1].Value);
                        }
                        else
                        {
                            // Đề phòng trường hợp đọc lại các dòng data cũ (lúc chưa gắn name= vào link)
                            name = "Ảnh Drive (Chưa có tên)"; 
                        }
                    }

                    var item = new ImageItem
                    {
                        DbRows        = group.ToList(),
                        DuongDanHinh  = url,
                        Order         = order,
                        OriginalOrder = order,
                        Status        = ImageStatus.Saved,
                        DisplayName   = name
                    };
                    _items.Add(item);
                }

                // Sort by order (items with order=0 / unparsed go to end)
                _items = _items.OrderBy(i => i.Order == 0 ? int.MaxValue : i.Order).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải hình ảnh: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RenderImageList();
        }

        // ─────────────────────────────────────────────────────────────────────
        // RENDER IMAGE LIST
        // ─────────────────────────────────────────────────────────────────────
        private void RenderImageList()
        {
            flpImages.SuspendLayout();
            // Dispose existing child controls to free resources (esp. PictureBoxes)
            foreach (Control c in flpImages.Controls)
                c.Dispose();
            flpImages.Controls.Clear();

            foreach (var item in _items)
            {
                if (item.Status == ImageStatus.Deleted)
                {
                    flpImages.Controls.Add(BuildDeletedRow(item));
                }
                else
                {
                    flpImages.Controls.Add(BuildActiveRow(item));
                }
            }

            flpImages.ResumeLayout();
        }

        // ── Row for a non-deleted item ────────────────────────────────────────
        private Panel BuildActiveRow(ImageItem item)
        {
            var row = new Panel
            {
                Width        = flpImages.ClientSize.Width - 20,
                Height       = 100,
                BorderStyle  = BorderStyle.FixedSingle,
                Margin       = new Padding(0, 0, 0, 4),
                BackColor    = SystemColors.Window
            };

            // Thumbnail
            var pic = new PictureBox
            {
                Width     = 90,
                Height    = 90,
                Location  = new Point(5, 5),
                SizeMode  = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };
            LoadThumbnail(pic, item);
            row.Controls.Add(pic);

            // Info panel (right of thumbnail)
            var info = new Panel
            {
                Location = new Point(102, 0),
                Width    = row.Width - 108,
                Height   = 100
            };
            row.Controls.Add(info);

            // Filename label
            var lblName = new Label
            {
                Text      = item.DisplayName,
                Location  = new Point(0, 6),
                Width     = info.Width - 10,
                Height    = 20,
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
                AutoEllipsis = true
            };
            info.Controls.Add(lblName);

            // Status badge
            var lblStatus = BuildStatusLabel(item.Status);
            lblStatus.Location = new Point(0, 28);
            info.Controls.Add(lblStatus);

            // Order row: "Thứ tự:" label + textbox
            var lblOrder = new Label
            {
                Text     = "Thứ tự:",
                Location = new Point(0, 52),
                Width    = 55,
                Height   = 22,
                TextAlign = ContentAlignment.MiddleLeft
            };
            info.Controls.Add(lblOrder);

            var txtOrder = new TextBox
            {
                Text     = item.Order.ToString(),
                Location = new Point(57, 52),
                Width    = 45,
                Height   = 22,
                Tag      = item
            };
            txtOrder.KeyPress += TxtOrder_KeyPress;
            info.Controls.Add(txtOrder);

            // Buttons
            var btnChange = new Button
            {
                Text     = "Thay đổi",
                Location = new Point(110, 50),
                Width    = 85,
                Height   = 26,
                Tag      = item
            };
            btnChange.Click += (s, e) => ChangeImage_Click(item, txtOrder);
            info.Controls.Add(btnChange);

            var btnRemove = new Button
            {
                Text      = "Xóa",
                Location  = new Point(202, 50),
                Width     = 60,
                Height    = 26,
                Tag       = item,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRemove.Click += (s, e) => RemoveImage_Click(item);
            info.Controls.Add(btnRemove);

            // Wire textbox change after item reference is captured
            txtOrder.Leave += (s, e) =>
            {
                int val;
                if (int.TryParse(txtOrder.Text, out val) && val != item.Order)
                {
                    item.Order = val;
                    if (item.Status == ImageStatus.Saved)
                        _hasUnsaved = true;
                }
            };

            return row;
        }

        // ── Row for a deleted item (greyed out, undo button) ──────────────────
        private Panel BuildDeletedRow(ImageItem item)
        {
            var row = new Panel
            {
                Width       = flpImages.ClientSize.Width - 20,
                Height      = 40,
                BorderStyle = BorderStyle.FixedSingle,
                Margin      = new Padding(0, 0, 0, 4),
                BackColor   = Color.FromArgb(250, 235, 235)
            };

            var lblName = new Label
            {
                Text      = item.DisplayName,
                Location  = new Point(5, 10),
                Width     = row.Width - 200,
                Height    = 20,
                Font      = new Font("Segoe UI", 9f, FontStyle.Strikeout),
                ForeColor = Color.Gray,
                AutoEllipsis = true
            };
            row.Controls.Add(lblName);

            var lblStatus = BuildStatusLabel(ImageStatus.Deleted);
            lblStatus.Location = new Point(lblName.Width + 10, 10);
            row.Controls.Add(lblStatus);

            var btnUndo = new Button
            {
                Text     = "Hoàn tác",
                Location = new Point(row.Width - 100, 8),
                Width    = 85,
                Height   = 24
            };
            btnUndo.Click += (s, e) =>
            {
                item.Status = (item.DbRows.Count > 0) ? ImageStatus.Saved : ImageStatus.New;
                _hasUnsaved = true;
                RenderImageList();
            };
            row.Controls.Add(btnUndo);

            return row;
        }

        // ── Status label helper ───────────────────────────────────────────────
        private Label BuildStatusLabel(ImageStatus status)
        {
            var lbl = new Label
            {
                AutoSize = true,
                Font     = new Font("Segoe UI", 8f, FontStyle.Bold),
                Padding  = new Padding(4, 2, 4, 2)
            };
            switch (status)
            {
                case ImageStatus.Saved:
                    lbl.Text      = "[Đã lưu]";
                    lbl.ForeColor = Color.FromArgb(25, 135, 84);
                    break;
                case ImageStatus.New:
                    lbl.Text      = "[Mới]";
                    lbl.ForeColor = Color.FromArgb(13, 110, 253);
                    break;
                case ImageStatus.Modified:
                    lbl.Text      = "[Đã sửa]";
                    lbl.ForeColor = Color.FromArgb(255, 143, 0);
                    break;
                case ImageStatus.Deleted:
                    lbl.Text      = "[Đã xóa]";
                    lbl.ForeColor = Color.FromArgb(220, 53, 69);
                    break;
            }
            return lbl;
        }

        // ── Thumbnail loading ─────────────────────────────────────────────────
        private void LoadThumbnail(PictureBox pic, ImageItem item)
        {
            if (item.Status == ImageStatus.New ||
               (item.Status == ImageStatus.Modified && !string.IsNullOrEmpty(item.LocalFilePath)))
            {
                // Local file — load synchronously (already on disk)
                try
                {
                    pic.Image = Image.FromFile(item.LocalFilePath);
                }
                catch
                {
                    pic.BackColor = Color.LightGray;
                }
            }
            else if (!string.IsNullOrEmpty(item.DuongDanHinh))
            {
                // Drive URL — download asynchronously to avoid blocking UI
                string url = item.DuongDanHinh;
                Task.Run(() =>
                {
                    try
                    {
                        byte[] data;
                        using (var wc = new WebClient())
                            data = wc.DownloadData(url);

                        if (data != null && data.Length > 0)
                        {
                            using (var ms = new MemoryStream(data))
                            {
                                var img = Image.FromStream(ms);
                                pic.BeginInvoke((Action)(() =>
                                {
                                    if (!pic.IsDisposed)
                                        pic.Image = img;
                                }));
                            }
                        }
                    }
                    catch { /* silently ignore — thumbnail is optional */ }
                });
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // DRAG AND DROP (both pnlDropZone and flpImages forward here)
        // ─────────────────────────────────────────────────────────────────────
        private void pnlDropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void pnlDropZone_DragDrop(object sender, DragEventArgs e)
        {
            if (_selectedIDThuoc < 0)
            {
                MessageBox.Show("Vui lòng chọn một thuốc trước khi thêm hình ảnh.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            StageFiles(files);
        }

        // ─────────────────────────────────────────────────────────────────────
        // BROWSE BUTTON
        // ─────────────────────────────────────────────────────────────────────
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (_selectedIDThuoc < 0)
            {
                MessageBox.Show("Vui lòng chọn một thuốc trước khi thêm hình ảnh.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title       = "Chọn file hình ảnh";
                dlg.Filter      = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp";
                dlg.Multiselect = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                    StageFiles(dlg.FileNames);
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // STAGE FILES
        // ─────────────────────────────────────────────────────────────────────
        private void StageFiles(string[] filePaths)
        {
            // Filter to image files only
            var imageFiles = filePaths
                .Where(f => ImageExtensions.Contains(
                    Path.GetExtension(f).ToLowerInvariant()))
                .OrderBy(f => Path.GetFileName(f), StringComparer.OrdinalIgnoreCase)
                .ToArray();

            if (imageFiles.Length == 0) return;

            // Next order number = max existing order + 1
            int nextOrder = _items.Any()
                ? _items.Max(i => i.Order) + 1
                : 1;

            foreach (var path in imageFiles)
            {
                var item = new ImageItem
                {
                    LocalFilePath = path,
                    DuongDanHinh  = "",
                    Order         = nextOrder++,
                    OriginalOrder = 0,
                    Status        = ImageStatus.New,
                    DisplayName   = Path.GetFileName(path)
                };
                _items.Add(item);
            }

            _hasUnsaved = true;
            RenderImageList();
        }

        // ─────────────────────────────────────────────────────────────────────
        // CHANGE IMAGE
        // ─────────────────────────────────────────────────────────────────────
        private void ChangeImage_Click(ImageItem item, TextBox orderBox)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title  = "Chọn file hình ảnh thay thế";
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    item.OldDuongDanHinh = item.DuongDanHinh; // remember for Drive deletion
                    item.LocalFilePath   = dlg.FileName;
                    item.DisplayName     = Path.GetFileName(dlg.FileName);
                    item.Status          = ImageStatus.Modified;
                    _hasUnsaved          = true;
                    RenderImageList();
                }
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // REMOVE IMAGE
        // ─────────────────────────────────────────────────────────────────────
        private void RemoveImage_Click(ImageItem item)
        {
            if (item.Status == ImageStatus.New)
            {
                _items.Remove(item);
            }
            else
            {
                item.Status = ImageStatus.Deleted;
                _hasUnsaved = true;
            }
            RenderImageList();
        }

        // ─────────────────────────────────────────────────────────────────────
        // CLEAR ALL
        // ─────────────────────────────────────────────────────────────────────
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn xóa tất cả hình ảnh của thuốc này không?\n" +
                "Thao tác sẽ chỉ thực hiện khi bạn nhấn \"Lưu\".",
                "Xác nhận xóa tất cả",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            for (int i = _items.Count - 1; i >= 0; i--)
            {
                if (_items[i].Status == ImageStatus.New)
                    _items.RemoveAt(i);
                else
                    _items[i].Status = ImageStatus.Deleted;
            }
            _hasUnsaved = true;
            RenderImageList();
        }

        // ─────────────────────────────────────────────────────────────────────
        // SAVE
        // ─────────────────────────────────────────────────────────────────────
        private void btnSave_Click(object sender, EventArgs e)
        {
            string folderId = ConfigurationManager.AppSettings["GoogleDriveFolderId"];
            if (string.IsNullOrEmpty(folderId) || folderId == "YOUR_FOLDER_ID_HERE")
            {
                MessageBox.Show(
                    "Chưa cấu hình Google Drive Folder ID.\n" +
                    "Vui lòng mở App.config và điền giá trị cho 'GoogleDriveFolderId'.",
                    "Thiếu cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnSave.Enabled    = false;
            btnClearAll.Enabled = false;
            Cursor = Cursors.WaitCursor;

            int successCount = 0;
            var errors = new System.Text.StringBuilder();

            try
            {
                // Get all NhanDang IDs for fan-out (used for NEW items)
                List<int> nhanDangIds = null;

                foreach (var item in _items.ToList())
                {
                    try
                    {
                        if (item.Status == ImageStatus.New)
                        {
                            // Lazy-load NhanDang IDs once
                            if (nhanDangIds == null)
                                nhanDangIds = getdata.GetDSNhanDangIDsByThuoc(_selectedIDThuoc);

                            string fileId = GoogleDriveHelper.UploadFile(item.LocalFilePath, folderId);
                            GoogleDriveHelper.SetFilePublic(fileId);
                            string url = GoogleDriveHelper.GetPublicUrl(fileId, item.DisplayName);

                            foreach (int idNhanDang in nhanDangIds)
                            {
                                insertdata.InsertHinhAnhThuocChiTiet(new HinhAnhThuocChiTiet
                                {
                                    IDNhanDang   = idNhanDang,
                                    DuongDanHinh = url,
                                    MoTa         = item.Order.ToString()
                                });
                            }
                            successCount++;
                        }
                        else if (item.Status == ImageStatus.Modified)
                        {
                            string fileId = GoogleDriveHelper.UploadFile(item.LocalFilePath, folderId);
                            GoogleDriveHelper.SetFilePublic(fileId);
                            string newUrl = GoogleDriveHelper.GetPublicUrl(fileId, item.DisplayName);

                            // Delete old Drive file
                            string oldFileId = GoogleDriveHelper.ExtractFileId(item.OldDuongDanHinh);
                            if (!string.IsNullOrEmpty(oldFileId))
                                GoogleDriveHelper.DeleteFile(oldFileId);

                            // Update all DB rows
                            foreach (var row in item.DbRows)
                                updatedata.UpdateHinhAnhThuocChiTiet(
                                    row.IDHinhAnh, row.IDNhanDang, newUrl, item.Order.ToString());

                            successCount++;
                        }
                        else if (item.Status == ImageStatus.Deleted)
                        {
                            // Delete Drive file
                            string fileId = GoogleDriveHelper.ExtractFileId(item.DuongDanHinh);
                            if (!string.IsNullOrEmpty(fileId))
                                GoogleDriveHelper.DeleteFile(fileId);

                            // Delete all DB rows
                            foreach (var row in item.DbRows)
                                deletedata.DeleteHinhAnhThuocChiTiet(row.IDHinhAnh);

                            successCount++;
                        }
                        else if (item.Status == ImageStatus.Saved && item.Order != item.OriginalOrder)
                        {
                            // Order changed — update MoTa for all fan-out rows
                            foreach (var row in item.DbRows)
                                updatedata.UpdateHinhAnhThuocChiTiet(
                                    row.IDHinhAnh, row.IDNhanDang, row.DuongDanHinh, item.Order.ToString());

                            successCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine(item.DisplayName + ": " + ex.Message);
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                btnSave.Enabled    = true;
                btnClearAll.Enabled = true;
            }

            if (errors.Length > 0)
                MessageBox.Show("Một số mục gặp lỗi:\n" + errors.ToString(),
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Lưu thành công " + successCount + " mục.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reload from DB to show the committed state
            LoadImagesForDrug(_selectedIDThuoc);
        }

        // ─────────────────────────────────────────────────────────────────────
        // HELPERS
        // ─────────────────────────────────────────────────────────────────────
        private void TxtOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, backspace, delete only
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
    }
}
