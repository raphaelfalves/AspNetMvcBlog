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

        public IActionResult GetAll()
        {
            var post = _context.Posts
                .Include(c => c.Category)
                .OrderByDescending(p => p.PublisheOn);

            return View("ListPosts", post);
        }

        [Route("Categoria/{Permalink?}")]
        public IActionResult Search(string? term, string? Permalink)
        {
                var post = from c in _context.Posts
                           .Include(c => c.Category)
                           select c;

            if (string.IsNullOrEmpty(term) && string.IsNullOrEmpty(Permalink))
            {
                post = post.OrderByDescending(c => c.PublisheOn);
                return View("ListPosts", post);
            }

            if (!string.IsNullOrEmpty(term))
            {
                post = _context.Posts
                           .Where(x =>
                           x.Title!.Contains(term) ||
                           x.Summary!.Contains(term) ||
                           x.Content!.Contains(term));                
            }

            if (!string.IsNullOrEmpty(Permalink))
            {
                post = post.Where(c => c.Category!.Permalink == Permalink);
            }
            
            post = post.OrderByDescending(c => c.PublisheOn);

            return View("ListPosts", post);
        }

        [Route("Post/{Permalink}")]
        public IActionResult Details(string? Permalink)
        {
             PostComment? post = _context.Posts
                .Include(c => c.Category)
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