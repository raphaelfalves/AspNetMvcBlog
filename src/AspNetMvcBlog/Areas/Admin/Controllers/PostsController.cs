using AspNetMvcBlog.Areas.Admin.Models;
using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly BlogContext? _context;

        public PostsController(BlogContext? context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(jQueryDataTableRequestModel request)
        {
            var post = _context!.Posts.OrderByDescending(x => x.PublisheOn);
            IQueryable<Posts> posts = _context.Posts.OrderByDescending(x => x.PublisheOn);

            if (!String.IsNullOrWhiteSpace(request.sSearch))
            {
                posts = posts.Where(x =>
                            x.Title!.Contains(request.sSearch) ||
                            x.Summary!.Contains(request.sSearch)
                        );
            }

            int total = posts.Count();
            posts = posts.Skip(request.iDisplayStart).Take(request.iDisplayLength);

            var model = new jQueryDataTableResponseModel
            {
                sEcho = request.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = from r in posts.ToList()
                         select new
                         {
                             PostId = r.Id,
                             Title = r.Title,
                             PublishedOn = r.PublisheOn.ToString("dd/MM/yyyy"),
                             //EditUrl = Url.RouteUrl("Admin.Posts.Edit", new { id = r.Id }),
                             //DeleteUrl = Url.RouteUrl("Admin.Posts.Delete", new { id = r.Id })
                         }
            };            
            return Json(model);
        }
    }
}
