namespace Library.Domain.Exceptions;

public class WrongImageExtensionBadRequestException : BadRequestException
{
    public WrongImageExtensionBadRequestException() : base("Wrong image extension. Required extension: .png")
    {
    }
}