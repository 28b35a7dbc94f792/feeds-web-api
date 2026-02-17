namespace FeedsWebApi.Dtos;

public class ImageDto
{
    public byte[] Data { get; set; } = default!;
    
    public string ContentType { get; set; } = "application/octet-stream";
}
