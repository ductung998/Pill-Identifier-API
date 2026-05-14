using System;
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

            var allThuocMauSac = db.GetDSThuoc_MauSac();
            var allHoatChat    = db.GetDSHoatChat();

            string imprintFront = filter.CoKhacDau == true ? filter.ImprintFront : null;
            string imprintBack  = filter.CoKhacDau == true ? filter.ImprintBack  : null;

            var shapeIds = filter.SelectedHinhDangIds != null && filter.SelectedHinhDangIds.Count > 0
                ? filter.SelectedHinhDangIds
                : null;

            // Returns one NhanDangThuoc per matched packaging variant
            var matchedNhanDangs = db.GetNhanDangThuoc(
                hasImprint:   filter.CoKhacDau,
                imprintFront: imprintFront,
                imprintBack:  imprintBack,
                idMausac1:    filter.IdMauSac1,
                idMausac2:    filter.IdMauSac2,
                idHinhdangs:  shapeIds,
                idDangthuoc:  filter.IdDangThuoc,
                idLoaiVi:     filter.IdLoaiVi,
                idLoaiRanh:   filter.IdLoaiRanh,
                kichThuoc:    filter.KichThuoc
            );

            // One result card per drug — use the first matched NhanDang for each drug
            var distinctMatches = matchedNhanDangs
                .GroupBy(x => x.IDThuoc)
                .Select(g => g.First())
                .ToList();

            var drugIds      = distinctMatches.Select(x => x.IDThuoc).ToList();
            var allThuocList = db.GetDSThuoc().Where(x => drugIds.Contains(x.IDThuoc)).ToList();

            model.Results = distinctMatches.Select(nhanDang =>
            {
                var thuoc     = allThuocList.FirstOrDefault(x => x.IDThuoc == nhanDang.IDThuoc);
                var mauSacIds = allThuocMauSac.Where(x => x.IDThuoc == nhanDang.IDThuoc).Select(x => x.IDMauSac).ToList();
                var mauSacs   = model.MauSacs.Where(x => mauSacIds.Contains(x.IDMauSac)).ToList();
                var hoatChat  = thuoc != null ? allHoatChat.FirstOrDefault(x => x.IDHoatChat == thuoc.IDHoatChat) : null;

                return new PillResultCard
                {
                    Thuoc        = thuoc,
                    NhanDang     = nhanDang,
                    Mausac       = mauSacs,
                    TenHinhDang  = model.HinhDangs.FirstOrDefault(x => x.IDHinhDang == nhanDang.IDHinhDang)?.TenHinhDang,
                    TenDangThuoc = model.DangThuocs.FirstOrDefault(x => x.IDDangThuoc == nhanDang.IDDangThuoc)?.TenDangThuoc,
                    TenLoaiRanh  = nhanDang.IDLoaiRanh > 0 ? model.LoaiRanhs.FirstOrDefault(x => x.IDLoaiRanh == nhanDang.IDLoaiRanh)?.TenLoaiRanh : null,
                    TenLoaiVi    = nhanDang.IDLoaiViThuoc > 0 ? model.LoaiViThuocs.FirstOrDefault(x => x.IDLoaiViThuoc == nhanDang.IDLoaiViThuoc)?.TenLoaiVi : null,
                    TenHoatChat  = hoatChat?.TenHoatChat
                };
            }).Where(c => c.Thuoc != null).ToList();

            foreach (var card in model.Results)
            {
                if (card.NhanDang != null && card.NhanDang.IDNhanDang > 0)
                {
                    var firstImg = db.GetDSHinhAnhbyThuoc(card.Thuoc.IDThuoc)
                        .Where(x => x.IDNhanDang == card.NhanDang.IDNhanDang)
                        .OrderBy(x => { int n; return int.TryParse(x.MoTa, out n) ? n : int.MaxValue; })
                        .Select(x => x.DuongDanHinh)
                        .FirstOrDefault();
                    card.FirstImageUrl = ToDriveDirectUrl(firstImg);
                }
            }

            return View("Results", model);
        }

        public ActionResult Detail(int id)
        {
            var db           = new KetnoiDB.GetData();
            var thuoc        = db.GetThuoc(id);
            if (thuoc == null) return HttpNotFound();

            var allNhanDangs = db.GetDSNhanDangByThuoc(id);
            var hinhDangs    = db.GetDSHinhDang();
            var dangThuocs   = db.GetDSDangThuoc();
            var loaiRanhs    = db.GetDSLoaiRanh();
            var loaiViThuocs = db.GetDSLoaiViThuoc();
            var allHoatChat  = db.GetDSHoatChat();

            // Images are drug-level (fanned out to each NhanDang) — load once via first NhanDang to avoid duplicates
            var firstNhanDangId = allNhanDangs.Select(x => x.IDNhanDang).FirstOrDefault();
            var hinhAnhList = db.GetDSHinhAnhbyThuoc(id)
                                .Where(x => x.IDNhanDang == firstNhanDangId)
                                .OrderBy(x => { int n; return int.TryParse(x.MoTa, out n) ? n : int.MaxValue; })
                                .ToList();
            foreach (var img in hinhAnhList)
                img.DuongDanHinh = ToDriveDirectUrl(img.DuongDanHinh);

            var nhanDangDetails = allNhanDangs.Select(nd => new NhanDangDetail
            {
                NhanDang     = nd,
                TenHinhDang  = hinhDangs.FirstOrDefault(x => x.IDHinhDang == nd.IDHinhDang)?.TenHinhDang,
                TenDangThuoc = dangThuocs.FirstOrDefault(x => x.IDDangThuoc == nd.IDDangThuoc)?.TenDangThuoc,
                TenLoaiRanh  = nd.IDLoaiRanh > 0 ? loaiRanhs.FirstOrDefault(x => x.IDLoaiRanh == nd.IDLoaiRanh)?.TenLoaiRanh : null,
                TenLoaiVi    = nd.IDLoaiViThuoc > 0 ? loaiViThuocs.FirstOrDefault(x => x.IDLoaiViThuoc == nd.IDLoaiViThuoc)?.TenLoaiVi : null,
            }).ToList();

            var model = new PillDetailModel
            {
                Thuoc       = thuoc,
                NhanDangs   = nhanDangDetails,
                Mausac      = thuoc.Mausac ?? new List<MauSac>(),
                TenHoatChat = allHoatChat.FirstOrDefault(x => x.IDHoatChat == thuoc.IDHoatChat)?.TenHoatChat,
                HinhAnhList = hinhAnhList
            };

            return View(model);
        }

        public ActionResult Instructions()
        {
            return View();
        }

        public ActionResult ImageProxy(string fileId)
        {
            if (string.IsNullOrEmpty(fileId)) return HttpNotFound();
            try
            {
                using (var wc = new System.Net.WebClient())
                {
                    wc.Headers[System.Net.HttpRequestHeader.UserAgent] =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36";
                    byte[] data = wc.DownloadData(
                        "https://drive.google.com/uc?export=view&id=" + fileId);
                    string ct     = wc.ResponseHeaders["Content-Type"] ?? "image/jpeg";
                    string mime   = ct.Split(';')[0].Trim();
                    if (!mime.StartsWith("image/")) return HttpNotFound();
                    Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
                    Response.Cache.SetMaxAge(TimeSpan.FromDays(7));
                    return File(data, mime);
                }
            }
            catch
            {
                return HttpNotFound();
            }
        }

        private static string ToDriveDirectUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return url;
            int idIdx = url.IndexOf("id=");
            if (idIdx < 0) return url;
            int start  = idIdx + 3;
            int end    = url.IndexOf('&', start);
            string fileId = end < 0 ? url.Substring(start) : url.Substring(start, end - start);
            return string.IsNullOrEmpty(fileId) ? url : "/Home/ImageProxy?fileId=" + fileId;
        }
    }
}
