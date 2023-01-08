using AspNetMvcBlog.Areas.Admin.Models.AzureModels;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ImageResizeWebApp.Helpers
{
    public static class StorageHelper
    {

        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { /*".jpg", */".png"/*, ".gif", ".jpeg"*/ };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<Uri> UploadFileToStorage(Stream fileStream, string fileName,
                                                            AzureStorageConfig _storageConfig)
        {
            // Create a URI to the blob
            Uri blobUri = new Uri("https://" +
                                  _storageConfig.AccountName +
                                  ".blob.core.windows.net/" +
                                  _storageConfig.ContainerName +
                                  "/" + fileName);

            // Create StorageSharedKeyCredentials object by reading
            // the values from the configuration (appsettings.json)
            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/png" };

            // Upload the file
            await blobClient.UploadAsync(fileStream, new BlobUploadOptions { HttpHeaders = blobHttpHeader});

            return blobUri;
        }

        //public static async Task<List<string>> GetThumbNailUrls(AzureStorageConfig _storageConfig)
        //{
        //    List<string> thumbnailUrls = new List<string>();

        //    // Create a URI to the storage account
        //    Uri accountUri = new Uri("https://" + _storageConfig.AccountName + ".blob.core.windows.net/");

        //    // Create BlobServiceClient from the account URI
        //    BlobServiceClient blobServiceClient = new BlobServiceClient(accountUri);

        //    // Get reference to the container
        //    BlobContainerClient container = blobServiceClient.GetBlobContainerClient(_storageConfig.ThumbnailContainer);

        //    if (container.Exists())
        //    {
        //        foreach (BlobItem blobItem in container.GetBlobs())
        //        {
        //            thumbnailUrls.Add(container.Uri + "/" + blobItem.Name);
        //        }
        //    }

        //    return await Task.FromResult(thumbnailUrls);
        //}
    }
}
