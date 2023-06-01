using EtestSingQR.Models;
using EtestSingQR.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web;

namespace EtestSingQR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIScanController : Controller
    {
        private readonly IScanQRService _SaQR;
        private readonly FunService _FunService;

        public APIScanController(IScanQRService ScanQRS, FunService FunS)
        {
            _SaQR = ScanQRS;
            _FunService = FunS;
        }

        /// <summary>
        /// 本日場次明細
        /// </summary>
        [HttpGet("GetDetListDay")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetDetListDay(string sLotID)
        {
            JSonRetobjdt<DetListDayViewModel> MyAPIJsobj = new JSonRetobjdt<DetListDayViewModel>();
            try
            {
                string JWTvifStr = _FunService.DecodedJWT(HttpContext.Request.Headers["Authorization"].ToString());   //驗證jwt
                MyAPIJsobj.successYN = 0;
                if (JWTvifStr == "")
                {
                    MyAPIJsobj.successYN = 2;   //沒有權限
                    MyAPIJsobj.msg = "身分驗證錯誤，請重新登入！";
                }
                else
                {
                    FunService.JWTDates MyJWTDates = JsonSerializer.Deserialize<FunService.JWTDates>(JWTvifStr); //取得單位代碼 MyJWTDates.TrnNo
                    MyAPIJsobj.sTPID = MyJWTDates.TPID;
                    MyAPIJsobj.jsondt = await _SaQR.SelSingToDaySet(MyJWTDates.TPID, sLotID.Split("_")[0]);
                    MyAPIJsobj.successYN = 1;   //正確
                }
            }
            catch (Exception ex) {
                MyAPIJsobj.msg = ex.Message;
            }
            return new JsonResult(MyAPIJsobj);
        }

        /// <summary>
        /// 本場未報到清單
        /// </summary>
        [HttpGet("GetDetListTime")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetDetListTime(string sLotID)
        {
            JSonRetobjdt<StudentViewModel> MyAPIJsobj = new JSonRetobjdt<StudentViewModel>();
            try
            {
                string JWTvifStr = _FunService.DecodedJWT(HttpContext.Request.Headers["Authorization"].ToString());   //驗證jwt
                MyAPIJsobj.successYN = 0;
                if (JWTvifStr == "")
                {
                    MyAPIJsobj.successYN = 2;   //沒有權限
                    MyAPIJsobj.msg = "身分驗證錯誤，請重新登入！";
                }
                else
                {
                    FunService.JWTDates MyJWTDates = JsonSerializer.Deserialize<FunService.JWTDates>(JWTvifStr); //取得單位代碼 MyJWTDates.TrnNo
                    MyAPIJsobj.sTPID = MyJWTDates.TPID;
                    string[] MyLotArr = sLotID.Split("_");
                    MyAPIJsobj.jsondt = await _SaQR.SelSingData(MyJWTDates.TPID, MyLotArr[0], MyLotArr[1]);
                    MyAPIJsobj.successYN = 1;   //正確
                }
            }
            catch (Exception ex)
            {
                MyAPIJsobj.msg = ex.Message;
            }
            return new JsonResult(MyAPIJsobj);
        }

        /// <summary>
        /// 查詢報到應檢人資料
        /// </summary>
        [HttpGet("GetStuDate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetStuDate(string sLotID, string sKeyPID)
        {
            JSonRetobjdt<SingStuerViewModel> MyAPIJsobj = new();
            try
            {
                string JWTvifStr = _FunService.DecodedJWT(HttpContext.Request.Headers["Authorization"].ToString());   //驗證jwt
                MyAPIJsobj.successYN = 0;
                if (JWTvifStr == "")
                {
                    MyAPIJsobj.successYN = 2;   //沒有權限
                    MyAPIJsobj.msg = "身分驗證錯誤，請重新登入！";
                }
                else
                {
                    FunService.JWTDates MyJWTDates = JsonSerializer.Deserialize<FunService.JWTDates>(JWTvifStr); //取得單位代碼 MyJWTDates.TrnNo
                    MyAPIJsobj.sTPID = MyJWTDates.TPID;
                    MyAPIJsobj.jsondt = await _SaQR.SelSingStuerDt(MyJWTDates.TPID, sLotID, sKeyPID.Replace("'", "").Split('-'));
                    MyAPIJsobj.successYN = 0;
                    if (MyAPIJsobj.jsondt.Count() > 0)
                    {
                        SingStuerViewModel DtFirst = MyAPIJsobj.jsondt.First();
                        if (DtFirst.SignType.ToString() == "")
                        {
                            //沒報到過FirstSignTime會是現在時間
                            if (_FunService.SingTimeOKYN(DtFirst.FirstSignTime.ToString(), DtFirst.SingStime.ToString(), DtFirst.SingEtime.ToString()))
                            { MyAPIJsobj.successYN = 1; }
                            else
                            {
                                MyAPIJsobj.msg = "非此應檢人報到時間！";
                            }
                        }
                        else
                        {
                            MyAPIJsobj.successYN = 3;
                            MyAPIJsobj.msg = "此應檢人已報到過了！";
                        }
                    }
                    else
                    {
                        MyAPIJsobj.msg = "今日期別查無此應檢人";
                    }
                }
            }
            catch (Exception ex)
            {
                MyAPIJsobj.msg = ex.Message;
            }
            return new JsonResult(MyAPIJsobj);
        }

        /// <summary>
        /// 實際寫入報到
        /// </summary>
        [HttpPut("EditSingDt")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSingDt(IScanQRService.InAPIobj Myobj)
        {
            JSonRetobjdt<SingStuerViewModel> MyAPIJsobj = new JSonRetobjdt<SingStuerViewModel>();
            try
            {
                string JWTvifStr = _FunService.DecodedJWT(HttpContext.Request.Headers["Authorization"].ToString());   //驗證jwt
                MyAPIJsobj.successYN = 0;
                if (JWTvifStr == "")
                {
                    MyAPIJsobj.successYN = 2;   //沒有權限
                    MyAPIJsobj.msg = "身分驗證錯誤，請重新登入！";
                }
                else
                {
                    FunService.JWTDates MyJWTDates = JsonSerializer.Deserialize<FunService.JWTDates>(JWTvifStr); //取得單位代碼 MyJWTDates.TrnNo
                    MyAPIJsobj.sTPID = MyJWTDates.TPID;
                    MyAPIJsobj.successYN = await _SaQR.EditSingStuer(Myobj.FmSelKry, Myobj.FmSingType, HttpUtility.UrlDecode(Myobj.FmErrItem), HttpUtility.UrlDecode(Myobj.FmErrNote), MyJWTDates.TPID);
                    if (MyAPIJsobj.successYN == 0) MyAPIJsobj.msg = "報到失敗，無此應檢人";
                }
            }
            catch (Exception ex)
            {
                MyAPIJsobj.msg = ex.Message;
            }
            return new JsonResult(MyAPIJsobj);
        }
    }
}
