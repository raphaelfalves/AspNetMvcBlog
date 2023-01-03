using System.ComponentModel.DataAnnotations;

namespace AspNetMvcBlog.Models.FormModel
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "O E-mail é obrigatório"), DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "A Mensagem é obrigatória")]
        public string? Message { get; set; }

    }
}
