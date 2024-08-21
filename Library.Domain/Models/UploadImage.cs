using Microsoft.AspNetCore.Http;

namespace Library.Domain.Models;

public class UploadImage
{
    public IFormFile Image { get; set; }
    public string Description { get; set; }
}