using AspNetMvcBlog.Application;
using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models.Entitys;
using AspNetMvcBlog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Drawing.Printing;

namespace AspNetMvcBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly BlogContext _context;
        public PostsController(IMemoryCache memoryCache, BlogContext context )
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [OutputCache(PolicyName = "DezSegundos")]
        public IActionResult GetAll(int? pageNumber)
        {
            int pageSize = 3;// teste

            var model = _context.Posts
                .Include(c => c.Category)
                .OrderByDescending(p => p.PublisheOn)
                .ToPaginetedList(pageNumber ?? 1, pageSize);

            return View("ListPosts", model);
        }

        [OutputCache(PolicyName = "DezSegundos")]
        [Route("Categoria/{Permalink?}")]
        public IActionResult Search(string? term, string? permalink, int? pageNumber, string? currentFilter, string? tag)
        {
            ViewData["permalink"] = permalink;
            ViewData["tag"] = tag;

            var post = from c in _context.Posts
                        .Include(c => c.Category)
                       select c;


            if (string.IsNullOrEmpty(term))
            {
                term = currentFilter;
            }
            else
            {
                pageNumber = 1;
            }

            ViewData["currentFilter"] = term;

            if (!string.IsNullOrEmpty(term))
            {
                post = post.Where(x =>
                           x.Title!.Contains(term) ||
                           x.Summary!.Contains(term) ||
                           x.Content!.Contains(term));
            }

            if (!string.IsNullOrEmpty(permalink))
            {
                post = post.Where(c => c.Category!.Permalink == permalink);
            }

            if(!string.IsNullOrEmpty(tag))
            {
                 post = _context.Posts
                .Where(x => x.Tags.Contains(tag));
            }

            int pageSize = 3;
            var model = post.OrderByDescending(c => c.PublisheOn)
                .ToPaginetedList(pageNumber ?? 1, pageSize);

            return View("ListPosts", model);
        }
        

        [OutputCache(PolicyName = "DezSegundos")]
        [Route("Post/{Permalink}")]
        public IActionResult Details(string? permalink)
        {
            var Cachekeys = "Post" + permalink;

            PostComment? model = _memoryCache.Get(Cachekeys) as PostComment;
            if(model == null)
            {
                 model = _context.Posts
                    .Include(c => c.Category)
                    .Include(c => c.Comments)
                    .FirstOrDefault(p => p.Permalink == permalink);


                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10));
                cacheEntryOptions.AbsoluteExpirationRelativeToNow = 
                    TimeSpan.FromSeconds(20) ;

                _memoryCache.Set(Cachekeys, model, cacheEntryOptions);
            }

            
            
            return View(model);
        }       
    }
}