using Microsoft.AspNetCore.Http;

namespace ClientService.Application.Common.Interfaces;

public interface IStorageService
{
    Task<byte[]> DownloadFileAsync(string fileName);

    Task<string> GetContentType(string fileName);
    
    Task<string> UploadFileAsync(IFormFile file);

    Task<bool> DeleteFileAsync(string fileName, string versionId = "");

    Task<bool> IsFileExists(string fileName, string versionId = "");

    Task<string> GetPresignedUrlAsync(string fileName);
}