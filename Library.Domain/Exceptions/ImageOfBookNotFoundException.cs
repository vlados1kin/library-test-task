namespace Library.Domain.Exceptions;

public class ImageOfBookNotFoundException : NotFoundException
{
    public ImageOfBookNotFoundException(string fileNameWithExtension) : base($"The image with name: {fileNameWithExtension} does not exist.")
    {
    }
}