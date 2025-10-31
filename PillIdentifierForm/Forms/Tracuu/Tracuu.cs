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
    public partial class Tracuu : Form
    {
        private List<Thuoc> allResults = new List<Thuoc>();
        private List<Thuoc> currentPageResults = new List<Thuoc>();
        private int currentPage = 1;
        private int itemsPerPage = 12;
        private int totalPages = 0;
        private KetnoiDB.GetData bll = new KetnoiDB.GetData();

        public Tracuu()
        {
            InitializeComponent();
            InitializeForm();
        }
        private void InitializeForm()
        {
            LoadComboBoxes();

            // Event handlers
            btnSearch.Click += BtnSearch_Click;
            btnClear.Click += BtnClear_Click;
            btnPrevPage.Click += BtnPrevPage_Click;
            btnNextPage.Click += BtnNextPage_Click;
            cboItemsPerPage.SelectedIndexChanged += CboItemsPerPage_SelectedIndexChanged;

            UpdatePaginationControls();
        }

        private void LoadComboBoxes()
        {
            try
            {
                List<MauSac> _listMauSac = bll.GetDSMauSac().OrderBy(h => h.TenMauSac).ToList();
                cboMauSac1.DataSource = _listMauSac;
                cboMauSac1.DisplayMember = "TenMauSac";
                cboMauSac1.ValueMember = "IDMauSac";
                
                cboMauSac2.DataSource = _listMauSac;
                cboMauSac2.DisplayMember = "TenMauSac";
                cboMauSac2.ValueMember = "IDMauSac";

                List<HinhDang> _listHinhDang = bll.GetDSHinhDang().OrderBy(h => h.TenHinhDang).ToList();
                cboHinhDang.DataSource = _listHinhDang;
                cboHinhDang.DisplayMember = "TenHinhDang";
                cboHinhDang.ValueMember = "IDHinhDang";

                List<DangThuoc> _listDangThuoc = bll.GetDSDangThuoc().OrderBy(h => h.TenDangThuoc).ToList();
                cboDangThuoc.DataSource = _listDangThuoc;
                cboDangThuoc.DisplayMember = "TenDangThuoc";
                cboDangThuoc.ValueMember = "IDDangThuoc";

                List<LoaiViThuoc> _listLoaiViThuoc = bll.GetDSLoaiViThuoc().OrderBy(h => h.TenLoaiVi).ToList();
                cboLoaiVi.DataSource = _listLoaiViThuoc;
                cboLoaiVi.DisplayMember = "TenLoaiVi";
                cboLoaiVi.ValueMember = "IDLoaiViThuoc";

                List<LoaiRanh> _listLoaiRanh = bll.GetDSLoaiRanh().OrderBy(h => h.TenLoaiRanh).ToList();
                cboLoaiRanh.DataSource = _listLoaiRanh;
                cboLoaiRanh.DisplayMember = "TenLoaiRanh";
                cboLoaiRanh.ValueMember = "IDLoaiRanh";

                cboItemsPerPage.Items.AddRange(new object[] { 6, 12, 24, 48 });
                cboItemsPerPage.SelectedIndex = 1; // Default 12

                cboMauSac1.SelectedIndex = -1;
                cboMauSac2.SelectedIndex = -1;
                cboHinhDang.SelectedIndex = -1;
                cboDangThuoc.SelectedIndex = -1;
                cboLoaiVi.SelectedIndex = -1;
                cboLoaiRanh.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate at least 1 search criterion
                if (string.IsNullOrWhiteSpace(txtImprintFront.Text) &&
                    string.IsNullOrWhiteSpace(txtImprintBack.Text) &&
                    cboMauSac1.SelectedValue == null &&
                    cboMauSac2.SelectedValue == null &&
                    cboHinhDang.SelectedValue == null &&
                    cboDangThuoc.SelectedValue == null &&
                    cboLoaiVi.SelectedValue == null &&
                    cboLoaiRanh.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập ít nhất 1 tiêu chí tìm kiếm!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string imprintFront = txtImprintFront.Text.Trim();
                string imprintBack = txtImprintBack.Text.Trim();
                int? idMauSac1 = cboMauSac1.SelectedValue as int?;
                int? idMauSac2 = cboMauSac2.SelectedValue as int?;
                int? idHinhDang = cboHinhDang.SelectedValue as int?;
                int? idDangThuoc = cboDangThuoc.SelectedValue as int?;
                int? idLoaiVi = cboLoaiVi.SelectedValue as int?;
                int? idLoaiRanh = cboLoaiRanh.SelectedValue as int?;

                double? kichThuoc = null;

                if (nudKichThuoc.Value > 0)
                {
                    kichThuoc = (double)nudKichThuoc.Value;
                }

                // Search
                allResults = bll.GetNhanDangThuoc(
                    string.IsNullOrWhiteSpace(imprintFront) ? null : imprintFront,
                    string.IsNullOrWhiteSpace(imprintBack) ? null : imprintBack,
                    idMauSac1, idMauSac2, idHinhDang, idDangThuoc,
                    idLoaiVi, idLoaiRanh, kichThuoc);

                currentPage = 1;
                totalPages = (int)Math.Ceiling((double)allResults.Count / itemsPerPage);

                lblTotalResults.Text = string.Format("Tổng: {0} kết quả", allResults.Count);

                if (allResults.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp!\n\n" +
                                   "Gợi ý:\n" +
                                   "- Kiểm tra lại chính tả khắc dấu\n" +
                                   "- Thử bỏ bớt một số tiêu chí\n" +
                                   "- Tăng dung sai kích thước",
                                   "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DisplayCurrentPage();
                UpdatePaginationControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtImprintFront.Clear();
            txtImprintBack.Clear();
            cboMauSac1.SelectedIndex = -1;
            cboMauSac2.SelectedIndex = -1;
            cboHinhDang.SelectedIndex = -1;
            cboDangThuoc.SelectedIndex = -1;
            cboLoaiVi.SelectedIndex = -1;
            cboLoaiRanh.SelectedIndex = -1;
            nudKichThuoc.Value = 0;

            allResults.Clear();
            flpResults.Controls.Clear();
            lblTotalResults.Text = "Tổng: 0 kết quả";
            currentPage = 1;
            UpdatePaginationControls();
        }

        private void BtnPrevPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayCurrentPage();
                UpdatePaginationControls();
                flpResults.ScrollControlIntoView(flpResults);
            }
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayCurrentPage();
                UpdatePaginationControls();
                flpResults.ScrollControlIntoView(flpResults);
            }
        }

        private void CboItemsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemsPerPage = (int)cboItemsPerPage.SelectedItem;
            currentPage = 1;
            totalPages = (int)Math.Ceiling((double)allResults.Count / itemsPerPage);
            DisplayCurrentPage();
            UpdatePaginationControls();
        }

        private void DisplayCurrentPage()
        {
            flpResults.Controls.Clear();

            if (allResults.Count == 0)
                return;

            int startIndex = (currentPage - 1) * itemsPerPage;
            currentPageResults = allResults.Skip(startIndex).Take(itemsPerPage).ToList();

            foreach (var thuoc in currentPageResults)
            {
                Panel cardPanel = CreateResultCard(thuoc);
                flpResults.Controls.Add(cardPanel);
            }
        }

        private Panel CreateResultCard(Thuoc thuoc)
        {
            NhanDangThuoc nhandang = bll.GetNhanDangByThuoc(thuoc);
            // Card Panel: 250x320 pixels
            Panel card = new Panel();
            card.Width = 250;
            card.Height = 320;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(10);
            card.BackColor = Color.White;
            card.Cursor = Cursors.Hand;

            // Picture Box
            PictureBox picDrug = new PictureBox();
            picDrug.Width = 230;
            picDrug.Height = 180;
            picDrug.Top = 10;
            picDrug.Left = 10;
            picDrug.SizeMode = PictureBoxSizeMode.Zoom;
            picDrug.BorderStyle = BorderStyle.FixedSingle;
            picDrug.BackColor = Color.WhiteSmoke;

            // Load image based on MaHinh
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Images", "Drugs", nhandang.MaHinh + ".jpg");
                if (File.Exists(imagePath))
                {
                    picDrug.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // Try PNG
                    imagePath = Path.Combine(Application.StartupPath, "Images", "Drugs", nhandang.MaHinh + ".png");
                    if (File.Exists(imagePath))
                    {
                        picDrug.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        // Placeholder text
                        Label lblNoImage = new Label();
                        lblNoImage.Text = "Không có hình";
                        lblNoImage.TextAlign = ContentAlignment.MiddleCenter;
                        lblNoImage.Dock = DockStyle.Fill;
                        lblNoImage.ForeColor = Color.Gray;
                        lblNoImage.Font = new Font("Arial", 10, FontStyle.Italic);
                        picDrug.Controls.Add(lblNoImage);
                    }
                }
            }
            catch
            {
                picDrug.BackColor = Color.LightGray;
            }

            // Drug name
            Label lblName = new Label();
            lblName.Text = thuoc.TenThuoc ?? "N/A";
            lblName.Top = 200;
            lblName.Left = 10;
            lblName.Width = 230;
            lblName.Height = 45;
            lblName.Font = new Font("Arial", 9, FontStyle.Bold);
            lblName.TextAlign = ContentAlignment.TopLeft;
            lblName.AutoEllipsis = true;

            // SDK
            Label lblSDK = new Label();
            lblSDK.Text = "SDK: " + (thuoc.SDK ?? "N/A");
            lblSDK.Top = 250;
            lblSDK.Left = 10;
            lblSDK.Width = 230;
            lblSDK.Height = 20;
            lblSDK.Font = new Font("Arial", 8, FontStyle.Regular);
            lblSDK.ForeColor = Color.DarkBlue;

            // MaHinh
            Label lblMaHinh = new Label();
            lblMaHinh.Text = "Mã: " + (nhandang.MaHinh ?? "N/A");
            lblMaHinh.Top = 275;
            lblMaHinh.Left = 10;
            lblMaHinh.Width = 115;
            lblMaHinh.Height = 20;
            lblMaHinh.Font = new Font("Arial", 7, FontStyle.Italic);
            lblMaHinh.ForeColor = Color.Gray;

            // Size (if available)
            Label lblSize = new Label();
            lblSize.Text = nhandang.KichThuoc > 0 ? string.Format("⌀ {0:F1}mm", nhandang.KichThuoc) : "";
            lblSize.Top = 275;
            lblSize.Left = 130;
            lblSize.Width = 110;
            lblSize.Height = 20;
            lblSize.Font = new Font("Arial", 7, FontStyle.Regular);
            lblSize.ForeColor = Color.DarkGreen;
            lblSize.TextAlign = ContentAlignment.TopRight;

            // Hover effects
            card.MouseEnter += delegate(object sender, EventArgs e)
            {
                card.BackColor = Color.FromArgb(230, 240, 255);
            };
            card.MouseLeave += delegate(object sender, EventArgs e)
            {
                card.BackColor = Color.White;
            };

            // Click handlers
            EventHandler clickHandler = delegate(object sender, EventArgs e)
            {
                ShowDrugDetails(thuoc);
            };
            card.Click += clickHandler;
            picDrug.Click += clickHandler;
            lblName.Click += clickHandler;
            lblSDK.Click += clickHandler;
            lblMaHinh.Click += clickHandler;
            lblSize.Click += clickHandler;

            // Add controls
            card.Controls.Add(picDrug);
            card.Controls.Add(lblName);
            card.Controls.Add(lblSDK);
            card.Controls.Add(lblMaHinh);
            card.Controls.Add(lblSize);

            return card;
        }

        private void UpdatePaginationControls()
        {
            btnPrevPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = currentPage < totalPages;

            if (totalPages > 0)
            {
                lblPageInfo.Text = string.Format("Trang {0}/{1}", currentPage, totalPages);
            }
            else
            {
                lblPageInfo.Text = "Trang 0/0";
            }
        }

        private void ShowDrugDetails(Thuoc thuoc)
        {
            NhanDangThuoc nhandang = bll.GetNhanDangByThuoc(thuoc);
            // Open detail form or show comprehensive info
            StringBuilder details = new StringBuilder();
            details.AppendLine("Tên thuốc: " + thuoc.TenThuoc);
            details.AppendLine("Số đăng ký: " + thuoc.SDK);
            details.AppendLine("Mã hình: " + nhandang.MaHinh);
            details.AppendLine("ID: " + thuoc.IDThuoc.ToString());

            if (nhandang.KichThuoc > 0)
                details.AppendLine(string.Format("Kích thước: {0:F1} mm", nhandang.KichThuoc));

            // Add more fields as available from your Thuoc class

            MessageBox.Show(details.ToString(), "Chi tiết thuốc",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}