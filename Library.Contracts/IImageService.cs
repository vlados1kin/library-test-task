using Library.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Library.Contracts;

public interface IImageService
{
    Task<(byte[] fileBytes, string contentType, string fileName)> DownloadImageAsync(Guid id);
    Task UploadImageAsync(Guid id, UploadImage uploadImage);
}