using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Attachment
{
    public class Attachment : IAttachment
    {
       
        private readonly IWebHostEnvironment env;
        

        public Attachment(IWebHostEnvironment _env)
        {
            env = _env;
        }
        
        public async Task<string?> UploadFileAsync(Stream fileStream, string fileName, string folderName, CancellationToken cancellationToken = default)
        {
          int maxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

            if (fileStream == null || fileStream.Length == 0 || !fileStream.CanRead)
            {
                return null;
            }
            if (fileStream.Length > maxFileSizeInBytes)
            {
                return null;
            }
            var fileExtension = Path.GetExtension(fileName).ToLower();
            if (fileExtension == null || !allowedExtensions.Contains(fileExtension))
            {
                return null;
            }

          var folderpath=Path.Combine(env.ContentRootPath, folderName);
            Directory.CreateDirectory(folderpath);
            var filename=$"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(folderpath, filename);
            try {
                await using (var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    await fileStream.CopyToAsync(fs, cancellationToken);
                    return filename;
                }
            }
            catch {
                return null;
            }
        }
        public async Task<(Stream stream, string ContentType)?> GetFileAsync(string fileName, string folderName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

        
            fileName = Path.GetFileName(fileName);

            var folderPath = Path.Combine(env.ContentRootPath, folderName);
            var filePath = Path.Combine(folderPath, fileName);

            if (!File.Exists(filePath))
                return null;

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            Stream stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);

            return await Task.FromResult((stream, contentType));
        }
        public Task<bool> DeleteFileAsync(string fileName, string folderName, CancellationToken cancellationToken = default)
        {
          if(string.IsNullOrWhiteSpace(fileName)|| string.IsNullOrWhiteSpace(folderName)) 
                return Task.FromResult(false);
            try
            {
                fileName = Path.GetFileName(fileName);
                var folderPath = Path.Combine(env.ContentRootPath, folderName);
                var filePath = Path.Combine(folderPath, fileName);
                if (!Directory.Exists(folderPath) || !File.Exists(filePath))
                    return Task.FromResult(false);
                File.Delete(filePath);
                return Task.FromResult(true);

            }
            catch
            {
                return Task.FromResult(false);
            }  
            
        }

       
       
    }
}
