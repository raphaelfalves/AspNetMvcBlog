using System.ComponentModel.DataAnnotations;

namespace AspNetMvcBlog.Models.Entitys
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string? Name { get; set; }

        [Required, MaxLength(80)]
        public string? Permalink { get; set; }

        public ICollection<Posts>? Posts
        {
            get { return _Posts ?? (_Posts = new List<Posts>()); }
            set { _Posts = value; }
        }
        ICollection<Posts>? _Posts;
    }
}
