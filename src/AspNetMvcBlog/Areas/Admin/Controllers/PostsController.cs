using AspNetMvcBlog.Application;
using AspNetMvcBlog.Areas.Admin.Models;
using AspNetMvcBlog.Data;
using AspNetMvcBlog.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AspNetMvcBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : BaseController
    {
        private readonly BlogContext? _context;

        public PostsController(BlogContext? context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(jQueryDataTableRequestModel request)
        {
            var post = _context!.Posts.OrderByDescending(x => x.PublisheOn);
            IQueryable<Posts> posts = _context.Posts.OrderByDescending(x => x.PublisheOn);

            if (!String.IsNullOrWhiteSpace(request.sSearch))
            {
                posts = posts.Where(x =>
                            x.Title!.Contains(request.sSearch) ||
                            x.Summary!.Contains(request.sSearch)
                        );
            }

            int total = posts.Count();
            posts = posts.Skip(request.iDisplayStart).Take(request.iDisplayLength);

            var model = new jQueryDataTableResponseModel
            {
                sEcho = request.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = from r in posts.ToList()
                         select new
                         {
                             PostId = r.Id,
                             Title = r.Title,
                             PublishedOn = r.PublisheOn.ToString("dd/MM/yyyy"),
                             //EditUrl = Url.RouteUrl("Admin.Posts.Edit", new { id = r.Id }),
                             editUrl = Url.Action("Edit", new { id = r.Id }),
                             deleteUrl = Url.Action("Delete", new { id = r.Id })
                         }
            };
            return Json(model);
        }

        public IActionResult Add()
        {
            var model = new PostModel();
            return View("AddOrEdit", model);
        }

        public IActionResult Edit(int id)
        {
            var post = _context!.Posts
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var model = PreparePostModel(post);

            return View("AddOrEdit", model);
        }

        [HttpPost]
        public IActionResult Save(PostModel model)
        {
            Posts post = null;
            if(model.PostId == 0)
            {
                post = new Posts();
                post.Permalink = model.Title!.ToSlug();
            }
            else
            {
                post = _context!.Posts.FirstOrDefault(x => x.Id == model.PostId);
            }

            post.Title = model.Title;
            post.Summary = model.Summary;
            post.Content = model.Content;


            if (!String.IsNullOrWhiteSpace(model.Category))
            {
                string permalink = model.Category.ToSlug();
                post.Category = _context!.Category.FirstOrDefault(p => p.Permalink == permalink);
                post.Category ??= new Category
                {
                    Name = model.Category,
                    Permalink = permalink,
                };
            }
            else
            {
                post.Category = null;
            }

            try
            {
                if (model.PostId == 0)
                {
                    _context!.Posts.Add(post);
                }
                else
                {
                    _context!.Posts.Update(post);
                }

                Success("Your post has been saved");
                
            }
            catch
            {
                Error("Your post cannot saved");
            }

            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = post.Id });
        }        

        public IActionResult Delete(int id)
        {
            var post = _context!.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }

            var model = PreparePostModel(post);

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletedConfirmed(int id)
        {
            var post = _context!.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }

            _context!.Posts.Remove(post);
            _context.SaveChanges();

            Success("Post Deleted");

            return View("Index");
        }

        [NonAction]
        private static PostModel PreparePostModel(Posts post)
        {
            var model = new PostModel
            {
                PostId = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content
            };
            if (post.Category != null)
            {
                model.Category = post.Category!.Name;
            }
            //model.Tags = String.Join(", ", post.Tags);
            return model;
        }
    }
}
