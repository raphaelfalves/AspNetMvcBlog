using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AspNetMvcBlog.Models.Entitys
{
    public class Comments
    {
        public int Id { get; set; }
        public int PostsId { get; set; }

        [Required(ErrorMessage = "Autor é obrigatório"), MaxLength(100)]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Email é obrigatório"), MaxLength(100), DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O comentario é obrigatório"), MinLength(3)]
        public string? Content { get; set; }

        [Required, DisplayFormat(DataFormatString = "dd/mm/yyyy hh:mm")]
        public DateTime? PublishOn { get; set; }

        [Required]
        public Posts? Posts { get; set; }
        public Comments()
        {
            PublishOn = DateTime.Now;
        }
    }
}
