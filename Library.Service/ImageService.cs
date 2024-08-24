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

    public async Task<(byte[] fileBytes, string contentType, string fileName)> DownloadImageAsync(Guid id)
    {
        var fileNameWithExtension = id + ".png";
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), BookUploadPath, fileNameWithExtension);

        if (!File.Exists(fullPath))
            throw new ImageOfBookNotFoundException(id);

        var fileBytes = await File.ReadAllBytesAsync(fullPath);
        var contentType = "image/png";
        return (fileBytes: fileBytes, contentType: contentType, fileName: fileNameWithExtension);
    }

    public async Task UploadImageAsync(Guid id, UploadImage uploadImage)
    {
        if (!Directory.Exists(BookUploadPath))
            Directory.CreateDirectory(BookUploadPath);

        var extension = Path.GetExtension(uploadImage.Image.FileName);
        if (string.IsNullOrEmpty(extension) || extension != ".png")
            throw new WrongImageExtensionBadRequestException();
        
        var filePath = Path.Combine(BookUploadPath, id + extension);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await uploadImage.Image.CopyToAsync(stream);
    }
}