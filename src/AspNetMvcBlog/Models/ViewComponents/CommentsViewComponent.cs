using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcBlog.Models.ViewComponents
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly BlogContext? _context;

        public CommentsViewComponent(BlogContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? permalink, int? pageNumber, string? currentFilter)
        {
            if (string.IsNullOrEmpty(permalink))
            {
                permalink = currentFilter;
            }
            else
            {
                pageNumber = 1;
            }

            ViewData["currentFilter"] = permalink;

            int pageSize = 3;
            var model = _context!.Comments
                .Include(p => p.Posts)
                .Where(p => p.Posts!.Permalink == permalink)
                .OrderByDescending(p => p.PublishOn)
                .ToPaginetedList(pageNumber ?? 1, pageSize);

            return View(model); ;
        }
    }
}
