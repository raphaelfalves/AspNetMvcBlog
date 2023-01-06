using AspNetMvcBlog.Application;
using AspNetMvcBlog.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        public IActionResult LogOn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", new { controller = "Dashboard" });
            }

            return View();
        }

        [HttpPost]
        public IActionResult LogOn(LogOnModel model)
        {
            return View();
        }
    }
}
