using AspNetMvcBlog.Models.FormModel;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using AspNetMvcBlog.Application;

namespace AspNetMvcBlog.Controllers
{
    public class ContactController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(ContactFormModel model)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("raphaelbatealves@gmail.com", "Blog ASP.NET");
            var subject = "Email de Contato do Blog ASP.NET MVC";
            var to = new EmailAddress(model.Email, model.Name);
            var plainTextContent = "Seu E-mail foi enviado com sucesso e logo responderemos";
            var htmlContent = "<span>Seu E-mail ' </span><em>" + model.Message + "</em><span> ' foi enviado com sucesso e <strong>logo</strong> responderemos.(MENTIRA kkkk)</span>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);

            Success("E-mail enviado");

            return RedirectToAction("Index");
        }
    }
}
