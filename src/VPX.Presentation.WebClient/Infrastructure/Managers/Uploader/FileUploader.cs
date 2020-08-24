using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace VPX.Presentation.WebClient.Infrastructure.Managers.Uploader
{
    public class FileUploader
    {
        public static async Task<string> UploadToBase64(IFormFile file)
        {
            using var stream = new MemoryStream();

            await file.CopyToAsync(stream);
            var base64 = Convert.ToBase64String(stream.ToArray());
            var mime = file.ContentType;
            var image = $"data:{mime};base64,{base64}";

            return image;
        }
    }
}
