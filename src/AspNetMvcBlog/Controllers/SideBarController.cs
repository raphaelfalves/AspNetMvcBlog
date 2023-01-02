using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcBlog.Controllers
{
    public class SideBarController : Controller
    {
        private readonly BlogContext? _context;

        public SideBarController(BlogContext? context)
        {
            _context = context;
        }

        public ActionResult Category()
        {
            IQueryable<CategoryItems> model = from c in _context!.Category
                                  select new CategoryItems
                                  {
                                      Name = c.Name,
                                      Permalink = c.Permalink,
                                      PostCount = c.Posts!.Count(),
                                  };
            return PartialView("_category", model);
        }
    }
}
