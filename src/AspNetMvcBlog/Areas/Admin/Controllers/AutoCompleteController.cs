using AspNetMvcBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace AspNetMvcBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AutoCompleteController : AdminController
    {
        private readonly BlogContext _context;

        public AutoCompleteController(BlogContext context)
        {
            _context = context;
        }

        [OutputCache(PolicyName = "OneDay")]
        public IActionResult Categories(string term)
        {
            string slug = term.ToSlug();

            var model = _context.Category
                .Where(x => x.Permalink!.StartsWith(slug))
                .Select(x => x.Name).ToList();

            return Json(model);
        }
    }
}
