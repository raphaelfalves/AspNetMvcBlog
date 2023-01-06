using AspNetMvcBlog.Application;
using AspNetMvcBlog.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
    }
}
