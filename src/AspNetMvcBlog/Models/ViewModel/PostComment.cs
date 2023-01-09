using AspNetMvcBlog.Models.Entitys;

namespace AspNetMvcBlog.Models.ViewModel
{
    public class PostComment
    {
        public Posts? Posts { get; set; }
        public Comments? Comment { get; set; }

        public static implicit operator PostComment?(Posts? p)
        {
            PostComment pc;
            Comments comments = new() { Posts = p };
            pc = new PostComment() { Posts = p, Comment = comments};
            return pc;

        }
        public IEnumerable<string> GetTags()
        {
            if (String.IsNullOrWhiteSpace(Posts!.Tags))
            {
                return Enumerable.Empty<string>();
            }

            return Posts.Tags.Split(',');
        }
    }
}
