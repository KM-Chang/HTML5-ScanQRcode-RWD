using EtestSingQR.Models;
using Microsoft.AspNetCore.Mvc;
using EtestSingQR.Services;

namespace EtestSingQR.ViewComponents
{
    [ViewComponent(Name = "ImgVerif")]
    public class ImgVerifViewComponent : ViewComponent
    {
        private readonly IImgVerifService _ImgVif;
        public ImgVerifViewComponent(IImgVerifService weatherService)
        {
            _ImgVif = weatherService;
        }
        public IViewComponentResult Invoke()
        {
            var Redata = _ImgVif.GetImgVerif();

            return View(Redata);
        }

    }
}
