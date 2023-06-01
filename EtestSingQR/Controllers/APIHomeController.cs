using Microsoft.AspNetCore.Mvc;
using EtestSingQR.Services;

namespace EtestSingQR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIHomeController : Controller
    {
        private readonly IImgVerifService _ImgVif;
        private readonly ILoginUserService _LoUser;
        private readonly FunService _FunService;

       public APIHomeController(IImgVerifService weatherService, ILoginUserService LoUserService, FunService FunS) 
        {
            _ImgVif = weatherService;
            _LoUser = LoUserService;
            _FunService = FunS;
        }

        [HttpGet("GetValidateCode")]
        public IActionResult ImgVerif()
        {
            var Redata = _ImgVif.GetImgVerif();
            TempData["BE_CheckCode"] = Redata.CheckCode;
            if (Redata.ImgBase64 != null)
            {
                return File(Convert.FromBase64String(Redata.ImgBase64.Replace("data:image/gif;base64, ","")), "image/Gif");
            }
            else { 
                return View();
            }
        }

        [HttpGet("SelTPlist")]
        public async Task<IActionResult> SelTPlist(string SkyStr)
        {
            return new JsonResult(await _LoUser.SelListTP(SkyStr));
        }
    }
}
