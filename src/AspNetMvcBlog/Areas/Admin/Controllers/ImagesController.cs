using AspNetMvcBlog;
using AspNetMvcBlog.Areas.Admin.Models.AzureModels;
using ImageResizeWebApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ImageResizeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        // make sure that appsettings.json is filled with the necessary details of the azure storage
        private readonly AzureStorageConfig storageConfig = null;

        public ImagesController(IOptions<AzureStorageConfig> config)
        {
            storageConfig = config.Value;
        }

        // POST /api/images/upload
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            try
            {
                if (file == null)
                    return BadRequest("No files received from the upload");

                if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)
                    return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (storageConfig.ContainerName == string.Empty)
                    return BadRequest("Please provide a name for your image container in the azure blob storage");

                
                if (StorageHelper.IsImage(file))
                {
                    using (Stream stream = file.OpenReadStream())
                    {
                        string fileName = String.Concat(Path.GetRandomFileName().ToSlug(), Path.GetExtension(file.FileName));
                        Uri filelink = await StorageHelper.UploadFileToStorage(stream, file.FileName, storageConfig);

                        var model = new
                        {
                            filelink = filelink,
                            title = fileName
                        };
                        
                        return Json(model);
                    }

                }
                else
                {
                    return new UnsupportedMediaTypeResult();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
