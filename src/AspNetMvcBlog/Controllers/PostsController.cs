using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models;
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
            var post = _context.Posts.OrderByDescending(p => p.PublisheOn);
            return View(post);
        }

        [Route("Posts/{Permalink}")]
        public IActionResult Details(string? Permalink)
        {
            var post = _context.Posts
                .FirstOrDefault(p => p.Permalink == Permalink);
            
            return View(post);
        }
       
    }
}