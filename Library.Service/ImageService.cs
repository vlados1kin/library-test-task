using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Library.Service;

public class ImageService : IImageService
{
    private readonly IOptions<ImageSettings> _imageSettings;

    private string AuthorUploadPath => _imageSettings.Value.AuthorUploadPath;
    private string BookUploadPath => _imageSettings.Value.BookUploadPath;

    public ImageService(IOptions<ImageSettings> imageSettings)
    {
        _imageSettings = imageSettings;
    }

    public async Task<(byte[] fileBytes, string contentType, string fileName)> DownloadImageAsync(string fileNameWithExtension)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), BookUploadPath, fileNameWithExtension);

        if (!File.Exists(fullPath))
            throw new ImageOfBookNotFoundException(fileNameWithExtension);

        var fileBytes = await File.ReadAllBytesAsync(fullPath);
        var contentType = GetContentType(fullPath);
        return (fileBytes: fileBytes, contentType: contentType, fileName: fileNameWithExtension);
    }

    public async Task<string> UploadImageAsync(Guid id, UploadImage uploadImage)
    {
        if (!Directory.Exists(BookUploadPath))
        {
            Directory.CreateDirectory(BookUploadPath);
        }

        var fileNameWithExtension = id + Path.GetExtension(uploadImage.Image.FileName);
        var filePath = Path.Combine(BookUploadPath, fileNameWithExtension);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await uploadImage.Image.CopyToAsync(stream);
        }

        return fileNameWithExtension;
    }
    
    private string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types.GetValueOrDefault(ext, "application/octet-stream");
    }

    private Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".bmp", "image/bmp" },
            { ".pdf", "application/pdf" }
        };
    }
}