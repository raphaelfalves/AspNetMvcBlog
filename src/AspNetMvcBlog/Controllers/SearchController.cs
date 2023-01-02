using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcBlog.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
