using Library.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Library.Contracts;

public interface IImageService
{
    Task<(byte[] fileBytes, string contentType, string fileName)> DownloadImageAsync(string fileNameWithExtension);
    Task<string> UploadImageAsync(Guid id, UploadImage uploadImage);
}