using AspNetMvcBlog.Application;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
