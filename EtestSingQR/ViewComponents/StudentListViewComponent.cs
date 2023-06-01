using EtestSingQR.Models;
using Microsoft.AspNetCore.Mvc;

namespace EtestSingQR.ViewComponents
{
    public class StudentListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(StudentViewModel MyModel)
        {
            return View(MyModel);
        }
    }
}
