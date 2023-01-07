using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models.Entitys;
using AspNetMvcBlog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly BlogContext _context;

        public CommentController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AddComment(int id)
        {
            PostComment? poco = await _context.Posts.FindAsync(id);
            return View(poco);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(PostComment comments)
        {
            Posts? poco = await _context.Posts.FindAsync(comments.Posts.Id);
            Comments comment = comments.Comment;
            comment.PostsId = comments.Posts.Id;
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", new RouteValueDictionary(
                    new { controller = "Posts", action = "Details", poco.Permalink }));
        }

        public async Task<IActionResult> LoadComments(string? permalink, int? pageNumber, string? currentFilter)
        {
            return ViewComponent("Comments", 
                new {permalink, pageNumber, currentFilter});
        }
    }
}
