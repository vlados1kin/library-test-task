using Library.Domain.Exceptions;
using Library.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Library.Service.ImageUseCases;

public class DownloadImageUseCase
{
    private readonly IOptions<ImageSettings> _imageSettings;
    private string BookUploadPath => _imageSettings.Value.BookUploadPath;

    public DownloadImageUseCase(IOptions<ImageSettings> imageSettings)
    {
        _imageSettings = imageSettings;
    }

    public async Task<(byte[] fileBytes, string contentType, string fileName)> ExecuteAsync(Guid id)
    {
        var fileNameWithExtension = id + ".png";
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), BookUploadPath, fileNameWithExtension);

        if (!File.Exists(fullPath))
            throw new ImageOfBookNotFoundException(id);

        var fileBytes = await File.ReadAllBytesAsync(fullPath);
        var contentType = "image/png";
        return (fileBytes: fileBytes, contentType: contentType, fileName: fileNameWithExtension);
    }
}