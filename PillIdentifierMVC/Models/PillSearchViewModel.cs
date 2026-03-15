using System.Collections.Generic;
using ClassChung;

namespace PillIdentifierMVC.Models
{
    public class PillSearchFilter
    {
        public bool? CoKhacDau { get; set; }
        public string ImprintFront { get; set; }
        public string ImprintBack { get; set; }
        public List<int> SelectedHinhDangIds { get; set; }
        public int? IdMauSac1 { get; set; }
        public int? IdMauSac2 { get; set; }
        public int? IdDangThuoc { get; set; }
        public int? IdLoaiVi { get; set; }
        public int? IdLoaiRanh { get; set; }
        public double? KichThuoc { get; set; }
        public int? IdChiDinh { get; set; } // placeholder — not wired to search yet
    }

    public class PillResultCard
    {
        public Thuoc Thuoc { get; set; }
        public NhanDangThuoc NhanDang { get; set; }
        public List<MauSac> Mausac { get; set; }
        public string TenHinhDang { get; set; }
        public string TenDangThuoc { get; set; }
        public string TenLoaiRanh { get; set; }
        public string TenLoaiVi { get; set; }
        public string TenHoatChat { get; set; }
    }

    public class PillSearchPageModel
    {
        public PillSearchFilter Filter { get; set; }
        public List<HinhDang> HinhDangs { get; set; }
        public List<MauSac> MauSacs { get; set; }
        public List<DangThuoc> DangThuocs { get; set; }
        public List<LoaiRanh> LoaiRanhs { get; set; }
        public List<LoaiViThuoc> LoaiViThuocs { get; set; }
        public List<ChiDinh> ChiDinhs { get; set; }
        public List<PillResultCard> Results { get; set; }
        public bool HasSearched { get; set; }
    }

    public class PillDetailModel
    {
        public Thuoc Thuoc { get; set; }
        public NhanDangThuoc NhanDang { get; set; }
        public List<MauSac> Mausac { get; set; }
        public string TenHinhDang { get; set; }
        public string TenDangThuoc { get; set; }
        public string TenLoaiRanh { get; set; }
        public string TenLoaiVi { get; set; }
        public string TenHoatChat { get; set; }
    }
}
