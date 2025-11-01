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
        private static string activeConnection = "ClassChung.Properties.Settings.PillIDConnectionString_Local";
        // private static string activeConnection = "ClassChung.Properties.Settings.PillIDConnectionString_Server";

        protected static DataClassesDataContext db = new DataClassesDataContext();
        #region Nhập liệu đơn
        public class InsertData
        {
            public bool InsertChiDinh(ChiDinh item)
            {
                try
                {
                    //Tạo object và gán giá trị nhập
                    d_ChiDinh cd = item.toChiDinhDB();
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
            public bool InsertDangThuoc(DangThuoc item)
            {
                try
                {
                    d_DangThuoc dt = item.toDangThuocDB();

                    db.d_DangThuocs.InsertOnSubmit(dt);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHinhAnhThuocChiTiet(HinhAnhThuocChiTiet item)
            {
                try
                {
                    d_HinhAnhThuocChiTiet ha = item.toHinhAnhThuocChiTietDB();

                    db.d_HinhAnhThuocChiTiets.InsertOnSubmit(ha);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHinhDang(HinhDang item)
            {
                try
                {
                    d_HinhDang hd = item.toHinhDangDB();

                    db.d_HinhDangs.InsertOnSubmit(hd);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHoatChat(HoatChat item)
            {
                try
                {
                    d_HoatChat hc = item.toHoatChatDB();

                    db.d_HoatChats.InsertOnSubmit(hc);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHoatChatGoc(HoatChatGoc item)
            {
                try
                {
                    d_HoatChatGoc hcg = item.toHoatChatGocDB();

                    db.d_HoatChatGocs.InsertOnSubmit(hcg);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertLoaiRanh(LoaiRanh item)
            {
                try
                {
                    d_LoaiRanh lr = item.toLoaiRanhDB();

                    db.d_LoaiRanhs.InsertOnSubmit(lr);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertLoaiViThuoc(LoaiViThuoc item)
            {
                try
                {
                    d_LoaiViThuoc lvt = item.toLoaiViThuocDB();

                    db.d_LoaiViThuocs.InsertOnSubmit(lvt);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertMauSac(MauSac item)
            {
                try
                {
                    d_MauSac ms = item.toMauSacDB();

                    db.d_MauSacs.InsertOnSubmit(ms);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertThuoc(Thuoc item)
            {
                try
                {
                    d_Thuoc dbRecord = new d_Thuoc
                    {
                        SDK = item.SDK,
                        IDHoatChat = item.IDHoatChat,
                        HamLuong = item.HamLuong,
                        DangBaoChe = item.DangBaoChe,
                        NhaSX = item.NhaSX,
                        GhiChu = item.GhiChu,
                        URL = item.URL
                    };

                    db.d_Thuocs.InsertOnSubmit(dbRecord);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHoatChat_HoatChatGoc(HoatChat_HoatChatGoc item)
            {
                try
                {
                    r_HoatChat_HoatChatGoc link = item.toHoatChat_HoatChatGocDB();

                    db.r_HoatChat_HoatChatGocs.InsertOnSubmit(link);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertHoatChatGoc_ChiDinh(HoatChatGoc_ChiDinh item)
            {
                try
                {
                    r_HoatChatGoc_ChiDinh link = item.toHoatChatGoc_ChiDinhDB();

                    db.r_HoatChatGoc_ChiDinhs.InsertOnSubmit(link);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertThuoc_MauSac(Thuoc_MauSac item)
            {
                try
                {
                    r_Thuoc_MauSac link = item.toThuoc_MauSacDB();

                    db.r_Thuoc_MauSacs.InsertOnSubmit(link);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public bool InsertNhanDangThuoc(NhanDangThuoc item)
            {
                try
                {
                    w_NhanDangThuoc dbRecord = item.toNhanDangThuocDB();

                    db.w_NhanDangThuocs.InsertOnSubmit(dbRecord);
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
                        d_ChiDinh a = i.toChiDinhDB();
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
                        d_DangThuoc a = i.toDangThuocDB();

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
                        d_HinhAnhThuocChiTiet a = i.toHinhAnhThuocChiTietDB();

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
                        d_HinhDang a = i.toHinhDangDB();

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
                        d_HoatChat a = i.toHoatChatDB();

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
                        d_HoatChatGoc a = i.toHoatChatGocDB();

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
                        d_LoaiRanh a = i.toLoaiRanhDB();

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
                        d_LoaiViThuoc a = i.toLoaiViThuocDB();

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
                        d_MauSac a = i.toMauSacDB();
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
                        d_Thuoc a = i.toThuocDB();

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
                        w_NhanDangThuoc a = i.toNhanDangThuocDB();

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
                        r_HoatChat_HoatChatGoc a = i.toHoatChat_HoatChatGocDB();
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
                        r_HoatChatGoc_ChiDinh a = i.toHoatChatGoc_ChiDinhDB();

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
                        r_Thuoc_MauSac a = i.toThuoc_MauSacDB();

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
                    kq.Add(ChiDinh.fromChiDinhDB(i));
                return kq;
            }
            // Lấy toàn bộ Dạng Thuốc
            public List<DangThuoc> GetDSDangThuoc()
            {
                List<DangThuoc> kq = new List<DangThuoc>();
                List<d_DangThuoc> ds = db.d_DangThuocs.ToList();
                foreach (d_DangThuoc i in ds)
                    kq.Add(DangThuoc.fromDangThuocDB(i));
                return kq;
            }
            // Lấy toàn bộ Hình Dạng
            public List<HinhDang> GetDSHinhDang()
            {
                List<HinhDang> kq = new List<HinhDang>();

                List<d_HinhDang> ds = db.d_HinhDangs.ToList();
                foreach (d_HinhDang i in ds)
                    kq.Add(HinhDang.fromHinhDangDB(i));

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
                    kq.Add(HoatChatGoc.fromHoatChatGocDB(i));

                return kq;
            }
            public List<HoatChat_HoatChatGoc> GetDSHoatChat_HoatChatGoc()
            {
                List<HoatChat_HoatChatGoc> kq = new List<HoatChat_HoatChatGoc>();

                List<r_HoatChat_HoatChatGoc> ds = db.r_HoatChat_HoatChatGocs.ToList();
                foreach (r_HoatChat_HoatChatGoc i in ds)
                    kq.Add(HoatChat_HoatChatGoc.fromHoatChat_HoatChatGocDB(i));
                return kq;
            }
            public List<HoatChatGoc_ChiDinh> GetDSHoatChatGoc_ChiDinh()
            {
                List<HoatChatGoc_ChiDinh> kq = new List<HoatChatGoc_ChiDinh>();

                List<r_HoatChatGoc_ChiDinh> ds = db.r_HoatChatGoc_ChiDinhs.ToList();
                foreach (r_HoatChatGoc_ChiDinh i in ds)
                    kq.Add(HoatChatGoc_ChiDinh.fromHoatChatGoc_ChiDinhDB(i));
                return kq;
            }
            // Lấy toàn bộ Loại Rãnh
            public List<LoaiRanh> GetDSLoaiRanh()
            {
                List<LoaiRanh> kq = new List<LoaiRanh>();

                List<d_LoaiRanh> ds = db.d_LoaiRanhs.ToList();
                foreach (d_LoaiRanh i in ds)
                    kq.Add(LoaiRanh.fromLoaiRanhDB(i));

                return kq;
            }
            // Lấy toàn bộ Loại Vỉ Thuốc
            public List<LoaiViThuoc> GetDSLoaiViThuoc()
            {
                List<LoaiViThuoc> kq = new List<LoaiViThuoc>();

                List<d_LoaiViThuoc> ds = db.d_LoaiViThuocs.ToList();
                foreach (d_LoaiViThuoc i in ds)
                    kq.Add(LoaiViThuoc.fromLoaiViThuocDB(i));

                return kq;
            }
            // Lấy toàn bộ Màu Sắc
            public List<MauSac> GetDSMauSac()
            {
                List<MauSac> kq = new List<MauSac>();

                List<d_MauSac> ds = db.d_MauSacs.ToList();
                foreach (d_MauSac i in ds)
                    kq.Add(MauSac.fromMauSacDB(i));
                return kq;
            }
            // Lấy toàn bộ Thuốc
            public List<Thuoc> GetDSThuoc()
            {
                List<Thuoc> kq = new List<Thuoc>();

                List<d_Thuoc> ds = (from data in db.d_Thuocs
                                    select data).ToList();

                foreach (d_Thuoc i in ds)
                    kq.Add(Thuoc.fromThuocDB(i));
                return kq;
            }
            public List<Thuoc_MauSac> GetDSThuoc_MauSac()
            {
                List<Thuoc_MauSac> kq = new List<Thuoc_MauSac>();

                List<r_Thuoc_MauSac> ds = db.r_Thuoc_MauSacs.ToList();
                foreach (r_Thuoc_MauSac i in ds)
                    kq.Add(Thuoc_MauSac.fromThuoc_MauSacDB(i));

                return kq;
            }
            // Lấy toàn bộ Nhận dạng thuốc
            public List<NhanDangThuoc> GetDSNhanDangThuoc()
            {
                List<NhanDangThuoc> kq = new List<NhanDangThuoc>();

                List<w_NhanDangThuoc> ds = (from data in db.w_NhanDangThuocs
                                            select data).ToList();

                foreach (w_NhanDangThuoc i in ds)
                {
                    NhanDangThuoc a = new NhanDangThuoc
                    {
                        IDNhanDang = i.IDNhanDang,
                        IDThuoc = i.IDThuoc,
                        CoKhacDau = i.CoKhacDau,
                        KhacDauMatTruoc = i.KhacDauMatTruoc ?? string.Empty,
                        KhacDauMatSau = i.KhacDauMatSau ?? string.Empty,
                        IDHinhDang = i.IDHinhDang,
                        IDDangThuoc = i.IDDangThuoc,
                        IDLoaiViThuoc = i.IDLoaiViThuoc ?? 0,
                        IDLoaiRanh = i.IDLoaiRanh ?? 0,
                        MaHinh = i.MaHinh ?? string.Empty,
                        KichThuoc = i.KichThuoc ?? 0.0
                    };
                    kq.Add(a);
                }
                return kq;
            }
            public List<ChiDinh> SearchChiDinh(string keyword)
            {
                List<ChiDinh> kq = new List<ChiDinh>();
                try
                {
                    List<d_ChiDinh> ds = db.d_ChiDinhs.Where(cd => cd.TenChiDinh.Contains(keyword)).ToList();

                    foreach (d_ChiDinh i in ds)
                        kq.Add(ChiDinh.fromChiDinhDB(i));
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
                    d_HoatChat hoatchat = (from data in db.d_HoatChats
                                     where data.IDHoatChat == idHoatChat
                                     select data).FirstOrDefault();
                    kq = HoatChat.fromHoatChatDB(hoatchat);
                    kq.dsHCG = GetHCGbyidHC(idHoatChat);

                    return kq;
                }
                catch
                {
                    return kq;
                }
            }
            public HoatChatGoc GetHCG(int idHoatChatGoc)
            {
                HoatChatGoc kq = new HoatChatGoc();
                try
                {
                    d_HoatChatGoc hoatchatgoc = (from data in db.d_HoatChatGocs
                                           where data.IDHoatChatGoc == idHoatChatGoc
                                           select data).FirstOrDefault();
                    kq = HoatChatGoc.fromHoatChatGocDB(hoatchatgoc);
                    kq.dsCD = GetCDbyidHCG(idHoatChatGoc);

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
                        kq.Add(HoatChatGoc.fromHoatChatGocDB(i));
                    return kq;
                }
                catch
                {
                    return kq;
                }
            }
            public List<ChiDinh> GetCDbyidHCG(int idHoatChatGoc)
            {
                List<ChiDinh> kq = new List<ChiDinh>();
                try
                {
                    List<d_ChiDinh> ds = (from data in db.d_ChiDinhs
                                              join s1 in db.r_HoatChatGoc_ChiDinhs on data.IDChiDinh equals s1.IDChiDinh
                                              where s1.IDHoatChatGoc == idHoatChatGoc
                                              select data).ToList();
                    foreach (d_ChiDinh i in ds)
                        kq.Add(ChiDinh.fromChiDinhDB(i));
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
                    d_Thuoc thuoc = (from data in db.d_Thuocs
                                    where data.IDThuoc == idThuoc
                                    select data).FirstOrDefault();
                    kq = Thuoc.fromThuocDB(thuoc);
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
                        kq.Add(MauSac.fromMauSacDB(i));
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
                        kq.Add(HinhAnhThuocChiTiet.fromHinhAnhThuocChiTietDB(i));
                    return kq;
                }
                catch
                {
                    return kq;
                }
            }

            public List<Thuoc> GetNhanDangThuoc(
                string imprintFront = null,
                string imprintBack = null,
                int? idMausac1 = null,
                int? idMausac2 = null,
                int? idHinhdang = null,
                int? idDangthuoc = null,
                int? idLoaiVi = null,
                int? idLoaiRanh = null,
                double? kichThuoc = null)
            {
                List<Thuoc> kq = new List<Thuoc>();
                try
                {
                    List<int> dsIDThuoc = new List<int>();

                    // ===== COLOR FILTERING =====
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

                    // ===== MAIN ATTRIBUTES FILTERING =====
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

                    // ===== SIZE FILTERING (with tolerance) =====
                    if (kichThuoc != null)
                    {
                        double? kichThuocMin = kichThuoc * 0.8;
                        double? kichThuocMax = kichThuoc * 1.2;
                        query2 = query2.Where(x => x.KichThuoc >= kichThuocMin && x.KichThuoc <= kichThuocMax);
                    }

                    // ===== IMPRINT FILTERING (Multi-level matching) =====
                    if (!string.IsNullOrEmpty(imprintFront) || !string.IsNullOrEmpty(imprintBack))
                    {
                        var imprintQuery = query2.ToList(); // Load to memory for complex string matching
                        List<w_NhanDangThuoc> matchedByImprint = new List<w_NhanDangThuoc>();

                        foreach (var item in imprintQuery)
                        {
                            bool matches = false;
                            string front = (item.KhacDauMatTruoc ?? "").Trim().ToUpper();
                            string back = (item.KhacDauMatSau ?? "").Trim().ToUpper();
                            string searchFront = (imprintFront ?? "").Trim().ToUpper();
                            string searchBack = (imprintBack ?? "").Trim().ToUpper();

                            // Priority 1: Exact match on specified side(s)
                            if (!string.IsNullOrEmpty(searchFront) && front == searchFront)
                                matches = true;
                            if (!string.IsNullOrEmpty(searchBack) && back == searchBack)
                                matches = true;

                            // Priority 2: If both sides specified, check if they match (either way)
                            if (!string.IsNullOrEmpty(searchFront) && !string.IsNullOrEmpty(searchBack))
                            {
                                if ((front == searchFront && back == searchBack) ||
                                    (front == searchBack && back == searchFront))
                                    matches = true;
                            }

                            // Priority 3: Contains match (partial match)
                            if (!matches)
                            {
                                if (!string.IsNullOrEmpty(searchFront) && front.Contains(searchFront))
                                    matches = true;
                                if (!string.IsNullOrEmpty(searchBack) && back.Contains(searchBack))
                                    matches = true;
                            }

                            // Priority 4: Cross-side contains (user might have sides confused)
                            if (!matches)
                            {
                                if (!string.IsNullOrEmpty(searchFront) && back.Contains(searchFront))
                                    matches = true;
                                if (!string.IsNullOrEmpty(searchBack) && front.Contains(searchBack))
                                    matches = true;
                            }

                            if (matches)
                                matchedByImprint.Add(item);
                        }

                        foreach (w_NhanDangThuoc i in matchedByImprint)
                        {
                            dsIDThuoc.Add(i.IDThuoc);
                        }
                    }
                    else
                    {
                        // No imprint filter, add all from query2
                        foreach (w_NhanDangThuoc i in query2)
                        {
                            dsIDThuoc.Add(i.IDThuoc);
                        }
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
            public NhanDangThuoc GetNhanDangByThuoc(Thuoc item)
            {
                NhanDangThuoc kq = new NhanDangThuoc();
                try
                {
                    w_NhanDangThuoc search = (from data in db.w_NhanDangThuocs
                                              where data.IDThuoc == item.IDThuoc
                                              select data).FirstOrDefault();

                    kq = NhanDangThuoc.fromNhanDangThuocDB(search);

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

            public bool UpdateNhanDangThuoc(NhanDangThuoc item)
            {
                try
                {
                    w_NhanDangThuoc nd = db.w_NhanDangThuocs.SingleOrDefault(x => x.IDNhanDang == item.IDNhanDang);
                    if (nd != null)
                    {
                        nd.IDThuoc = item.IDThuoc;
                        nd.CoKhacDau = item.CoKhacDau;
                        nd.KhacDauMatTruoc = string.IsNullOrEmpty(item.KhacDauMatTruoc) ? null : item.KhacDauMatTruoc;
                        nd.KhacDauMatSau = string.IsNullOrEmpty(item.KhacDauMatSau) ? null : item.KhacDauMatSau;
                        nd.IDHinhDang = item.IDHinhDang;
                        nd.IDDangThuoc = item.IDDangThuoc;
                        nd.IDLoaiViThuoc = item.IDLoaiViThuoc == 0 ? (int?)null : item.IDLoaiViThuoc;
                        nd.IDLoaiRanh = item.IDLoaiRanh == 0 ? (int?)null : item.IDLoaiRanh;
                        nd.MaHinh = string.IsNullOrEmpty(item.MaHinh) ? null : item.MaHinh;
                        nd.KichThuoc = item.KichThuoc == 0.0 ? (double?)null : item.KichThuoc;
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

            public bool DeleteHoatChatGoc_ChiDinh(int idHoatChatGoc)
            {
                try
                {
                    r_HoatChatGoc_ChiDinh link = db.r_HoatChatGoc_ChiDinhs.SingleOrDefault(x =>
                        x.IDHoatChatGoc == idHoatChatGoc);
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
            MoTa = "";
        }
        public static ChiDinh fromChiDinhDB(d_ChiDinh item)
        {
            if (item == null)
                return null;
            ChiDinh kq = new ChiDinh
            {
                IDChiDinh = item.IDChiDinh,
                TenChiDinh = item.TenChiDinh,
                MoTa = item.MoTa
            };
            return kq;
        }
        public d_ChiDinh toChiDinhDB()
        {
            d_ChiDinh kq = new d_ChiDinh
            {
                IDChiDinh = this.IDChiDinh,
                TenChiDinh = this.TenChiDinh,
                MoTa = this.MoTa
            };
            return kq;
        }
    }
    public class DangThuoc
    {
        public int IDDangThuoc { get; set; }
        public string TenDangThuoc { get; set; }
        public DangThuoc()
        {

        }
        public static DangThuoc fromDangThuocDB(d_DangThuoc item)
        {
            if (item == null)
                return null;
            DangThuoc kq = new DangThuoc
            {
                IDDangThuoc = item.IDDangThuoc,
                TenDangThuoc = item.TenDangThuoc
            };
            return kq;
        }
        public d_DangThuoc toDangThuocDB()
        {
            d_DangThuoc kq = new d_DangThuoc
            {
                IDDangThuoc = this.IDDangThuoc,
                TenDangThuoc = this.TenDangThuoc
            };
            return kq;
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
        public static HinhAnhThuocChiTiet fromHinhAnhThuocChiTietDB(d_HinhAnhThuocChiTiet item)
        {
            if (item == null)
                return null;
            HinhAnhThuocChiTiet kq = new HinhAnhThuocChiTiet
            {
                IDHinhAnh = item.IDHinhAnh,
                IDNhanDang = item.IDNhanDang,
                DuongDanHinh = item.DuongDanHinh,
                MoTa = item.MoTa
            };
            return kq;
        }
        public d_HinhAnhThuocChiTiet toHinhAnhThuocChiTietDB()
        {
            d_HinhAnhThuocChiTiet kq = new d_HinhAnhThuocChiTiet
            {
                IDHinhAnh = this.IDHinhAnh,
                IDNhanDang = this.IDNhanDang,
                DuongDanHinh = this.DuongDanHinh,
                MoTa = this.MoTa
            };
            return kq;
        }
    }
    public class HinhDang
    {
        public int IDHinhDang { get; set; }
        public string TenHinhDang { get; set; }
        public HinhDang()
        {

        }
        public static HinhDang fromHinhDangDB(d_HinhDang item)
        {
            if (item == null)
                return null;
            HinhDang kq = new HinhDang
            {
                IDHinhDang = item.IDHinhDang,
                TenHinhDang = item.TenHinhDang
            };
            return kq;
        }
        public d_HinhDang toHinhDangDB()
        {
            d_HinhDang kq = new d_HinhDang
            {
                IDHinhDang = this.IDHinhDang,
                TenHinhDang = this.TenHinhDang
            };
            return kq;
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
            LoaiHoatChat = "";
        }
        public static HoatChat fromHoatChatDB(d_HoatChat item)
        {
            if (item == null)
                return null;
            HoatChat kq = new HoatChat
            {
                IDHoatChat = item.IDHoatChat,
                TenHoatChat = item.TenHoatChat,
                LoaiHoatChat = item.LoaiHoatChat
            };
            return kq;
        }
        public d_HoatChat toHoatChatDB()
        {
            d_HoatChat kq = new d_HoatChat
            {
                IDHoatChat = this.IDHoatChat,
                TenHoatChat = this.TenHoatChat,
                LoaiHoatChat = this.LoaiHoatChat
            };
            return kq;
        }
    }
    public class HoatChatGoc
    {
        public int IDHoatChatGoc { get; set; }
        public string TenHoatChat { get; set; }
        public string GhiChu { get; set; }
        public List<ChiDinh> dsCD { get; set; }
        public HoatChatGoc()
        {

        }
        public static HoatChatGoc fromHoatChatGocDB(d_HoatChatGoc item)
        {
            if (item == null)
                return null;
            HoatChatGoc kq = new HoatChatGoc
            {
                IDHoatChatGoc = item.IDHoatChatGoc,
                TenHoatChat = item.TenHoatChat
            };
            return kq;
        }
        public d_HoatChatGoc toHoatChatGocDB()
        {
            d_HoatChatGoc kq = new d_HoatChatGoc
            {
                IDHoatChatGoc = this.IDHoatChatGoc,
                TenHoatChat = this.TenHoatChat
            };
            return kq;
        }
    }
    public class LoaiRanh
    {
        public int IDLoaiRanh { get; set; }
        public string TenLoaiRanh { get; set; }
        public LoaiRanh()
        {

        }
        public static LoaiRanh fromLoaiRanhDB(d_LoaiRanh item)
        {
            if (item == null)
                return null;
            LoaiRanh kq = new LoaiRanh
            {
                IDLoaiRanh = item.IDLoaiRanh,
                TenLoaiRanh = item.TenLoaiRanh
            };
            return kq;
        }
        public d_LoaiRanh toLoaiRanhDB()
        {
            d_LoaiRanh kq = new d_LoaiRanh
            {
                IDLoaiRanh = this.IDLoaiRanh,
                TenLoaiRanh = this.TenLoaiRanh
            };
            return kq;
        }
    }
    public class LoaiViThuoc
    {
        public int IDLoaiViThuoc { get; set; }
        public string TenLoaiVi { get; set; }
        public LoaiViThuoc()
        {

        }
        public static LoaiViThuoc fromLoaiViThuocDB(d_LoaiViThuoc item)
        {
            if (item == null)
                return null;
            LoaiViThuoc kq = new LoaiViThuoc
            {
                IDLoaiViThuoc = item.IDLoaiViThuoc,
                TenLoaiVi = item.TenLoaiVi
            };
            return kq;
        }
        public d_LoaiViThuoc toLoaiViThuocDB()
        {
            d_LoaiViThuoc kq = new d_LoaiViThuoc
            {
                IDLoaiViThuoc = this.IDLoaiViThuoc,
                TenLoaiVi = this.TenLoaiVi
            };
            return kq;
        }
    }
    public class MauSac
    {
        public int IDMauSac { get; set; }
        public string TenMauSac { get; set; }
        public MauSac()
        {

        }
        public static MauSac fromMauSacDB(d_MauSac item)
        {
            if (item == null)
                return null;
            MauSac kq = new MauSac
            {
                IDMauSac = item.IDMauSac,
                TenMauSac = item.TenMauSac
            };
            return kq;
        }
        public d_MauSac toMauSacDB()
        {
            d_MauSac kq = new d_MauSac
            {
                IDMauSac = this.IDMauSac,
                TenMauSac = this.TenMauSac
            };
            return kq;
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
        public string URL { get; set; }
        public List<MauSac> Mausac { get; set; }
        public List<HinhAnhThuocChiTiet> HinhAnhChiTiet { get; set; }

        public Thuoc()
        {
            HamLuong = "";
            DangBaoChe = "";
            NhaSX = "";
            GhiChu = "";
            URL = "";
        }

        public static Thuoc fromThuocDB(d_Thuoc item)
        {
            if (item == null)
                return null;
            Thuoc kq = new Thuoc
            {
                IDThuoc = item.IDThuoc,
                TenThuoc = item.TenThuoc,
                SDK = item.SDK,
                IDHoatChat = item.IDHoatChat,
                HamLuong = item.HamLuong,
                DangBaoChe = item.DangBaoChe,
                NhaSX = item.NhaSX,
                GhiChu = item.GhiChu,
                URL = item.URL
            };
            return kq;
        }
        public d_Thuoc toThuocDB()
        {
            d_Thuoc kq = new d_Thuoc
            {
                IDThuoc = this.IDThuoc,
                TenThuoc = this.TenThuoc,
                SDK = this.SDK,
                IDHoatChat = this.IDHoatChat,
                HamLuong = this.HamLuong,
                DangBaoChe = this.DangBaoChe,
                NhaSX = this.NhaSX,
                GhiChu = this.GhiChu,
                URL = this.URL
            };
            return kq;
        }
    }
    public class HoatChat_HoatChatGoc
    {
        public int IDHoatChat { get; set; }
        public int IDHoatChatGoc { get; set; }
        public HoatChat_HoatChatGoc()
        {

        }
        public static HoatChat_HoatChatGoc fromHoatChat_HoatChatGocDB(r_HoatChat_HoatChatGoc item)
        {
            if (item == null)
                return null;
            HoatChat_HoatChatGoc kq = new HoatChat_HoatChatGoc
            {
                IDHoatChat = item.IDHoatChat,
                IDHoatChatGoc = item.IDHoatChatGoc
            };
            return kq;
        }
        public r_HoatChat_HoatChatGoc toHoatChat_HoatChatGocDB()
        {
            r_HoatChat_HoatChatGoc kq = new r_HoatChat_HoatChatGoc
            {
                IDHoatChat = this.IDHoatChat,
                IDHoatChatGoc = this.IDHoatChatGoc
            };
            return kq;
        }
    } //chua
    public class HoatChatGoc_ChiDinh
    {
        public int IDHoatChatGoc { get; set; }
        public int IDChiDinh { get; set; }
        public HoatChatGoc_ChiDinh()
        {

        }
        public static HoatChatGoc_ChiDinh fromHoatChatGoc_ChiDinhDB(r_HoatChatGoc_ChiDinh item)
        {
            if (item == null)
                return null;
            HoatChatGoc_ChiDinh kq = new HoatChatGoc_ChiDinh
            {
                IDHoatChatGoc = item.IDHoatChatGoc,
                IDChiDinh = item.IDChiDinh
            };
            return kq;
        }
        public r_HoatChatGoc_ChiDinh toHoatChatGoc_ChiDinhDB()
        {
            r_HoatChatGoc_ChiDinh kq = new r_HoatChatGoc_ChiDinh
            {
                IDHoatChatGoc = this.IDHoatChatGoc,
                IDChiDinh = this.IDChiDinh
            };
            return kq;
        }
    } //chua
    public class Thuoc_MauSac
    {
        public int IDThuoc { get; set; }
        public int IDMauSac { get; set; }
        public Thuoc_MauSac()
        {

        }
        public static Thuoc_MauSac fromThuoc_MauSacDB(r_Thuoc_MauSac item)
        {
            if (item == null)
                return null;
            Thuoc_MauSac kq = new Thuoc_MauSac
            {
                IDThuoc = item.IDThuoc,
                IDMauSac = item.IDMauSac
            };
            return kq;
        }
        public r_Thuoc_MauSac toThuoc_MauSacDB()
        {
            r_Thuoc_MauSac kq = new r_Thuoc_MauSac
            {
                IDThuoc = this.IDThuoc,
                IDMauSac = this.IDMauSac
            };
            return kq;
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
        public int IDLoaiViThuoc { get; set; }
        public int IDLoaiRanh { get; set; }
        public string MaHinh { get; set; }
        public double KichThuoc { get; set; }

        public NhanDangThuoc()
        {
            // Set default values for nullable fields
            KhacDauMatTruoc = string.Empty;
            KhacDauMatSau = string.Empty;
            IDLoaiViThuoc = 0;
            IDLoaiRanh = 0;
            MaHinh = string.Empty;
            KichThuoc = 0.0;
        }
        public static NhanDangThuoc fromNhanDangThuocDB(w_NhanDangThuoc entity)
        {
            if (entity == null) return null;

            return new NhanDangThuoc
            {
                IDNhanDang = entity.IDNhanDang,
                IDThuoc = entity.IDThuoc,
                CoKhacDau = entity.CoKhacDau,
                KhacDauMatTruoc = entity.KhacDauMatTruoc ?? string.Empty,
                KhacDauMatSau = entity.KhacDauMatSau ?? string.Empty,
                IDHinhDang = entity.IDHinhDang,
                IDDangThuoc = entity.IDDangThuoc,
                IDLoaiViThuoc = entity.IDLoaiViThuoc ?? 0,
                IDLoaiRanh = entity.IDLoaiRanh ?? 0,
                MaHinh = entity.MaHinh ?? string.Empty,
                KichThuoc = entity.KichThuoc ?? 0.0
            };
        }

        // Convert to database entity
        public w_NhanDangThuoc toNhanDangThuocDB()
        {
            return new w_NhanDangThuoc
            {
                IDNhanDang = this.IDNhanDang,
                IDThuoc = this.IDThuoc,
                CoKhacDau = this.CoKhacDau,
                KhacDauMatTruoc = string.IsNullOrEmpty(this.KhacDauMatTruoc) ? "" : this.KhacDauMatTruoc,
                KhacDauMatSau = string.IsNullOrEmpty(this.KhacDauMatSau) ? "" : this.KhacDauMatSau,
                IDHinhDang = this.IDHinhDang,
                IDDangThuoc = this.IDDangThuoc,
                IDLoaiViThuoc = this.IDLoaiViThuoc == 0 ? (int?)null : this.IDLoaiViThuoc,
                IDLoaiRanh = this.IDLoaiRanh == 0 ? (int?)null : this.IDLoaiRanh,
                MaHinh = string.IsNullOrEmpty(this.MaHinh) ? null : this.MaHinh,
                KichThuoc = this.KichThuoc == 0.0 ? (double?)null : this.KichThuoc
            };
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
