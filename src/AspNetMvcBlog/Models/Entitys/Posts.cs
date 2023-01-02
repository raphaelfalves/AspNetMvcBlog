using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace AspNetMvcBlog.Models.Entitys
{
    public class Posts
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        
        [Required , MaxLength(100)]
        public string? Permalink { get; set; }
        
        [Required, MaxLength(70)]
        public string? Title { get; set; }
        
        [Required, MaxLength(500)]
        public string? Summary { get; set; }

        public string? Content { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublisheOn { get; set; }
        public ICollection<Comments> Comments 
        {
            get { return _comments ?? (_comments = new List<Comments>()); }
            set { _comments = value; } 
        }
        ICollection<Comments>? _comments;
        public Category? Category { get; set; }
        public Posts()
        {
            PublisheOn= DateTime.Now;
        }

    }
}
