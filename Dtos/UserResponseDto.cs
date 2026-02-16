namespace FeedsWebApi.Dtos;

public class UserResponseDto : UserBaseDto
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
}
