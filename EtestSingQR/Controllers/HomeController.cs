using EtestSingQR.Models;
using EtestSingQR.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace EtestSingQR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginUserService _LoUser;
        private readonly FunService _FunService;

        public HomeController(ILogger<HomeController> logger, ILoginUserService LoUserService, FunService funService)
        {
            _logger = logger;
            _LoUser = LoUserService;
            _FunService = funService;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            return View(await _LoUser.SelListTP());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> IndexAsync(string DListLodID, string ImgCode)
        {
            if (TempData["BE_CheckCode"]?.ToString() == ImgCode)
            {
                string[] MyLoadID = DListLodID.Split('-');
                {
                    if (MyLoadID.Length == 2 && MyLoadID[1].Length == 3)
                    {
                        //改用學科單位代碼查詢
                        MyLoadID[0] = MyLoadID[1];
                    }
                    var LoingerUser = _LoUser.SelUserLogin(MyLoadID[0],"","");
                    if (LoingerUser.GetAwaiter().GetResult().Any())
                    {
                        List<LoginUserDate> MyLoUsD = new List<LoginUserDate>();
                        foreach (var item in LoingerUser.GetAwaiter().GetResult())
                        {
                            MyLoUsD.Add(item);
                        }
                        TempData["sSwitchUnit"] = JsonSerializer.Serialize(MyLoUsD);
                        return RedirectToAction("SwitchUnit");
                    }
                    else {
                        ViewData["LabAlertMsg"] = "<strong>查無帳號密碼喔！</strong>";
                    }
                }
            }
            else {
                ViewData["LabAlertMsg"] = "<strong>驗證碼錯誤喔！</strong>";
            }
            return View(await _LoUser.SelListTP());
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult SwitchUnit()
        {
            if (TempData["sSwitchUnit"]?.ToString() == null)
            {
                return RedirectToAction("Index");
            }
            else {
                List<LoginUserDate>? LoingerUser = JsonSerializer.Deserialize<List<LoginUserDate>>(TempData["sSwitchUnit"]?.ToString() ?? "") ?? new List<LoginUserDate>();
                //預設寫入第一個即測考場當JWT憑證
                ViewData["sJWTToken"]= _FunService.EncodedJWT(new { alg = "HS256", typ = "JWT" }, new { TPID = LoingerUser[0].TestPlaceID, LoadID = LoingerUser[0].LaborID, UserID = LoingerUser[0].UserID });
                return View(LoingerUser);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult SwitchUnit(string JWTVal, string TPID)
        {
            FunService.JWTDates MyJWT = JsonSerializer.Deserialize<FunService.JWTDates>(_FunService.DecodedJWT(JWTVal));
            if (MyJWT.TPID == TPID)
            {
                ViewData["sJWTToken"] = JWTVal;
            }
            else {
                ViewData["sJWTToken"] = _FunService.EncodedJWT(new { alg = "HS256", typ = "JWT" }, new { TPID = TPID, LoadID = MyJWT.LoadID, UserID = MyJWT.UserID });
            }
            List<LoginUserDate>? LoingerUser =new List<LoginUserDate>() { new LoginUserDate() { LaborID = MyJWT.LoadID, TestPlaceID = TPID } };
            return View(LoingerUser);
        }

        [Route("~/Error/{*ErrNoMsg}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string ErrNoMsg)
        {
            string? errtype = ErrNoMsg ?? "";
            string LabErrMsg = "抱歉，發生未預期錯誤！";
            string BackBtns = "<a class=\"btn btn-lg MybtnColor1\" onClick=\"javascript:window.history.go(-1);\">返回前頁</a>";
            switch (errtype)
            {
                case "CIx001":
                    LabErrMsg = "您沒有這個頁面的權限或連線逾時請重新登入！";
                    BackBtns = "<a class=\"btn btn-lg MybtnColor1\" href=\"../\">返回登入頁</a>";
                    break;
                default:
                    var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                    {
                        LabErrMsg = "查無此檔案";
                    }
                    else
                    {
                        LabErrMsg = exceptionHandlerPathFeature?.Error.Message ?? "正在找尋原因中";
                    }

                    if (exceptionHandlerPathFeature?.Path == "/")
                    {
                        LabErrMsg ??= string.Empty;
                        LabErrMsg += " 路徑錯誤.";
                    }
                    break;
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,ErrType= errtype,ErrMsg= LabErrMsg,butUrl= BackBtns });
        }
    }
}