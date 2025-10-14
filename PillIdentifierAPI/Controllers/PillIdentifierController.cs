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
        #region Get các danh sách

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
        #region Insert
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
    }
}
