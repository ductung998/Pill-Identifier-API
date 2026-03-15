using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClassChung;
using PillIdentifierMVC.Models;

namespace PillIdentifierMVC.Controllers
{
    public class HomeController : Controller
    {
        private PillSearchPageModel LoadReferenceData()
        {
            var db = new KetnoiDB.GetData();
            return new PillSearchPageModel
            {
                HinhDangs = db.GetDSHinhDang(),
                MauSacs = db.GetDSMauSac(),
                DangThuocs = db.GetDSDangThuoc(),
                LoaiRanhs = db.GetDSLoaiRanh(),
                LoaiViThuocs = db.GetDSLoaiViThuoc(),
                ChiDinhs = db.GetDSChiDinh(),
                Filter = new PillSearchFilter()
            };
        }

        public ActionResult Index()
        {
            return View(LoadReferenceData());
        }

        [HttpPost]
        public ActionResult Search(PillSearchFilter filter)
        {
            var model = LoadReferenceData();
            model.Filter = filter;
            model.HasSearched = true;

            var db = new KetnoiDB.GetData();

            // Bulk load — fixed DB call count regardless of result size
            var allNhanDang   = db.GetDSNhanDangThuoc();
            var allThuocMauSac = db.GetDSThuoc_MauSac();
            var allHoatChat   = db.GetDSHoatChat();

            // Only pass imprint text when user indicated the pill HAS an imprint
            string imprintFront = filter.CoKhacDau == true ? filter.ImprintFront : null;
            string imprintBack  = filter.CoKhacDau == true ? filter.ImprintBack  : null;

            // Multi-select shape: OR logic — one call per selected shape, union results
            var shapeIds = filter.SelectedHinhDangIds != null && filter.SelectedHinhDangIds.Count > 0
                ? filter.SelectedHinhDangIds
                : new List<int> { 0 }; // 0 = sentinel for "no shape filter"

            var searchResults = new List<Thuoc>();
            foreach (var shapeId in shapeIds)
            {
                var partial = db.GetNhanDangThuoc(
                    imprintFront: imprintFront,
                    imprintBack:  imprintBack,
                    idMausac1:    filter.IdMauSac1,
                    idMausac2:    filter.IdMauSac2,
                    idHinhdang:   shapeId > 0 ? (int?)shapeId : null,
                    idDangthuoc:  filter.IdDangThuoc,
                    idLoaiVi:     filter.IdLoaiVi,
                    idLoaiRanh:   filter.IdLoaiRanh,
                    kichThuoc:    filter.KichThuoc
                );
                searchResults.AddRange(partial);
            }

            // Deduplicate by IDThuoc
            searchResults = searchResults.GroupBy(x => x.IDThuoc).Select(g => g.First()).ToList();

            // Join in memory — build result cards
            var drugIds      = searchResults.Select(x => x.IDThuoc).ToList();
            var nhanDangMap  = allNhanDang.Where(x => drugIds.Contains(x.IDThuoc)).ToList();

            model.Results = searchResults.Select(thuoc =>
            {
                var nhanDang   = nhanDangMap.FirstOrDefault(x => x.IDThuoc == thuoc.IDThuoc);
                var mauSacIds  = allThuocMauSac.Where(x => x.IDThuoc == thuoc.IDThuoc).Select(x => x.IDMauSac).ToList();
                var mauSacs    = model.MauSacs.Where(x => mauSacIds.Contains(x.IDMauSac)).ToList();
                var hoatChat   = allHoatChat.FirstOrDefault(x => x.IDHoatChat == thuoc.IDHoatChat);

                return new PillResultCard
                {
                    Thuoc        = thuoc,
                    NhanDang     = nhanDang,
                    Mausac       = mauSacs,
                    TenHinhDang  = nhanDang != null ? model.HinhDangs.FirstOrDefault(x => x.IDHinhDang == nhanDang.IDHinhDang)?.TenHinhDang : null,
                    TenDangThuoc = nhanDang != null ? model.DangThuocs.FirstOrDefault(x => x.IDDangThuoc == nhanDang.IDDangThuoc)?.TenDangThuoc : null,
                    TenLoaiRanh  = nhanDang != null && nhanDang.IDLoaiRanh > 0 ? model.LoaiRanhs.FirstOrDefault(x => x.IDLoaiRanh == nhanDang.IDLoaiRanh)?.TenLoaiRanh : null,
                    TenLoaiVi    = nhanDang != null && nhanDang.IDLoaiViThuoc > 0 ? model.LoaiViThuocs.FirstOrDefault(x => x.IDLoaiViThuoc == nhanDang.IDLoaiViThuoc)?.TenLoaiVi : null,
                    TenHoatChat  = hoatChat?.TenHoatChat
                };
            }).ToList();

            return View("Results", model);
        }

        public ActionResult Detail(int id)
        {
            var db      = new KetnoiDB.GetData();
            var thuoc   = db.GetThuoc(id);
            if (thuoc == null) return HttpNotFound();

            var nhanDang     = db.GetNhanDangByThuoc(id);
            var hinhDangs    = db.GetDSHinhDang();
            var dangThuocs   = db.GetDSDangThuoc();
            var loaiRanhs    = db.GetDSLoaiRanh();
            var loaiViThuocs = db.GetDSLoaiViThuoc();
            var allHoatChat  = db.GetDSHoatChat();

            var model = new PillDetailModel
            {
                Thuoc        = thuoc,
                NhanDang     = nhanDang,
                Mausac       = thuoc.Mausac ?? new List<MauSac>(),
                TenHinhDang  = nhanDang != null ? hinhDangs.FirstOrDefault(x => x.IDHinhDang == nhanDang.IDHinhDang)?.TenHinhDang : null,
                TenDangThuoc = nhanDang != null ? dangThuocs.FirstOrDefault(x => x.IDDangThuoc == nhanDang.IDDangThuoc)?.TenDangThuoc : null,
                TenLoaiRanh  = nhanDang != null && nhanDang.IDLoaiRanh > 0 ? loaiRanhs.FirstOrDefault(x => x.IDLoaiRanh == nhanDang.IDLoaiRanh)?.TenLoaiRanh : null,
                TenLoaiVi    = nhanDang != null && nhanDang.IDLoaiViThuoc > 0 ? loaiViThuocs.FirstOrDefault(x => x.IDLoaiViThuoc == nhanDang.IDLoaiViThuoc)?.TenLoaiVi : null,
                TenHoatChat  = allHoatChat.FirstOrDefault(x => x.IDHoatChat == thuoc.IDHoatChat)?.TenHoatChat
            };

            return View(model);
        }

        public ActionResult Instructions()
        {
            return View();
        }
    }
}
