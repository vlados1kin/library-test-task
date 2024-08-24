namespace Library.Domain.Exceptions;

public class ImageOfBookNotFoundException : NotFoundException
{
    public ImageOfBookNotFoundException(Guid id) : base($"The image with id: {id} does not exist.")
    {
    }
}