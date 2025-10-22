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
        #region Nhập liệu đơn
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
            public bool InsertThuoc(string tenThuoc, string sdk, int idHoatChat,
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
        #region Nhập liệu hàng loạt
        public class BulkInsertData
        {
            public bool BulkInsertChiDinh(List<ChiDinh> listChiDinh)
            {
                try
                {
                    List<d_ChiDinh> dsimport = new List<d_ChiDinh>();
                    foreach (ChiDinh i in listChiDinh)
                    {
                        d_ChiDinh a = new d_ChiDinh();
                        a.TenChiDinh = i.TenChiDinh;
                        a.MoTa = i.MoTa;
                        dsimport.Add(a);
                    }
                    db.d_ChiDinhs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertDangThuoc(List<DangThuoc> listDangThuoc)
            {
                try
                {
                    List<d_DangThuoc> dsimport = new List<d_DangThuoc>();
                    foreach (DangThuoc i in listDangThuoc)
                    {
                        d_DangThuoc a = new d_DangThuoc();
                        a.TenDangThuoc = i.TenDangThuoc;
                        dsimport.Add(a);
                    }
                    db.d_DangThuocs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertHinhAnhThuocChiTiet(List<HinhAnhThuocChiTiet> listHinhAnh)
            {
                try
                {
                    List<d_HinhAnhThuocChiTiet> dsimport = new List<d_HinhAnhThuocChiTiet>();
                    foreach (HinhAnhThuocChiTiet i in listHinhAnh)
                    {
                        d_HinhAnhThuocChiTiet a = new d_HinhAnhThuocChiTiet();
                        a.IDNhanDang = i.IDNhanDang;
                        a.DuongDanHinh = i.DuongDanHinh;
                        a.MoTa = i.MoTa;
                        dsimport.Add(a);
                    }
                    db.d_HinhAnhThuocChiTiets.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertHinhDang(List<HinhDang> listHinhDang)
            {
                try
                {
                    List<d_HinhDang> dsimport = new List<d_HinhDang>();
                    foreach (HinhDang i in listHinhDang)
                    {
                        d_HinhDang a = new d_HinhDang();
                        a.TenHinhDang = i.TenHinhDang;
                        dsimport.Add(a);
                    }
                    db.d_HinhDangs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertHoatChat(List<HoatChat> listHoatChat)
            {
                try
                {
                    List<d_HoatChat> dsimport = new List<d_HoatChat>();
                    foreach (HoatChat i in listHoatChat)
                    {
                        d_HoatChat a = new d_HoatChat();
                        a.TenHoatChat = i.TenHoatChat;
                        a.LoaiHoatChat = i.LoaiHoatChat;
                        dsimport.Add(a);
                    }
                    db.d_HoatChats.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertHoatChatGoc(List<HoatChatGoc> listHoatChatGoc)
            {
                try
                {
                    List<d_HoatChatGoc> dsimport = new List<d_HoatChatGoc>();
                    foreach (HoatChatGoc i in listHoatChatGoc)
                    {
                        d_HoatChatGoc a = new d_HoatChatGoc();
                        a.TenHoatChat = i.TenHoatChat;
                        a.GhiChu = i.GhiChu;
                        dsimport.Add(a);
                    }
                    db.d_HoatChatGocs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertLoaiRanh(List<LoaiRanh> listLoaiRanh)
            {
                try
                {
                    List<d_LoaiRanh> dsimport = new List<d_LoaiRanh>();
                    foreach (LoaiRanh i in listLoaiRanh)
                    {
                        d_LoaiRanh a = new d_LoaiRanh();
                        a.TenLoaiRanh = i.TenLoaiRanh;
                        dsimport.Add(a);
                    }
                    db.d_LoaiRanhs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertLoaiViThuoc(List<LoaiViThuoc> listLoaiViThuoc)
            {
                try
                {
                    List<d_LoaiViThuoc> dsimport = new List<d_LoaiViThuoc>();
                    foreach (LoaiViThuoc i in listLoaiViThuoc)
                    {
                        d_LoaiViThuoc a = new d_LoaiViThuoc();
                        a.TenLoaiVi = i.TenLoaiVi;
                        dsimport.Add(a);
                    }
                    db.d_LoaiViThuocs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertMauSac(List<MauSac> listMauSac)
            {
                try
                {
                    List<d_MauSac> dsimport = new List<d_MauSac>();
                    foreach (MauSac i in listMauSac)
                    {
                        d_MauSac a = new d_MauSac();
                        a.TenMauSac = i.TenMauSac;
                        dsimport.Add(a);
                    }
                    db.d_MauSacs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertThuoc(List<Thuoc> listThuoc)
            {
                try
                {
                    List<d_Thuoc> dsimport = new List<d_Thuoc>();

                    foreach (Thuoc i in listThuoc)
                    {
                        d_Thuoc a = new d_Thuoc();
                        a.TenThuoc = i.TenThuoc;
                        a.SDK = i.SDK;
                        a.IDHoatChat = i.IDHoatChat;
                        a.HamLuong = i.HamLuong;
                        a.DangBaoChe = i.DangBaoChe;
                        a.NhaSX = i.NhaSX;
                        a.GhiChu = i.GhiChu;
                        dsimport.Add(a);
                    }
                    db.d_Thuocs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertNhanDangThuoc(List<NhanDangThuoc> listNhanDang)
            {
                try
                {
                    List<w_NhanDangThuoc> dsimport = new List<w_NhanDangThuoc>();
                    foreach (NhanDangThuoc i in listNhanDang)
                    {
                        w_NhanDangThuoc a = new w_NhanDangThuoc();
                        a.IDThuoc = i.IDThuoc;
                        a.CoKhacDau = i.CoKhacDau;
                        a.KhacDauMatTruoc = i.KhacDauMatTruoc;
                        a.KhacDauMatSau = i.KhacDauMatSau;
                        a.IDHinhDang = i.IDHinhDang;
                        a.IDDangThuoc = i.IDDangThuoc;
                        a.IDLoaiViThuoc = i.IDLoaiViThuoc;
                        a.IDLoaiRanh = i.IDLoaiRanh;
                        a.MaHinh = i.MaHinh;
                        dsimport.Add(a);
                    }
                    db.w_NhanDangThuocs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            // Relationship tables
            public bool BulkInsertHoatChat_HoatChatGoc(List<HoatChat_HoatChatGoc> listLinks)
            {
                try
                {
                    List<r_HoatChat_HoatChatGoc> dsimport = new List<r_HoatChat_HoatChatGoc>();
                    foreach (HoatChat_HoatChatGoc i in listLinks)
                    {
                        r_HoatChat_HoatChatGoc a = new r_HoatChat_HoatChatGoc();
                        a.IDHoatChat = i.IDHoatChat;
                        a.IDHoatChatGoc = i.IDHoatChatGoc;
                        dsimport.Add(a);
                    }
                    db.r_HoatChat_HoatChatGocs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertHoatChatGoc_ChiDinh(List<HoatChatGoc_ChiDinh> listLinks)
            {
                try
                {
                    List<r_HoatChatGoc_ChiDinh> dsimport = new List<r_HoatChatGoc_ChiDinh>();
                    foreach (HoatChatGoc_ChiDinh i in listLinks)
                    {
                        r_HoatChatGoc_ChiDinh a = new r_HoatChatGoc_ChiDinh();
                        a.IDHoatChatGoc = i.IDHoatChatGoc;
                        a.IDChiDinh = i.IDChiDinh;
                        dsimport.Add(a);
                    }
                    db.r_HoatChatGoc_ChiDinhs.InsertAllOnSubmit(dsimport);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool BulkInsertThuoc_MauSac(List<Thuoc_MauSac> listLinks)
            {
                try
                {
                    List<r_Thuoc_MauSac> dsimport = new List<r_Thuoc_MauSac>();
                    foreach (Thuoc_MauSac i in listLinks)
                    {
                        r_Thuoc_MauSac a = new r_Thuoc_MauSac();
                        a.IDThuoc = i.IDThuoc;
                        a.IDMauSac = i.IDMauSac;
                        dsimport.Add(a);
                    }
                    db.r_Thuoc_MauSacs.InsertAllOnSubmit(dsimport);
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
                    kq.Add(GetHC(i.IDHoatChat));

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

                List<d_Thuoc> ds = (from data in db.d_Thuocs
                                    select data).ToList();

                foreach (d_Thuoc i in ds)
                    kq.Add(new Thuoc(i.IDThuoc, i.TenThuoc, i.SDK, i.IDHoatChat, i.HamLuong, i.DangBaoChe, i.NhaSX, i.GhiChu));
                return kq;
            }
            // Lấy toàn bộ Nhận dạng thuốc
            public List<NhanDangThuoc> GetDSNhanDangThuoc()
            {
                List<NhanDangThuoc> kq = new List<NhanDangThuoc>();

                List<w_NhanDangThuoc> ds = (from data in db.w_NhanDangThuocs
                                            select data).ToList();

                foreach (w_NhanDangThuoc i in ds)
                    kq.Add(new NhanDangThuoc(i.IDNhanDang, i.IDThuoc, i.CoKhacDau, i.KhacDauMatTruoc, i.KhacDauMatSau, i.IDHinhDang, i.IDDangThuoc, i.IDLoaiViThuoc, i.IDLoaiRanh, i.MaHinh));
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
            public HoatChat GetHC(int idHoatChat)
            {
                HoatChat kq = new HoatChat();
                try
                {
                    d_HoatChat d_hc = (from data in db.d_HoatChats
                                       where data.IDHoatChat == idHoatChat
                                       select data).FirstOrDefault();
                    kq.IDHoatChat = d_hc.IDHoatChat;
                    kq.TenHoatChat = d_hc.TenHoatChat;
                    kq.LoaiHoatChat = d_hc.LoaiHoatChat;
                    kq.dsHCG = GetHCGbyidHC(kq.IDHoatChat);

                    return kq;
                }
                catch
                {
                    return kq;
                }
            }
            public List<HoatChatGoc> GetHCGbyidHC(int idHoatChat)
            {
                List<HoatChatGoc> kq = new List<HoatChatGoc>();
                try
                {
                    List<d_HoatChatGoc> ds = (from data in db.d_HoatChatGocs
                                              join s1 in db.r_HoatChat_HoatChatGocs on data.IDHoatChatGoc equals s1.IDHoatChatGoc
                                              where s1.IDHoatChat == idHoatChat
                                              select data).ToList();
                    foreach (d_HoatChatGoc i in ds)
                        kq.Add(new HoatChatGoc(i.IDHoatChatGoc, i.TenHoatChat, i.GhiChu));
                    return kq;
                }
                catch
                {
                    return kq;
                }
            }
            public Thuoc GetThuoc(int idThuoc)
            {
                Thuoc kq = new Thuoc();
                try
                {
                    d_Thuoc d_hc = (from data in db.d_Thuocs
                                       where data.IDThuoc == idThuoc
                                       select data).FirstOrDefault();
                    kq.IDThuoc = d_hc.IDThuoc;
                    kq.TenThuoc = d_hc.TenThuoc;
                    kq.SDK = d_hc.SDK;
                    kq.IDHoatChat = d_hc.IDHoatChat;
                    kq.HamLuong = d_hc.HamLuong;
                    kq.DangBaoChe = d_hc.DangBaoChe;
                    kq.NhaSX = d_hc.NhaSX;
                    kq.GhiChu = d_hc.GhiChu;
                    kq.Mausac = GetMauSacbyIDThuoc(idThuoc);
                    kq.HinhAnhChiTiet = GetDSHinhAnhbyThuoc(idThuoc);

                    return kq;
                }
                catch
                {
                    return kq;
                }
            }
            public List<MauSac> GetMauSacbyIDThuoc(int idThuoc)
            {
                List<MauSac> kq = new List<MauSac>();
                try
                {
                    List<d_MauSac> ds = (from data in db.d_MauSacs
                                              join s1 in db.r_Thuoc_MauSacs on data.IDMauSac equals s1.IDMauSac
                                              where s1.IDThuoc == idThuoc
                                              select data).ToList();
                    foreach (d_MauSac i in ds)
                        kq.Add(new MauSac(i.IDMauSac, i.TenMauSac));
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

            public List<Thuoc> GetNhanDangThuoc(string imprint, int? idMausac1 = null, int? idMausac2 = null,
                int? idHinhdang = null, int? idDangthuoc = null, int? idLoaiVi = null, int? idLoaiRanh = null)
            {
                List<Thuoc> kq = new List<Thuoc>();
                try
                {
                    List<int> dsIDThuoc = new List<int>();

                    IQueryable<r_Thuoc_MauSac> query1 = from data in db.r_Thuoc_MauSacs
                                                        select data;

                    if (idMausac1 != null && idMausac2 != null)
                    {
                        List<int> thuocWithColor1 = query1.Where(x => x.IDMauSac == idMausac1).Select(x => x.IDThuoc).ToList();
                        List<int> thuocWithColor2 = query1.Where(x => x.IDMauSac == idMausac2).Select(x => x.IDThuoc).ToList();
                        List<int> thuocWithBothColors = thuocWithColor1.Intersect(thuocWithColor2).ToList();
                        dsIDThuoc.AddRange(thuocWithBothColors);
                    }
                    else if (idMausac1 != null)
                    {
                        dsIDThuoc.AddRange(query1.Where(x => x.IDMauSac == idMausac1).Select(x => x.IDThuoc).ToList());
                    }
                    else if (idMausac2 != null)
                    {
                        dsIDThuoc.AddRange(query1.Where(x => x.IDMauSac == idMausac2).Select(x => x.IDThuoc).ToList());
                    }

                    IQueryable<w_NhanDangThuoc> query2 = from data in db.w_NhanDangThuocs
                                                         select data;
                    if (idHinhdang != null)
                        query2 = query2.Where(x => x.IDHinhDang == idHinhdang);
                    if (idDangthuoc != null)
                        query2 = query2.Where(x => x.IDDangThuoc == idDangthuoc);
                    if (idLoaiVi != null)
                        query2 = query2.Where(x => x.IDLoaiViThuoc == idLoaiVi);
                    if (idLoaiRanh != null)
                        query2 = query2.Where(x => x.IDLoaiRanh == idLoaiRanh);
                    if (!string.IsNullOrEmpty(imprint))
                        query2 = query2.Where(x => x.KhacDauMatTruoc.Contains(imprint) || x.KhacDauMatSau.Contains(imprint));

                    foreach (w_NhanDangThuoc i in query2)
                    {
                        dsIDThuoc.Add(i.IDThuoc);
                    }

                    dsIDThuoc = dsIDThuoc.Distinct().ToList();

                    kq = GetDSThuoc().Where(x => dsIDThuoc.Contains(x.IDThuoc)).ToList();

                    return kq;
                }
                catch
                {
                    return kq;
                }
            }
        }
        #endregion
        #region Update dữ liệu
        public class UpdateData
        {
            public bool UpdateChiDinh(int idChiDinh, string chiDinh, string moTa)
            {
                try
                {
                    d_ChiDinh cd = db.d_ChiDinhs.SingleOrDefault(x => x.IDChiDinh == idChiDinh);
                    if (cd != null)
                    {
                        cd.TenChiDinh = chiDinh;
                        cd.MoTa = moTa;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateDangThuoc(int idDangThuoc, string tenDangThuoc)
            {
                try
                {
                    d_DangThuoc dt = db.d_DangThuocs.SingleOrDefault(x => x.IDDangThuoc == idDangThuoc);
                    if (dt != null)
                    {
                        dt.TenDangThuoc = tenDangThuoc;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateHinhAnhThuocChiTiet(int idHinhAnh, int idNhanDang, string duongDanHinh, string moTa)
            {
                try
                {
                    d_HinhAnhThuocChiTiet ha = db.d_HinhAnhThuocChiTiets.SingleOrDefault(x => x.IDHinhAnh == idHinhAnh);
                    if (ha != null)
                    {
                        ha.IDNhanDang = idNhanDang;
                        ha.DuongDanHinh = duongDanHinh;
                        ha.MoTa = moTa;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateHinhDang(int idHinhDang, string tenHinhDang)
            {
                try
                {
                    d_HinhDang hd = db.d_HinhDangs.SingleOrDefault(x => x.IDHinhDang == idHinhDang);
                    if (hd != null)
                    {
                        hd.TenHinhDang = tenHinhDang;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateHoatChat(int idHoatChat, string tenHoatChat, string loaiHoatChat)
            {
                try
                {
                    d_HoatChat hc = db.d_HoatChats.SingleOrDefault(x => x.IDHoatChat == idHoatChat);
                    if (hc != null)
                    {
                        hc.TenHoatChat = tenHoatChat;
                        hc.LoaiHoatChat = loaiHoatChat;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateHoatChatGoc(int idHoatChatGoc, string tenHoatChat, string ghiChu)
            {
                try
                {
                    d_HoatChatGoc hcg = db.d_HoatChatGocs.SingleOrDefault(x => x.IDHoatChatGoc == idHoatChatGoc);
                    if (hcg != null)
                    {
                        hcg.TenHoatChat = tenHoatChat;
                        hcg.GhiChu = ghiChu;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateLoaiRanh(int idLoaiRanh, string tenLoaiRanh)
            {
                try
                {
                    d_LoaiRanh lr = db.d_LoaiRanhs.SingleOrDefault(x => x.IDLoaiRanh == idLoaiRanh);
                    if (lr != null)
                    {
                        lr.TenLoaiRanh = tenLoaiRanh;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateLoaiViThuoc(int idLoaiViThuoc, string tenLoaiVi)
            {
                try
                {
                    d_LoaiViThuoc lvt = db.d_LoaiViThuocs.SingleOrDefault(x => x.IDLoaiViThuoc == idLoaiViThuoc);
                    if (lvt != null)
                    {
                        lvt.TenLoaiVi = tenLoaiVi;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateMauSac(int idMauSac, string tenMauSac)
            {
                try
                {
                    d_MauSac ms = db.d_MauSacs.SingleOrDefault(x => x.IDMauSac == idMauSac);
                    if (ms != null)
                    {
                        ms.TenMauSac = tenMauSac;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateThuoc(int idThuoc, string tenThuoc, string sdk, int idHoatChat,
                            string hamLuong, string dangBaoChe, string nhaSX, string ghiChu)
            {
                try
                {
                    d_Thuoc t = db.d_Thuocs.SingleOrDefault(x => x.IDThuoc == idThuoc);
                    if (t != null)
                    {
                        t.TenThuoc = tenThuoc;
                        t.SDK = sdk;
                        t.IDHoatChat = idHoatChat;
                        t.HamLuong = hamLuong;
                        t.DangBaoChe = dangBaoChe;
                        t.NhaSX = nhaSX;
                        t.GhiChu = ghiChu;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateHoatChat_HoatChatGoc(int idHoatChat, int idHoatChatGocOld, int idHoatChatGocNew)
            {
                try
                {
                    r_HoatChat_HoatChatGoc link = db.r_HoatChat_HoatChatGocs.SingleOrDefault(x =>
                        x.IDHoatChat == idHoatChat && x.IDHoatChatGoc == idHoatChatGocOld);
                    if (link != null)
                    {
                        link.IDHoatChatGoc = idHoatChatGocNew;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateHoatChatGoc_ChiDinh(int idHoatChatGoc, int idChiDinhOld, int idChiDinhNew)
            {
                try
                {
                    r_HoatChatGoc_ChiDinh link = db.r_HoatChatGoc_ChiDinhs.SingleOrDefault(x =>
                        x.IDHoatChatGoc == idHoatChatGoc && x.IDChiDinh == idChiDinhOld);
                    if (link != null)
                    {
                        link.IDChiDinh = idChiDinhNew;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateThuoc_MauSac(int idThuoc, int idMauSacOld, int idMauSacNew)
            {
                try
                {
                    r_Thuoc_MauSac link = db.r_Thuoc_MauSacs.SingleOrDefault(x =>
                        x.IDThuoc == idThuoc && x.IDMauSac == idMauSacOld);
                    if (link != null)
                    {
                        link.IDMauSac = idMauSacNew;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool UpdateNhanDangThuoc(int idNhanDang, int idThuoc, bool coKhacDau,
                                    string khacDauMatTruoc, string khacDauMatSau,
                                    int idHinhDang, int idDangThuoc,
                                    int? idLoaiViThuoc, int? idLoaiRanh, string maHinh)
            {
                try
                {
                    w_NhanDangThuoc nd = db.w_NhanDangThuocs.SingleOrDefault(x => x.IDNhanDang == idNhanDang);
                    if (nd != null)
                    {
                        nd.IDThuoc = idThuoc;
                        nd.CoKhacDau = coKhacDau;
                        nd.KhacDauMatTruoc = khacDauMatTruoc;
                        nd.KhacDauMatSau = khacDauMatSau;
                        nd.IDHinhDang = idHinhDang;
                        nd.IDDangThuoc = idDangThuoc;
                        nd.IDLoaiViThuoc = idLoaiViThuoc;
                        nd.IDLoaiRanh = idLoaiRanh;
                        nd.MaHinh = maHinh;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion
        #region Xóa dữ liệu
        public class DeleteData
        {
            public bool DeleteChiDinh(int idChiDinh)
            {
                try
                {
                    // Delete related records in r_HoatChatGoc_ChiDinh first
                    IQueryable<r_HoatChatGoc_ChiDinh> relatedLinks = db.r_HoatChatGoc_ChiDinhs.Where(x => x.IDChiDinh == idChiDinh);
                    db.r_HoatChatGoc_ChiDinhs.DeleteAllOnSubmit(relatedLinks);

                    // Delete the main record
                    d_ChiDinh cd = db.d_ChiDinhs.SingleOrDefault(x => x.IDChiDinh == idChiDinh);
                    if (cd != null)
                    {
                        db.d_ChiDinhs.DeleteOnSubmit(cd);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteDangThuoc(int idDangThuoc)
            {
                try
                {
                    // Check if there are related records in w_NhanDangThuoc
                    IQueryable<w_NhanDangThuoc> relatedRecords = db.w_NhanDangThuocs.Where(x => x.IDDangThuoc == idDangThuoc);
                    if (relatedRecords.Any())
                    {
                        // Cannot delete if there are related drug identification records
                        return false;
                    }

                    d_DangThuoc dt = db.d_DangThuocs.SingleOrDefault(x => x.IDDangThuoc == idDangThuoc);
                    if (dt != null)
                    {
                        db.d_DangThuocs.DeleteOnSubmit(dt);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteHinhAnhThuocChiTiet(int idHinhAnh)
            {
                try
                {
                    d_HinhAnhThuocChiTiet ha = db.d_HinhAnhThuocChiTiets.SingleOrDefault(x => x.IDHinhAnh == idHinhAnh);
                    if (ha != null)
                    {
                        db.d_HinhAnhThuocChiTiets.DeleteOnSubmit(ha);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteHinhDang(int idHinhDang)
            {
                try
                {
                    // Check if there are related records in w_NhanDangThuoc
                    IQueryable<w_NhanDangThuoc> relatedRecords = db.w_NhanDangThuocs.Where(x => x.IDHinhDang == idHinhDang);
                    if (relatedRecords.Any())
                    {
                        return false;
                    }

                    d_HinhDang hd = db.d_HinhDangs.SingleOrDefault(x => x.IDHinhDang == idHinhDang);
                    if (hd != null)
                    {
                        db.d_HinhDangs.DeleteOnSubmit(hd);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteHoatChat(int idHoatChat)
            {
                try
                {
                    // Check if there are related records in d_Thuoc
                    IQueryable<d_Thuoc> relatedThuoc = db.d_Thuocs.Where(x => x.IDHoatChat == idHoatChat);
                    if (relatedThuoc.Any())
                    {
                        return false;
                    }

                    // Delete related records in r_HoatChat_HoatChatGoc
                    IQueryable<r_HoatChat_HoatChatGoc> relatedLinks = db.r_HoatChat_HoatChatGocs.Where(x => x.IDHoatChat == idHoatChat);
                    db.r_HoatChat_HoatChatGocs.DeleteAllOnSubmit(relatedLinks);

                    // Delete the main record
                    d_HoatChat hc = db.d_HoatChats.SingleOrDefault(x => x.IDHoatChat == idHoatChat);
                    if (hc != null)
                    {
                        db.d_HoatChats.DeleteOnSubmit(hc);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteHoatChatGoc(int idHoatChatGoc)
            {
                try
                {
                    // Delete related records in r_HoatChat_HoatChatGoc
                    IQueryable<r_HoatChat_HoatChatGoc> relatedHoatChat = db.r_HoatChat_HoatChatGocs.Where(x => x.IDHoatChatGoc == idHoatChatGoc);
                    db.r_HoatChat_HoatChatGocs.DeleteAllOnSubmit(relatedHoatChat);

                    // Delete related records in r_HoatChatGoc_ChiDinh
                    IQueryable<r_HoatChatGoc_ChiDinh> relatedChiDinh = db.r_HoatChatGoc_ChiDinhs.Where(x => x.IDHoatChatGoc == idHoatChatGoc);
                    db.r_HoatChatGoc_ChiDinhs.DeleteAllOnSubmit(relatedChiDinh);

                    // Delete the main record
                    d_HoatChatGoc hcg = db.d_HoatChatGocs.SingleOrDefault(x => x.IDHoatChatGoc == idHoatChatGoc);
                    if (hcg != null)
                    {
                        db.d_HoatChatGocs.DeleteOnSubmit(hcg);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteLoaiRanh(int idLoaiRanh)
            {
                try
                {
                    // Check if there are related records in w_NhanDangThuoc
                    IQueryable<w_NhanDangThuoc> relatedRecords = db.w_NhanDangThuocs.Where(x => x.IDLoaiRanh == idLoaiRanh);
                    if (relatedRecords.Any())
                    {
                        return false;
                    }

                    d_LoaiRanh lr = db.d_LoaiRanhs.SingleOrDefault(x => x.IDLoaiRanh == idLoaiRanh);
                    if (lr != null)
                    {
                        db.d_LoaiRanhs.DeleteOnSubmit(lr);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteLoaiViThuoc(int idLoaiViThuoc)
            {
                try
                {
                    // Check if there are related records in w_NhanDangThuoc
                    IQueryable<w_NhanDangThuoc> relatedRecords = db.w_NhanDangThuocs.Where(x => x.IDLoaiViThuoc == idLoaiViThuoc);
                    if (relatedRecords.Any())
                    {
                        return false;
                    }

                    d_LoaiViThuoc lvt = db.d_LoaiViThuocs.SingleOrDefault(x => x.IDLoaiViThuoc == idLoaiViThuoc);
                    if (lvt != null)
                    {
                        db.d_LoaiViThuocs.DeleteOnSubmit(lvt);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteMauSac(int idMauSac)
            {
                try
                {
                    // Delete related records in r_Thuoc_MauSac first
                    IQueryable<r_Thuoc_MauSac> relatedLinks = db.r_Thuoc_MauSacs.Where(x => x.IDMauSac == idMauSac);
                    db.r_Thuoc_MauSacs.DeleteAllOnSubmit(relatedLinks);

                    // Delete the main record
                    d_MauSac ms = db.d_MauSacs.SingleOrDefault(x => x.IDMauSac == idMauSac);
                    if (ms != null)
                    {
                        db.d_MauSacs.DeleteOnSubmit(ms);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteThuoc(int idThuoc)
            {
                try
                {
                    // Delete related records in w_NhanDangThuoc
                    IQueryable<w_NhanDangThuoc> relatedNhanDang = db.w_NhanDangThuocs.Where(x => x.IDThuoc == idThuoc);
                    foreach (w_NhanDangThuoc nd in relatedNhanDang)
                    {
                        // Delete related images first
                        IQueryable<d_HinhAnhThuocChiTiet> relatedImages = db.d_HinhAnhThuocChiTiets.Where(x => x.IDNhanDang == nd.IDNhanDang);
                        db.d_HinhAnhThuocChiTiets.DeleteAllOnSubmit(relatedImages);
                    }
                    db.w_NhanDangThuocs.DeleteAllOnSubmit(relatedNhanDang);

                    // Delete related records in r_Thuoc_MauSac
                    IQueryable<r_Thuoc_MauSac> relatedMauSac = db.r_Thuoc_MauSacs.Where(x => x.IDThuoc == idThuoc);
                    db.r_Thuoc_MauSacs.DeleteAllOnSubmit(relatedMauSac);

                    // Delete the main record
                    d_Thuoc t = db.d_Thuocs.SingleOrDefault(x => x.IDThuoc == idThuoc);
                    if (t != null)
                    {
                        db.d_Thuocs.DeleteOnSubmit(t);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteHoatChat_HoatChatGoc(int idHoatChat)
            {
                try
                {
                    List<r_HoatChat_HoatChatGoc> link = (from data in db.r_HoatChat_HoatChatGocs
                                                         where data.IDHoatChat == idHoatChat
                                                         select data).ToList();
                    if (link != null)
                    {
                        db.r_HoatChat_HoatChatGocs.DeleteAllOnSubmit(link);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteHoatChatGoc_ChiDinh(int idHoatChatGoc, int idChiDinh)
            {
                try
                {
                    r_HoatChatGoc_ChiDinh link = db.r_HoatChatGoc_ChiDinhs.SingleOrDefault(x =>
                        x.IDHoatChatGoc == idHoatChatGoc && x.IDChiDinh == idChiDinh);
                    if (link != null)
                    {
                        db.r_HoatChatGoc_ChiDinhs.DeleteOnSubmit(link);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteThuoc_MauSac(int idThuoc)
            {
                try
                {
                    r_Thuoc_MauSac link = db.r_Thuoc_MauSacs.SingleOrDefault(x =>
                        x.IDThuoc == idThuoc);
                    if (link != null)
                    {
                        db.r_Thuoc_MauSacs.DeleteOnSubmit(link);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool DeleteNhanDangThuoc(int idNhanDang)
            {
                try
                {
                    // Delete related images first
                    IQueryable<d_HinhAnhThuocChiTiet> relatedImages = db.d_HinhAnhThuocChiTiets.Where(x => x.IDNhanDang == idNhanDang);
                    db.d_HinhAnhThuocChiTiets.DeleteAllOnSubmit(relatedImages);

                    // Delete the main record
                    w_NhanDangThuoc nd = db.w_NhanDangThuocs.SingleOrDefault(x => x.IDNhanDang == idNhanDang);
                    if (nd != null)
                    {
                        db.w_NhanDangThuocs.DeleteOnSubmit(nd);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
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
        public List<HoatChatGoc> dsHCG { get; set; }
        public ChiDinh()
        {

        }
        public ChiDinh(int _IDChiDinh, string _TenChiDinh, string _MoTa)
        {
            IDChiDinh = _IDChiDinh;
            TenChiDinh = _TenChiDinh;
            MoTa = _MoTa;
        }
        public ChiDinh(int _IDChiDinh, string _TenChiDinh, string _MoTa, List<HoatChatGoc> _dsHCG)
        {
            IDChiDinh = _IDChiDinh;
            TenChiDinh = _TenChiDinh;
            MoTa = _MoTa;
            dsHCG = _dsHCG;
        }
    }
    public class DangThuoc
    {
        public int IDDangThuoc { get; set; }
        public string TenDangThuoc { get; set; }
        public DangThuoc()
        {

        }
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
        public HinhAnhThuocChiTiet()
        {

        }
        public HinhAnhThuocChiTiet(int _IDHinhAnh, int _IDNhanDang, string _DuongDanHinh, string _MoTa)
        {
            IDHinhAnh = _IDHinhAnh;
            IDNhanDang = _IDNhanDang;
            DuongDanHinh = _DuongDanHinh;
            MoTa = _MoTa;
        }
    } //chua
    public class HinhDang
    {
        public int IDHinhDang { get; set; }
        public string TenHinhDang { get; set; }
        public HinhDang()
        {

        }
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
        public List<HoatChatGoc> dsHCG { get; set; }
        public HoatChat()
        {

        }
        public HoatChat(int _IDHoatChat, string _TenHoatChat, string _LoaiHoatChat)
        {
            IDHoatChat = _IDHoatChat;
            TenHoatChat = _TenHoatChat;
            LoaiHoatChat = _LoaiHoatChat;
        }
        public HoatChat(int _IDHoatChat, string _TenHoatChat, string _LoaiHoatChat, List<HoatChatGoc> _dsHCG)
        {
            IDHoatChat = _IDHoatChat;
            TenHoatChat = _TenHoatChat;
            LoaiHoatChat = _LoaiHoatChat;
            dsHCG = _dsHCG;
        }
    }
    public class HoatChatGoc
    {
        public int IDHoatChatGoc { get; set; }
        public string TenHoatChat { get; set; }
        public string GhiChu { get; set; }
        public HoatChatGoc()
        {

        }
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
        public LoaiRanh()
        {

        }
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
        public LoaiViThuoc()
        {

        }
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
        public MauSac()
        {

        }
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
        public int IDHoatChat { get; set; }
        public string HamLuong { get; set; }
        public string DangBaoChe { get; set; }
        public string NhaSX { get; set; }
        public string GhiChu { get; set; }
        public List<MauSac> Mausac { get; set; }
        public List<HinhAnhThuocChiTiet> HinhAnhChiTiet { get; set; }

        public Thuoc()
        {

        }
        public Thuoc(int _IDThuoc, string _TenThuoc, string _SDK, int _IDHoatChat, string _HamLuong, string _DangBaoChe, string _NhaSX, string _GhiChu)
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
        public HoatChat_HoatChatGoc()
        {

        }
        public HoatChat_HoatChatGoc(int _IDHoatChat, int _IDHoatChatGoc)
        {
            IDHoatChat = _IDHoatChat;
            IDHoatChatGoc = _IDHoatChatGoc;
        }
    } //chua
    public class HoatChatGoc_ChiDinh
    {
        public int IDHoatChatGoc { get; set; }
        public int IDChiDinh { get; set; }
        public HoatChatGoc_ChiDinh()
        {

        }
        public HoatChatGoc_ChiDinh(int _IDHoatChatGoc, int _IDChiDinh)
        {
            IDHoatChatGoc = _IDHoatChatGoc;
            IDChiDinh = _IDChiDinh;
        }
    } //chua
    public class Thuoc_MauSac
    {
        public int IDThuoc { get; set; }
        public int IDMauSac { get; set; }
        public Thuoc_MauSac()
        {

        }
        public Thuoc_MauSac(int _IDThuoc, int _IDMauSac)
        {
            IDThuoc = _IDThuoc;
            IDMauSac = _IDMauSac;
        }
    } //chua
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
        public NhanDangThuoc()
        {

        }
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

    #region API
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    #endregion
}
