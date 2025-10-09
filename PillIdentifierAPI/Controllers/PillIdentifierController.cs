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
        [HttpGet]
        [Route("api/GetData/GetDSChidinh")]
        public List<ChiDinh> DSChidinh()
        {
            KetnoiDB.GetData db = new KetnoiDB.GetData();
            return db.GetDSChiDinh();
        }

    }
}
