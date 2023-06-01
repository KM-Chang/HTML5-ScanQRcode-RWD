using EtestSingQR.Models;
using EtestSingQR.Services;
using Microsoft.AspNetCore.Mvc;

namespace EtestSingQR.Controllers
{
    public class ScanQRController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScanQRService _ScanQR;
        private readonly FunService _FunService;

        public ScanQRController(ILogger<HomeController> logger, IScanQRService sq, FunService fs)
        {
            _logger = logger;
            _ScanQR = sq;
            _FunService = fs;
        }

        public IActionResult Index(string sTPID)
        {
            if (string.IsNullOrEmpty(sTPID)) return Redirect("../Error/cx001");
            string sToday = DateTime.Now.ToString("yyyy-MM-dd");
            ScanHomeViewModel? ThisViewModel = new ScanHomeViewModel()
            {
                SingToday = sToday,
                TestPlaceID = sTPID
            };
            var SQLViewModel = _ScanQR.SelSingSetDet(sTPID, sToday).Result;

            if (SQLViewModel.Any())
            {
                var OlnyModle = SQLViewModel.FirstOrDefault();
                ViewData["Title"] = (OlnyModle?.TestPlaceInit) ?? "報到";

                if (!string.IsNullOrEmpty(OlnyModle?.SetTestID))
                {
                    ThisViewModel = OlnyModle;
                }
                else{
                    ThisViewModel.TestLotID = (OlnyModle?.TestLotID) ?? "";
                    ThisViewModel.SetTestID = "今日已開考完畢！";
                }
                ViewData["sLotID"] = ThisViewModel.TestLotID + "_" + ThisViewModel.SetTestID;
            }
            else {
                ThisViewModel.TestLotID = "無開考期別";
                ThisViewModel.SetTestID = "無可報到場次";
            }

            return View(ThisViewModel);
        }

        public IActionResult NowDetList(string sLotID)
        {
            ViewData["sLotID"] = sLotID;
            return View();
        }

        public IActionResult ToDayDetList(string sLotID)
        {
            ViewData["sLotID"] = sLotID;
            return View();
        }
    }
}
