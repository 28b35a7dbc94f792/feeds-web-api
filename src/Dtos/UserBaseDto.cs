namespace FeedsWebApi.Dtos;

public abstract class UserBaseDto
{
    public string Username { get; set; } = string.Empty;

    public string? Name { get; set; }
}
