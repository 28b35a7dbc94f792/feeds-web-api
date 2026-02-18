namespace FeedsWebApi.Dtos;

public class UserResponseDto
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string? Name { get; set; }

    public DateTime CreationDate { get; set; }
}
