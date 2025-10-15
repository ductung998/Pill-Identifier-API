using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassChung;

namespace PillIdentifierAPI.Controllers
{
    public class PillIdentifierController : ApiController
    {
        #region Get data

        [HttpGet]
        [Route("api/v1/GetData/GetDSChidinh")]
        public IHttpActionResult DSChidinh()
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<ChiDinh> kq = db.GetDSChiDinh();
                return Ok(new ApiResponse<List<ChiDinh>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<ChiDinh>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetDSDangThuoc")]
        public IHttpActionResult DSDangThuoc()
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<DangThuoc> kq = db.GetDSDangThuoc();
                return Ok(new ApiResponse<List<DangThuoc>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<DangThuoc>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetDSHinhDang")]
        public IHttpActionResult DSHinhDang()
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<HinhDang> kq = db.GetDSHinhDang();
                return Ok(new ApiResponse<List<HinhDang>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<HinhDang>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetDSHoatChat")]
        public IHttpActionResult DSHoatChat()
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<HoatChat> kq = db.GetDSHoatChat();
                return Ok(new ApiResponse<List<HoatChat>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<HoatChat>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetDSHoatChatGoc")]
        public IHttpActionResult DSHoatChatGoc()
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<HoatChatGoc> kq = db.GetDSHoatChatGoc();
                return Ok(new ApiResponse<List<HoatChatGoc>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<HoatChatGoc>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetDSLoaiRanh")]
        public IHttpActionResult DSLoaiRanh()
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<LoaiRanh> kq = db.GetDSLoaiRanh();
                return Ok(new ApiResponse<List<LoaiRanh>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<LoaiRanh>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetDSLoaiViThuoc")]
        public IHttpActionResult DSLoaiViThuoc()
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<LoaiViThuoc> kq = db.GetDSLoaiViThuoc();
                return Ok(new ApiResponse<List<LoaiViThuoc>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<LoaiViThuoc>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetDSMauSac")]
        public IHttpActionResult DSMauSac()
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<MauSac> kq = db.GetDSMauSac();
                return Ok(new ApiResponse<List<MauSac>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<MauSac>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetDSThuoc")]
        public IHttpActionResult DSThuoc()
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<Thuoc> kq = db.GetDSThuoc();
                return Ok(new ApiResponse<List<Thuoc>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<Thuoc>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/SearchChiDinh")]
        public IHttpActionResult SearchChiDinh(string keyword)
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<ChiDinh> kq = db.SearchChiDinh(keyword);
                return Ok(new ApiResponse<List<ChiDinh>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<ChiDinh>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetIDHoatChatbyChiDinh")]
        public IHttpActionResult GetIDHoatChatbyChiDinh(int idChiDinh)
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<int> kq = db.GetIDHoatChatbyChiDinh(idChiDinh);
                return Ok(new ApiResponse<List<int>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<int>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetDSHinhAnhbyThuoc")]
        public IHttpActionResult GetDSHinhAnhbyThuoc(int idThuoc)
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<HinhAnhThuocChiTiet> kq = db.GetDSHinhAnhbyThuoc(idThuoc);
                return Ok(new ApiResponse<List<HinhAnhThuocChiTiet>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<HinhAnhThuocChiTiet>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/v1/GetData/GetNhanDangThuoc")]
        public IHttpActionResult GetNhanDangThuoc(
            string imprint = null,
            int? idMausac1 = null,
            int? idMausac2 = null,
            int? idHinhdang = null,
            int? idDangthuoc = null,
            int? idLoaiVi = null,
            int? idLoaiRanh = null)
        {
            try
            {
                KetnoiDB.GetData db = new KetnoiDB.GetData();
                List<Thuoc> kq = db.GetNhanDangThuoc(imprint, idMausac1, idMausac2, idHinhdang, idDangthuoc, idLoaiVi, idLoaiRanh);
                return Ok(new ApiResponse<List<Thuoc>> { Success = true, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<List<Thuoc>> { Success = false, Message = ex.Message });
            }
        }
        #endregion
        #region Insert data
        [HttpPost]
        [Route("api/v1/InsertData/InsertChiDinh")]
        public IHttpActionResult InsertChiDinh([FromBody] ChiDinh cd)
        {
            try
            {
                if (cd == null || string.IsNullOrWhiteSpace(cd.TenChiDinh))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool result = db.InsertChiDinh(cd.TenChiDinh, cd.MoTa);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Thêm chỉ định thành công." : "Không thể thêm chỉ định."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/v1/InsertData/InsertDangThuoc")]
        public IHttpActionResult InsertDangThuoc([FromBody] DangThuoc data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertDangThuoc(data.TenDangThuoc);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertHinhDang")]
        public IHttpActionResult InsertHinhDang([FromBody] HinhDang data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertHinhDang(data.TenHinhDang);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertHoatChat")]
        public IHttpActionResult InsertHoatChat([FromBody] HoatChat data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertHoatChat(data.TenHoatChat, data.LoaiHoatChat);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertHoatChatGoc")]
        public IHttpActionResult InsertHoatChatGoc([FromBody] HoatChatGoc data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertHoatChatGoc(data.TenHoatChat, data.GhiChu);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertLoaiRanh")]
        public IHttpActionResult InsertLoaiRanh([FromBody] LoaiRanh data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertLoaiRanh(data.TenLoaiRanh);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertLoaiViThuoc")]
        public IHttpActionResult InsertLoaiViThuoc([FromBody] LoaiViThuoc data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertLoaiViThuoc(data.TenLoaiVi);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertMauSac")]
        public IHttpActionResult InsertMauSac([FromBody] MauSac data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertMauSac(data.TenMauSac);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/v1/InsertData/InsertThuoc")]
        public IHttpActionResult InsertThuoc([FromBody] Thuoc data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertThuoc(data.TenThuoc, data.SDK, data.IDHoatChat,
                                         data.HamLuong, data.DangBaoChe, data.NhaSX, data.GhiChu);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertHoatChat_HoatChatGoc")]
        public IHttpActionResult InsertHoatChat_HoatChatGoc([FromBody] HoatChat_HoatChatGoc data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertHoatChat_HoatChatGoc(data.IDHoatChat, data.IDHoatChatGoc);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertHoatChatGoc_ChiDinh")]
        public IHttpActionResult InsertHoatChatGoc_ChiDinh([FromBody] HoatChatGoc_ChiDinh data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertHoatChatGoc_ChiDinh(data.IDHoatChatGoc, data.IDChiDinh);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertThuoc_MauSac")]
        public IHttpActionResult InsertThuoc_MauSac([FromBody] Thuoc_MauSac data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertThuoc_MauSac(data.IDThuoc, data.IDMauSac);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/v1/InsertData/InsertNhanDangThuoc")]
        public IHttpActionResult InsertNhanDangThuoc([FromBody] NhanDangThuoc data)
        {
            try
            {
                KetnoiDB.InsertData db = new KetnoiDB.InsertData();
                bool kq = db.InsertNhanDangThuoc(data.IDThuoc, data.CoKhacDau,
                                                 data.KhacDauMatTruoc, data.KhacDauMatSau,
                                                 data.IDHinhDang, data.IDDangThuoc,
                                                 data.IDLoaiViThuoc, data.IDLoaiRanh, data.MaHinh);
                return Ok(new ApiResponse<bool> { Success = kq, Data = kq });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }
        #endregion
        #region Update data
        [HttpPut]
        [Route("api/v1/UpdateData/UpdateChiDinh")]
        public IHttpActionResult UpdateChiDinh([FromBody] ChiDinh cd)
        {
            try
            {
                if (cd == null || cd.IDChiDinh <= 0 || string.IsNullOrWhiteSpace(cd.TenChiDinh))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateChiDinh(cd.IDChiDinh, cd.TenChiDinh, cd.MoTa);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật chỉ định thành công." : "Không thể cập nhật chỉ định."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateDangThuoc")]
        public IHttpActionResult UpdateDangThuoc([FromBody] DangThuoc dt)
        {
            try
            {
                if (dt == null || dt.IDDangThuoc <= 0 || string.IsNullOrWhiteSpace(dt.TenDangThuoc))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateDangThuoc(dt.IDDangThuoc, dt.TenDangThuoc);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật dạng thuốc thành công." : "Không thể cập nhật dạng thuốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateHinhAnhThuocChiTiet")]
        public IHttpActionResult UpdateHinhAnhThuocChiTiet([FromBody] HinhAnhThuocChiTiet ha)
        {
            try
            {
                if (ha == null || ha.IDHinhAnh <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateHinhAnhThuocChiTiet(ha.IDHinhAnh, ha.IDNhanDang, ha.DuongDanHinh, ha.MoTa);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật hình ảnh thuốc chi tiết thành công." : "Không thể cập nhật hình ảnh thuốc chi tiết."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateHinhDang")]
        public IHttpActionResult UpdateHinhDang([FromBody] HinhDang hd)
        {
            try
            {
                if (hd == null || hd.IDHinhDang <= 0 || string.IsNullOrWhiteSpace(hd.TenHinhDang))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateHinhDang(hd.IDHinhDang, hd.TenHinhDang);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật hình dạng thành công." : "Không thể cập nhật hình dạng."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateHoatChat")]
        public IHttpActionResult UpdateHoatChat([FromBody] HoatChat hc)
        {
            try
            {
                if (hc == null || hc.IDHoatChat <= 0 || string.IsNullOrWhiteSpace(hc.TenHoatChat))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateHoatChat(hc.IDHoatChat, hc.TenHoatChat, hc.LoaiHoatChat);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật hoạt chất thành công." : "Không thể cập nhật hoạt chất."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateHoatChatGoc")]
        public IHttpActionResult UpdateHoatChatGoc([FromBody] HoatChatGoc hcg)
        {
            try
            {
                if (hcg == null || hcg.IDHoatChatGoc <= 0 || string.IsNullOrWhiteSpace(hcg.TenHoatChat))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateHoatChatGoc(hcg.IDHoatChatGoc, hcg.TenHoatChat, hcg.GhiChu);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật hoạt chất gốc thành công." : "Không thể cập nhật hoạt chất gốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateLoaiRanh")]
        public IHttpActionResult UpdateLoaiRanh([FromBody] LoaiRanh lr)
        {
            try
            {
                if (lr == null || lr.IDLoaiRanh <= 0 || string.IsNullOrWhiteSpace(lr.TenLoaiRanh))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateLoaiRanh(lr.IDLoaiRanh, lr.TenLoaiRanh);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật loại rãnh thành công." : "Không thể cập nhật loại rãnh."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateLoaiViThuoc")]
        public IHttpActionResult UpdateLoaiViThuoc([FromBody] LoaiViThuoc lvt)
        {
            try
            {
                if (lvt == null || lvt.IDLoaiViThuoc <= 0 || string.IsNullOrWhiteSpace(lvt.TenLoaiVi))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateLoaiViThuoc(lvt.IDLoaiViThuoc, lvt.TenLoaiVi);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật loại vỉ thuốc thành công." : "Không thể cập nhật loại vỉ thuốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateMauSac")]
        public IHttpActionResult UpdateMauSac([FromBody] MauSac ms)
        {
            try
            {
                if (ms == null || ms.IDMauSac <= 0 || string.IsNullOrWhiteSpace(ms.TenMauSac))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateMauSac(ms.IDMauSac, ms.TenMauSac);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật màu sắc thành công." : "Không thể cập nhật màu sắc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateThuoc")]
        public IHttpActionResult UpdateThuoc([FromBody] Thuoc t)
        {
            try
            {
                if (t == null || t.IDThuoc <= 0 || string.IsNullOrWhiteSpace(t.TenThuoc))
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateThuoc(t.IDThuoc, t.TenThuoc, t.SDK, t.IDHoatChat, t.HamLuong,
                                             t.DangBaoChe, t.NhaSX, t.GhiChu);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật thuốc thành công." : "Không thể cập nhật thuốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/v1/UpdateData/UpdateNhanDangThuoc")]
        public IHttpActionResult UpdateNhanDangThuoc([FromBody] NhanDangThuoc nd)
        {
            try
            {
                if (nd == null || nd.IDNhanDang <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu dữ liệu đầu vào." });

                var db = new KetnoiDB.UpdateData();
                bool result = db.UpdateNhanDangThuoc(nd.IDNhanDang, nd.IDThuoc, nd.CoKhacDau,
                                                     nd.KhacDauMatTruoc, nd.KhacDauMatSau,
                                                     nd.IDHinhDang, nd.IDDangThuoc,
                                                     nd.IDLoaiViThuoc, nd.IDLoaiRanh, nd.MaHinh);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Cập nhật nhận dạng thuốc thành công." : "Không thể cập nhật nhận dạng thuốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }
        #endregion
        #region Delete data
        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteChiDinh/{id}")]
        public IHttpActionResult DeleteChiDinh(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID chỉ định hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteChiDinh(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa chỉ định thành công." : "Không thể xóa chỉ định."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteDangThuoc/{id}")]
        public IHttpActionResult DeleteDangThuoc(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID dạng thuốc hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteDangThuoc(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa dạng thuốc thành công." : "Không thể xóa dạng thuốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteHinhAnhThuocChiTiet/{id}")]
        public IHttpActionResult DeleteHinhAnhThuocChiTiet(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID hình ảnh hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteHinhAnhThuocChiTiet(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa hình ảnh thành công." : "Không thể xóa hình ảnh."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteHinhDang/{id}")]
        public IHttpActionResult DeleteHinhDang(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID hình dạng hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteHinhDang(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa hình dạng thành công." : "Không thể xóa hình dạng."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteHoatChat/{id}")]
        public IHttpActionResult DeleteHoatChat(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID hoạt chất hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteHoatChat(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa hoạt chất thành công." : "Không thể xóa hoạt chất."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteHoatChatGoc/{id}")]
        public IHttpActionResult DeleteHoatChatGoc(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID hoạt chất gốc hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteHoatChatGoc(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa hoạt chất gốc thành công." : "Không thể xóa hoạt chất gốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteLoaiRanh/{id}")]
        public IHttpActionResult DeleteLoaiRanh(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID loại rãnh hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteLoaiRanh(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa loại rãnh thành công." : "Không thể xóa loại rãnh."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteLoaiViThuoc/{id}")]
        public IHttpActionResult DeleteLoaiViThuoc(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID loại vỉ thuốc hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteLoaiViThuoc(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa loại vỉ thuốc thành công." : "Không thể xóa loại vỉ thuốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteMauSac/{id}")]
        public IHttpActionResult DeleteMauSac(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID màu sắc hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteMauSac(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa màu sắc thành công." : "Không thể xóa màu sắc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteThuoc/{id}")]
        public IHttpActionResult DeleteThuoc(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID thuốc hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteThuoc(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa thuốc thành công." : "Không thể xóa thuốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/v1/DeleteData/DeleteNhanDangThuoc/{id}")]
        public IHttpActionResult DeleteNhanDangThuoc(int id)
        {
            try
            {
                if (id <= 0)
                    return Ok(new ApiResponse<bool> { Success = false, Message = "Thiếu ID nhận dạng thuốc hợp lệ." });

                KetnoiDB.DeleteData db = new KetnoiDB.DeleteData();
                bool result = db.DeleteNhanDangThuoc(id);

                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result ? "Xóa nhận dạng thuốc thành công." : "Không thể xóa nhận dạng thuốc."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }
        #endregion
    }
}
