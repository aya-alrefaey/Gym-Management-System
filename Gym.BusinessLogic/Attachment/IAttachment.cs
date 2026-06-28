using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Attachment
{
    public interface IAttachment
    {
        Task<string?> UploadFileAsync(Stream fileStream, string fileName, string folderName, CancellationToken cancellationToken = default);
        Task<bool> DeleteFileAsync(string fileName, string folderName, CancellationToken cancellationToken = default);
        Task<(Stream stream, string ContentType)?> GetFileAsync(string fileName, string folderName, CancellationToken cancellationToken = default);
    }
}
