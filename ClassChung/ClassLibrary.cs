using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassChung
{
    #region Kết nối CSDL
    public class KetnoiDB
    {
        protected static DataClassesDataContext db = new DataClassesDataContext();
        #region Nhập liệu
        public class InsertData
        {
            public bool InsertChiDinh(string chiDinh, string moTa)
            {
                try
                {
                    //Tạo object và gán giá trị nhập
                    d_ChiDinh cd = new d_ChiDinh();
                    cd.TenChiDinh = chiDinh;
                    cd.MoTa = moTa;
                    //Insert vào DB và submits
                    db.d_ChiDinhs.InsertOnSubmit(cd);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertDangThuoc(string tenDangThuoc)
            {
                try
                {
                    d_DangThuoc dt = new d_DangThuoc();
                    dt.TenDangThuoc = tenDangThuoc;

                    db.d_DangThuocs.InsertOnSubmit(dt);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHinhAnhThuocChiTiet(int idNhanDang, string duongDanHinh, string moTa)
            {
                try
                {
                    d_HinhAnhThuocChiTiet ha = new d_HinhAnhThuocChiTiet();
                    ha.IDNhanDang = idNhanDang;
                    ha.DuongDanHinh = duongDanHinh;
                    ha.MoTa = moTa;

                    db.d_HinhAnhThuocChiTiets.InsertOnSubmit(ha);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHinhDang(string tenHinhDang)
            {
                try
                {
                    d_HinhDang hd = new d_HinhDang();
                    hd.TenHinhDang = tenHinhDang;

                    db.d_HinhDangs.InsertOnSubmit(hd);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHoatChat(string tenHoatChat, string loaiHoatChat)
            {
                try
                {
                    d_HoatChat hc = new d_HoatChat();
                    hc.TenHoatChat = tenHoatChat;
                    hc.LoaiHoatChat = loaiHoatChat;

                    db.d_HoatChats.InsertOnSubmit(hc);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHoatChatGoc(string tenHoatChat, string ghiChu)
            {
                try
                {
                    d_HoatChatGoc hcg = new d_HoatChatGoc();
                    hcg.TenHoatChat = tenHoatChat;
                    hcg.GhiChu = ghiChu;

                    db.d_HoatChatGocs.InsertOnSubmit(hcg);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertLoaiRanh(string tenLoaiRanh)
            {
                try
                {
                    d_LoaiRanh lr = new d_LoaiRanh();
                    lr.TenLoaiRanh = tenLoaiRanh;

                    db.d_LoaiRanhs.InsertOnSubmit(lr);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertLoaiViThuoc(string tenLoaiVi)
            {
                try
                {
                    d_LoaiViThuoc lvt = new d_LoaiViThuoc();
                    lvt.TenLoaiVi = tenLoaiVi;

                    db.d_LoaiViThuocs.InsertOnSubmit(lvt);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertMauSac(string tenMauSac)
            {
                try
                {
                    d_MauSac ms = new d_MauSac();
                    ms.TenMauSac = tenMauSac;

                    db.d_MauSacs.InsertOnSubmit(ms);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertThuoc(string tenThuoc, string sdk, int? idHoatChat,
                            string hamLuong, string dangBaoChe, string nhaSX, string ghiChu)
            {
                try
                {
                    d_Thuoc t = new d_Thuoc();
                    t.TenThuoc = tenThuoc;
                    t.SDK = sdk;
                    t.IDHoatChat = idHoatChat;
                    t.HamLuong = hamLuong;
                    t.DangBaoChe = dangBaoChe;
                    t.NhaSX = nhaSX;
                    t.GhiChu = ghiChu;

                    db.d_Thuocs.InsertOnSubmit(t);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHoatChat_HoatChatGoc(int idHoatChat, int idHoatChatGoc)
            {
                try
                {
                    r_HoatChat_HoatChatGoc link = new r_HoatChat_HoatChatGoc();
                    link.IDHoatChat = idHoatChat;
                    link.IDHoatChatGoc = idHoatChatGoc;

                    db.r_HoatChat_HoatChatGocs.InsertOnSubmit(link);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHoatChatGoc_ChiDinh(int idHoatChatGoc, int idChiDinh)
            {
                try
                {
                    r_HoatChatGoc_ChiDinh link = new r_HoatChatGoc_ChiDinh();
                    link.IDHoatChatGoc = idHoatChatGoc;
                    link.IDChiDinh = idChiDinh;

                    db.r_HoatChatGoc_ChiDinhs.InsertOnSubmit(link);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertThuoc_MauSac(int idThuoc, int idMauSac)
            {
                try
                {
                    r_Thuoc_MauSac link = new r_Thuoc_MauSac();
                    link.IDThuoc = idThuoc;
                    link.IDMauSac = idMauSac;

                    db.r_Thuoc_MauSacs.InsertOnSubmit(link);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertNhanDangThuoc(int idThuoc, bool coKhacDau,
                                    string khacDauMatTruoc, string khacDauMatSau,
                                    int idHinhDang, int idDangThuoc,
                                    int? idLoaiViThuoc, int? idLoaiRanh, string maHinh)
            {
                try
                {
                    w_NhanDangThuoc nd = new w_NhanDangThuoc();
                    nd.IDThuoc = idThuoc;
                    nd.CoKhacDau = coKhacDau;
                    nd.KhacDauMatTruoc = khacDauMatTruoc;
                    nd.KhacDauMatSau = khacDauMatSau;
                    nd.IDHinhDang = idHinhDang;
                    nd.IDDangThuoc = idDangThuoc;
                    nd.IDLoaiViThuoc = idLoaiViThuoc;
                    nd.IDLoaiRanh = idLoaiRanh;
                    nd.MaHinh = maHinh;

                    db.w_NhanDangThuocs.InsertOnSubmit(nd);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion
        #region Lấy dữ liệu
        public class GetData
        {
            // Lấy toàn bộ Chỉ Định
            public List<ChiDinh> GetDSChiDinh()
            {
                List<ChiDinh> kq = new List<ChiDinh>();
                List<d_ChiDinh> ds = (from data in db.d_ChiDinhs
                                      select data).ToList();
                foreach (d_ChiDinh i in ds)
                    kq.Add(new ChiDinh(i.IDChiDinh, i.TenChiDinh, i.MoTa));
                return kq;
            }
            // Lấy toàn bộ Dạng Thuốc
            public List<DangThuoc> GetDSDangThuoc()
            {
                List<DangThuoc> kq = new List<DangThuoc>();
                List<d_DangThuoc> ds = db.d_DangThuocs.ToList();
                foreach (d_DangThuoc i in ds)
                    kq.Add(new DangThuoc(i.IDDangThuoc, i.TenDangThuoc));
                return kq;
            }
            // Lấy toàn bộ Hình Dạng
            public List<HinhDang> GetDSHinhDang()
            {
                List<HinhDang> kq = new List<HinhDang>();

                List<d_HinhDang> ds = db.d_HinhDangs.ToList();
                foreach (d_HinhDang i in ds)
                    kq.Add(new HinhDang(i.IDHinhDang, i.TenHinhDang));

                return kq;
            }
            // Lấy toàn bộ Hoạt Chất
            public List<HoatChat> GetDSHoatChat()
            {
                List<HoatChat> kq = new List<HoatChat>();

                List<d_HoatChat> ds = db.d_HoatChats.ToList();
                foreach (d_HoatChat i in ds)
                    kq.Add(new HoatChat(i.IDHoatChat, i.TenHoatChat, i.LoaiHoatChat));

                return kq;
            }
            // Lấy toàn bộ Hoạt Chất Gốc
            public List<HoatChatGoc> GetDSHoatChatGoc()
            {
                List<HoatChatGoc> kq = new List<HoatChatGoc>();

                List<d_HoatChatGoc> ds = db.d_HoatChatGocs.ToList();
                foreach (d_HoatChatGoc i in ds)
                    kq.Add(new HoatChatGoc(i.IDHoatChatGoc, i.TenHoatChat, i.GhiChu));

                return kq;
            }
            // Lấy toàn bộ Loại Rãnh
            public List<LoaiRanh> GetDSLoaiRanh()
            {
                List<LoaiRanh> kq = new List<LoaiRanh>();

                List<d_LoaiRanh> ds = db.d_LoaiRanhs.ToList();
                foreach (d_LoaiRanh i in ds)
                    kq.Add(new LoaiRanh(i.IDLoaiRanh, i.TenLoaiRanh));

                return kq;
            }
            // Lấy toàn bộ Loại Vỉ Thuốc
            public List<LoaiViThuoc> GetDSLoaiViThuoc()
            {
                List<LoaiViThuoc> kq = new List<LoaiViThuoc>();

                List<d_LoaiViThuoc> ds = db.d_LoaiViThuocs.ToList();
                foreach (d_LoaiViThuoc i in ds)
                    kq.Add(new LoaiViThuoc(i.IDLoaiViThuoc, i.TenLoaiVi));

                return kq;
            }
            // Lấy toàn bộ Màu Sắc
            public List<MauSac> GetDSMauSac()
            {
                List<MauSac> kq = new List<MauSac>();

                List<d_MauSac> ds = db.d_MauSacs.ToList();
                foreach (d_MauSac i in ds)
                    kq.Add(new MauSac(i.IDMauSac, i.TenMauSac));
                return kq;
            }
            // Lấy toàn bộ Thuốc
            public List<Thuoc> GetDSThuoc()
            {
                List<Thuoc> kq = new List<Thuoc>();

                List<d_Thuoc> ds = db.d_Thuocs.ToList();
                foreach (d_Thuoc i in ds)
                    kq.Add(new Thuoc(i.IDThuoc, i.TenThuoc, i.SDK, i.IDHoatChat, i.HamLuong, i.DangBaoChe, i.NhaSX, i.GhiChu));
                return kq;
            }

            public List<ChiDinh> SearchChiDinh(string keyword)
            {
                List<ChiDinh> kq = new List<ChiDinh>();
                try
                {
                    List<d_ChiDinh> ds = db.d_ChiDinhs.Where(cd => cd.TenChiDinh.Contains(keyword)).ToList();

                    foreach (d_ChiDinh i in ds)
                        kq.Add(new ChiDinh(i.IDChiDinh, i.TenChiDinh, i.MoTa));
                    return kq;
                }
                catch
                {
                    return kq;
                }
            }
            public List<int> GetIDHoatChatbyChiDinh(int idChiDinh)
            {
                List<int> kq = new List<int>();
                try
                {
                    kq = (from hc in db.d_HoatChats
                          join relaHC_HCG in db.r_HoatChat_HoatChatGocs
                              on hc.IDHoatChat equals relaHC_HCG.IDHoatChat
                          join relaHCG_CD in db.r_HoatChatGoc_ChiDinhs
                              on relaHC_HCG.IDHoatChatGoc equals relaHCG_CD.IDHoatChatGoc
                          where relaHCG_CD.IDChiDinh == idChiDinh
                          select hc.IDHoatChat).Distinct().ToList();
                    return kq;
                }
                catch
                {
                    return kq;
                }
            }

            public List<HinhAnhThuocChiTiet> GetDSHinhAnhbyThuoc(int idThuoc)
            {
                List<HinhAnhThuocChiTiet> kq = new List<HinhAnhThuocChiTiet>();
                try
                {
                    List<d_HinhAnhThuocChiTiet> ds = (from data in db.d_HinhAnhThuocChiTiets
                                                      join rela in db.w_NhanDangThuocs
                                                  on data.IDNhanDang equals rela.IDNhanDang
                                                      where rela.IDThuoc == idThuoc
                                                      select data).ToList();
                    foreach (d_HinhAnhThuocChiTiet i in ds)
                        kq.Add(new HinhAnhThuocChiTiet(i.IDHinhAnh, i.IDNhanDang, i.DuongDanHinh, i.MoTa));
                    return kq;
                }
                catch
                {
                    return kq;
                }
            }

            /// Lọc theo màu, hình dạng, dạng thuốc
            public List<w_NhanDangThuoc> GetNhanDangByBasicFeatures(int? idMauSac = null, int? idHinhDang = null, int? idDangThuoc = null)
            {
                var query = from nd in db.w_NhanDangThuocs
                            join t in db.d_Thuocs on nd.IDThuoc equals t.IDThuoc
                            select nd;

                if (idHinhDang.HasValue)
                    query = query.Where(x => x.IDHinhDang == idHinhDang.Value);

                if (idDangThuoc.HasValue)
                    query = query.Where(x => x.IDDangThuoc == idDangThuoc.Value);

                if (idMauSac.HasValue)
                    query = from nd in query
                            join r in db.r_Thuoc_MauSacs on nd.IDThuoc equals r.IDThuoc
                            where r.IDMauSac == idMauSac.Value
                            select nd;

                return query.Distinct().ToList();
            }

            /// <summary>
            /// Lọc theo chữ khắc trên viên (imprint)
            /// </summary>
            public List<w_NhanDangThuoc> GetNhanDangByImprint(string imprint)
            {
                return db.w_NhanDangThuocs.Where(nd => nd.KhacDauMatTruoc.Contains(imprint)
                                   || nd.KhacDauMatSau.Contains(imprint)).ToList();
            }

            /// <summary>
            /// Lọc kết hợp nhiều tiêu chí: màu, hình, chữ khắc, loại vỉ, loại rãnh
            /// </summary>
            public List<w_NhanDangThuoc> GetNhanDangByMultipleFilters(
                string imprint = null,
                int? idMauSac = null,
                int? idHinhDang = null,
                int? idDangThuoc = null,
                int? idLoaiVi = null,
                int? idLoaiRanh = null)
            {
                var query = db.w_NhanDangThuocs.AsQueryable();

                if (!string.IsNullOrEmpty(imprint))
                    query = query.Where(x => x.KhacDauMatTruoc.Contains(imprint) || x.KhacDauMatSau.Contains(imprint));

                if (idHinhDang.HasValue)
                    query = query.Where(x => x.IDHinhDang == idHinhDang.Value);

                if (idDangThuoc.HasValue)
                    query = query.Where(x => x.IDDangThuoc == idDangThuoc.Value);

                if (idLoaiVi.HasValue)
                    query = query.Where(x => x.IDLoaiViThuoc == idLoaiVi.Value);

                if (idLoaiRanh.HasValue)
                    query = query.Where(x => x.IDLoaiRanh == idLoaiRanh.Value);

                if (idMauSac.HasValue)
                {
                    query = from nd in query
                            join r in db.r_Thuoc_MauSacs on nd.IDThuoc equals r.IDThuoc
                            where r.IDMauSac == idMauSac.Value
                            select nd;
                }

                return query.Distinct().ToList();
            }

            /// <summary>
            /// Truy xuất thuốc từ mã hình (MaHinh) – để hiển thị chi tiết khi click vào ảnh
            /// </summary>
            public w_NhanDangThuoc GetNhanDangByMaHinh(string maHinh)
            {
                return db.w_NhanDangThuocs.FirstOrDefault(nd => nd.MaHinh == maHinh);
            }
        }
        #endregion


    }
    #endregion
    #region Class hứng data
    public class ChiDinh
    {
        public int IDChiDinh { get; set; }
        public string TenChiDinh { get; set; }
        public string MoTa { get; set; }

        public ChiDinh(int _IDChiDinh, string _TenChiDinh, string _MoTa)
        {
            IDChiDinh = _IDChiDinh;
            TenChiDinh = _TenChiDinh;
            MoTa = _MoTa;
        }
    }

    public class DangThuoc
    {
        public int IDDangThuoc { get; set; }
        public string TenDangThuoc { get; set; }
        public DangThuoc(int _IDDangThuoc, string _TenDangThuoc)
        {
            IDDangThuoc = _IDDangThuoc;
            TenDangThuoc = _TenDangThuoc;
        }
    }

    public class HinhAnhThuocChiTiet
    {
        public int IDHinhAnh { get; set; }
        public int IDNhanDang { get; set; }
        public string DuongDanHinh { get; set; }
        public string MoTa { get; set; }
        public HinhAnhThuocChiTiet(int _IDHinhAnh, int _IDNhanDang, string _DuongDanHinh, string _MoTa)
        {
            IDHinhAnh = _IDHinhAnh;
            IDNhanDang = _IDNhanDang;
            DuongDanHinh = _DuongDanHinh;
            MoTa = _MoTa;
        }
    }
    public class HinhDang
    {
        public int IDHinhDang { get; set; }
        public string TenHinhDang { get; set; }

        public HinhDang(int _IDHinhDang, string _TenHinhDang)
        {
            IDHinhDang = _IDHinhDang;
            TenHinhDang = _TenHinhDang;
        }
    }

    public class HoatChat
    {
        public int IDHoatChat { get; set; }
        public string TenHoatChat { get; set; }
        public string LoaiHoatChat { get; set; }

        public HoatChat(int _IDHoatChat, string _TenHoatChat, string _LoaiHoatChat)
        {
            IDHoatChat = _IDHoatChat;
            TenHoatChat = _TenHoatChat;
            LoaiHoatChat = _LoaiHoatChat;
        }
    }

    public class HoatChatGoc
    {
        public int IDHoatChatGoc { get; set; }
        public string TenHoatChat { get; set; }
        public string GhiChu { get; set; }

        public HoatChatGoc(int _IDHoatChatGoc, string _TenHoatChat, string _GhiChu)
        {
            IDHoatChatGoc = _IDHoatChatGoc;
            TenHoatChat = _TenHoatChat;
            GhiChu = _GhiChu;
        }
    }

    public class LoaiRanh
    {
        public int IDLoaiRanh { get; set; }
        public string TenLoaiRanh { get; set; }

        public LoaiRanh(int _IDLoaiRanh, string _TenLoaiRanh)
        {
            IDLoaiRanh = _IDLoaiRanh;
            TenLoaiRanh = _TenLoaiRanh;
        }
    }

    public class LoaiViThuoc
    {
        public int IDLoaiViThuoc { get; set; }
        public string TenLoaiVi { get; set; }

        public LoaiViThuoc(int _IDLoaiViThuoc, string _TenLoaiVi)
        {
            IDLoaiViThuoc = _IDLoaiViThuoc;
            TenLoaiVi = _TenLoaiVi;
        }
    }

    public class MauSac
    {
        public int IDMauSac { get; set; }
        public string TenMauSac { get; set; }

        public MauSac(int _IDMauSac, string _TenMauSac)
        {
            IDMauSac = _IDMauSac;
            TenMauSac = _TenMauSac;
        }
    }

    public class Thuoc
    {
        public int IDThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string SDK { get; set; }
        public int? IDHoatChat { get; set; }
        public string HamLuong { get; set; }
        public string DangBaoChe { get; set; }
        public string NhaSX { get; set; }
        public string GhiChu { get; set; }

        public Thuoc(int _IDThuoc, string _TenThuoc, string _SDK, int? _IDHoatChat, string _HamLuong, string _DangBaoChe, string _NhaSX, string _GhiChu)
        {
            IDThuoc = _IDThuoc;
            TenThuoc = _TenThuoc;
            SDK = _SDK;
            IDHoatChat = _IDHoatChat;
            HamLuong = _HamLuong;
            DangBaoChe = _DangBaoChe;
            NhaSX = _NhaSX;
            GhiChu = _GhiChu;
        }
    }

    public class HoatChat_HoatChatGoc
    {
        public int IDHoatChat { get; set; }
        public int IDHoatChatGoc { get; set; }

        public HoatChat_HoatChatGoc(int _IDHoatChat, int _IDHoatChatGoc)
        {
            IDHoatChat = _IDHoatChat;
            IDHoatChatGoc = _IDHoatChatGoc;
        }
    }

    public class HoatChatGoc_ChiDinh
    {
        public int IDHoatChatGoc { get; set; }
        public int IDChiDinh { get; set; }

        public HoatChatGoc_ChiDinh(int _IDHoatChatGoc, int _IDChiDinh)
        {
            IDHoatChatGoc = _IDHoatChatGoc;
            IDChiDinh = _IDChiDinh;
        }
    }

    public class Thuoc_MauSac
    {
        public int IDThuoc { get; set; }
        public int IDMauSac { get; set; }

        public Thuoc_MauSac(int _IDThuoc, int _IDMauSac)
        {
            IDThuoc = _IDThuoc;
            IDMauSac = _IDMauSac;
        }
    }

    public class NhanDangThuoc
    {
        public int IDNhanDang { get; set; }
        public int IDThuoc { get; set; }
        public bool CoKhacDau { get; set; }
        public string KhacDauMatTruoc { get; set; }
        public string KhacDauMatSau { get; set; }
        public int IDHinhDang { get; set; }
        public int IDDangThuoc { get; set; }
        public int? IDLoaiViThuoc { get; set; }
        public int? IDLoaiRanh { get; set; }
        public string MaHinh { get; set; }

        public NhanDangThuoc(int _IDNhanDang, int _IDThuoc, bool _CoKhacDau, string _KhacDauMatTruoc, string _KhacDauMatSau, int _IDHinhDang, int _IDDangThuoc, int? _IDLoaiViThuoc, int? _IDLoaiRanh, string _MaHinh)
        {
            IDNhanDang = _IDNhanDang;
            IDThuoc = _IDThuoc;
            CoKhacDau = _CoKhacDau;
            KhacDauMatTruoc = _KhacDauMatTruoc;
            KhacDauMatSau = _KhacDauMatSau;
            IDHinhDang = _IDHinhDang;
            IDDangThuoc = _IDDangThuoc;
            IDLoaiViThuoc = _IDLoaiViThuoc;
            IDLoaiRanh = _IDLoaiRanh;
            MaHinh = _MaHinh;
        }
    }
    #endregion
}
