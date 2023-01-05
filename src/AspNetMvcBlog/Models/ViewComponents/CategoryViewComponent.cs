using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcBlog.Models.ViewComponents
{
    [ViewComponent]
    public class CategoryViewComponent : ViewComponent
    {
        private readonly BlogContext? _context;

        public CategoryViewComponent(BlogContext? context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            IQueryable<CategoryItems> model = from c in _context!.Category
                                               select new CategoryItems
                                              {
                                                  Id = c.Id,
                                                  Name = c.Name,
                                                  Permalink = c.Permalink,
                                                  PostCount = c.Posts!.Count(),
                                              };
            return View(model);
        }
    }
}
