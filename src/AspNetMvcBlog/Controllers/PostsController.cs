using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models.Entitys;
using AspNetMvcBlog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AspNetMvcBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        public IActionResult GetAll(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                var posts = _context.Posts.OrderByDescending(p => p.PublisheOn);
                return View("ListPosts", posts);
            }

            var post = _context.Posts
                            .Where(x =>
                            x.Title.Contains(term) ||
                            x.Summary.Contains(term) ||
                            x.Content.Contains(term))
                            .OrderByDescending(x => x.PublisheOn);
                return View("ListPosts", post);
        }

        public IActionResult Details(string? Permalink)
        {
             PostComment? post = _context.Posts
                .Include(c => c.Comments)
                .FirstOrDefault(p => p.Permalink == Permalink);
            
            return View(post);
        }

        public IActionResult LoadComment(int? id)
        {
            var model = _context.Comments
                .Where(p => p.Posts!.Id == id)
                .OrderByDescending(p => p.PublishOn);
            return PartialView("_comments", model);
        }
    }
}