using FeedsWebApi.Dtos;
using FeedsWebApi.Models;

namespace FeedsWebApi.Factories;

public interface IUserResponseDtoFactory
{
    UserResponseDto Create(User user);
}

public class UserResponseDtoFactory : IUserResponseDtoFactory
{
    public UserResponseDto Create(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName
        };
    }
}
