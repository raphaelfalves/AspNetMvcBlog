using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models.Entitys;
using AspNetMvcBlog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;

namespace AspNetMvcBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly BlogContext _context;
        public CommentController(IMemoryCache memoryCache, BlogContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AddComment(int id)
        {
            PostComment? poco = await _context.Posts.FindAsync(id);
            return View(poco);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(PostComment? comments)
        {
            Posts? poco = await _context.Posts.FindAsync(comments.Posts.Id);
            Comments comment = comments.Comment;
            comment.PostsId = poco.Id;
            _context.Comments.Add(comment);
            _context.SaveChanges();

            _memoryCache.Remove("Post" + poco.Permalink);

            return RedirectToAction("Details", new { controller = "Posts", permalink = poco.Permalink });
        }

        public async Task<IActionResult> LoadComments(string? permalink, int? pageNumber, string? currentFilter)
        {
            return ViewComponent("Comments", 
                new {permalink, pageNumber, currentFilter});
        }
    }
}
