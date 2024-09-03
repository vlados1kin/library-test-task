using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Library.Service.ImageUseCases;

public class UploadImageUseCase
{
    private readonly IOptions<ImageSettings> _imageSettings;
    private string BookUploadPath => _imageSettings.Value.BookUploadPath;

    public UploadImageUseCase(IOptions<ImageSettings> imageSettings)
    {
        _imageSettings = imageSettings;
    }
    
    public async Task ExecuteAsync(Guid id, UploadImage uploadImage)
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